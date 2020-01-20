using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meet_Twitch.Core.Models
{
    public class Game
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("box_art_url")]
        public string BoxArtUrl { get; set; }
    }
}
