using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace connectivesport
{
	public class FriendGoalAdapter:RecyclerView.Adapter
	{
		List<User> _friendChallengeList;

		public FriendGoalAdapter() : base()
		{
		}

		public FriendGoalAdapter(List<User> friendChallengeList)
		{
			_friendChallengeList = friendChallengeList;
		}

		public override int ItemCount
		{
			get
			{
				return _friendChallengeList.Count;
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var vh = holder as FriendChallengeViewHolder;
			vh.Text.Text = _friendChallengeList[position].Username;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_friend_challenge, parent, false);
			var vh = new FriendChallengeViewHolder(itemView);
			return vh;
		}

		class FriendChallengeViewHolder:RecyclerView.ViewHolder
		{

			public TextView Text { get; private set; }

			public FriendChallengeViewHolder(View itemView):base(itemView)
			{
				
				Text = itemView.FindViewById<TextView>(Resource.Id.textViewName);
			}
		}
	}
}
