
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

namespace connectivesport
{
	[Activity(Label = "AddGoalActivity", Theme="@style/BaseTheme")]
	public class AddGoalActivity : Activity
	{
		TextView _dateDisplay;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.activity_add_goal);

			_dateDisplay = FindViewById<TextView>(Resource.Id.textViewDate);
			Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerSport);

			spinner.ItemSelected += spinner_ItemSelected;
			var adapter = ArrayAdapter.CreateFromResource(
					this, Resource.Array.planets_array, Android.Resource.Layout.SimpleSpinnerItem);
			
			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

			spinner.Adapter = adapter;

			var spinnerGoalType = FindViewById<Spinner>(Resource.Id.spinnerGoalType);
			//spinner.ItemSelected += onSpinnerGoalTypeSelected;
			_dateDisplay.Click += DateSelect_OnClick;
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
