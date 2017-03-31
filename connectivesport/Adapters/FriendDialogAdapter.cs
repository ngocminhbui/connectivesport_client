using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace connectivesport
{
	public class FriendDialogAdapter : RecyclerView.Adapter
	{
		List<User> _friendList;
		public FriendDialogAdapter(List<User> friendList)
		{
			_friendList = friendList;
		}

		public override int ItemCount
		{
			get
			{
				return _friendList.Count;
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var vh = holder as FriendDialogViewHolder;
			vh.Text.Text = _friendList[position].Username;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_friend_dialog, parent, false);
			var vh = new FriendDialogViewHolder(itemView);
			return vh;
		}
	}

	class FriendDialogViewHolder : RecyclerView.ViewHolder
	{
		public TextView Text { get; private set; }

		public FriendDialogViewHolder(View itemView) : base(itemView)
		{
			Text = itemView.FindViewById<TextView>(Resource.Id.textViewName);
			// itemView.Click += (sender, e) => listener(base.Position);
		}
	}
}
