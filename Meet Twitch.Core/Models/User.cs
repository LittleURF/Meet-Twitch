using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meet_Twitch.Core.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("broadcaster_type")]
        public string BroadcasterType { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("profile_image_url")]
        public string profileImageUrl { get; set; }

        [JsonProperty("offline_image_url")]
        public string OfflineImageUrl { get; set; }

        [JsonProperty("View_count")]
        public int ViewCount { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
