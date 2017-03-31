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
        public static List<Friend> friend_list;

        public async Task getData(MobileServiceClient client)
        {
            medal_list = await client.GetTable<Medal>().ToListAsync();
            sport_list = await client.GetTable<Sport>().ToListAsync();
            friend_list = await client.GetTable<Friend>().ToListAsync();

            return;
        }

        public void getFriendsList(string token)
        {
            FacebookClient fb = new FacebookClient(token);
            JToken friends = JToken.Parse(fb.Get("me/friends").ToString());
            for(int i = 0; i < friends["data"].Count() ; i++)
            {
                string id = friends["data"][i]["id"].ToString();
                JToken friend = JToken.Parse(fb.Get(id + "/?fields=picture,name").ToString());
                string name = friend["name"].ToString();
                string url = friend["picture"]["data"]["url"].ToString();
                //friend_list.Add(new FriendInfo(name, url));
            }
        }
    }
}