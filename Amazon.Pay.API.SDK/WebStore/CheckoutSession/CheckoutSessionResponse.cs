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
            RecurringMetadata = new RecurringMetadata();
            ShippingAddressList = new List<AddressWithId>();
            MultiAddressesCheckoutMetadataList = new List<MultiAddressesCheckoutMetadata>();
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
        /// List of shipping addresses selected by the buyer in PayAndShipMultiAddress productType.
        /// </summary>
        [JsonProperty(PropertyName = "shippingAddressList")]
        public List<AddressWithId> ShippingAddressList { get; internal set; }

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
        /// UTC date and time when the Charge will expire in ISO 8601 format.
        /// </summary>
        /// <remarks>The Charge Permission will expire 180 days after it's confirmed</remarks>
        [JsonProperty(PropertyName = "expirationTimestamp")]
        public DateTime? ExpirationTimestamp { get; internal set; }

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

        /// <summary>
        /// Configure OneTime or Recurring payment chargePermissionType
        /// </summary>
        [JsonProperty(PropertyName = "chargePermissionType")]
        public ChargePermissionType? ChargePermissionType { get; internal set; }

        /// <summary>
        /// Metadata about how the recurring Charge Permission will be used. Amazon Pay only uses this information to calculate the Charge Permission expiration date and in buyer communication.
        /// </summary>
        [JsonProperty(PropertyName = "recurringMetadata")]
        public RecurringMetadata RecurringMetadata { get; internal set; }

        /// <summary>
        /// Metadata about how payment method on file charge permission will be used. Amazon Pay will use it to get additional information related to payment method on file.
        /// </summary>
        [JsonProperty(PropertyName = "paymentMethodOnFileMetadata")]
        public PaymentMethodOnFileMetadata PaymentMethodOnFileMetadata { get; internal set; }

        /// <summary>
        /// Metadata about the the addresses and the amounts selected by buyer on Merchant Review page in PayAndShipMultiAddress productType.
        /// </summary>
        [JsonProperty(PropertyName = "multiAddressesCheckoutMetadata")]
        public List<MultiAddressesCheckoutMetadata> MultiAddressesCheckoutMetadataList { get; internal set; }

        /// <summary>
        /// The text a merchant can use for checkout Button after redirecting to checkout page.
        /// </summary>
        [JsonProperty(PropertyName = "checkoutButtonText")]
        public string CheckoutButtonText { get; internal set; }
    }
}
