using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace connectivesport
{
    public class UserHealthDetail : EntityData
    {
        [JsonProperty(PropertyName = "userhealth-weight")]
        public double? Weight { get; set; } = (double?)null;

        [JsonProperty(PropertyName = "userhealth-height")]
        public double? Height { get; set; } = (double?)null;

        [JsonProperty(PropertyName = "userhealth-date")]
        public DateTime? UpdateDate { get; set; } = (DateTime?)null;

        [JsonProperty(PropertyName = "userhealth-valid")]
        public bool? IsValid { get; set; } = true;

        [JsonProperty(PropertyName = "userhealth-description")]
        public string Description { get; set; }

        public virtual User User { get; set; }
    }
}
