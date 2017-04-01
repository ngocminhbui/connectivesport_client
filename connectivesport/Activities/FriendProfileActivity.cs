
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
	[Activity(Label = "FriendProfile")]
	public class FriendProfileActivity : Activity
	{
		RecyclerView mRecyclerView;

		GridLayoutManager mLayoutManager;

		MedalAdapter mAdapter;

		List<Medal> mMedalList;

		public const string USER_ID = "0";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.user_profile);
			mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

			mLayoutManager = new GridLayoutManager(this, 3);
			mRecyclerView.SetLayoutManager(mLayoutManager);

			// Plug in my adapter:
			mMedalList = LocalDataManager.instance.lsMedal;
			mAdapter = new MedalAdapter(this, mMedalList);
			mRecyclerView.SetAdapter(mAdapter);


			string userID = Intent.GetStringExtra(USER_ID);
			User user = LocalDataManager.instance.getUserById(userID);

			var textViewName = FindViewById<TextView>(Resource.Id.user_profile_name);
			var textViewLastActive = FindViewById<TextView>(Resource.Id.user_profile_last_active);

			textViewName.Text = user.Username;
			textViewLastActive.Text = user.LastLogin.Value.ToString();


			var imageViewAvatar = FindViewById<ImageView>(Resource.Id.user_profile_photo);
			var drawableImage = Resources.GetIdentifier(user.AvatarURL, "drawable", PackageName);
			imageViewAvatar.SetImageResource(drawableImage);

		}
	}
}
