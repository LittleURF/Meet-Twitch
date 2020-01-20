using Meet_Twitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meet_Twitch.Core.Services
{
    public class TwitchService : ITwitchService
    {
        public string SetTwitchUrlImageSize(string imageUrl, int width, int height)
        {
            if (String.IsNullOrEmpty(imageUrl))
                return null;

            imageUrl = imageUrl.Replace("%{width}", width.ToString());
            imageUrl = imageUrl.Replace("%{height}", height.ToString());

            return imageUrl;
        }

        // return type makes it hard, to remove prolly or refactor
        public List<string> SetTwitchUrlImageSize(List<string> imageUrls, int width, int height)
        {

            if (imageUrls == null || imageUrls.Count == 0)
                return null;

            foreach (var imageUrl in imageUrls)
            {
                imageUrl.Replace("%{width}", width.ToString());
                imageUrl.Replace("%{height}", height.ToString());
            }

            return imageUrls;
        }
    }
}
