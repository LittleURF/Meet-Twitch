using Meet_Twitch.Core.Models;
using RestSharp;
using System.Threading.Tasks;

namespace Meet_Twitch.Core.ApiClients
{
    public interface ITwitchApi
    {
        RestClient RestClient { get; }
        AuthToken GetApiToken();
    }
}