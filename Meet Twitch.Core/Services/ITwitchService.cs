using System.Collections.Generic;

namespace Meet_Twitch.Core.Services
{
    public interface ITwitchService
    {
        string SetTwitchUrlImageSize(string imageUrl, int width, int height);
        List<string> SetTwitchUrlImageSize(List<string> imageUrls, int width, int height);
    }
}