using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using connectivesport;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Facebook;

namespace connectivesport
{
	[Activity(MainLauncher = true,
			   Icon = "@drawable/ic_launcher", Label = "@string/app_name",
			   Theme = "@style/AppTheme")]

	public class AddUserActivity : Activity
	{

		public static MobileServiceClient client;
		const string applicationURL = @"https://connectivesport.azurewebsites.net";

        private MobileServiceUser user;

        private async Task<bool> Authenticate()
        {
            var success = false;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                user = await client.LoginAsync(this,
                    MobileServiceAuthenticationProvider.Facebook);

                success = true;
            }
            catch (Exception ex)
            {
                //CreateAndShowDialog(ex, "Authentication failed");
            }
            return success;
        }

        protected override async void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
            var contextRef = Application.Context.GetSharedPreferences("localData", Android.Content.FileCreationMode.Append); 
            if (contextRef.GetBoolean("IsLogin", false))
            {
                await Authenticate();
                StartActivity(typeof(MainTab_Activity));
            }
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Activity_Add_User);

			CurrentPlatform.Init();

			// Create the client instance, using the mobile app backend URL.
			client = new MobileServiceClient(applicationURL);

			var UserTable = client.GetTable<User>();

			Button addUser = (Button)FindViewById(Resource.Id.adduser);
			addUser.Click += async (sender, e) => {
                if (await Authenticate())
                {
                    string token = user.MobileServiceAuthenticationToken; 

                    Newtonsoft.Json.Linq.JToken x = await client.InvokeApiAsync("/.auth/me");
                    string s = (string)x[0]["access_token"];

                    var contextEdit = contextRef.Edit();
                    contextEdit.PutString("FBToken", s);
                    contextEdit.PutBoolean("IsLogin", true);
                    contextEdit.Commit();

                    FacebookClient fb = new FacebookClient(s);
                    JToken profile = JToken.Parse(fb.Get("me/?fields=picture,name,email").ToString());
                    User nUser = new User() { Username = (string)profile["name"], AvatarURL = (string)profile["picture"]["data"]["url"], Email = (string)profile["email"], LastLocationX = 5.3, LastLocationY = 3.2, UserID = "def", LastLogin = DateTime.Now, RegistrationDate = DateTime.Now };
                    await UserTable.InsertAsync(nUser);
                    StartActivity(typeof(MainTab_Activity));
                }
			};
		}


}
}
