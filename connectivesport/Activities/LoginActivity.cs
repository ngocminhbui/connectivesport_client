
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
	[Activity(Label = "LoginActivity", MainLauncher = false)]
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

                    var UserTable = UserManager.DefaultManager.CurrentClient.GetTable<User>();
                    var test = from each in UserTable where each.UserID == (string)profile["id"] select each.UserID;
                    if (test.Parameters.Count == 0)
                    {
                        User nUser = new User() { Username = (string)profile["name"], AvatarURL = (string)profile["picture"]["data"]["url"], Email = (string)profile["email"], LastLocationX = 5.3, LastLocationY = 3.2, UserID = (string)profile["id"], LastLogin = DateTime.Now, RegistrationDate = DateTime.Now };
                        await UserTable.InsertAsync(nUser);

                        /*JToken friends = JToken.Parse(fb.Get("me/friends").ToString());
                        for (int i = 0; i < friends["data"].Count(); i++)
                        {
                            string id = friends["data"][i]["id"].ToString();
                            JToken friend = JToken.Parse(fb.Get(id + "/?fields=picture,name").ToString());
                            string name = friend["name"].ToString();
                            string url = friend["picture"]["data"]["url"].ToString();
                            Friend nFriend = new Friend() { AcceptDate = DateTime.Now, valid = true, RequestUserId = (string)friend["id"], AcceptUserId = (string)profile["id"], UserId = "abc"};
                            await FriendTable.InsertAsync(nFriend);
                        }*/
                    }

                    /*var contextEdit = contextRef.Edit();
                    contextEdit.PutString("FBToken", s);
                    contextEdit.PutBoolean("IsLogin", true);
                    contextEdit.Commit();*/

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
