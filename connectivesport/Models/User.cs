using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace connectivesport
{
    public class User: EntityData
    {
        public User()
        {

            HealthDetails = new List<UserHealthDetail>();
            RequestFriends = new List<Friend>();
            AcceptFriends = new List<Friend>();
            Achievements = new List<Achievement>();
            Memberships = new List<SportMembership>();
            Challengees = new List<Challenge>();
            Challengers = new List<Challenge>();
            Goals = new List<Goal>();
        }


        [JsonProperty(PropertyName = "user-id")]
        public string UserID { get; set; }


        [JsonProperty(PropertyName = "user-name")]
        public string Username { get; set; } 


        [JsonProperty(PropertyName = "user-email")]
        public string Email { get; set; } 


        [JsonProperty(PropertyName = "user-avatar-url")]
        public string AvatarURL { get; set; } 
    

        [JsonProperty(PropertyName = "user-registration-date")]
        public DateTime? RegistrationDate { get; set; } = (DateTime?)null;


        [JsonProperty(PropertyName = "user-last-login")]
        public DateTime? LastLogin { get; set; } = (DateTime?)null;


        [JsonProperty(PropertyName = "user-last-locationX")]
        public double? LastLocationX { get; set; } =(double?) null;


        [JsonProperty(PropertyName = "user-last-locationY")]
        public double? LastLocationY { get; set; } = (double?)null;


        public virtual  ICollection<UserHealthDetail> HealthDetails { get; set; }

        public virtual ICollection<Friend> RequestFriends { get; set; }
        public virtual ICollection<Friend> AcceptFriends { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }

        public virtual ICollection<SportMembership> Memberships { get; set; }

        public virtual ICollection<Challenge> Challengers { get; set; }
        public virtual ICollection<Challenge> Challengees { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }


        public string StatusId;
        public virtual UserStatus Status { get; set; }
    }
}