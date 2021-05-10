using System.Collections.Generic;
using Amazon.Pay.API.AuthorizationToken;
using Amazon.Pay.API.DeliveryTracker;

namespace Amazon.Pay.API
{
    public interface IClient
    {
        /// <summary>
        /// Sends delivery tracking information that will trigger a Alexa Delivery Notification when the item is shipped or about to be delivered.
        /// </summary>
        /// <returns>DeliveryTrackerResponse response</returns>
        DeliveryTrackerResponse SendDeliveryTrackingInformation(DeliveryTrackerRequest deliveryTrackersRequest, Dictionary<string, string> headers = null);

        
        /// <summary>
        /// Retrieves a delegated authorization token used in order to make API calls on behalf of a merchant.
        /// </summary>
        /// <param name="mwsAuthToken">The MWS Auth Token that the solution provider currently uses to make V1 API calls on behalf of the merchant.</param>
        /// <param name="merchantId">The Amazon Pay merchant ID.</param>
        /// <returns>HS256 encoded JWT Token that will be used to make V2 API calls on behalf of the merchant.</returns>
        AuthorizationTokenResponse GetAuthorizationToken(string mwsAuthToken, string merchantId, Dictionary<string, string> headers = null);
    }
}