using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Charge;
using Amazon.Pay.API.WebStore.ChargePermission;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Refund;
using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore
{
    public class WebStoreClient : Client
    {
        public WebStoreClient(ApiConfiguration payConfiguration) : base(payConfiguration)
        {
            
        }

        /// <summary>
        /// Creates a new Checkout Session.
        /// </summary>
        public CheckoutSessionResponse CreateCheckoutSession(CreateCheckoutSessionRequest body, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.CheckoutSessions);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, body, headers);

            var result = CallAPI<CheckoutSessionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Gets a Checkout Session.
        /// </summary>
        public CheckoutSessionResponse GetCheckoutSession(string checkoutSessionId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.CheckoutSessions, checkoutSessionId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<CheckoutSessionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Updates a Checkout Session.
        /// </summary>
        public CheckoutSessionResponse UpdateCheckoutSession(string checkoutSessionId, UpdateCheckoutSessionRequest body, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.CheckoutSessions, checkoutSessionId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.PATCH, body, headers);

            var result = CallAPI<CheckoutSessionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Completes a Checkout Session.
        /// </summary>
        public CheckoutSessionResponse CompleteCheckoutSession(string checkoutSessionId, CompleteCheckoutSessionRequest completeRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.CheckoutSessions, checkoutSessionId, Constants.Methods.Complete);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, completeRequest, headers);

            var result = CallAPI<CheckoutSessionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Helps the solution provider get details of a chargePermission
        /// </summary>
        /// <param name="chargePermissionId"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public ChargePermissionResponse GetChargePermission(string chargePermissionId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.ChargePermissions, chargePermissionId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<ChargePermissionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Updates a Charge Permission.
        /// </summary>
        public ChargePermissionResponse UpdateChargePermission(string chargePermissionId, UpdateChargePermissionRequest request, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.ChargePermissions, chargePermissionId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.PATCH, request, headers);

            var result = CallAPI<ChargePermissionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Closes a Charge Permission.
        /// </summary>
        public ChargePermissionResponse CloseChargePermission(string chargePermissionId, CloseChargePermissionRequest request, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.ChargePermissions, chargePermissionId, Constants.Methods.Close);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.DELETE, request, headers);

            var result = CallAPI<ChargePermissionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Create a Charge to authorize payment.
        /// </summary>
        /// <returns>ChargeResponse response</returns>
        public ChargeResponse CreateCharge(CreateChargeRequest createChargeRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Charges);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, createChargeRequest, headers);

            var result = CallAPI<ChargeResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Capture payment on a Charge in the Authorized state.
        /// </summary>
        /// <returns>ChargeResponse response</returns>
        public ChargeResponse CaptureCharge(string chargeId, CaptureChargeRequest captureRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Charges, chargeId, Constants.Methods.Capture);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, captureRequest, headers);

            var result = CallAPI<ChargeResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Gets a Charge object.
        /// </summary>
        /// <returns>ChargeResponse response</returns>
        public ChargeResponse GetCharge(string chargeId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Charges, chargeId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<ChargeResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Moves Charge to Canceled state and releases any authorized payments.
        /// </summary>
        /// <returns>AmazonPayResponse response</returns>
        /// <remarks>
        /// You can call this operation until Capture is initiated while Charge is in an AuthorizationInitiated or Authorized state.
        /// </remarks>
        public ChargeResponse CancelCharge(string chargeId, CancelChargeRequest cancelRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Charges, chargeId, Constants.Methods.Cancel);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.DELETE, cancelRequest, headers);

            var result = CallAPI<ChargeResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Initiate a full or partial refund for a charge.
        /// </summary>
        /// <param name="refundRequest"></param>
        /// <param name="headers"></param>
        /// <returns>RefundResponse response</returns>
        public RefundResponse CreateRefund(CreateRefundRequest refundRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Refunds);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, refundRequest, headers);

            var result = CallAPI<RefundResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Get refund details.
        /// </summary>
        /// <param name="refundId"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public RefundResponse GetRefund(string refundId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Refunds, refundId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<RefundResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Generates the signature string for the Amazon Pay front-end button.
        /// </summary>
        /// <param name="jsonString">The payload for generating a CheckoutSession as JSON string.</param>
        /// <returns>Signature string that can be assigned to the front-end button's "signature" parameter.</returns>
        public string GenerateButtonSignature(string jsonString)
        {
            var signatureHelper = new SignatureHelper(payConfiguration, new CanonicalBuilder());
            var stringToSign = signatureHelper.CreateStringToSign(jsonString);
            var signature = signatureHelper.GenerateSignature(stringToSign);

            return signature;
        }

        /// <summary>
        /// Generates the signature string for the Amazon Pay front-end button.
        /// </summary>
        /// <param name="request">The payload for generating a CheckoutSession as CreateCheckoutSessionRequest object.</param>
        /// <returns>Signature string that can be assigned to the front-end button's "signature" parameter.</returns>
        public string GenerateButtonSignature(CreateCheckoutSessionRequest request)
        {
            var signature = GenerateButtonSignature(request.ToJson());

            return signature;
        }
    }
}
