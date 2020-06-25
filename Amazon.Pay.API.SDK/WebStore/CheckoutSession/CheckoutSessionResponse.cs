using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore.CheckoutSession
{
    public class CheckoutSessionResponse : AmazonPayResponse
    {
        public CheckoutSessionResponse()
        {
            WebCheckoutDetails = new WebCheckoutDetails();
            PaymentDetails = new PaymentDetails();
            MerchantMetadata = new MerchantMetadata();
            Buyer = new Types.Buyer();
            ProviderMetadata = new ProviderMetadata();
            ShippingAddress = new Address();
            BillingAddress = new Address();
            PaymentPreferences = new List<PaymentPreferences>();
            Constraints = new List<Constraint>();
        }

        /// <summary>
        /// Login with Amazon client ID. Do not use the application ID.
        /// </summary>
        [JsonProperty(PropertyName = "storeId")]
        public string StoreId { get; set; }

        /// <summary>
        /// Checkout Session identifer.
        /// </summary>
        [JsonProperty(PropertyName = "checkoutSessionId")]
        public string CheckoutSessionId { get; internal set; }

        /// <summary>
        /// URLs associated to the Checkout Session used for completing checkout
        /// </summary>
        [JsonProperty(PropertyName = "webCheckoutDetails")]
        public WebCheckoutDetails WebCheckoutDetails { get; internal set; }

        /// <summary>
        /// Amazon Pay integration type.
        /// </summary>
        [JsonProperty(PropertyName = "productType")]
        public string ProductType { get; internal set; }

        /// <summary>
        /// Payment details specified by the merchant, such as the amount and method for charging the buyer
        /// </summary>
        [JsonProperty(PropertyName = "paymentDetails")]
        public PaymentDetails PaymentDetails { get; internal set; }

        /// <summary>
        /// Merchant-provided order info.
        /// </summary>
        [JsonProperty(PropertyName = "merchantMetadata")]
        public MerchantMetadata MerchantMetadata { get; internal set; }

        /// <summary>
        /// Details about the buyer, such as their unique identifer, name, and email.
        /// </summary>
        [JsonProperty(PropertyName = "buyer")]
        public Types.Buyer Buyer { get; internal set; }

        /// <summary>
        /// State of the Checkout Session object.
        /// </summary>
        [JsonProperty(PropertyName = "statusDetails")]
        public StatusDetails StatusDetails { get; internal set; }

        /// <summary>
        /// Supplementary data.
        /// </summary>
        [JsonProperty(PropertyName = "supplementaryData")]
        public string SupplementaryData { get; set; }

        /// <summary>
        /// Merchant identifer of the Solution Provider (SP) - also known as ecommerce provider.
        /// </summary>
        [JsonProperty(PropertyName = "platformId")]
        public string PlatformId { get; set; }

        /// <summary>
        /// Payment service provider (PSP)-provided order information.
        /// </summary>
        [JsonProperty(PropertyName = "providerMetadata")]
        public ProviderMetadata ProviderMetadata { get; internal set; }

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
        /// Constraints that must be addressed to complete Amazon Pay checkout.
        /// </summary>
        [JsonProperty(PropertyName = "constraints")]
        public List<Constraint> Constraints { get; internal set; }

        /// <summary>
        /// Universal Time Coordinated (UTC) date and time when the Checkout Session was created in ISO 8601 format.
        /// </summary>
        [JsonProperty(PropertyName = "creationTimestamp")]
        public DateTime CreationTimestamp { get; internal set; }

        /// <summary>
        /// Charge permission identifier returned after Checkout Session is complete.
        /// </summary>
        [JsonProperty(PropertyName = "chargePermissionId")]
        public string ChargePermissionId { get; internal set; }

        /// <summary>
        /// Charge identifier returned after Checkout Session is complete.
        /// </summary>
        [JsonProperty(PropertyName = "chargeId")]
        public string ChargeId { get; internal set; }

        /// <summary>
        /// Specify shipping restrictions to prevent buyers from selecting unsupported addresses from their Amazon address book.
        /// </summary>
        [JsonProperty(PropertyName = "deliverySpecifications")]
        public DeliverySpecifications DeliverySpecifications { get; internal set; }

        /// <summary>
        /// The environment of the Amazon Pay API (live or sandbox).
        /// </summary>
        [JsonProperty(PropertyName = "releaseEnvironment")]
        public string ReleaseEnvironment { get; internal set; }
    }
}
