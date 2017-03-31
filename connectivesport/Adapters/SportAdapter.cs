using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace connectivesport
{
	public class SportAdapter: BaseAdapter
	{
		List<Sport> _sportList;
		Activity _activity;
		public SportAdapter(Activity activity, List<Sport> sportList)
		{
			_activity = activity;
			_sportList = sportList;
		}

		public override int Count
		{
			get
			{
				return _sportList.Count;
			}
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _activity.LayoutInflater.Inflate(
				Resource.Layout.row_spinner_sport, parent, false);
			var sportName = view.FindViewById<TextView>(Resource.Id.SportName);
			var sportImage = view.FindViewById<ImageView>(Resource.Id.SportImage);

			sportName.Text = _sportList[position].Name;
			var drawableImage = _activity.Resources.GetIdentifier(_sportList[position].ImageUrl, "drawable", _activity.PackageName);
			sportImage.SetImageResource(drawableImage);
			return view;
		}
	}
}
