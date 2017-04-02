using System;
using System.Collections.Generic;
using System.Net;
using Android.App;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace connectivesport
{
	public class FriendAdapter: RecyclerView.Adapter
	{
		List<User> _friendList;
		Activity _activity;
		public FriendAdapter(Activity activity, List<User> friendList)
		{
			_friendList = friendList;
			_activity = activity;

		}

		public override int ItemCount
		{
			get
			{
				return _friendList.Count;
			}
		}




		public Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient())
			{
				var imageBytes = webClient.DownloadData(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var vh = holder as FriendViewHolder;
			vh.Text.Text = _friendList[position].Username;
			if (_friendList[position].AvatarURL != null)
			{
				
				var drawableImage = _activity.Resources.GetIdentifier(_friendList[position].AvatarURL, "drawable", _activity.PackageName);
				vh.Avatar.SetImageResource(drawableImage);
				//var imageBitmap = GetImageBitmapFromUrl(_friendList[position].AvatarURL);
				//vh.Avatar.SetImageBitmap(imageBitmap);
			}
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_friend, parent, false);
			var vh = new FriendViewHolder(itemView);
			return vh;
		}
	}


	class FriendViewHolder:RecyclerView.ViewHolder
	{
		public TextView Text { get; private set; }

		public ImageView Avatar { get; private set; }


		public FriendViewHolder(View itemView) : base(itemView)
		{
			Text = itemView.FindViewById<TextView>(Resource.Id.textViewName);
			Avatar = itemView.FindViewById<ImageView>(Resource.Id.imageViewAvatar);
			// itemView.Click += (sender, e) => listener(base.Position);
		}
	}
}
