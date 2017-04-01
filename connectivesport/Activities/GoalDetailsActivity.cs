﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace connectivesport
{
	[Activity(Label = "GoalDetailsActivity")]
	public class GoalDetailsActivity : Activity
	{
		Goal _goal;
		public const string GOAL_ID = "0";
		void ButtonChallenge_Click(object sender, EventArgs e)
		{
			Bundle bundle = new Bundle();
			FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
			FriendDialogFragment friendDialog = FriendDialogFragment.NewInstance();
			//bundle.PutString("goalID", _goal.Id);
			friendDialog.Arguments = bundle;
			friendDialog.Show(fragmentTransaction, "Friend List");
		}

		RecyclerView mRecyclerView;

		LinearLayoutManager mLayoutManager;

		List<User> mFriendList;

		FriendGoalAdapter mAdapter;

		Button buttonChallenge;
		Button buttonPractice;

		ProgressBar progrssBarObj;

		TextView textViewGoalMessage;




		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.details_goal);

			string goalID = Intent.GetStringExtra(GOAL_ID);

			Goal goal = LocalDataManager.instance.getGoalById(goalID);

			textViewGoalMessage = FindViewById<TextView>(Resource.Id.textViewGoalMessage);
			textViewGoalMessage.Text = goal.CustomMessage;

			progrssBarObj = FindViewById<ProgressBar>(Resource.Id.progressBarSync);
			progrssBarObj.Max = goal.Count.Value;
			progrssBarObj.Progress = goal.Current_Count.Value;

			mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

			mLayoutManager = new LinearLayoutManager(this);
			mRecyclerView.SetLayoutManager(mLayoutManager);

			mFriendList = LocalDataManager.instance.lsUser;
			// Plug in my adapter:
			mAdapter = new FriendGoalAdapter(this,mFriendList);
			mRecyclerView.SetAdapter(mAdapter);

			buttonChallenge = FindViewById<Button>(Resource.Id.buttonChallenge);

			buttonChallenge.Click += ButtonChallenge_Click;


			buttonPractice = FindViewById<Button>(Resource.Id.buttonStart);
			buttonPractice.Click += (sender, e) =>
			{
				Context context = this;
				Intent intent = new Intent(context, typeof(Practice));

				context.StartActivity(intent);
			};

		}


	}
}
