
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Gcm.Client;
using Java.Lang;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using WindowsAzure.Messaging;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
namespace connectivesport
{
	[Service(Name = "connectivesport.gcmservice")]
	public class GcmService : GcmServiceBase
	{
		public static string RegistrationID { get; private set; }
		private NotificationHub Hub { get; set; }




		public GcmService() : base(Constants.SenderID) {

			Log.Info(PushHandlerBroadcastReceiver.TAG, "PushHandlerService() constructor");

		}

		protected override void OnMessage(Context context, Intent intent)
		{
			Log.Info(PushHandlerBroadcastReceiver.TAG, "GCM Message Received!");

			var msg = new StringBuilder();

			if (intent != null && intent.Extras != null)
			{
				foreach (var key in intent.Extras.KeySet())
					msg.Append(key + "=" + intent.Extras.Get(key).ToString());
			}

			string messageText = intent.Extras.GetString("message");
			if (!string.IsNullOrEmpty(messageText))
			{
				CreateNotification("New hub message!", messageText);
			}
			else
			{
				CreateNotification("Unknown message details", msg.ToString());
			}
		}

		void CreateNotification(string title, string desc)
		{
			//Create notification
			var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

			//Create an intent to show UI
			var uiIntent = new Intent(this, typeof(AddUserActivity));

			//Create the notification
			var notification = new Notification(Android.Resource.Drawable.SymActionEmail, title);

			//Auto-cancel will remove the notification once the user touches it
			notification.Flags = NotificationFlags.AutoCancel;

			//Set the notification info
			//we use the pending intent, passing our ui intent over, which will get called
			//when the notification is tapped.
			notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, 0));

			//Show the notification
			notificationManager.Notify(1, notification);
			dialogNotify(title, desc);
		}
		protected void dialogNotify(string title, string message)
		{

			AddUserActivity.instance.RunOnUiThread(() =>
			{
				AlertDialog.Builder dlg = new AlertDialog.Builder(AddUserActivity.instance);
				AlertDialog alert = dlg.Create();
				alert.SetTitle(title);
				alert.SetButton("Ok", delegate
				{
					alert.Dismiss();
				});
				alert.SetMessage(message);
				alert.Show();
			});
		}


		protected override void OnError(Context context, string errorId)
		{
			Log.Error("PushHandlerBroadcastReceiver", "GCM Error: " + errorId);
		}

		protected override void OnRegistered(Context context, string registrationId)
		{
			Log.Verbose(PushHandlerBroadcastReceiver.TAG, "GCM Registered: " + registrationId);
			RegistrationID = registrationId;

			CreateNotification("PushHandlerService-GCM Registered...",
								"The device has been Registered!");

			Hub = new NotificationHub(Constants.NotificationHubName, Constants.ListenConnectionString,
										context);
			try
			{
				Hub.UnregisterAll(registrationId);
			}
			catch (Java.Lang.Exception ex)
			{
				Log.Error(PushHandlerBroadcastReceiver.TAG, ex.Message);
			}

			//var tags = new List<string>() { "falcons" }; // create tags if you want
			var tags = new List<string>() { };

			try
			{
				var hubRegistration = Hub.Register(registrationId, tags.ToArray());
			}
			catch (Java.Lang.Exception ex)
			{
				Log.Error(PushHandlerBroadcastReceiver.TAG, ex.Message);
			}
		}

		protected override void OnUnRegistered(Context context, string registrationId)
		{
			Log.Verbose(PushHandlerBroadcastReceiver.TAG, "GCM Unregistered: " + registrationId);

			CreateNotification("GCM Unregistered...", "The device has been unregistered!");
		}

		public async void s(Microsoft.WindowsAzure.MobileServices.Push push, IEnumerable<string> tags)
		{
			try
			{
				const string templateBodyGCM = "{\"data\":{\"message\":\"$(messageParam)\"}}";

				JObject templates = new JObject();
				templates["genericMessage"] = new JObject
			{
				{"body", templateBodyGCM}
			};

				await push.RegisterAsync(RegistrationID, templates);
				Log.Info("Push Installation Id", push.InstallationId.ToString());
			}
			catch (Java.Lang.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				Debugger.Break();
			}
		}
	}

}
