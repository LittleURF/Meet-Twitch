using Meet_Twitch.Core.Enums;
using Meet_Twitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meet_Twitch.ViewModels
{
    public class StreamersAndVideos
    {
        public List<User> Streamers { get; set; }
        public List<Video> Videos { get; set; }
        public Game Game { get; set; }
        public TwitchLanguages Language { get; set; }
    }
}
