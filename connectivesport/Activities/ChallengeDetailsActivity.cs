
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
	[Activity(Label = "ChallengeDetailsActivity")]
	public class ChallengeDetailsActivity : Activity
	{
		Challenge _challenge;
		public const string CHALLENGE_ID = "0";
		void ButtonFinish_Click(object sender, EventArgs e)
		{
			AlertDialog.Builder alert = new AlertDialog.Builder(this);
			alert.SetTitle("Confirm Finish");
			alert.SetMessage("Are you sure?");
			alert.SetPositiveButton("Finish", (senderAlert, args) =>
			{
				Toast.MakeText(this, "Finish!", ToastLength.Short).Show();
			});

			alert.SetNegativeButton("Cancel", (senderAlert, args) =>
			{
				Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
			});

			Dialog dialog = alert.Create();
			dialog.Show();
		}

		RecyclerView mRecyclerView;

		LinearLayoutManager mLayoutManager;

		List<User> mFriendList;

		FriendGoalAdapter mAdapter;

		Button buttonFinish;
		Button buttonPractice;

		ProgressBar progrssBarObj;

		TextView textViewGoalMessage;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.details_challenge);

			string challengeId = Intent.GetStringExtra(CHALLENGE_ID);

			Challenge challenge = LocalDataManager.instance.getChallengeById(challengeId);

			textViewGoalMessage = FindViewById<TextView>(Resource.Id.textViewGoalMessage);
			textViewGoalMessage.Text = challenge.CustomMessage;

			progrssBarObj = FindViewById<ProgressBar>(Resource.Id.progressBarSync);
			progrssBarObj.Max = 100;
			progrssBarObj.Progress = 70;

			mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

			mLayoutManager = new LinearLayoutManager(this);
			mRecyclerView.SetLayoutManager(mLayoutManager);

			mFriendList = LocalDataManager.instance.lsUserChallenge;
			// Plug in my adapter:
			mAdapter = new FriendGoalAdapter(this, mFriendList);
			mRecyclerView.SetAdapter(mAdapter);

			buttonFinish = FindViewById<Button>(Resource.Id.buttonFinish);

			buttonFinish.Click += ButtonFinish_Click;


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
