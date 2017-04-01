using System;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Microsoft.Band;

namespace connectivesport
{
	public class BandAdapter : RecyclerView.Adapter
	{
		Activity activity;
		IBandInfo[] lsBand;

		public BandAdapter(Activity activity, IBandInfo[] lsBand)
		{
			this.activity = activity;
			this.lsBand = lsBand;
		}

		public override int ItemCount
		{
			get
			{
				return lsBand.Length;
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			BandViewHolder vh = holder as BandViewHolder;
			vh.Text.Text = lsBand[position].Name;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_band, parent, false);
			var vh = new BandViewHolder(itemView);
			return vh;
		}

		class BandViewHolder : RecyclerView.ViewHolder
		{
			public TextView Text { get; private set; }

			public BandViewHolder(View itemView) : base(itemView)
			{
				Text = itemView.FindViewById<TextView>(Resource.Id.textViewName);
			}
		}
	}

}