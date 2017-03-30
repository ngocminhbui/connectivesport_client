
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
                    StartActivity(typeof(Practice));
                }
            };
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
