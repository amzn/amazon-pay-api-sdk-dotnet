using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Amazon.Pay.API
{
    public class CanonicalBuilder
    {
        private readonly string LineSeparator = "\n";

        /// <summary>
        /// Generates a canonical headers string that consists of a list of all 
        /// HTTP headers that are included with the request.
        /// </summary>
        /// <param name="preSignedHeaders"></param>
        /// <returns>canonical header string</returns>
        public virtual string GetCanonicalizedHeaderString(Dictionary<string, List<string>> preSignedHeaders)
        {
            List<string> sortedHeaders = new List<string>(preSignedHeaders.Keys);
            sortedHeaders.Sort(StringComparer.OrdinalIgnoreCase);

            Dictionary<string, List<string>> requestHeaders = preSignedHeaders;
            StringBuilder headerString = new StringBuilder();
            foreach (string header in sortedHeaders)
            {
                string key = header.ToLower();
                List<string> values = requestHeaders[header];
                StringBuilder headerValue = new StringBuilder();
                foreach (string value in values)
                {
                    if (headerValue.Length > 0)
                    {
                        headerValue.Append(",");
                    }
                    headerValue.Append(Regex.Replace(value.Trim(), "\\s+", " "));
                }
                headerString.Append(Regex.Replace(key.Trim(), "\\s+", " "))
                            .Append(":");
                if (headerValue != null)
                {
                    headerString.Append(headerValue);
                }
                headerString.Append(LineSeparator);
            }

            return headerString.ToString();
        }

        /// <summary>
        /// Generates a canonical string that consists of all the query parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>canonical query string</returns>
        public virtual string GetCanonicalizedQueryString(Dictionary<string, List<string>> parameters)
        {
            SortedDictionary<string, List<string>> sorted = new SortedDictionary<string, List<string>>();

            if (parameters != null)
            {
                foreach (KeyValuePair<string, List<string>> pair in parameters)
                {
                    string encodedParamName = Util.UrlEncode(pair.Key, false);
                    List<string> paramValues = pair.Value;
                    List<string> encodedValues = new List<string>(paramValues.Count);
                    foreach (string value in paramValues)
                    {
                        encodedValues.Add(Util.UrlEncode(value, false));
                    }
                    encodedValues.Sort(StringComparer.Ordinal);
                    sorted.Add(encodedParamName, encodedValues);
                }
            }

            StringBuilder result = new StringBuilder();
            foreach (KeyValuePair<string, List<string>> pair in sorted)
            {
                foreach (string value in pair.Value)
                {
                    if (result.Length > 0)
                    {
                        result.Append("&");
                    }
                    result.Append(pair.Key)
                           .Append("=")
                           .Append(value);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Generates a canonical URI string
        /// </summary>
        /// <param name="path"></param>
        /// <returns>canonical URI string</returns>
        public virtual string GetCanonicalizedURI(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "/";
            }
            else
            {
                string value = Util.UrlEncode(path, true);
                if (value.StartsWith("/"))
                {
                    return value;
                }
                else
                {
                    return string.Concat("/", value);
                }
            }
        }

        /// <summary>
        /// Generates a string that is a list of headers names that
        /// are included in the canonical headers.
        /// </summary>
        /// <param name="preSignedHeaders"></param>
        /// <returns>signed header string</returns>
        public virtual string GetSignedHeadersString(Dictionary<string, List<string>> preSignedHeaders)
        {
            List<string> sortedHeaders = new List<string>(preSignedHeaders.Keys);
            sortedHeaders.Sort(StringComparer.OrdinalIgnoreCase);

            StringBuilder headerString = new StringBuilder();
            foreach (string header in sortedHeaders)
            {
                if (headerString.Length > 0)
                {
                    headerString.Append(";");
                }
                headerString.Append(header.ToLower());
            }

            return headerString.ToString();
        }

        /// <summary>
        /// Generates a Hex encoded string from a hashed value of the payload string
        /// </summary>
        /// <param name="value"></param>
        /// <returns>hashed payload string</returns>
        public virtual string HashThenHexEncode(string value)
        {
            StringBuilder hashValue = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                foreach (Byte b in result)
                {
                    hashValue.Append(b.ToString("x2"));
                }
            }

            return hashValue.ToString();
        }
    }
}
