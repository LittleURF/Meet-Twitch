using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Meet_Twitch.Core.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Linq;
using RestSharp;
using Meet_Twitch.Core.Utilities;
using Meet_Twitch.Core.ApiClients;
using Meet_Twitch.Core.Enums;
using Microsoft.Extensions.Configuration;

namespace Meet_Twitch.Core.Data
{
    public class TwitchRepository : ITwitchRepository
    {
        private readonly ITwitchApi _twitchApi;
        private readonly IConfiguration _config;

        public TwitchRepository(ITwitchApi twitchApi, IConfiguration config)
        {
            _twitchApi = twitchApi;
            _config = config;
        }

        public async Task<Video> GetVideoAsync(string id)
        {
            Video video = null;

            try
            {
                if (id == null)
                    return video;

                var request = new RestRequest("videos", DataFormat.Json);
                request.AddQueryParameter("game_id", id);
                var httpResponse = _twitchApi.RestClient.Get(request);
                if (httpResponse.IsSuccessful)
                {
                    var httpResponseData = httpResponse.Content;
                    video = Serializer.SerializeTwitchJsonSingle<Video>(httpResponseData);
                }
            }
            catch (Exception e)
            {
                // log
                throw;
            }

            return video;
        }

        public async Task<List<Video>> GetVideosAsync(List<string> ids)
        {
            List<Video> videos = new List<Video>();

            try
            {
                if (ids == null || ids.Count == 0)
                    return videos;

                string videosIds = String.Join(",", ids);

                var request = new RestRequest("videos", DataFormat.Json);
                request.AddQueryParameter("game_id", videosIds);

                var httpResponse = _twitchApi.RestClient.Get(request);
                if (httpResponse.IsSuccessful)
                {
                    var httpResponseData = httpResponse.Content;
                    videos = Serializer.SerializeTwitchJsonList<Video>(httpResponseData);
                }
            }
            catch (Exception e)
            {
                // log
                throw;
            }

            return videos;
        }

        public async Task<List<Video>> GetVideosAsync(string gameId, TwitchLanguages? language = null, TwitchPeriods? period = null, TwitchSorts? sort = null)
        {
            List<Video> videos = new List<Video>();

            try
            {
                // split into separate method? PrepareRequest
                var request = new RestRequest("videos", DataFormat.Json);
                request.AddQueryParameter("game_id", gameId);
                request.AddQueryParameter("language", language.ToString());
                if (period != null)
                    request.AddQueryParameter("period", period.ToString().ToLower());
                if (sort != null)
                    request.AddQueryParameter("sort", sort.ToString().ToLower());


                var httpResponse = _twitchApi.RestClient.Get(request);
                if (httpResponse.IsSuccessful)
                {
                    var httpResponseData = httpResponse.Content;
                    videos = Serializer.SerializeTwitchJsonList<Video>(httpResponseData);
                }

            }
            catch (Exception e)
            {
                // log
                throw;
            }

            return videos;
        }

        public async Task<Game> GetGameAsync(string id)
        {
            Game game = new Game();

            try
            {
                var request = new RestRequest("games", DataFormat.Json);
                request.AddQueryParameter("id", id);

                var httpResponse = _twitchApi.RestClient.Get(request);
                if (httpResponse.IsSuccessful)
                {
                    var httpResponseData = httpResponse.Content;
                    game = Serializer.SerializeTwitchJsonSingle<Game>(httpResponseData);
                }
            }
            catch (Exception e)
            {
                // log
                throw;
            }

            return game;
        }

        public async Task<List<Game>> GetTopGamesAsync()
        {
            List<Game> games = new List<Game>();

            try
            {
                var request = new RestRequest("games/top?first=100", DataFormat.Json);
                var httpResponse = _twitchApi.RestClient.Get(request);
                if (httpResponse.IsSuccessful)
                {
                    var httpResponseData = httpResponse.Content;
                    games = Serializer.SerializeTwitchJsonList<Game>(httpResponseData);
                }
            }
            catch (Exception e)
            {
                // log
                throw;
            }

            return games;
        }

        public async Task<List<User>> GetUsers(List<string> ids)
        {
            var users = new List<User>();

            try
            {
                if (ids == null || ids.Count == 0)
                    return users;

                var request = new RestRequest("users", DataFormat.Json);
                foreach (var id in ids)
                {
                    request.AddQueryParameter("id", id);
                }

                var httpResponse = _twitchApi.RestClient.Get(request);
                if (httpResponse.IsSuccessful)
                {
                    var httpResponseData = httpResponse.Content;
                    users = Serializer.SerializeTwitchJsonList<User>(httpResponseData);
                }
            }
            catch (Exception)
            {

                throw;
            }


            return users;
        }

    }
}
