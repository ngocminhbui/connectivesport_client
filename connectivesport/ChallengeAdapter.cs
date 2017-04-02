using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace connectivesport
{
	public class ChallengeAdapter:RecyclerView.Adapter
	{
		public List<Challenge> mChallengeList;
		Activity _activity;
		public ChallengeAdapter(Activity activity, List<Challenge> challengeList)
		{
			_activity = activity;
			mChallengeList = challengeList;
		}

		public override int ItemCount
		{
			get
			{
				return mChallengeList.Count;
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var vh = holder as ChallengeViewHolder;
			vh.Text.Text = mChallengeList[position].CustomMessage;
			Sport sportGoal = LocalDataManager.instance.getSportById(mChallengeList[position].SportId);
			if (sportGoal != null)
			{
				var drawableImage = _activity.Resources.GetIdentifier(sportGoal.ImageUrl, "drawable", _activity.PackageName);
				vh.ImageSport.SetImageResource(drawableImage);
			}

		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_challenge, parent, false);

			var vh = new ChallengeViewHolder(itemView);
			return vh;
		}
	}

	class ChallengeViewHolder:RecyclerView.ViewHolder
	{
		public TextView Text { get; private set; }

		public ImageView ImageSport { get; private set; }

		public ImageView ImageAvatar { get; private set; }



		public ChallengeViewHolder(View itemView) : base(itemView)
		{
			Text = itemView.FindViewById<TextView>(Resource.Id.textViewChallenge);
			ImageSport = itemView.FindViewById<ImageView>(Resource.Id.imageViewSport);
			ImageAvatar = itemView.FindViewById<ImageView>(Resource.Id.imageViewAvatar);

			//itemView.Click += (sender, e) => listener(base.Position);
		}
	}
}
