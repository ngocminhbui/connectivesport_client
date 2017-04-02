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
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
namespace connectivesport
{
	public class UserFragment : Fragment
	{

		public static UserFragment NewInstance()
		{
			UserFragment fragment = new UserFragment();
			return fragment;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
			mMedalList = LocalDataManager.instance.lsMedal;
			user = LocalDataManager.instance.lsUser[0];
		}

		User user;

		RecyclerView mRecyclerView;

		GridLayoutManager mLayoutManager;
		 
		List<Medal> mMedalList;

		MedalAdapter mAdapter;

		public const string USER_ID = "0";

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			var rootView = inflater.Inflate(Resource.Layout.user_profile, container, false);
			mRecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerView);

			mLayoutManager = new GridLayoutManager(this.Activity,3);
			mRecyclerView.SetLayoutManager(mLayoutManager);

			// Plug in my adapter:

			mAdapter = new MedalAdapter(this.Activity, mMedalList);
			mRecyclerView.SetAdapter(mAdapter);

			var textViewName = rootView.FindViewById<TextView>(Resource.Id.user_profile_name);
			var textViewLastActive = rootView.FindViewById<TextView>(Resource.Id.user_profile_last_active);

			textViewName.Text = user.Username;
			textViewLastActive.Text = user.LastLogin.Value.ToString();

			return rootView;
		}
	}
}
