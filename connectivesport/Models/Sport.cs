using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace connectivesport
{
    public class Sport:EntityData
    {
        public Sport()
        {
            Memberships = new List<SportMembership>();
            Challenges = new List<Challenge>();
        }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("RulesUrl")]
        public string RulesUrl { get; set; }

        [JsonProperty("Season")]
        public int Season { get; set; }

        [JsonProperty("MaxChallengeRange")]
        public int MaxChallengeRange { get; set; }

        [JsonProperty("MinHoursBetweenChallenge")]
        public int MinHoursBetweenChallenge { get; set; }

        [JsonProperty("MatchGameCount")]
        public int MatchGameCount { get; set; }

        [JsonProperty("IsAcceptingMembers")]
        public bool IsAcceptingMembers { get; set; }

        [JsonProperty("IsEnabled")]
        public bool IsEnabled { get; set; }

        [JsonProperty("StartDate")]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("EndDate")]
        public DateTimeOffset? EndDate { get; set; }

        [JsonProperty("HasStarted")]
        public bool HasStarted { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("CreatedByAthleteId")]
        public string CreatedByAthleteId { get; set; }


        //**
        [JsonProperty("SportTypeId")]
        public string SportTypeId { get; set; }
        public virtual SportType SportType { get; set; }

        public ICollection<SportMembership> Memberships { get; set; }
        public ICollection<Challenge> Challenges { get; set; }

    }
}