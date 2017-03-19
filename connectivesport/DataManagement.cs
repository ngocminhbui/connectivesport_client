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

namespace connectivesport
{
    class DataManagement
    {
        public static List<Medal> medal_list;
        public static List<Sport> sport_list;
        public static List<Friend> friend_list;

        public async Task getData(MobileServiceClient client)
        {
            var MedalTable = client.GetTable<Medal>();
            var SportTable = client.GetTable<Sport>();
            var FriendTable = client.GetTable<Friend>();

            medal_list = await MedalTable.ToListAsync();
            sport_list = await SportTable.ToListAsync();
            friend_list = await FriendTable.ToListAsync();

            return;
        }
    }
}