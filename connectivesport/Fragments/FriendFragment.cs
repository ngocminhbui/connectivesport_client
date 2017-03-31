
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
namespace connectivesport
{
	public class FriendFragment : Fragment
	{
		public static FriendFragment NewInstance()
		{
			FriendFragment fragment = new FriendFragment();
			return fragment;
		}
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
			mFriendList = LocalDataManager.instance.lsUser;
		}

		RecyclerView mRecyclerView;

		LinearLayoutManager mLayoutManager;

		FriendAdapter mAdapter;

		List<User> mFriendList;




		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			var rootView = inflater.Inflate(Resource.Layout.fragment_friend, container, false);
			mRecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerView);

			mLayoutManager = new LinearLayoutManager(this.Activity);
			mRecyclerView.SetLayoutManager(mLayoutManager);

			//Task.Run(async () =>
			//{
			//	mFriendList = await ListFriendData();
			//	mAdapter = new FriendAdapter(mFriendList);
			//	mRecyclerView.SetAdapter(mAdapter);
			//});

			// mFriendList = ListFriendDemo();
				mAdapter = new FriendAdapter(this.Activity, mFriendList);
				mRecyclerView.SetAdapter(mAdapter);


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
		async Task<List<User>> ListFriendData()
		{
			return await UserManager.DefaultManager.CurrentClient.GetTable<User>().ToListAsync();
		}
	}
}
