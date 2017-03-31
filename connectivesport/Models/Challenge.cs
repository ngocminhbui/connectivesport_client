using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace connectivesport
{
    public class Challenge : EntityData

    {
        public Challenge()
        {

        }

        [JsonProperty("CustomMessage")]
        public string CustomMessage { get; set; }

        [JsonProperty("BattleForRank")]
        public int? BattleForRank { get; set; }

        [JsonProperty("Count")]
        public int? Count { get; set; }

        [JsonProperty("CurrentCount")]
        public int? Current_Count { get; set; }


        [JsonProperty("Length")]
        public double? Length { get; set; }

        [JsonProperty("CurrentLength")]
        public int? Current_Length { get; set; }


        [JsonProperty("Frequency")]
        public int? Frequency { get; set; }

        [JsonProperty("CurrentFrequency")]
        public int? Current_Frequency { get; set; }

        [JsonProperty("ProposedTime")]
        public DateTime? ProposedTime { get; set; }

        [JsonProperty("CurrentProposedTime")]
        public int? Current_ProposedTime { get; set; }




        [JsonProperty("DateAccepted")]
        public DateTime? DateAccepted { get; set; }

        [JsonProperty("DateCompleted")]
        public DateTime? DateCompleted { get; set; }


        public virtual Sport Sport {get;set;}

        [JsonProperty("SportId")]
        public string SportId { get; set; }


        public User ChallengerUser { get; set; }

        [JsonProperty("ChallengerUserId")]
        public string ChallengerUserId { get; set; }

        public User ChallengeeUser { get; set; }

        [JsonProperty("ChallengeeUserId")]
        public string ChallengeeUserId { get; set; }

    }
}