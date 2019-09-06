using AmazonPayV2.Exceptions;
using AmazonPayV2.types;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AmazonPayV2
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
        /// Generate the service URI for in-store calls
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns>service URI</returns>
        public static string GetServiceURI(PayConfiguration configuration)
        {
            string serviceURL;
            string clientEnvironment = "";

            if (configuration.Environment == Environments.sandbox)
            {
                clientEnvironment = "/sandbox/";
            }
            else
            {
                clientEnvironment = "/live/";
            }

            if (Constants.endpointMappings["service"] == "")
            {
                if (Constants.endpointMappings.ContainsKey(configuration.Region.ToString()))
                {
                    serviceURL = Constants.endpointMappings[configuration.Region.ToString()] + clientEnvironment;
                }
                else
                {
                    throw new AmazonPayClientException(configuration.Region.ToString() + " is not a valid region");
                }
            }
            else
            {
                serviceURL = Constants.endpointMappings["service"] + clientEnvironment;
            }
            return serviceURL;
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
            string osVersion = Environment.OSVersion.ToString();

            StringBuilder userAgentBuilder = new StringBuilder(Constants.GithubSDKName).Append("/")
                .Append(Constants.SDKClientVersion)
                .Append("(").Append(osVersion).Append(")");

            return userAgentBuilder.ToString();
        }

        /// <summary>
        /// Helper method to check and modify the scan data if it an employee badge id
        /// </summary>
        /// <param name="scanRequest"></param>
        /// <returns></returns>
        public static JObject CheckAndModifyIfBadgePayRequest(JObject scanRequest)
        {
            if (!string.IsNullOrEmpty((string)scanRequest.SelectToken(Constants.ScanData)))
            {
                string scanData = (string)scanRequest.SelectToken(Constants.ScanData);

                // Some badge readers prepend a '!' character to the badge RFID
                // Strip the first character from the string if it starts with a '!'
                if (scanData[Constants.ScanDataStartIndex] == '!')
                {
                    scanData = scanData.Substring(Constants.ScanDataUpdatedStartIndex);
                }

                if (scanData.Length >= 7 && scanData.Length <= 11 && scanData.All(char.IsDigit))
                {
                    // This is only for sandbox testing for badge pay use case
                    if (scanData.Equals("10658219"))
                    {
                        scanData = "1234567";
                    }
                    scanData = string.Concat(Constants.BadgePayPrefix, scanData);

                    // If the badge id starts with '!', scanRequest will be 
                    // updated with the stripped scanData only if its length is 7
                    // and it contains only digits. Otherwise the scanData will be sent 
                    // as-is.
                    scanRequest[Constants.ScanData] = scanData;
                }
            }

            return scanRequest;
        }
        /// <summary>
        /// Helper method to check and modify the scan data if it an employee badge id
        /// </summary>
        /// <param name="scanRequest"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ValidateIDPKey(Dictionary<string, string> headers = null)
        {
            if (headers == null) headers = new Dictionary<string, string>();

            String idpKey = Guid.NewGuid().ToString().Replace("-", "");
            if (!headers.ContainsKey("x-amz-pay-Idempotency-Key")) headers.Add("x-amz-pay-Idempotency-Key", idpKey);

            return headers;
        }
    }
}