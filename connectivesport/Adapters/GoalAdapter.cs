using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace connectivesport
{
	class GoalAdapter : RecyclerView.Adapter
	{


		public List<Goal> mGoalList;
		Activity _activity;
		public GoalAdapter(Activity activity, List<Goal> goalList)
		{
				_activity = activity;
				mGoalList = goalList;
		}
		public override int ItemCount
		{
			get
			{
				return mGoalList.Count;
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var vh = holder as GoalViewHolder;
			vh.Text.Text = mGoalList[position].CustomMessage;
			Sport sportGoal = LocalDataManager.instance.getSportById(mGoalList[position].SportId);
			if (sportGoal != null)
			{
				var drawableImage = _activity.Resources.GetIdentifier(sportGoal.ImageUrl, "drawable", _activity.PackageName);
				vh.Image.SetImageResource(drawableImage);
			}

			vh.progrssBarObj.Max = mGoalList[position].Count.Value;
			vh.progrssBarObj.Progress = mGoalList[position].Current_Count.Value;
			vh.CurrentProgress.Text = mGoalList[position].Current_Count.Value.ToString();
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

		public ImageView Image { get; private set; }

		public ProgressBar progrssBarObj { get; private set; }

		public TextView CurrentProgress { get; private set; }




		public GoalViewHolder(View itemView) : base(itemView)
		{
			Text = itemView.FindViewById<TextView>(Resource.Id.textView);
			Image = itemView.FindViewById<ImageView>(Resource.Id.imageViewSport);
			progrssBarObj = itemView.FindViewById<ProgressBar>(Resource.Id.progressBarSync);
			CurrentProgress = itemView.FindViewById<TextView>(Resource.Id.txtProgress);                        
			//itemView.Click += (sender, e) => listener(base.Position);
		}
	}
}