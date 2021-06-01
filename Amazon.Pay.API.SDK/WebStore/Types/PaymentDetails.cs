using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Amazon.Pay.API.WebStore.Types
{
    public class PaymentDetails
    {
        public PaymentDetails()
        {
            ChargeAmount = new Price();
            TotalOrderAmount = new Price();
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext content)
        {
            // skip 'chargeAmount' if there wasn't provided anything
            if (ChargeAmount != null && ChargeAmount.Amount == 0 && ChargeAmount.CurrencyCode == null)
            {
                ChargeAmount = null;
            }

            // skip 'totalOrderAmount' if there wasn't provided anything
            if (TotalOrderAmount != null && TotalOrderAmount.Amount == 0 && TotalOrderAmount.CurrencyCode == null)
            {
                TotalOrderAmount = null;
            }
        }

        [OnSerialized]
        internal void OnSerialized(StreamingContext content)
        {
            if (ChargeAmount == null)
            {
                ChargeAmount = new Price();
            }

            if (TotalOrderAmount == null)
            {
                TotalOrderAmount = new Price();
            }
        }

        /// <summary>
        /// Payment flow for charging the buyer.
        /// </summary>
        [JsonProperty(PropertyName = "paymentIntent")]
        public PaymentIntent? PaymentIntent { get; set; }

        /// <summary>
        /// Boolean that indicates whether merchant can handle pending response.
        /// </summary>
        [JsonProperty(PropertyName = "canHandlePendingAuthorization")]
        public bool? CanHandlePendingAuthorization { get; set; }

        /// <summary>
        /// Amount to be processed using paymentIntent during checkout.
        /// </summary>
        [JsonProperty(PropertyName = "chargeAmount")]
        public Price ChargeAmount { get; internal set; }

        /// <summary>
        /// Total order amount, only use this parameter if you need to process additional payments after checkout.
        /// </summary>
        [JsonProperty(PropertyName = "totalOrderAmount")]
        public Price TotalOrderAmount { get; internal set; }

        /// <summary>
        /// The currency that the buyer will be charged in ISO 4217 format. Example: USD.
        /// </summary>
        [JsonProperty(PropertyName = "presentmentCurrency")]
        public Currency? PresentmentCurrency { get; set; }

        /// <summary>
        /// Boolean that indicates whether merchant can charge the buyer beyond the specified order amount.
        /// </summary>
        [JsonProperty(PropertyName = "allowOvercharge")]
        public bool? AllowOvercharge { get; set; }

        /// <summary>
        /// Boolean that indicates whether to create a ChargePermission with an extended expiration
        /// period of 13 months as compared to the default expiration period of 180 days(6 months).
        /// </summary>
        [JsonProperty(PropertyName = "extendExpiration")]
        public bool? ExtendExpiration { get; set; }

        /// <summary>
        /// Description shown on the buyer payment instrument statement, if PaymentIntent is set to AuthorizeWithCapture.
        /// Do not use if PaymentIntent is set to Confirm or Authorize.
        /// </summary>
        [JsonProperty(PropertyName = "softDescriptor")]
        public string SoftDescriptor { get; set; }

        /// <summary>
        /// Estimate Order Total. TODO : Replace this with the version from the guide, once available
        /// </summary>
        [JsonProperty(PropertyName = "estimateOrderTotal")]
        public Price EstimateOrderTotal { get; set; }
    }
}
