using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AmazonPayV2.types
{
    /// <summary>
    /// Generic response object for all calls 
    /// </summary>

    public class AmazonPayResponse
    {
        public AmazonPayResponse() { }

        public Uri Url { get; set; }
        public HTTPMethods Method { get; set; }
        public string RawRequest { get; set; }
        public JObject Response { get; set; }
        public string RawResponse { get; set; }
        public string RequestId { get; set; }
        public int Status { get; set; }
        public int Retries { get; set; }
        public long Duration { get; set; }
        public bool Success
        {
            get
            {
                if ( Status == 200 || Status == 201 ) return true;
                else return false;
            }
        }
        public Dictionary<string,string> Headers { get; set; }
    }
}
