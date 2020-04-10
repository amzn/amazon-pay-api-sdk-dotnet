using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Amazon.Pay.API
{
    public class Util
    {
        /// <summary>
        /// Generates a url encoded string from the given string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        /// <returns>url encoded string</returns>
        public static string UrlEncode(string data, bool path)
        {
            if (data == null)
            {
                return "";
            }

            if (path)
            {
                data = Regex.Replace(data, @"/+", "/");
            }

            StringBuilder encoded = new StringBuilder();
            string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~" + (path ? "/" : "");

            foreach (char symbol in Encoding.UTF8.GetBytes(data))
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    encoded.Append(symbol);
                }
                else
                {
                    encoded.Append("%" + String.Format("{0:X2}", (int)symbol));
                }
            }

            return encoded.ToString();
        }

        /// <summary>
        /// Formats date as ISO 8601 timestamp
        /// </summary>
        /// <returns>formatted timestamp</returns>
        public static string GetFormattedTimestamp()
        {
            return DateTime.UtcNow.ToString("yyyyMMdd\\THHmmss\\Z",
                                        CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Generates the next wait interval, in milliseconds, using an exponential
        /// backoff algorithm.
        /// </summary>
        /// <param name="retryCount"></param>
        /// <returns>wait time</returns>
        public static int GetExponentialWaitTime(int retryCount)
        {
            return ((int)Math.Pow(2, retryCount) * 1000);
        }

        /// <summary>
        /// Generates the user agent header
        /// </summary>
        /// <returns>user agent string</returns>
        public static string BuildUserAgentHeader()
        {
            string osVersion = System.Environment.OSVersion.ToString();

            StringBuilder userAgentBuilder = new StringBuilder(Constants.SdkName).Append("/")
                .Append(Constants.SdkVersion)
                .Append("(").Append(osVersion).Append(")");

            return userAgentBuilder.ToString();
        }
    }
}