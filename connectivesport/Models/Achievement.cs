using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace connectivesport
{
    public class Achievement : EntityData
    {
        public Achievement()
        {

        }

        [JsonProperty("AchieveDate")]
        public DateTime? AchieveDate { get; set; } = (DateTime?)null;


        [JsonProperty("AchieveUserId")]
        public string AchieveUserId { get; set; }

        [JsonProperty("MedalId")]
        public string MedalId { get; set; }

        public virtual User AchieveUser { get; set; }


        public virtual Medal Medal {get;set;}
    }
}