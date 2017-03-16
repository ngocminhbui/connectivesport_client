using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.Util;
using Gcm.Client;
using WindowsAzure.Messaging;


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



			return true;
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
			RegistrationID = registrationId;



			Hub = new NotificationHub(Constants.NotificationHubName, Constants.ListenConnectionString,
										context);
			try
			{
				Hub.UnregisterAll(registrationId);
			}
			catch (Java.Lang.Exception ex)
			{
				Log.Error(MyBroadcastReceiver.TAG, ex.Message);
			}

			var tags = new List<string>() { "falcons" }; // create tags if you want
			//var tags = new List<string>() { };

			try
			{
				var hubRegistration = Hub.Register(registrationId, tags.ToArray());
			}
			catch (Java.Lang.Exception ex)
			{
				Log.Error(MyBroadcastReceiver.TAG, ex.Message);
			}
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
