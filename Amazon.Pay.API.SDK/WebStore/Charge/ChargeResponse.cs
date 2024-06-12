using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.WebStore.Charge
{
    public class ChargeResponse : AmazonPayResponse
    {
        public ChargeResponse()
        {
            WebCheckoutDetails = new WebCheckoutDetails();
        }

        /// <summary>
        /// Charge identifer.
        /// </summary>
        [JsonProperty(PropertyName = "chargeId")]
        public string ChargeId { get; internal set; }

        /// <summary>
        /// Charge Permission identifer.
        /// </summary>
        [JsonProperty(PropertyName = "chargePermissionId")]
        public string ChargePermissionId { get; internal set; }

        /// <summary>
        /// Represents the amount to be charged/authorized.
        /// </summary>
        [JsonProperty(PropertyName = "chargeAmount")]
        public Price ChargeAmount { get; internal set; }

        /// <summary>
        /// The total amount that has been captured using this Charge.
        /// </summary>
        [JsonProperty(PropertyName = "captureAmount")]
        public Price CaptureAmount { get; internal set; }

        /// <summary>
        /// The total amount that has been refunded using this Charge.
        /// </summary>
        [JsonProperty(PropertyName = "refundedAmount")]
        public Price RefundedAmount { get; internal set; }

        /// <summary>
        /// Description shown on the buyer payment instrument statement, if CaptureNow is set to true. Do not set this value if CaptureNow is set to false.
        /// </summary>
        [JsonProperty(PropertyName = "softDescriptor")]
        public string SoftDescriptor { get; internal set; }

        /// <summary>
        /// Platform ID for each Charge to be set by Solution Providers
        /// </summary>
        [JsonProperty(PropertyName = "platformId")]
        public string PlatformId { get; internal set; }

        /// <summary>
        /// Boolean that indicates whether or not Charge should be captured immediately after a successful authorization.
        /// </summary>
        [JsonProperty(PropertyName = "captureNow")]
        public bool? CaptureNow { get; internal set; }

        /// <summary>
        /// Boolean that indicates whether merchant can handle pending response.
        /// </summary>
        [JsonProperty(PropertyName = "canHandlePendingAuthorization")]
        public bool? CanHandlePendingAuthorization { get; internal set; }

        /// <summary>
        /// Payment service provider (PSP)-provided order details.
        /// </summary>
        [JsonProperty(PropertyName = "providerMetadata")]
        public ProviderMetadata ProviderMetadata { get; internal set; }

        /// <summary>
        /// Universal Time Coordinated (UTC) date and time when the Charge was created in ISO 8601 format.
        /// </summary>
        [JsonProperty(PropertyName = "creationTimestamp")]
        public DateTime CreationTimestamp { get; internal set; }

        /// <summary>
        /// UTC date and time when the Charge will expire in ISO 8601 format.
        /// </summary>
        /// <remarks>The Charge Permission will expire 180 days after it's confirmed</remarks>
        [JsonProperty(PropertyName = "expirationTimestamp")]
        public DateTime ExpirationTimestamp { get; internal set; }

        /// <summary>
        /// State of the Charge object.
        /// </summary>
        [JsonProperty(PropertyName = "statusDetails")]
        public StatusDetails StatusDetails { get; internal set; }

        /// <summary>
        /// The amount captured in disbursement currency. This is calculated using: chargeAmount/conversionRate.
        /// </summary>
        [JsonProperty(PropertyName = "convertedAmount")]
        public Price ConvertedAmount { get; internal set; }

        /// <summary>
        /// The rate used to calculate convertedAmount.
        /// </summary>
        [JsonProperty(PropertyName = "conversionRate")]
        public decimal? ConversionRate { get; internal set; }

        /// <summary>
        /// The environment of the Amazon Pay API (live or sandbox).
        /// </summary>
        [JsonProperty(PropertyName = "releaseEnvironment")]
        public string ReleaseEnvironment { get; internal set; }

        /// <summary>
        /// Merchant-provided order info.
        /// </summary>
        [JsonProperty(PropertyName = "merchantMetadata")]
        public MerchantMetadata MerchantMetadata { get; internal set; }

        /// <summary>
        /// URLs associated to the Checkout Session used for completing checkout
        /// </summary>
        [JsonProperty(PropertyName = "webCheckoutDetails")]
        public WebCheckoutDetails WebCheckoutDetails { get; internal set; }
    }
}
