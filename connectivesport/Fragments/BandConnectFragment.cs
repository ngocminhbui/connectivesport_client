using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using connectivesport.Helpers;
using Microsoft.Band;

namespace connectivesport
{
	public class BandConnectFragment : DialogFragment
	{

		public static BandConnectFragment NewInstance()
		{
			BandConnectFragment fragment = new BandConnectFragment();
			return fragment;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		RecyclerView mRecyclerView;

		LinearLayoutManager mLayoutManager;

		BandAdapter mAdapter;

		IBandInfo myBand;
		IBandInfo[] lsBand;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			var rootView = inflater.Inflate(Resource.Layout.dialog_fragment_band_list, container, false);
			mRecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerView);

			mLayoutManager = new LinearLayoutManager(this.Activity);
			mRecyclerView.SetLayoutManager(mLayoutManager);



			lsBand = BandConnector.instance.FindBand();
			if (lsBand.Length != 0)
			{
				mAdapter = new BandAdapter(this.Activity, lsBand);
				mRecyclerView.SetAdapter(mAdapter);

			}
			else
			{
				Toast.MakeText(this.Activity, "No Band found", ToastLength.Long);
				Dismiss();
			}

			mRecyclerView.SetItemClickListener((rv, position, view) => 
			{
				//An item has been clicked
				// Context context = view.Context;
				// Intent intent = new Intent(context, typeof(GoalDetailsActivity));
				//intent.PutExtra(GoalDetailsActivity.EXTRA_NAME, values[position]);
				Task.Run(async () =>
				{
					bool result = await BandConnector.instance.BandConnect(lsBand[position]);
					if (result)
					{
						
						Toast.MakeText(this.Activity, "Connect successful", ToastLength.Short).Show();
					}
					else
					{
						Toast.MakeText(this.Activity, "Fail to connect", ToastLength.Short).Show();
					}
					Dismiss();
				});

			});
			return rootView;

		}

	}
}
