using Meet_Twitch.Core;
using Meet_Twitch.Core.Enums;
using Meet_Twitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meet_Twitch.ViewModels
{
    public class GamesAndLanguagesViewModel
    {
        public List<Game> Games { get; set; }
        public TwitchLanguages TwitchLanguages { get; set; }
    }
}
