using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace connectivesport
{
    public class Friend : EntityData
    {

        [JsonProperty("friend-valid")]
        public bool? valid { get; set; } = true;

        [JsonProperty("friend-acceptdate")]
        public DateTime? AcceptDate { get; set; } = (DateTime?)null;

        [JsonProperty("friend-requestuserid")]
        public string RequestUserId { get; set; }

        [JsonProperty("friend-acceptuserid")]
        public string AcceptUserId { get; set; }

        public virtual User RequestUser { get; set; }

        public virtual User AcceptUser { get; set; }
    }
}
