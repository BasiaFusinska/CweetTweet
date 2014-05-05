using System;
using System.Globalization;

namespace TwitterModel
{
    internal class TwitterDateTimeParser
    {
        private const string TwitterFormatString = "ddd MMM dd HH:mm:ss zzzz yyyy";

        public static DateTime Parse(string dateTimeString)
        {
            return DateTime.ParseExact(dateTimeString, TwitterFormatString, CultureInfo.InvariantCulture).ToUniversalTime();
        }
    }
}
