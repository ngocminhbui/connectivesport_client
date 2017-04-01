﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace connectivesport
{
	public class MedalAdapter:RecyclerView.Adapter
	{
		List<Medal> _medalList;
		Activity _activity;
		public MedalAdapter(Activity activity, List<Medal> medalList)
		{
			_activity = activity;
			_medalList = medalList;
		}

		public override int ItemCount
		{
			get
			{
				return _medalList.Count;
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var vh = holder as MedalViewHolder;
			vh.Text.Text = _medalList[position].Name;
			var drawableImage = _activity.Resources.GetIdentifier(_medalList[position].ImageURL, "drawable", _activity.PackageName);
			vh.image.SetImageResource(drawableImage);
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_medal, parent, false);
			var vh = new MedalViewHolder(itemView);
			return vh;
		}
	}
	class MedalViewHolder : RecyclerView.ViewHolder
	{
		public TextView Text { get; private set; }

		public ImageView image { get; private set; }



		public MedalViewHolder(View itemView) : base(itemView)
		{
			Text = itemView.FindViewById<TextView>(Resource.Id.textViewName);
			image = itemView.FindViewById<ImageView>(Resource.Id.imageViewMedal);
			// itemView.Click += (sender, e) => listener(base.Position);
		}
	}
}
