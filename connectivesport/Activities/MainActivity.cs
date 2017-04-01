
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

using Android.Support.V7.App;

using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
namespace connectivesport
{

	[Activity(Label = "ConnectiveSport", MainLauncher = false, Theme = "@style/BaseTheme")]

	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here

			SetContentView(Resource.Layout.activity_main);

			BottomNavigationView bottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottomNavigationView);


			bottomNavigationView.NavigationItemSelected += (sender, e) =>
			{
				Fragment selectedFragment = HomeFragment.NewInstance();
				switch (e.Item.ItemId)
				{

					case Resource.Id.home_item:
						//Toast.MakeText(this, "home click", ToastLength.Short).Show();
						selectedFragment = HomeFragment.NewInstance();
						break;
					case Resource.Id.friend_item:
						//Toast.MakeText(this, "friend click", ToastLength.Short).Show();
						selectedFragment = FriendFragment.NewInstance();
						break;
					case Resource.Id.profile_item:
						//Toast.MakeText(this, "friend click", ToastLength.Short).Show();
						selectedFragment = UserFragment.NewInstance();
						break;
				}
				FragmentTransaction transaction = SupportFragmentManager.BeginTransaction();
				transaction.Replace(Resource.Id.frameLayout, selectedFragment);
				transaction.Commit();
			};

			FragmentTransaction transaction1 = SupportFragmentManager.BeginTransaction();
			transaction1.Replace(Resource.Id.frameLayout, HomeFragment.NewInstance());
			transaction1.Commit();
		}
	}
}
