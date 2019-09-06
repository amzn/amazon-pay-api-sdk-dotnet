using AmazonPayV2.Exceptions;
using AmazonPayV2.types;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AmazonPayV2
{
    public class AmazonPayClient
    {
        protected PayConfiguration payConfiguration;
        protected SignatureHelper signatureHelper;
        protected CanonicalBuilder canonicalBuilder;
        protected string privateKey;
        protected Dictionary<string, List<string>> queryParametersMap = new Dictionary<string, List<string>>();

        public AmazonPayClient(PayConfiguration payConfiguration)
        {
            CheckIfConfigParametersAreSet(payConfiguration);
            this.payConfiguration = payConfiguration;
            canonicalBuilder = new CanonicalBuilder();
            signatureHelper = new SignatureHelper(payConfiguration, canonicalBuilder);
            privateKey = payConfiguration.PrivateKey;
        }

        /// <summary>
        /// API to process the request and return the signed headers
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="queryParameters"></param>
        /// <param name="request"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse CallAPI(Uri uri,
                               HTTPMethods httpMethod,
                               Dictionary<string, List<string>> queryParameters,
                               string request,
                               Dictionary<string, string> headers)
        {
            Dictionary<string, string> postSignedHeaders;

            if (headers.Count > 0)
            {
                postSignedHeaders = SignRequest(uri, httpMethod, queryParameters, request, headers);
            }
            else
            {
                postSignedHeaders = SignRequest(uri, httpMethod, queryParameters, request);
            }

            return ProcessRequest(uri, postSignedHeaders, request, httpMethod);
        }

        /// <summary>
        /// Signs the request provided and returns the signed headers map
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethodName"></param>
        /// <param name="queryParamaters"></param>
        /// <param name="requestPayload"></param>
        /// <returns>signed headers</returns>
        public Dictionary<string, string> SignRequest(Uri uri,
                                                      HTTPMethods httpMethod,
                                                      Dictionary<string, List<string>> queryParamaters,
                                                      string requestPayload,
                                                      Dictionary<string, string> headers = null)
        {
            string publicKeyId = payConfiguration.PublicKeyId;
            Dictionary<string, List<string>> preSignedHeaders = signatureHelper.CreatePreSignedHeaders(uri);

            if (headers != null && headers.Count>0)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    preSignedHeaders[header.Key.ToLower()] = new List<string>
                     {
                         header.Value
                     };
                }
            }

            string userAgent = Util.BuildUserAgentHeader();

            string canonicalRequest = signatureHelper.CreateCanonicalRequest(uri, httpMethod, queryParamaters, requestPayload, preSignedHeaders);
            string stringToSign = signatureHelper.CreateStringToSign(canonicalRequest);
            string signature = signatureHelper.GenerateSignature(stringToSign, privateKey);

            string authorizationHeader = BuildAuthorizationHeader(payConfiguration.PublicKeyId, preSignedHeaders, signature);

            Dictionary<string, string> postSignedHeadersMap = new Dictionary<string, string>();

            foreach (string key in preSignedHeaders.Keys)
            {
                postSignedHeadersMap.Add(key.ToLower(), preSignedHeaders[key][0]);
            }

            postSignedHeadersMap.Add("authorization", authorizationHeader);
            postSignedHeadersMap.Add("user-agent", userAgent);

            return postSignedHeadersMap;
        }

        /// <summary>
        /// Helper method to send the request and also retry in case 
        /// the request is throttled
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="postSignedHeaders"></param>
        /// <param name="payload"></param>
        /// <param name="httpMethodName"></param>
        /// <returns>AmazonPayResponse response</returns>
        protected AmazonPayResponse ProcessRequest(Uri uri,
                                       Dictionary<string, string> postSignedHeaders,
                                       string payload,
                                       HTTPMethods httpMethod)
        {
            Dictionary<string, string> responseDict;
            AmazonPayResponse responseObject = new AmazonPayResponse();
            responseObject.Url = uri;
            responseObject.Method = httpMethod;
            if (!String.IsNullOrEmpty(payload))
            {
                responseObject.RawRequest = payload;
            }
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            try
            {
                responseDict = SendRequest(uri, postSignedHeaders, payload, httpMethod);
                int status = int.Parse(responseDict["status"]);
                int retry = 0;
                while (Constants.serviceErrors.ContainsValue(status) && retry < payConfiguration.MaxRetries)
                {
                    retry++;
                    int delay = Util.GetExponentialWaitTime(retry);
                    System.Threading.Thread.Sleep(delay);

                    responseDict = SendRequest(uri, postSignedHeaders, payload, httpMethod);
                    status = int.Parse(responseDict["status"]);
                }

                responseObject.RequestId = responseDict["requestId"];
                responseObject.Status = status;
                responseObject.Retries = retry;
                responseObject.Duration = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - milliseconds;
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }

            if (!String.IsNullOrEmpty(responseDict["response"]))
            {
                responseObject.RawResponse = responseDict["response"];
                responseObject.Response = JObject.Parse(responseDict["response"]);
            }
            return responseObject;
        }

        /// <summary>
        /// Helper method to execute the request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="postSignedHeaders"></param>
        /// <param name="payload"></param>
        /// <param name="httpMethodName"></param>
        /// <returns>response</returns>
        protected Dictionary<string, string> SendRequest(Uri uri,
                                                       Dictionary<string, string> postSignedHeaders,
                                                       string payload,
                                                       HTTPMethods httpMethod)
        {
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            short status = 0;
            string response = "";
            Dictionary<string, string> responseDict = new Dictionary<string, string>();
            String requestId = "";

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
            request.Method = httpMethod.ToString();

            if (httpMethod != HTTPMethods.GET)
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                }
            }

            try
            {
                using (HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse)
                {
                    status = (short)httpResponse.StatusCode;
                    StreamReader reader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8);
                    response = reader.ReadToEnd();
                    if ((httpResponse.Headers["X-Amz-Pay-Request-Id"] ?? "").Trim().Length > 0)
                    {
                        requestId = httpResponse.Headers["X-Amz-Pay-Request-Id"];
                    }
                }
            }
            catch (WebException we)
            {
                using (HttpWebResponse httpErrorResponse = (HttpWebResponse)we.Response as HttpWebResponse)
                {

                    if (httpErrorResponse == null)
                    {
                        throw new AmazonPayClientException("Http Response is empty " + we);
                    }
                    if (httpErrorResponse != null)
                    {
                        status = (short)httpErrorResponse.StatusCode;
                        using (StreamReader reader = new StreamReader(httpErrorResponse.GetResponseStream(), Encoding.UTF8))
                        {
                            response = reader.ReadToEnd();
                        }
                    }
                }
            }

            responseDict.Add("response", response);
            responseDict.Add("status", status.ToString());
            responseDict.Add("requestId", requestId);
            return responseDict;
        }

        /// <summary>
        /// Builds the authorization header
        /// </summary>
        /// <param name="publicKeyId"></param>
        /// <param name="preSignedHeaders"></param>
        /// <param name="signature"></param>
        /// <returns>authorization header</returns>
        protected string BuildAuthorizationHeader(string publicKeyId,
                                                Dictionary<string, List<string>> preSignedHeaders,
                                                string signature)
        {
            StringBuilder authorizationBuilder = new StringBuilder(Constants.AmazonSignatureAlgorithm)
                    .Append(" PublicKeyId=").Append(publicKeyId).Append(", ").Append("SignedHeaders=")
                    .Append(canonicalBuilder.GetSignedHeadersString(preSignedHeaders))
                    .Append(", Signature=").Append(signature);

            return authorizationBuilder.ToString();
        }

        /// <summary>
        /// Check if the necessary parameters are set
        /// </summary>
        /// <param name="payConfiguration"></param>
        protected void CheckIfConfigParametersAreSet(PayConfiguration payConfiguration)
        {
            if (String.IsNullOrEmpty(payConfiguration.Region.ToString()))
            {
                GenerateException(Constants.Region);
            }
            if (String.IsNullOrEmpty(payConfiguration.PrivateKey))
            {
                GenerateException(Constants.PrivateKey);
            }
            if (String.IsNullOrEmpty(payConfiguration.PublicKeyId))
            {
                GenerateException(Constants.PublicKeyId);
            }
        }

        /// <summary>
        /// Helper method to throw exceptions for missing config parameters
        /// </summary>
        /// <param name="parameter"></param>
        protected void GenerateException(string parameter)
        {
            throw new InvalidDataException(parameter + " is not set in the config, this is a required parameter");
        }

        /// <summary>
        /// Helps the solution provider make the DeliveryTrackers request with their auth token
        /// </summary>
        /// <param name="deliveryTrackersRequest"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse DeliveryTrackers(JObject deliveryTrackersRequest, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                Uri deliveryTrackersUri = new Uri( Util.GetServiceURI(payConfiguration ) + Constants.AmazonPayAPIPathDeliveryTrackers + Constants.AmazonPayAPIVersionDeliveryTrackers + "/" + Constants.DeliveryTrackers );
                return CallAPI(deliveryTrackersUri, HTTPMethods.POST, queryParametersMap, deliveryTrackersRequest.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }
    }
    public class InStoreClient : AmazonPayClient
    {
        public InStoreClient(PayConfiguration payConfiguration) : base(payConfiguration)
        {
        }

        /// <summary>
        /// Helps the solution provider make the MerchantScan request with their auth token
        /// </summary>
        /// <param name="scanRequest"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse MerchantScan(JObject scanRequest, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                scanRequest = Util.CheckAndModifyIfBadgePayRequest(scanRequest);
                Uri scanUri = new Uri(Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathInStore + Constants.AmazonPayAPIVersionInStore + "/" + Constants.MerchantScan);
                return CallAPI(scanUri, HTTPMethods.POST, queryParametersMap, scanRequest.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider make the Charge request with their auth token
        /// </summary>
        /// <param name="chargeRequest"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse Charge(JObject chargeRequest, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                Uri chargeUri = new Uri(Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathInStore + Constants.AmazonPayAPIVersionInStore + "/" + Constants.Charge);
                return CallAPI(chargeUri, HTTPMethods.POST, queryParametersMap, chargeRequest.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider make the Refund request with their auth token
        /// </summary>
        /// <param name="refundRequest"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse Refund(JObject refundRequest, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                Uri refundUri = new Uri(Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathInStore + Constants.AmazonPayAPIVersionInStore + "/" + Constants.Refund);
                return CallAPI(refundUri, HTTPMethods.POST, queryParametersMap, refundRequest.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }
    }
    public class WebStoreClient : AmazonPayClient
    {
        public WebStoreClient(PayConfiguration payConfiguration) : base(payConfiguration)
        {
        }
        /// <summary>
        /// Helps the solution provider create the createCheckoutSessions request
        /// </summary>
        /// <param name="checkoutSessions"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse CreateCheckoutSession(JObject checkoutSessions, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = Util.ValidateIDPKey(headers);
                Uri uri = new Uri( Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.CheckoutSessions);

                return CallAPI(uri, HTTPMethods.POST , queryParametersMap, checkoutSessions.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider get a CheckoutSession request
        /// </summary>
        /// <param name="checkoutSessionId"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse GetCheckoutSession(String checkoutSessionId, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                Uri uri = new Uri( Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.CheckoutSessions + "/" + checkoutSessionId);
                return CallAPI(uri, HTTPMethods.GET, queryParametersMap, "", headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider update a Checkout Session
        /// </summary>
        /// <param name="checkoutSessionId"></param>
        /// <param name="updateObject"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse UpdateCheckoutSession(String checkoutSessionId, JObject updateObject, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                Uri uri = new Uri(Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.CheckoutSessions + "/" + checkoutSessionId);

                return CallAPI(uri, HTTPMethods.PATCH, queryParametersMap, updateObject.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider get details of a chargePermission
        /// </summary>
        /// <param name="chargePermissionId"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse GetChargePermission(String chargePermissionId, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                Uri uri = new Uri(Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.ChargePermissions + "/" + chargePermissionId);
                return CallAPI(uri, HTTPMethods.GET, queryParametersMap, "", headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider update a ChargePermission
        /// </summary>
        /// <param name="chargePermissionId"></param>
        /// <param name="updateObject"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse UpdateChargePermission(String chargePermissionId, JObject updateObject, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                Uri uri = new Uri(Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.ChargePermissions + "/" + chargePermissionId);
                return CallAPI(uri, HTTPMethods.PATCH, queryParametersMap, updateObject.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider close a ChargePermission
        /// </summary>
        /// <param name="chargePermissionId"></param>
        /// <param name="closeRequest"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse CloseChargePermission(String chargePermissionId, JObject closeRequest, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                Uri uri = new Uri(Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.ChargePermissions + "/" + chargePermissionId + "/close");
                return CallAPI(uri, HTTPMethods.DELETE, queryParametersMap, closeRequest.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider create a Charge
        /// </summary>
        /// <param name="createChargeRequest"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse CreateCharge(JObject createChargeRequest, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = Util.ValidateIDPKey(headers);
                Uri uri = new Uri(Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.Charges);
                return CallAPI(uri, HTTPMethods.POST, queryParametersMap, createChargeRequest.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider capture an existing Charge
        /// </summary>
        /// <param name="chargeId"></param>
        /// <param name="captureRequest"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse CaptureCharge(string chargeId, JObject captureRequest, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = Util.ValidateIDPKey(headers);
                Uri uri = new Uri(Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.Charges + "/" + chargeId + "/capture");
                return CallAPI(uri, HTTPMethods.POST, queryParametersMap, captureRequest.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider get details of a charge
        /// </summary>
        /// <param name="chargeId"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse GetCharge(String chargeId, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                Uri uri = new Uri( Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.Charges + "/" + chargeId);
                return CallAPI(uri, HTTPMethods.GET, queryParametersMap, "", headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider close a charge
        /// </summary>
        /// <param name="chargeId"></param>
        /// <param name="cancelRequest"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse CancelCharge(String chargeId, JObject cancelRequest, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                Uri uri = new Uri( Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.Charges + "/" + chargeId + "/cancel");
                return CallAPI(uri, HTTPMethods.DELETE, queryParametersMap, cancelRequest.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider generate a refund
        /// </summary>
        /// <param name="refundRequest"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse CreateRefund(JObject refundRequest, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = Util.ValidateIDPKey(headers);
                Uri uri = new Uri( Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.Refunds);
                return CallAPI(uri, HTTPMethods.POST, queryParametersMap, refundRequest.ToString(), headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Helps the solution provider get details of a refund
        /// </summary>
        /// <param name="refundId"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public AmazonPayResponse GetRefund(String refundId, Dictionary<string, string> headers = null)
        {
            try
            {
                headers = headers ?? new Dictionary<string, string>();
                Uri uri = new Uri(Util.GetServiceURI(payConfiguration) + Constants.AmazonPayAPIPathWebStore + Constants.AmazonPayAPIVersionWebStore + "/" + Constants.Refunds + "/" + refundId);
                return CallAPI(uri, HTTPMethods.GET, queryParametersMap, "", headers);
            }
            catch (Exception ex)
            {
                throw new AmazonPayClientException(ex.Message, ex);
            }
        }

    }
}
