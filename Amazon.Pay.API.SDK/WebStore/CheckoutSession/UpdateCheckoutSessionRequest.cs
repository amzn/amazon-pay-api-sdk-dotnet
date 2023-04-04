using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Amazon.Pay.API.WebStore.CheckoutSession
{
    public class UpdateCheckoutSessionRequest : ApiRequestBody
    {

        public UpdateCheckoutSessionRequest()
        {
            WebCheckoutDetails = new WebCheckoutDetails();
            PaymentDetails = new PaymentDetails();
            MerchantMetadata = new MerchantMetadata();
            ProviderMetadata = new ProviderMetadata();
            RecurringMetadata = new RecurringMetadata();
            MultiAddressesCheckoutMetadataList = new List<MultiAddressesCheckoutMetadata>();
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext content)
        {
            // skip 'RecurringMetadata' if there wasn't provided anything
            if (RecurringMetadata != null && RecurringMetadata.Frequency == null && RecurringMetadata.Amount == null)
            {
                RecurringMetadata = null;
            }

            // skip 'multiAddressesCheckoutMetadata' if there weren't any provided
            if (MultiAddressesCheckoutMetadataList != null && MultiAddressesCheckoutMetadataList.Count == 0)
            {
                MultiAddressesCheckoutMetadataList = null;
            }
        }

        [OnSerialized]
        internal void OnSerialized(StreamingContext content)
        {
            if (RecurringMetadata == null)
            {
                RecurringMetadata = new RecurringMetadata();
            }

            if (MultiAddressesCheckoutMetadataList == null)
            {
                MultiAddressesCheckoutMetadataList = new List<MultiAddressesCheckoutMetadata>();
            }
        }

        /// <summary>
        /// URLs associated to the Checkout Session used for completing checkout
        /// </summary>
        [JsonProperty(PropertyName = "webCheckoutDetails")]
        public WebCheckoutDetails WebCheckoutDetails { get; internal set; }

        /// <summary>
        /// Payment details specified by the merchant, such as the amount and method for charging the buyer
        /// </summary>
        [JsonProperty(PropertyName = "paymentDetails")]
        public PaymentDetails PaymentDetails { get; set; }

        /// <summary>
        /// Merchant-provided order info.
        /// </summary>
        [JsonProperty(PropertyName = "merchantMetadata")]
        public MerchantMetadata MerchantMetadata { get; internal set; }

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
        /// Configure OneTime, Recurring or PaymentMethodOnFile payment chargePermissionType
        /// </summary>
        [JsonProperty(PropertyName = "chargePermissionType")]
        public ChargePermissionType? ChargePermissionType { get; set; }

        /// <summary>
        /// Metadata about how the recurring Charge Permission will be used. Amazon Pay only uses this information to calculate the Charge Permission expiration date and in buyer communication,
        /// </summary>
        [JsonProperty(PropertyName = "recurringMetadata")]
        public RecurringMetadata RecurringMetadata { get; internal set; }

        /// <summary>
        /// Metadata about the the addresses and the amounts selected by buyer on Merchant Review page in PayAndShipMultiAddress productType.
        /// </summary>
        [JsonProperty(PropertyName = "multiAddressesCheckoutMetadata")]
        public List<MultiAddressesCheckoutMetadata> MultiAddressesCheckoutMetadataList { get; set; }

    }
}
