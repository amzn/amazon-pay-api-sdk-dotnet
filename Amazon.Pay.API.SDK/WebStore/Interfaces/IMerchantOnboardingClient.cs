using System.Collections.Generic;
using Amazon.Pay.API.WebStore.AccountManagement;
using Amazon.Pay.API.WebStore.Types;

namespace Amazon.Pay.API.WebStore.Interfaces
{
    public interface IMerchantOnboardingClient : IClient
    {
        // ----------------------------------- Merchant Onboarding & Account Management APIs --------------------

        /// <summary>
        /// Creates a non-logginable account for your merchant partners. These would be special accounts through which Merchants would not be able to login to Amazon or access Seller Central.
        /// </summary>
        /// <param name="request">The payload for creating a amazon pay account.</param>
        /// <param name="headers"></param>
        /// <returns>RegisterAmazonPayAccountResponse response</returns>
        RegisterAmazonPayAccountResponse RegisterAmazonPayAccount(RegisterAmazonPayAccountRequest request, Dictionary<string, string> headers = null);

        /// <summary>
        /// Updates a merchant account for the given Merchant Account ID. We would be allowing our partners to update only a certain set of fields which won’t change the legal business entity itself.
        /// </summary>
        /// <param name="merchantAccountId">Internal Merchant ID for updating the Amazon Pay Account</param>
        /// <param name="request">The payload for updating amazon pay account.</param>
        /// <param name="headers"></param>
        UpdateAmazonPayAccountResponse UpdateAmazonPayAccount(string merchantAccountId, UpdateAmazonPayAccountRequest request, Dictionary<string, string> headers = null);

        /// <summary>
        /// Deletes the Merchant account for the given Merchant Account ID. Partners can close the merchant accounts created for their merchant partners.
        /// </summary>
        /// <param name="merchantAccountId">Internal Merchant ID for updating the Amazon Pay Account</param>
        /// <param name="headers"></param>
        DeleteAmazonPayAccountResponse DeleteAmazonPayAccount(string merchantAccountId, Dictionary<string, string> headers = null);
    }
}