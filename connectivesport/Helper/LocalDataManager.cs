using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace connectivesport
{
    class LocalDataManager
    {
        public static LocalDataManager instance = new LocalDataManager();
        public List<Goal> lsGoal;
        public List<User> lsUser;
        public List<Medal> lsMedal;
        public List<Sport> lsSport;

        public LocalDataManager()
        {
            lsGoal = loadGoal();
            lsSport = loadSport();
            lsMedal = loadMedal();
            lsUser = loadUser();
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
		private List<Medal> loadMedal()
        {
            List<Medal> result = new List<Medal>();
            Medal m1 = new Medal { Name = "First Achievement", Id = "md1", ImageURL = "", Description = "first medal" };
            result.Add(m1);

            return result;
        }

        private List<User> loadUser()
        {
            List<User> result = new List<User>();
			//User user1 = new User { UserID = "user1", Id = "user1", Username = "Luong Quoc An", Email = "lqa@gmail.com", LastLocationX = 10.7549819, LastLocationY = 106.6614459, LastLogin = DateTime.Now, AvatarURL = "https://scontent.xx.fbcdn.net/v/t1.0-1/p50x50/11207295_792318130877638_232756703618362371_n.jpg?oh=d3e1f303d32252ae230a592132683b24&oe=595C1C5B" };
			//User user2 = new User { UserID = "user2", Id = "user2", Username = "Bui Ngoc Minh", Email = "nminhbui@gmail.com", LastLocationX = 10.7533198, LastLocationY = 106.6631528, LastLogin = DateTime.Now, AvatarURL = "https://scontent.xx.fbcdn.net/v/t1.0-1/p50x50/14202620_600734660087573_1014109962882287185_n.jpg?oh=7010e437355909cdf4e6d2c0a506c8b6&oe=5953FD4E" };
			//User user3 = new User { UserID = "user3", Id = "user3", Username = "Do Trong Le", Email = "dtle@gmail.com", LastLocationX = 10.7531633, LastLocationY = 106.6616659, LastLogin = DateTime.Now, AvatarURL = "https://scontent.fsgn5-1.fna.fbcdn.net/v/t1.0-9/299134_435176599907982_1640959701_n.jpg?oh=414d46c47281a9d63afc9d4cf9f2d802&oe=59973A77" };

			User user1 = new User { UserID = "user1", Id = "user1", Username = "Luong Quoc An", Email = "lqa@gmail.com", LastLocationX = 10.7549819, LastLocationY = 106.6614459, LastLogin = DateTime.Now, AvatarURL = "avatar_an"};
            User user2 = new User { UserID = "user2", Id = "user2", Username = "Bui Ngoc Minh", Email = "nminhbui@gmail.com", LastLocationX = 10.7533198, LastLocationY = 106.6631528, LastLogin = DateTime.Now, AvatarURL = "avatar_minh" };
            User user3 = new User { UserID = "user3", Id = "user3", Username = "Do Trong Le", Email = "dtle@gmail.com", LastLocationX = 10.7531633, LastLocationY = 106.6616659, LastLogin = DateTime.Now, AvatarURL = "avatar_le" };
            result.Add(user1);
            result.Add(user2);
            result.Add(user3);

            return result;
        }

		public Goal getGoalById(string goalID)
		{
			return lsGoal.Find(x => x.Id == goalID);
		}

		private List<Goal> loadGoal()
        {
            List<Goal> Result = new List<Goal>();
			Goal g1 = new Goal { CustomMessage = "my first goal", BattleForRank = 5, Count = 1000, Frequency = 0, Id = "g1", SportId = "sprt1", UserId = "user1", Length = 0.0, DateAccepted = DateTime.Now, DateCompleted = null, Current_Count = 376, Current_Frequency = 0, Current_Length = 0, Current_ProposedTime = 0};
            Result.Add(g1);

            return Result;
        }

		public Sport getSportById(string sportId)
		{
			return lsSport.Find(x => x.Id == sportId);
		}

        private List<Sport> loadSport()
        {
            List<Sport> result = new List<Sport>();
            Sport sp1 = new Sport { Description = "Running", Name = "Running", SportTypeId = "outdoor", ImageUrl = "sport4", Id = "sprt1", CreatedByAthleteId = "user1", MinHoursBetweenChallenge = 5, IsEnabled = true, MatchGameCount = 10, MaxChallengeRange = 10, HasStarted = true};
            result.Add(sp1);

            return result;
        }
    }
}