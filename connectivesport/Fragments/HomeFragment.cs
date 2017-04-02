
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
using Microsoft.Band;
using System.Threading.Tasks;

namespace connectivesport
{
	public class HomeFragment : Fragment
	{
		RecyclerView mRecyclerView;
		RecyclerView mRecyclerViewChallenge;
		RecyclerView.LayoutManager mLayoutManager;
		GoalAdapter mAdapter;
		List<Goal> mGoalList;
		List<Challenge> mChallengeList;
		FloatingActionButton mbutton;
		TextView txtcalories;
		TextView txtstep;
		TextView txtHeartRate;

		public static HomeFragment NewInstance()
		{
			HomeFragment fragment = new HomeFragment();
			return fragment;
		}

		public override async void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
			if (Settings.User == null)
				Settings.User= await DataManagement.instance.GetApplicationUser(Settings.fbid); //force to get user before get list
			mGoalList = await DataManagement.instance.getGoal(Settings.User);
			//mGoalList = LocalDataManager.instance.lsGoal;
			mChallengeList = LocalDataManager.instance.lsChallenge;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment

			var rootView = inflater.Inflate(Resource.Layout.fragment_home, container, false);
			mRecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerView);
			mRecyclerViewChallenge = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerViewChallenge);
			txtcalories = rootView.FindViewById<TextView>(Resource.Id.textViewCalories);
			txtstep = rootView.FindViewById<TextView>(Resource.Id.textViewSteps);
			txtHeartRate = rootView.FindViewById<TextView>(Resource.Id.textViewBeat);

			mLayoutManager = new LinearLayoutManager(this.Activity);
			mRecyclerView.SetLayoutManager(mLayoutManager);

			// Plug in my adapter:
			PlugGoal();
			PlugChallenge();






			mbutton = rootView.FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton);
			mbutton.Click += AddNewGoal;
				
			return rootView;
		}

		public async Task PlugGoal()
		{
			if (mGoalList == null)
				//force goal list
				mGoalList = await DataManagement.instance.getGoal(Settings.User);

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
		}

		public async Task PlugChallenge()
		{
			if (mChallengeList == null)
				//force goal list
				mChallengeList = await DataManagement.instance.getChallenge(Settings.User);

			
			mRecyclerViewChallenge.SetLayoutManager(new LinearLayoutManager(this.Activity));

			var adapterChallenge = new ChallengeAdapter(this.Activity, mChallengeList);
			mRecyclerViewChallenge.SetAdapter(adapterChallenge);

			mRecyclerViewChallenge.SetItemClickListener((rv, position, view) =>
			{
				//An item has been clicked
				Context context = view.Context;
				Intent intent = new Intent(context, typeof(ChallengeDetailsActivity));
				intent.PutExtra(ChallengeDetailsActivity.CHALLENGE_ID, mChallengeList[position].Id);

				context.StartActivity(intent);
			});
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

				//if (BandConnector.instance.BandClient.SensorManager.CurrentHeartRateConsent != UserConsent.Granted)
				//{
				//	BandConnector.instance.BandClient.SensorManager.RequestHeartRateConsentTaskAsync(this.Activity).Wait();
				//}
				//else sensorHeartRate.StartReadings();
					
				sensorCalories.StartReadings();
				sensorDistance.StartReadings();

			}
		}

		void DistanceSensors_ReadingChanged(object sender, IBandSensorEventEventArgs<IBandDistanceEvent> e)
		{
			try
			{
				this.Activity.RunOnUiThread(() =>
				{
					txtstep.Text = e.SensorReading.DistanceToday.ToString();
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
					txtHeartRate.Text = e.SensorReading.HeartRate.ToString();
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
