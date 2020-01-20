using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meet_Twitch.Core.Utilities
{
    public class Serializer
    {
        // try catch on converting etc.?
        public static T SerializeTwitchJsonSingle<T>(string json)
        {
            if (String.IsNullOrWhiteSpace(json))
                return default;

            JObject jsonObject = JObject.Parse(json);

            JToken jsonToken = null;
            if (jsonObject["data"] == null)
                jsonToken = jsonObject;
            else
                jsonToken = jsonObject["data"].First;

            T result = jsonToken.ToObject<T>();

            return (T)Convert.ChangeType(result, typeof(T));
        }

        public static List<T> SerializeTwitchJsonList<T>(string json)
        {
            if (String.IsNullOrWhiteSpace(json))
                return default;

            List<T> result = new List<T>();
            JObject jsonObject = JObject.Parse(json);

            List<JToken> jsonTokens = null;
            if (jsonObject["data"] == null)
                jsonTokens = jsonObject.Children().ToList();
            else
                jsonTokens = jsonObject["data"].Children().ToList();


            foreach (var token in jsonTokens)
            {
                result.Add(token.ToObject<T>());
            }

            return (List<T>)Convert.ChangeType(result, typeof(List<T>));
        }
    }
}
