using Meet_Twitch.Core.Data;
using Meet_Twitch.Core.Exceptions;
using Meet_Twitch.Core.Models;
using Meet_Twitch.Core.Utilities;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meet_Twitch.Core.ApiClients
{
    public class TwitchApi : ITwitchApi
    {
        public RestClient RestClient { get; private set; }
        public static AuthToken Token { get; private set; }
        private readonly IConfiguration _config;

        public TwitchApi(IConfiguration configuration)
        {
            _config = configuration;
            Initialize();
        }

        private void Initialize()
        {
            RestClient = new RestClient("https://api.twitch.tv/helix");
            RestClient.AddDefaultHeader("Client-ID", "lp978j4tcrtwpkgm11vgk4gic5nox6");
            RestClient.AddDefaultHeader("Client-Secret", "wwn9vhr9liutnvmoy7y00dokhanvxs");

            if (IsRefreshRequired())
                 RefreshToken();

            var accessToken = _config.GetSection("Twitch")["AccessToken"];
            if (String.IsNullOrWhiteSpace(accessToken))
                throw new ApplicationException("No Authorization Token specified in Configuration File");

            RestClient.AddDefaultHeader("Authorization", "Bearer " + accessToken);
        }

        private bool IsRefreshRequired()
        {
            var tokenCreationDateData = _config.GetSection("Twitch")["TokenCreationDate"];

            DateTime.TryParse(tokenCreationDateData, out DateTime tokenCreationDate);
            if (tokenCreationDate == null) throw new ApplicationException("No Authorization Token creation date specified in Configuration File");

            if ((DateTime.Now - tokenCreationDate) > TimeSpan.FromDays(20))
                return true;

            return true;
        }

        private void RefreshToken()
        {
            AuthToken token = null;

            try
            {
                token =  GetApiToken();
                if (token == null || String.IsNullOrWhiteSpace(token.AccessToken)) throw new DataNotFoundException("Getting Authorization Token from Twitch API Failed");
            }
            catch (Exception ex)
            {
                // log
                throw;
            }

            _config.GetSection("Twitch")["AccessToken"] = token.AccessToken;
            _config.GetSection("Twitch")["TokenCreationDate"] = token.CreatedAt.ToShortDateString();
        }

        public AuthToken GetApiToken()
        {
            var token = new AuthToken();
            try
            {
                string tokenBaseUrl = _config.GetSection("Twitch")["TokenBaseUrl"];
                string twitchClientId = _config.GetSection("Twitch")["ClientId"];
                string twitchClientSecret = _config.GetSection("Twitch")["ClientSecret"];


                var request = new RestRequest(tokenBaseUrl, DataFormat.Json);
                request.AddQueryParameter("client_id", twitchClientId);
                request.AddQueryParameter("client_secret", twitchClientSecret);
                request.AddQueryParameter("grant_type", "client_credentials");

                var httpResponse = RestClient.Post(request);
                if (httpResponse.IsSuccessful)
                {
                    var httpResponseData = httpResponse.Content;
                    token = Serializer.SerializeTwitchJsonSingle<AuthToken>(httpResponseData);
                }
            }
            catch (Exception e)
            {
                // log it.
                throw;
            }
            return token;
        }
    }
}
