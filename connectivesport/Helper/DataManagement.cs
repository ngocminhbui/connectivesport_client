using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Facebook;
using Newtonsoft.Json.Linq;

namespace connectivesport
{
    class DataManagement
    {
        public static DataManagement instance = new DataManagement();
        public static List<Medal> medal_list;
        public static List<Sport> sport_list;
        public static List<User> friend_list;
        public string Userid;
        public MobileServiceClient client = new MobileServiceClient(Constants.ApplicationURL);

        public async Task getData()
        {
            medal_list = await this.client.GetTable<Medal>().ToListAsync();
            sport_list = await this.client.GetTable<Sport>().ToListAsync();

            return;
        }

        public async Task<List<Medal>> GetUserAchievement()
        {
            List<Medal> result = new List<Medal>();
            var AchievementTable = client.GetTable<Achievement>();

            return result;
        }
    }
}