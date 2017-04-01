
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Android.Graphics;
using Android.Gms.Maps.Model;

namespace connectivesport
{
	[Activity(Label = "ViewSportShopActivity", MainLauncher = false, Icon = "@mipmap/icon")]
	public class ViewSportShopActivity : Activity, IOnMapReadyCallback, RequestWebListenerInterface
	{
		GoogleMap map;

		Location _currentLocation;


		bool setCamera = false;
		public void OnLocationChanged(Location location)
		{
			
		}
		int REQUEST_STORE = 394349;
		int REQUEST_GYM = 393583;
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
						map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(new Android.Gms.Maps.Model.LatLng(_currentLocation.Latitude, _currentLocation.Longitude), 18));

					}
					setCamera = true;
					string url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={e.Location.Latitude},{e.Location.Longitude}&radius=500&keyword=sport%20store&key=AIzaSyCm5dIYBD1Bemm9U5RzXLOVggIOb6Gl3ss";
					new RequestWeb(url, this).Execute(REQUEST_STORE);
					url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={e.Location.Latitude},{e.Location.Longitude}&radius=500&keyword=gym&key=AIzaSyCm5dIYBD1Bemm9U5RzXLOVggIOb6Gl3ss";
					new RequestWeb(url, this).Execute(REQUEST_GYM);

				}
			
			}; 

		}

	


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ShopMapLayout);



			// Create your application here

			FragmentManager.FindFragmentById<MapFragment>(Resource.Id.shopmap).GetMapAsync(this);


		}


		public void OnWebRequestResult(string responseText, int code)
		{
			if (code == REQUEST_STORE)
			{
				try
				{
					dynamic json = JObject.Parse(responseText);
					var results = json["results"];

					List<SportStore> list_stores = JsonConvert.DeserializeObject<IEnumerable<SportStore>>(results.ToString());
					foreach (var store in list_stores)
					{
						map.AddMarker(new Android.Gms.Maps.Model.MarkerOptions()
						              .SetPosition(new Android.Gms.Maps.Model.LatLng(store.geometry.location.lat, store.geometry.location.lng))
						              .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.storemarker50))
						              .SetTitle(store.name));
										
					}
				}
				catch (Exception e)
				{

				}
			}
			else if (code == REQUEST_GYM)
			{
				try
				{
					dynamic json = JObject.Parse(responseText);
					var results = json["results"];

					List<Gym> list_stores = JsonConvert.DeserializeObject<IEnumerable<Gym>>(results.ToString());
					foreach (var store in list_stores)
					{
						map.AddMarker(new Android.Gms.Maps.Model.
													  MarkerOptions().SetPosition(new Android.Gms.Maps.Model.LatLng(store.geometry.location.lat, store.geometry.location.lng))
																	 .SetTitle(store.name).SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.gym)
						              	
						                                                                          ));
					}
				}
				catch (Exception e)
				{

				}
			}


		}
	}
}
