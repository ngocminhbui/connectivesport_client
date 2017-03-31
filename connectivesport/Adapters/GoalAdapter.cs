using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace connectivesport
{
	class GoalAdapter : RecyclerView.Adapter
	{


		public GoalList mGoalList;
		public GoalAdapter(GoalList goalList)
			{
				mGoalList = goalList;
			}
		public override int ItemCount
		{
			get
			{
				return mGoalList.length;
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var vh = holder as GoalViewHolder;
			vh.Text.Text = mGoalList[position].CustomMessage;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_goal, parent, false);

			var vh = new GoalViewHolder(itemView);
			return vh;
		}
	}

	class GoalViewHolder:RecyclerView.ViewHolder
	{
		public TextView Text { get; private set; }

		public GoalViewHolder(View itemView) : base(itemView)
		{
			Text = itemView.FindViewById<TextView>(Resource.Id.textView);
			//itemView.Click += (sender, e) => listener(base.Position);
		}
	}
}