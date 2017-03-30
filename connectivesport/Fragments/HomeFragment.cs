﻿
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
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
namespace connectivesport
{
	public class HomeFragment : Fragment
	{
		RecyclerView mRecyclerView;
		RecyclerView.LayoutManager mLayoutManager;
		GoalAdapter mAdapter;
		GoalList mGoalList;
		FloatingActionButton mbutton;

		public static HomeFragment NewInstance()
		{
			HomeFragment fragment = new HomeFragment();
			return fragment;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
			mGoalList = new GoalList();

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment

			var rootView = inflater.Inflate(Resource.Layout.fragment_home, container, false);
			mRecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerView);

			mLayoutManager = new LinearLayoutManager(this.Activity);
			mRecyclerView.SetLayoutManager(mLayoutManager);

			// Plug in my adapter:
			mAdapter = new GoalAdapter(mGoalList);
			mRecyclerView.SetAdapter(mAdapter);

			mbutton = rootView.FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton);
			mbutton.Click += AddNewGoal;
				
			return rootView;
		}

		void AddNewGoal(object sender, EventArgs e)
		{
			Context context = this.Context;
			Intent intent = new Intent(context, typeof(AddGoalActivity));
			context.StartActivity(intent);
		}
}
}