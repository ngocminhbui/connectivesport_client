
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using connectivesport.Helpers;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using Microsoft.Band.Sensors;

namespace connectivesport
{
	public class HomeFragment : Fragment
	{
		RecyclerView mRecyclerView;
		RecyclerView.LayoutManager mLayoutManager;
		GoalAdapter mAdapter;
		List<Goal> mGoalList;
		FloatingActionButton mbutton;
		TextView txtcalories;
		TextView txtstep;
		TextView txtbeat;

		public static HomeFragment NewInstance()
		{
			HomeFragment fragment = new HomeFragment();
			return fragment;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
			mGoalList = LocalDataManager.instance.lsGoal;

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment

			var rootView = inflater.Inflate(Resource.Layout.fragment_home, container, false);
			mRecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerView);
			txtcalories = rootView.FindViewById<TextView>(Resource.Id.textViewCalories);
			txtstep = rootView.FindViewById<TextView>(Resource.Id.textViewSteps);
			txtbeat = rootView.FindViewById<TextView>(Resource.Id.textViewBeat);

			mLayoutManager = new LinearLayoutManager(this.Activity);
			mRecyclerView.SetLayoutManager(mLayoutManager);

			// Plug in my adapter:
			mAdapter = new GoalAdapter(this.Activity, mGoalList);
			mRecyclerView.SetAdapter(mAdapter);

			mRecyclerView.SetItemClickListener((rv, position, view) =>
			{
				//An item has been clicked
				Context context = view.Context;
				Intent intent = new Intent(context, typeof(GoalDetailsActivity));
				intent.PutExtra(GoalDetailsActivity.GOAL_ID, mGoalList[position].Id);

				context.StartActivity(intent);
			});
			mbutton = rootView.FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton);
			mbutton.Click += AddNewGoal;
				
			return rootView;
		}

		public override void OnResume()
		{
			base.OnResume();
			if (BandConnector.instance.isConnected())
			{
				CaloriesSensor sensorCalories = BandConnector.instance.BandClient.SensorManager.CreateCaloriesSensor();
				sensorCalories.ReadingChanged += CaloriesSensors_ReadingChanged;

				HeartRateSensor sensorHeartRate = BandConnector.instance.BandClient.SensorManager.CreateHeartRateSensor();
				sensorHeartRate.ReadingChanged += HeartRateSensors_ReadingChanged;

				DistanceSensor sensorDistance = BandConnector.instance.BandClient.SensorManager.CreateDistanceSensor();
				sensorDistance.ReadingChanged += DistanceSensors_ReadingChanged;


				sensorCalories.StartReadings();
				sensorDistance.StartReadings();
				//sensorHeartRate.StartReadings();
			}
		}

		void DistanceSensors_ReadingChanged(object sender, IBandSensorEventEventArgs<IBandDistanceEvent> e)
		{
			try
			{
				this.Activity.RunOnUiThread(() =>
				{
					txtstep.Text = e.SensorReading.TotalDistance.ToString();
				});
			}catch (Exception ex)
			{
				Console.WriteLine(ex);

			}
		}

		void HeartRateSensors_ReadingChanged(object sender, IBandSensorEventEventArgs<IBandHeartRateEvent> e)
		{
			try
			{
				this.Activity.RunOnUiThread(() =>
				{
					txtstep.Text = e.SensorReading.HeartRate.ToString();
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);

			}
		}

		void AddNewGoal(object sender, EventArgs e)
		{
			Context context = this.Context;
			Intent intent = new Intent(context, typeof(AddGoalActivity));
			context.StartActivity(intent);
		}

		private void CaloriesSensors_ReadingChanged(object sender, IBandSensorEventEventArgs<IBandCaloriesEvent> e)
		{
			try
			{
				this.Activity.RunOnUiThread(() =>
				{
					txtcalories.Text = e.SensorReading.CaloriesToday.ToString();
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);

			}

		}
}
}
