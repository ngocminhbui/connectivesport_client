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

namespace connectivesport
{
    [Activity(Label = "MainTab_Activity")]
    public class MainTab_Activity : Activity
    {
        DataManagement DAO = new DataManagement();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainTab_Activity);

            await DAO.getData(AddUserActivity.client);
            EditText t = (EditText)FindViewById(Resource.Id.version);
            t.Text = DataManagement.medal_list.Count.ToString();           

            Button connect = (Button)FindViewById(Resource.Id.connectband);
            connect.Click += (sender, e) =>
            {
                StartActivity(typeof(Practice));
            };

            Button sign_out = (Button)FindViewById(Resource.Id.signout);
            sign_out.Click += async (sender, e) =>
            {
                var contextRef = Application.Context.GetSharedPreferences("localData", Android.Content.FileCreationMode.Append);
                var contextEdit = contextRef.Edit();
                contextEdit.PutBoolean("IsLogin", false);
                contextEdit.Commit();

                await AddUserActivity.client.LogoutAsync();
                StartActivity(typeof(AddUserActivity));
            };
        }
    }
}