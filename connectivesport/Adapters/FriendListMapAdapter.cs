using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace connectivesport
{
	class FriendListMapAdapter : BaseAdapter
	{
		Context context;
		System.Collections.Generic.List<User> friends;

		public FriendListMapAdapter(Context context, System.Collections.Generic.List<User> friends)
		{
			this.context = context;
			this.friends = friends;
		}

		public override int Count
		{
			get
			{
				return friends.Count;
			}
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}
		public User GetUser(int position)
		{
			return friends[position];
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			if (convertView == null)
			{

				convertView = LayoutInflater.From(context).Inflate(Resource.Layout.ListItem_FriendListMap, null);

			}
			TextView name = (TextView)convertView.FindViewById(Resource.Id.flm_name);
			TextView status = (TextView)convertView.FindViewById(Resource.Id.flm_status);

			name.Text = friends[position].Username;
			status.Text = "Working now";
			return convertView;
		}
	}
}