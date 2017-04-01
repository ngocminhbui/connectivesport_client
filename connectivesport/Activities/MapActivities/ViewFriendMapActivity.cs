
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;

namespace connectivesport
{
	[Activity(Label = "connectivesport", MainLauncher = true, Icon = "@mipmap/icon",Theme = "@style/BaseTheme")]
	public class ViewFriendMapActivity : AppCompatActivity, IOnMapReadyCallback
	{
		GoogleMap map;

		Location _currentLocation;
		NavigationView navigationView;

		DrawerLayout drawerLayout;

		List<User> friends;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.FriendMapLayout);
			// Create your application here

			FragmentManager.FindFragmentById<MapFragment>(Resource.Id.friendmap).GetMapAsync(this);


			drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

			// Initialize toolbar
			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
			SetSupportActionBar(toolbar);
			SupportActionBar.SetTitle(Resource.String.app_name);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetDisplayShowHomeEnabled(true);

			// Attach item selected handler to navigation view
			navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

			// Create ActionBarDrawerToggle button and add it to the toolbar
			var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
			drawerLayout.SetDrawerListener(drawerToggle);
			drawerToggle.SyncState();

			//Load default screen
			//var ft = FragmentManager.BeginTransaction();
			//ft.AddToBackStack(null);
			//ft.Add(Resource.Id.HomeFrameLayout, new HomeFragment());
			//ft.Commit();

			BindFriendList();
		}
		void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
		{
			if (e.MenuItem.ItemId == Resource.Id.nav_ar)
			{
				//start an's AR.
				drawerLayout.CloseDrawers();

				return;
			}

			User u;
			try
			{
				u = friends[e.MenuItem.ItemId];
				drawerLayout.CloseDrawers();
				if (u.LastLocationX != null && u.LastLocationY != null)
					map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new Android.Gms.Maps.Model.LatLng((double)u.LastLocationX, (double)u.LastLocationY), 18));
			}
			catch (Exception f)
			{
				
			}

			// Close drawer
		}

		public async Task BindFriendList()
		{
			Contract.Ensures(Contract.Result<Task>() != null);
			//ListView lv_friendlist = null;//(ListView)FindViewById(Resource.Id.listview_friendlistmap);

			try
			{
				var client = new MobileServiceClient(Constants.ApplicationURL);//raapplicationURL);
				friends = await client.GetTable<User>().ToListAsync();
				for (int i = 0; i < friends.Count; i++)
				{
					var f = friends[i];
					if (f.LastLocationX != null && f.LastLocationY != null)
						map.AddMarker(new Android.Gms.Maps.Model.MarkerOptions().SetPosition(new Android.Gms.Maps.Model.LatLng((double)f.LastLocationX, (double)f.LastLocationY)).SetTitle(f.Username).SetSnippet("Practicing"));

					IMenuItem menu_item_1= navigationView.Menu.GetItem(1);
					menu_item_1.SubMenu.Add(Resource.Id.nav_groupuser, i, i, f.Username);

				}


			}
			catch (Exception e)
			{
			}
		}
		bool setCamera = false;

		public void OnMapReady(GoogleMap googleMap)
		{
			this.map = googleMap;
			this.map.MyLocationEnabled = true;


			this.map.MyLocationChange += (object sender, GoogleMap.MyLocationChangeEventArgs e) => { 
			

				this._currentLocation = e.Location;
				if (!setCamera)
				{
					if (this._currentLocation != null)
					{
						map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new Android.Gms.Maps.Model.LatLng(_currentLocation.Latitude, _currentLocation.Longitude), 18));
					}
					setCamera = true;
				}


			};
		

		}


	}
}
