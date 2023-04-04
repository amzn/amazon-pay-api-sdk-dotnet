using Amazon.Pay.API.AuthorizationToken;
using Amazon.Pay.API.DeliveryTracker;
using Amazon.Pay.API.Exceptions;
using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Amazon.Pay.API
{
    public abstract class Client 
    {
        protected ApiUrlBuilder apiUrlBuilder;
        protected ApiConfiguration payConfiguration;
        protected virtual ISignatureHelper signatureHelper { get; private set; }
        protected CanonicalBuilder canonicalBuilder;
        protected Dictionary<string, List<string>> queryParametersMap = new Dictionary<string, List<string>>();

        public Client(ApiConfiguration payConfiguration)
        {
            apiUrlBuilder = new ApiUrlBuilder(payConfiguration);

            this.payConfiguration = payConfiguration;
            canonicalBuilder = new CanonicalBuilder();
            signatureHelper = new SignatureHelper(payConfiguration, canonicalBuilder);
        }


        /// <summary>
        /// API to process the request and return the signed headers
        /// </summary>
        public T CallAPI<T>(ApiRequest apiRequest) where T : AmazonPayResponse, new()
        {
            if (apiRequest.Headers == null)
            {
                apiRequest.Headers = new Dictionary<string, string>();
            }

            // for POST calls, add an idempotency key if it hasn't been provided yet
            if (apiRequest.HttpMethod == HttpMethod.POST && !apiRequest.Headers.ContainsKey(Constants.Headers.IdempotencyKey))
            {
                // remove dashes from GUID as these aren't supported characters for the idempotency Key
                apiRequest.Headers.Add(Constants.Headers.IdempotencyKey, Guid.NewGuid().ToString().Replace("-", ""));
            }

            Dictionary<string, string> postSignedHeaders = SignRequest(apiRequest);

            return ProcessRequest<T>(apiRequest, postSignedHeaders);
        }

        /// <summary>
        /// Signs the request provided and returns the signed headers map
        /// </summary>
        internal Dictionary<string, string> SignRequest(ApiRequest request)
        {
            Dictionary<string, List<string>> preSignedHeaders = signatureHelper.CreateDefaultHeaders(request.Path);

            if (request.Headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> header in request.Headers)
                {
                    preSignedHeaders[header.Key.ToLower()] = new List<string>
                     {
                         header.Value
                     };
                }
            }

            string canonicalRequest = signatureHelper.CreateCanonicalRequest(request, preSignedHeaders);
            string stringToSign = signatureHelper.CreateStringToSign(canonicalRequest);
            string signature = signatureHelper.GenerateSignature(stringToSign);

            Dictionary<string, string> postSignedHeadersMap = new Dictionary<string, string>();

            foreach (string key in preSignedHeaders.Keys)
            {
                postSignedHeadersMap.Add(key.ToLower(), preSignedHeaders[key][0]);
            }

            string authorizationHeader = BuildAuthorizationHeader(preSignedHeaders, signature);
            postSignedHeadersMap.Add("authorization", authorizationHeader);

            string userAgent = Util.BuildUserAgentHeader();
            postSignedHeadersMap.Add("user-agent", userAgent);

            return postSignedHeadersMap;
        }

        /// <summary>
        /// Sends the API requests and processes the result by filling the AmazonPayResponse object.
        /// </summary>
        protected virtual T ProcessRequest<T>(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders)
            where T : AmazonPayResponse, new()
        {
            var responseObject = new T();

            long startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            try
            {
                int retries = 0;

                while (retries <= payConfiguration.MaxRetries)
                {
                    using (HttpWebResponse httpWebResponse = SendRequest(apiRequest, postSignedHeaders))
                    using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        // if API call failed with a service error, try again
                        if (Constants.serviceErrors.ContainsValue((int)httpWebResponse.StatusCode))
                        {
                            retries++;
                            int delay = Util.GetExponentialWaitTime(retries);
                            System.Threading.Thread.Sleep(delay);
                        }
                        // otherwise parse the result (even if it returned with 400 or any other "expected" error)
                        else
                        {
                            // if we have a response, deserialize it first as deserialization will overwrite any earlier assigned fields 
                            string response = reader.ReadToEnd();
                            if (!string.IsNullOrEmpty(response))
                            {
                                var dateConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyyMMddTHHmmssZ" };

                                JObject jsonResponse = JObject.Parse(response);
                                if (jsonResponse.ContainsKey("shippingAddressList")) {
                                    JArray shippingAddressList = (JArray)jsonResponse["shippingAddressList"];
                                    for (int i = 0; i < shippingAddressList.Count; i++)
                                    {
                                        shippingAddressList[i] = JObject.Parse(shippingAddressList[i].ToString());
                                    }
                                    response = jsonResponse.ToString();
                                }
                                responseObject = JsonConvert.DeserializeObject<T>(response, dateConverter);
                            }

                            // get the headers of the response objects
                            responseObject.Headers = new Dictionary<string, string>();
                            foreach (string key in httpWebResponse.Headers)
                            {
                                responseObject.Headers.Add(key, httpWebResponse.Headers.Get(key));

                                if (key.Equals(Constants.Headers.RequestId, StringComparison.OrdinalIgnoreCase))
                                {
                                    responseObject.RequestId = httpWebResponse.Headers[Constants.Headers.RequestId];
                                }
                            }

                            responseObject.Status = (int)httpWebResponse.StatusCode;
                            responseObject.RawResponse = response;
                            responseObject.Url = apiRequest.Path;
                            responseObject.Method = apiRequest.HttpMethod;
                            responseObject.RawRequest = apiRequest.Body?.ToJson();
                            responseObject.Retries = retries;

                            break;
                        }
                    }
                }

                responseObject.Duration = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - startTime;
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }

            return responseObject;
        }

        /// <summary>
        /// Helper method to execute the request
        /// </summary>
        protected virtual HttpWebResponse SendRequest(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders)
        {

            string path = apiRequest.Path.ToString();

            // add the query parameters to the URL, if there are any
            if (apiRequest.QueryParameters != null && apiRequest.QueryParameters.Count > 0)
            {
                var canonicalBuilder = new CanonicalBuilder();
                path += "?" + canonicalBuilder.GetCanonicalizedQueryString(apiRequest.QueryParameters);
            }

            // TODO: move setting of SecurityProtocol into constructor

            // ensure the right minimum TLS version is being used
            if (Util.IsObsoleteSecurityProtocol(ServicePointManager.SecurityProtocol))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }

            // TODO: consider switching to HttpClient class for web requests

            // create the web request
            HttpWebRequest request = WebRequest.Create(path) as HttpWebRequest;

            foreach (KeyValuePair<string, string> header in postSignedHeaders)
            {
                if (WebHeaderCollection.IsRestricted(header.Key))
                {
                    switch (header.Key)
                    {
                        case "accept":
                            request.Accept = header.Value;
                            break;
                        case "content-type":
                            request.ContentType = header.Value;
                            break;
                        case "user-agent":
                            request.UserAgent = header.Value;
                            break;
                        default:
                            throw new AmazonPayClientException("unknown header" + " " + header.Key);
                    }
                }
                else
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            request.Method = apiRequest.HttpMethod.ToString();

            if (apiRequest.HttpMethod != HttpMethod.GET)
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(apiRequest.Body.ToJson());
                    streamWriter.Flush();
                }
            }

            HttpWebResponse httpResponse;
            try
            {
                httpResponse = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException we)
            {
                httpResponse = (HttpWebResponse)we.Response as HttpWebResponse;

                if (httpResponse == null)
                {
                    throw new AmazonPayClientException("Http Response is empty " + we);
                }
            }

            return httpResponse;
        }

        /// <summary>
        /// Builds the authorization header
        /// </summary>
        protected string BuildAuthorizationHeader(Dictionary<string, List<string>> preSignedHeaders, string signature)
        {
            StringBuilder authorizationBuilder = new StringBuilder(payConfiguration.Algorithm.GetName())
                    .Append(" PublicKeyId=").Append(payConfiguration.PublicKeyId).Append(", ").Append("SignedHeaders=")
                    .Append(canonicalBuilder.GetSignedHeadersString(preSignedHeaders))
                    .Append(", Signature=").Append(signature);

            return authorizationBuilder.ToString();
        }

        /// <summary>
        /// Sends delivery tracking information that will trigger a Alexa Delivery Notification when the item is shipped or about to be delivered.
        /// </summary>
        /// <returns>DeliveryTrackerResponse response</returns>
        public DeliveryTrackerResponse SendDeliveryTrackingInformation(DeliveryTrackerRequest deliveryTrackersRequest, Dictionary<string, string> headers = null)
        {
            var apiUrl = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.DeliveryTracker);
            var apiRequest = new ApiRequest(apiUrl, HttpMethod.POST, deliveryTrackersRequest, headers);

            var result = CallAPI<DeliveryTrackerResponse>(apiRequest);

            return result;
        }


        /// <summary>
        /// Retrieves a delegated authorization token used in order to make API calls on behalf of a merchant.
        /// </summary>
        /// <param name="mwsAuthToken">The MWS Auth Token that the solution provider currently uses to make V1 API calls on behalf of the merchant.</param>
        /// <param name="merchantId">The Amazon Pay merchant ID.</param>
        /// <returns>HS256 encoded JWT Token that will be used to make V2 API calls on behalf of the merchant.</returns>
        public AuthorizationTokenResponse GetAuthorizationToken(string mwsAuthToken, string merchantId, Dictionary<string, string> headers = null)
        {
            var apiUrl = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.TokenExchange, mwsAuthToken);
            var apiRequest = new ApiRequest(apiUrl, HttpMethod.GET);
            apiRequest.Headers = headers;

            apiRequest.QueryParameters.Add("merchantId", new List<string>() { merchantId });

            var result = CallAPI<AuthorizationTokenResponse>(apiRequest);

            return result;
        }
    }
}
