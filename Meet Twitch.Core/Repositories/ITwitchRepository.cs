using Meet_Twitch.Core.Enums;
using Meet_Twitch.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meet_Twitch.Core.Data
{
    public interface ITwitchRepository
    {
        Task<Video> GetVideoAsync(string url);
        Task<List<User>> GetUsers(List<string> ids);
        Task<List<Video>> GetVideosAsync(List<string> ids);
        Task<List<Video>> GetVideosAsync(string gameId, TwitchLanguages? language = null, TwitchPeriods? period = null, TwitchSorts? sort = null);
        Task<Game> GetGameAsync(string id);
        Task<List<Game>> GetTopGamesAsync();
    }
}