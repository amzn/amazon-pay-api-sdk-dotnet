using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore.ChargePermission
{
    public class ChargePermissionResponse : AmazonPayResponse
    {
        /// <summary>
        /// Charge Permission identifer.
        /// </summary>
        [JsonProperty(PropertyName = "chargePermissionId")]
        public string ChargePermissionId { get; internal set; }

        /// <summary>
        /// Limits that apply to this ChargePermission.
        /// </summary>
        [JsonProperty(PropertyName = "limits")]
        public Limits Limits { get; internal set; }

        /// <summary>
        /// The environment of the Amazon Pay API (live or sandbox).
        /// </summary>
        [JsonProperty(PropertyName = "releaseEnvironment")]
        public string ReleaseEnvironment { get; internal set; }

        /// <summary>
        /// Details about the buyer, such as their unique identifer, name, and email.
        /// </summary>
        [JsonProperty(PropertyName = "buyer")]
        public Types.Buyer Buyer { get; internal set; }

        /// <summary>
        /// Shipping address selected by the buyer.
        /// </summary>
        [JsonProperty(PropertyName = "shippingAddress")]
        public Address ShippingAddress { get; internal set; }

        /// <summary>
        /// Billing address for buyer-selected payment instrument. Billing address is only available in EU or for PayOnly product type.
        /// </summary>
        [JsonProperty(PropertyName = "billingAddress")]
        public Address BillingAddress { get; internal set; }

        /// <summary>
        /// List of payment instruments selected by the buyer.
        /// </summary>
        [JsonProperty(PropertyName = "paymentPreferences")]
        public List<PaymentPreferences> PaymentPreferences { get; internal set; }

        /// <summary>
        /// Merchant-provided order info.
        /// </summary>
        [JsonProperty(PropertyName = "merchantMetadata")]
        public MerchantMetadata MerchantMetadata { get; internal set; }

        /// <summary>
        /// Merchant identifer of the Solution Provider (SP) - also known as ecommerce provider.
        /// </summary>
        [JsonProperty(PropertyName = "platformId")]
        public string PlatformId { get; internal set; }

        /// <summary>
        /// Universal Time Coordinated (UTC) date and time when the Charge Permission was created in ISO 8601 format.
        /// </summary>
        [JsonProperty(PropertyName = "creationTimestamp")]
        public DateTime CreationTimestamp { get; internal set; }

        /// <summary>
        /// UTC date and time when the Charge Permission will expire in ISO 8601 format.
        /// </summary>
        /// <remarks>The Charge Permission will expire 180 days after it's confirmed</remarks>
        [JsonProperty(PropertyName = "expirationTimestamp")]
        public DateTime? ExpirationTimestamp { get; internal set; }

        /// <summary>
        /// State of the Charge Permission object.
        /// </summary>
        [JsonProperty(PropertyName = "statusDetails")]
        public ChargePermissionStatusDetails StatusDetails { get; internal set; }

        /// <summary>
        /// The currency that the buyer will be charged in ISO 4217 format.
        /// </summary>
        [JsonProperty(PropertyName = "presentmentCurrency")]
        public Currency? PresentmentCurrency { get; internal set; }

        /// <summary>
        /// The type of Charge Permission requested.
        /// </summary>
        [JsonProperty(PropertyName = "chargePermissionType")]
        public ChargePermissionType? ChargePermissionType { get; internal set; }

        /// <summary>
        /// Metadata about how the recurring Charge Permission will be used.
        /// </summary>
        [JsonProperty(PropertyName = "recurringMetadata")]
        public RecurringMetadata RecurringMetadata { get; internal set; }
    }
}
