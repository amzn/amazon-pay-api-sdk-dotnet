using System.Collections.Generic;
using Amazon.Pay.API.DeliveryTracker;
using Amazon.Pay.API.WebStore.Buyer;
using Amazon.Pay.API.WebStore.Charge;
using Amazon.Pay.API.WebStore.ChargePermission;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Refund;
using Amazon.Pay.API.WebStore.Types;

namespace Amazon.Pay.API.WebStore.Interfaces
{
    public interface IWebStoreClient : IClient
    {
        /// <summary>
        /// Creates a new Checkout Session.
        /// </summary>
        CheckoutSessionResponse CreateCheckoutSession(CreateCheckoutSessionRequest body, Dictionary<string, string> headers = null);

        /// <summary>
        /// Gets a Checkout Session.
        /// </summary>
        CheckoutSessionResponse GetCheckoutSession(string checkoutSessionId, Dictionary<string, string> headers = null);

        /// <summary>
        /// Updates a Checkout Session.
        /// </summary>
        CheckoutSessionResponse UpdateCheckoutSession(string checkoutSessionId, UpdateCheckoutSessionRequest body, Dictionary<string, string> headers = null);

        /// <summary>
        /// Completes a Checkout Session.
        /// </summary>
        CheckoutSessionResponse CompleteCheckoutSession(string checkoutSessionId, CompleteCheckoutSessionRequest completeRequest, Dictionary<string, string> headers = null);
        
        /// <summary>
        /// FinalizeCheckoutSession API which enables Pay to validate payment critical attributes and also update book-keeping attributes present in merchantMetadata
        /// </summary>
        CheckoutSessionResponse FinalizeCheckoutSession(string checkoutSessionId, FinalizeCheckoutSessionRequest finalizeCheckoutSessionRequest, Dictionary<string, string> headers = null);

        /// <summary>
        /// Helps the solution provider get details of a chargePermission
        /// </summary>
        /// <param name="chargePermissionId"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        ChargePermissionResponse GetChargePermission(string chargePermissionId, Dictionary<string, string> headers = null);

        /// <summary>
        /// Updates a Charge Permission.
        /// </summary>
        ChargePermissionResponse UpdateChargePermission(string chargePermissionId, UpdateChargePermissionRequest request, Dictionary<string, string> headers = null);

        /// <summary>
        /// Closes a Charge Permission.
        /// </summary>
        ChargePermissionResponse CloseChargePermission(string chargePermissionId, CloseChargePermissionRequest request, Dictionary<string, string> headers = null);

        /// <summary>
        /// Create a Charge to authorize payment.
        /// </summary>
        /// <returns>ChargeResponse response</returns>
        ChargeResponse CreateCharge(CreateChargeRequest createChargeRequest, Dictionary<string, string> headers = null);

        /// <summary>
        /// Capture payment on a Charge in the Authorized state.
        /// </summary>
        /// <returns>ChargeResponse response</returns>
        ChargeResponse CaptureCharge(string chargeId, CaptureChargeRequest captureRequest, Dictionary<string, string> headers = null);

        /// <summary>
        /// Gets a Charge object.
        /// </summary>
        /// <returns>ChargeResponse response</returns>
        ChargeResponse GetCharge(string chargeId, Dictionary<string, string> headers = null);

        /// <summary>
        /// Moves Charge to Canceled state and releases any authorized payments.
        /// </summary>
        /// <returns>AmazonPayResponse response</returns>
        /// <remarks>
        /// You can call this operation until Capture is initiated while Charge is in an AuthorizationInitiated or Authorized state.
        /// </remarks>
        ChargeResponse CancelCharge(string chargeId, CancelChargeRequest cancelRequest, Dictionary<string, string> headers = null);

        /// <summary>
        /// Initiate a full or partial refund for a charge.
        /// </summary>
        /// <param name="refundRequest"></param>
        /// <param name="headers"></param>
        /// <returns>RefundResponse response</returns>
        RefundResponse CreateRefund(CreateRefundRequest refundRequest, Dictionary<string, string> headers = null);

        /// <summary>
        /// Get refund details.
        /// </summary>
        /// <param name="refundId"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        RefundResponse GetRefund(string refundId, Dictionary<string, string> headers = null);

        /// <summary>
        /// Get buyer details.
        /// </summary>
        /// <param name="buyerToken">Token used to retrieve buyer details. This value is appended as a query parameter to signInReturnUrl.</param>
        /// <param name="headers"></param>
        /// <returns>Object with buyer details.</returns>
        /// <remarks>
        /// Get Buyer will only return buyerId by default. You must explicitly request access to additional buyer details using the button signInScopes parameter.
        /// Amazon Pay will only provide the token required to retrieve buyer details after the buyer signs in. It will be appended to the signInReturnUrl as a query parameter and expires after 24 hours.
        /// </remarks>
        BuyerResponse GetBuyer(string buyerToken, Dictionary<string, string> headers = null);

        /// <summary>
        /// Generates the signature string for the Amazon Pay front-end button.
        /// </summary>
        /// <param name="jsonString">The payload for generating a CheckoutSession as JSON string.</param>
        /// <returns>Signature string that can be assigned to the front-end button's "signature" parameter.</returns>
        string GenerateButtonSignature(string jsonString);

        /// <summary>
        /// Generates the signature string for the Amazon Pay Checkout button.
        /// </summary>
        /// <param name="request">The payload for generating a CheckoutSession as CreateCheckoutSessionRequest object.</param>
        /// <returns>Signature string that can be assigned to the front-end button's "signature" parameter.</returns>
        string GenerateButtonSignature(CreateCheckoutSessionRequest request);

        /// <summary>
        /// Generates the signature string for the Amazon Pay Login button.
        /// </summary>
        /// <param name="request">The payload for generating a SignIn Request.</param>
        /// <returns>Signature string that can be assigned to the front-end button's "signature" parameter.</returns>
        string GenerateButtonSignature(SignInRequest request);

    }
}