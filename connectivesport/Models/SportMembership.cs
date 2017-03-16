using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace connectivesport
{
    public class SportMembership:EntityData
    {
        public SportMembership()
        {

        }


        [JsonProperty("isValid")]
        public bool? isValid { get; set; } = (bool?)true;

        [JsonProperty("joinDate")]
        public DateTime? joinDate { get; set; } = (DateTime?)null;


        [JsonProperty("lastActivity")]
        public DateTime? lastActivity { get; set; } = (DateTime?)null;

        [JsonProperty("SportId")]
        public string SportId { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }


        public virtual Sport Sport { get; set; }
        public virtual User User { get; set; }
    }
}