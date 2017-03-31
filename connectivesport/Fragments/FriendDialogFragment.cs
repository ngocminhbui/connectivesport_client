
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
using connectivesport.Helpers;
namespace connectivesport
{
	public class FriendDialogFragment : DialogFragment
	{

		public static FriendDialogFragment NewInstance()
		{
			FriendDialogFragment fragment = new FriendDialogFragment();
			return fragment;
		}
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		RecyclerView mRecyclerView;

		LinearLayoutManager mLayoutManager;

		FriendAdapter mAdapter;

		List<User> mFriendList;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			var goalID = Arguments.GetString("goalID");

			var rootView = inflater.Inflate(Resource.Layout.dialog_fragment_friend_list, container, false);
			mRecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerView);

			mLayoutManager = new LinearLayoutManager(this.Activity);
			mRecyclerView.SetLayoutManager(mLayoutManager);

			//Task.Run(async () =>
			//{
			//	mFriendList = await ListFriendData();
			//	mAdapter = new FriendAdapter(mFriendList);
			//	mRecyclerView.SetAdapter(mAdapter);
			//});

			mFriendList = ListFriendDemo();
			mAdapter = new FriendAdapter(mFriendList);
			mRecyclerView.SetAdapter(mAdapter);

			mRecyclerView.SetItemClickListener((rv, position, view) =>
			{
				//An item has been clicked
				// Context context = view.Context;
				// Intent intent = new Intent(context, typeof(GoalDetailsActivity));
				//intent.PutExtra(GoalDetailsActivity.EXTRA_NAME, values[position]);

				Dismiss();
			});
			return rootView;

		}

		List<User> ListFriendDemo()
		{
			List<User> lu = new List<User>();

			lu.Add(new User { Username = "Luong Quoc An" });
			lu.Add(new User { Username = "Luong Quoc An" });
			lu.Add(new User { Username = "Luong Quoc An" });
			lu.Add(new User { Username = "Luong Quoc An" });
			lu.Add(new User { Username = "Luong Quoc An" });
			lu.Add(new User { Username = "Luong Quoc An" });
			lu.Add(new User { Username = "Luong Quoc An" });

			return lu;
		}
	}
}
