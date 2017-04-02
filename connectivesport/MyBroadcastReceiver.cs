using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.Util;
using Gcm.Client;
using WindowsAzure.Messaging;
using System.Net.Http;
using System;
using System.Threading.Tasks;


[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is needed only for Android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
namespace connectivesport
{
	[BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
	Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
	Categories = new string[] { "@PACKAGE_NAME@" })]
	[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
	Categories = new string[] { "@PACKAGE_NAME@" })]
	public class MyBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
	{
		public static string[] SENDER_IDS = new string[] { Constants.SenderID };

		public const string TAG = "MyBroadcastReceiver-GCM";

		public override void OnReceive(Context context, Intent intent)
		{
			global::Android.OS.PowerManager.WakeLock sWakeLock;
			var pm = global::Android.OS.PowerManager.FromContext(context);
			sWakeLock = pm.NewWakeLock(global::Android.OS.WakeLockFlags.Partial, "GCM Broadcast Reciever Tag");
			sWakeLock.Acquire();

			if (!HandlePushNotification(context, intent))
			{
				base.OnReceive(context, intent);
			}

			sWakeLock.Release();
		}

		internal static bool HandlePushNotification(Context context, Intent intent)
		{
			string message;
			if (!intent.Extras.ContainsKey("message"))
				return false;


			message = intent.Extras.Get("message").ToString();
			var title = intent.Extras.Get("title").ToString();

			var activityIntent = new Intent(context, typeof(AddUserActivity));
			var payloadValue = GetPayload(intent);
			activityIntent.PutExtra("payload", payloadValue);
			activityIntent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);
			var pendingIntent = PendingIntent.GetActivity(context, 0, activityIntent, PendingIntentFlags.UpdateCurrent);

			var n = new Notification.Builder(context);
			n.SetSmallIcon(Resource.Drawable.ic_noti);
			n.SetLights(global::Android.Graphics.Color.Blue, 300, 1000);
			n.SetContentIntent(pendingIntent);
			n.SetContentTitle(title);
			n.SetTicker(message);
			n.SetLargeIcon(global::Android.Graphics.BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.ic_noti));
			n.SetSmallIcon(Resource.Drawable.ic_noti);
			n.SetContentText(message);
			n.SetVibrate(new long[] {
				200,
				200,
				100,
			});

			var nm = NotificationManager.FromContext(context);
			nm.Notify(0, n.Build());


			return true;
		}

		internal static string GetPayload(Intent intent)
		{
			if (intent.Extras.ContainsKey("payload"))
				return intent.Extras.Get("payload").ToString();

			return null;
		}

	}


	[Service] // Must use the service tag
	public class PushHandlerService : GcmServiceBase
	{
		public static string RegistrationID { get; private set; }
		private NotificationHub Hub { get; set; }

		public PushHandlerService() : base(Constants.SenderID)
		{
			Log.Info(MyBroadcastReceiver.TAG, "PushHandlerService() constructor");
		}


		protected override void OnMessage(Context context, Intent intent)
		{
			MyBroadcastReceiver.HandlePushNotification(context, intent);
		}



		protected override void OnRegistered(Context context, string registrationId)
		{
			Log.Verbose(MyBroadcastReceiver.TAG, "GCM Registered: " + registrationId);
			Settings.PNSRegistrationId = registrationId;
			RegistrationID = registrationId;

			//begin registration to hub

			var tags = new List<string>() { "all" }; // create tags if you want
																	   //var tags = new List<string>() { };

			InvokeAPIRegistration(new DeviceRegistration() { Platform = "Android", Handle = registrationId, Tags = tags.ToArray() });


		}

		public async Task InvokeAPIRegistration(DeviceRegistration deviceRegistration)
		{
			var hubregistrationid=await MobileClient.client.InvokeApiAsync<DeviceRegistration, string>("Notification/registerWithHub", deviceRegistration, HttpMethod.Put, null);
			Settings.HubRegistrationId = hubregistrationid;
		}
		private async Task InvokeAPI()
		{
			var ret = await MobileClient.client.InvokeApiAsync<string>("Notification/HubRegistration", HttpMethod.Get, null);
			Log.Info(ret, ret);
		}

		protected override void OnError(Context context, string errorId)
		{
			Log.Error("MyBroadcastReceiver", "GCM Error: " + errorId);
		}	
		protected override void OnUnRegistered(Context context, string registrationId)
		{
			Log.Verbose(MyBroadcastReceiver.TAG, "GCM Unregistered: " + registrationId);

		}



	}
}
