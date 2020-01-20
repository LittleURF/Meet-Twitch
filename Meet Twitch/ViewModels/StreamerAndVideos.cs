using Meet_Twitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meet_Twitch.ViewModels
{
    public class StreamerAndVideos
    {
        public User Streamer { get; set; }
        public List<Video> Videos { get; set; }
    }
}
