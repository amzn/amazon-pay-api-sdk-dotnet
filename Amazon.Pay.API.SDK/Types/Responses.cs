﻿using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.Types
{
    /// <summary>
    /// Response object for API calls 
    /// </summary>
    public class AmazonPayResponse
    {
        public Uri Url { get; internal set; }
        
        public HttpMethod Method { get; internal set; }
        
        public string RawRequest { get; internal set; }
        
        public string RawResponse { get; internal set; }
        
        public string RequestId { get; internal set; }
        
        public int Status { get; internal set; }
        
        public int Retries { get; internal set; }
        
        public long Duration { get; internal set; }
        
        public bool Success
        {
            get
            {
                if ( Status == 200 || Status == 201 ) return true;
                else return false;
            }
        }
        
        public Dictionary<string,string> Headers { get; internal set; }
    }
}
