
using System;
using System.Collections.Generic;
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
using Android.Views;
using Android.Widget;

namespace connectivesport
{
	[Activity(Label = "connectivesport", MainLauncher = true, Icon = "@mipmap/icon")]
	public class ViewFriendMapActivity : Activity, IOnMapReadyCallback
	{
		GoogleMap map;

		Location _currentLocation;




		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.FriendMapLayout);
			// Create your application here

			FragmentManager.FindFragmentById<MapFragment>(Resource.Id.friendmap).GetMapAsync(this);

			BindFriendList();
		}

		public async Task BindFriendList()
		{
			Contract.Ensures(Contract.Result<Task>() != null);
			ListView lv_friendlist = (ListView)FindViewById(Resource.Id.listview_friendlistmap);

			try
			{
			var friends = await UserManager.DefaultManager.GetUsersAsync(true);

				foreach (var f in friends)
				{
					if (f.LastLocationX != null && f.LastLocationY != null)
						map.AddMarker(new Android.Gms.Maps.Model.MarkerOptions().SetPosition(new Android.Gms.Maps.Model.LatLng(3, 3)).SetTitle(f.Username).SetSnippet("Practicing"));
				}

				FriendListMapAdapter adp = new FriendListMapAdapter(this, friends.ToList());
				lv_friendlist.Adapter = adp;
				lv_friendlist.ItemClick += (sender, e) =>
				{
					User u = adp.GetUser(e.Position);
					if (u.LastLocationX != null && u.LastLocationY != null)
						map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new Android.Gms.Maps.Model.LatLng((double)u.LastLocationX, (double)u.LastLocationY), 18));
				};
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
