using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.Types
{
    public class ApiRequest
    {
        private ApiRequestBody body;

        public ApiRequest(Uri path, HttpMethod method)
        {
            Headers = new Dictionary<string, string>();
            QueryParameters = new Dictionary<string, List<string>>();

            Path = path;
            HttpMethod = method;
        }

        public ApiRequest(Uri path, HttpMethod method, ApiRequestBody body, Dictionary<string, string> headers)
            : this(path, method)
        {
            Body = body;
            Headers = headers;
        }

        public Uri Path { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public Dictionary<string, List<string>> QueryParameters;

        public ApiRequestBody Body
        {
            get => body;
            set
            {
                body = value;
                BodyAsJsonString = body.ToJson();
            }
        }

        public string BodyAsJsonString { get; private set; }
    }
}
