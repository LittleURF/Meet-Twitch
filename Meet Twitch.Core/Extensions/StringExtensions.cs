using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meet_Twitch.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ShortenTo(this string givenString, int maxLength)
        {
            if (givenString.Length > maxLength)
            {
                givenString = givenString.Substring(0, maxLength - 3);
                givenString += "...";
            }
            return givenString;
        }
    }
}
