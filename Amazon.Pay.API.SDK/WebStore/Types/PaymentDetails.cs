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
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext content)
        {
            // skip 'chargeAmount' if there wasn't provided anything
            if (ChargeAmount.Amount == 0 && ChargeAmount.CurrencyCode == null)
            {
                ChargeAmount = null;
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
        public bool CanHandlePendingAuthorization { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        [JsonProperty(PropertyName = "chargeAmount")]
        public Price ChargeAmount { get; internal set; }

        /// <summary>
        /// The currency that the buyer will be charged in ISO 4217 format. Example: USD.
        /// </summary>
        [JsonProperty(PropertyName = "presentmentCurrency")]
        public Currency? PresentmentCurrency { get; set; }
    }
}
