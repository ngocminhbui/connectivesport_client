using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace connectivesport
{
    public class Medal:EntityData
    {
        public Medal()
        {

        }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ImageURL")]
        public string ImageURL { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }
    }
}