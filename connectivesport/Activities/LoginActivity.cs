
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
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Facebook;
using Newtonsoft.Json.Linq;

namespace connectivesport
{
	[Activity(Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login);





            Button login = (Button)FindViewById(Resource.Id.btnFacebook);
            login.Click += async (sender , e) =>
            {
                bool result = await Authenticate();
                if (result)
                {
                    string token = user.MobileServiceAuthenticationToken;

                    Newtonsoft.Json.Linq.JToken x = await UserManager.DefaultManager.CurrentClient.InvokeApiAsync("/.auth/me");
                    string s = (string)x[0]["access_token"];

                    FacebookClient fb = new FacebookClient(s);
                    JToken profile = JToken.Parse(fb.Get("me/?fields=picture,name,email").ToString());


					await PushNewUser(profile,(string)profile["id"]);




					Settings.fbid = (string)profile["id"];
                    GetApplicationUser((string)profile["id"]);
					StartActivity(typeof(MainActivity));
                }
            };
		}

		public async Task PushNewUser(JToken profile,string fbid)
		{
			if ( await DataManagement.instance.IsUserExisted(fbid) == false)
			{
				User nUser = new User() { Username = (string)profile["name"], AvatarURL = (string)profile["picture"]["data"]["url"], Email = (string)profile["email"], LastLocationX = 5.3, LastLocationY = 3.2, UserID = (string)profile["id"], LastLogin = DateTime.Now, RegistrationDate = DateTime.Now };
				 
				await DataManagement.instance.client.GetTable<User>().InsertAsync(nUser);

			}
		}

		public async Task GetApplicationUser(string fbid)
		{
			Settings.User = await DataManagement.instance.GetApplicationUser(fbid);
		}
        private MobileServiceUser user;

        private async Task<bool> Authenticate()
        {
            var success = false;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                user = await UserManager.DefaultManager.CurrentClient.LoginAsync(this,
                    MobileServiceAuthenticationProvider.Facebook);

                success = true;
            }
            catch (Exception ex)
            {
                //CreateAndShowDialog(ex, "Authentication failed");
            }
            return success;
        }
    }
}
