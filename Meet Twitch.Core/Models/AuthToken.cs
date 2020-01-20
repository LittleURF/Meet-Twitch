using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meet_Twitch.Core.Models
{
    public class AuthToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}
