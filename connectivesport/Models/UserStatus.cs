using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace connectivesport
{
    public class UserStatus:EntityData
    {
        public UserStatus()
        {

        }

        [JsonProperty("isAvailable")]
        public bool? isAvailable { get; set; } = (bool?)null;

        [JsonProperty("status")]
        public string status { get; set; }


        [JsonProperty("UserId")]
        public string UserId { get; set; }

        public virtual User User { get; set; }
        
    }
}