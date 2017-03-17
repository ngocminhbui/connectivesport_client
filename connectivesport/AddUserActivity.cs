
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gcm.Client;

using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Android.Gms.Common;


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;

namespace connectivesport
{
	[Activity(MainLauncher = true,
			   Icon = "@drawable/ic_launcher", Label = "@string/app_name",
			   Theme = "@style/AppTheme")]
	public class AddUserActivity : Activity
	{
		public static AddUserActivity CurrentActivity { get; private set; }

		TextView msgText;
		public static AddUserActivity instance;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			instance = this;
			// Create your application here
			SetContentView((Resource.Layout.Activity_Add_User));

			msgText = FindViewById<TextView>(Resource.Id.msgText);
			IsPlayServicesAvailable();	CurrentActivity = this;

			RegisterWithGCM();


            
            InvokeAPI();
           


            Button AddUserButton = (Button)FindViewById(Resource.Id.buttonAdduser);
			AddUserButton.Click += (sender, e) =>
			{
				UserManager usrmng = UserManager.DefaultManager;
				var client = usrmng.CurrentClient;

				var table = client.GetTable<User>();
				table.InsertAsync(new User { Username = ((EditText)FindViewById(Resource.Id.userid)).Text.ToString()});
				Toast.MakeText(this, "Added user", ToastLength.Long).Show();


			};

		}

        private async Task InvokeAPI()
        {
            var client = new MobileServiceClient(Settings.ApplicationURL);
            Dictionary<String, String> pr = new Dictionary<String, String>();
            pr["athleteId"] = "6";

            var ret = await client.InvokeApiAsync("SendTestPushNotification", HttpMethod.Get, pr);


        }

        private void RegisterWithGCM()
		{
			try
			{
				// Check to ensure everything's set up right
				GcmClient.CheckDevice(this);
				GcmClient.CheckManifest(this);

				// Register for push notifications
				Log.Info("MainActivity", "Registering...");
				GcmClient.Register(this, MyBroadcastReceiver.SENDER_IDS);
				//GcmClient.UnRegister(this);
			}
			catch (Exception e)
			{
			}
		}
		public bool IsPlayServicesAvailable()
		{
			int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			if (resultCode != ConnectionResult.Success)
			{
				if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
					msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
				else
				{
					msgText.Text = "This device is not supported";
					Finish();
				}
				return false;
			}
			else
			{
				msgText.Text = "Google Play Services is available.";
				return true;
			}
		}
	}
}
