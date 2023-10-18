using Newtonsoft.Json;
using Amazon.Pay.API.Types;

namespace Amazon.Pay.API.WebStore.AccountManagement
{
    public class RegisterAmazonPayAccountResponse : AmazonPayResponse
    {
        /// <summary>
        /// Gets or sets the unique reference id.
        /// </summary>
        [JsonProperty(PropertyName = "uniqueReferenceId")]
        public string UniqueReferenceId { get; internal set; }

        /// <summary>
        /// Gets or sets the merchant account id.
        /// </summary>
        [JsonProperty(PropertyName = "merchantAccountId")]
        public string MerchantAccountId { get; internal set; }
    }
}