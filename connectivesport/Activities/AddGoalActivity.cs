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
		Goal _goal=new Goal();
		Spinner spinner;
		EditText _goalValue;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.activity_add_goal);

			//_dateDisplay = FindViewById<TextView>(Resource.Id.textViewDate);
			spinner = FindViewById<Spinner>(Resource.Id.spinnerSport);
			Spinner spinnerGoalType = FindViewById<Spinner>(Resource.Id.spinnerGoalType);
			Spinner spinnerGoalTime = FindViewById<Spinner>(Resource.Id.spinnerGoalTime);
			_cancelButton = FindViewById<Button>(Resource.Id.buttonCancel);
			_saveButton = FindViewById<Button>(Resource.Id.buttonSave);
			_goalValue = FindViewById<EditText>(Resource.Id.editTextVal);

			_cancelButton.Click += (sender, e) => OnBackPressed();

			_saveButton.Click += (sender, e) => AddNewGoal();
			//Task.Run(async () =>
			//{
			//	var a = new SportAdapter(this, await ListSportData());

			//	spinner.Adapter = a;
			//});


			PlugSport();



			var adapterGoalType = ArrayAdapter.CreateFromResource(
				this, Resource.Array.goal_type, Android.Resource.Layout.SimpleSpinnerItem);
				adapterGoalType.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

			var adapterGoalTime = ArrayAdapter.CreateFromResource(
				this, Resource.Array.goal_time, Android.Resource.Layout.SimpleSpinnerItem);
			adapterGoalTime.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinnerGoalType.Adapter = adapterGoalType;

			spinnerGoalTime.Adapter = adapterGoalTime;

			spinnerGoalTime.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
			{
				Spinner spinnerTime = (Spinner)sender;

				goalTime = string.Format("{0}", spinnerTime.GetItemAtPosition(e.Position));
			};

			spinnerGoalType.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
			{
				Spinner spinnerType = (Spinner)sender;

				goalType = string.Format("{0}", spinnerType.GetItemAtPosition(e.Position));
			};


			//spinner.ItemSelected += onSpinnerGoalTypeSelected;
		}


		string goalTime;
		string goalType;
		void AddNewGoal()
		{
			//throw new NotImplementedException();
			_goal.Count = int.Parse(_goalValue.Text);
			_goal.Current_Count = 0;
			Sport sport = LocalDataManager.instance.getSportById(_goal.SportId);
			_goal.CustomMessage = sport.Name + " " + _goal.Count + " " + goalType + " a " + goalTime;
			//LocalDataManager.instance.lsGoal.Add(_goal);
			DataManagement.instance.addGoal(Goal, Settings.User);
			Finish();
		}


		List<Sport> ls;
		public async Task PlugSport()
		{
			ls = await DataManagement.instance.getSport();

			var a = new SportAdapter(this, ls);
			spinner.Adapter = a;
			spinner.ItemSelected += spinner_ItemSelected;

		}


		async Task<List<Sport>> ListSportData()
		{
			return await UserManager.DefaultManager.CurrentClient.GetTable<Sport>().ToListAsync();
			//return sportList;
		}




		void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			try
			{
				Spinner spinner = (Spinner)sender;
				_goal.SportId = ls[e.Position].Id;
				//_goal.SportId = spinner.GetItemAtPosition(e.Position);
				//string toast = string.Format("The planet is {0}", e.Position.ToString());
				//Toast.MakeText(this, toast, ToastLength.Long).Show();
			}
			catch
			{
			}
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
