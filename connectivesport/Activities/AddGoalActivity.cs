﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace connectivesport
{
	[Activity(Label = "AddGoalActivity", Theme="@style/BaseTheme")]
	public class AddGoalActivity : Activity
	{
		TextView _dateDisplay;
		Button _cancelButton;
		Button _saveButton;
		Goal _goal;
		protected async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.activity_add_goal);

			_dateDisplay = FindViewById<TextView>(Resource.Id.textViewDate);
			Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerSport);
			Spinner spinnerGoalType = FindViewById<Spinner>(Resource.Id.spinnerGoalType);
			Spinner spinnerGoalTime = FindViewById<Spinner>(Resource.Id.spinnerGoalTime);
			_cancelButton = FindViewById<Button>(Resource.Id.buttonCancel);
			_saveButton = FindViewById<Button>(Resource.Id.buttonSave);

			_cancelButton.Click += (sender, e) => OnBackPressed();

			_saveButton.Click += (sender, e) => AddNewGoal();
			//await Task.Run(async () =>
			//{
			//	var a = new SportAdapter(this, await ListSportData());

			//	spinner.Adapter = a;
			//});

			var a = new SportAdapter(this, await ListSportData());

			spinner.Adapter = a;
			spinner.ItemSelected += spinner_ItemSelected;

			var adapter = ArrayAdapter.CreateFromResource(
				this, Resource.Array.planets_array, Android.Resource.Layout.SimpleSpinnerItem);
			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

			spinnerGoalType.Adapter = adapter;

			spinnerGoalTime.Adapter = adapter;

			var progrssBarObj = FindViewById<ProgressBar>(Resource.Id.progressBarSync);
			progrssBarObj.Max = 70;
			progrssBarObj.Progress = 50;
			//await Task.Run(() =>
			//{
			//	for (int i = 0; i < 100; i++)
			//	{
			//		int cnt = 0;
			//		cnt = cnt + i;
			//		progrssBarObj.Progress = cnt;
			//	}
			//});


			//spinner.ItemSelected += onSpinnerGoalTypeSelected;
		}

		void AddNewGoal()
		{
			throw new NotImplementedException();
			Finish();
		}

		async Task<List<Sport>> ListSportData()
		{
			return await UserManager.DefaultManager.CurrentClient.GetTable<Sport>().ToListAsync();
			//return sportList;
		}


		void onSpinnerGoalTypeSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			throw new NotImplementedException();
		}

		void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
			Toast.MakeText(this, toast, ToastLength.Long).Show();
		}

		void DateSelect_OnClick(object sender, EventArgs eventArgs)
		{
			DateTime acceptedTime;
			DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
																	 {
																		 _dateDisplay.Text = time.ToLongDateString();
																		 acceptedTime = time;
																	 });
			frag.Show(FragmentManager, DatePickerFragment.TAG);
		}
	}
}
