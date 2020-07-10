using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.CheckoutSession
{
    public class CompleteCheckoutSessionRequest : ApiRequestBody
    {
        public CompleteCheckoutSessionRequest(decimal amount, Currency currency)
        {
            ChargeAmount = new Price(amount, currency);
            TotalOrderAmount = new Price();
        }

        public CompleteCheckoutSessionRequest()
        {
            ChargeAmount = new Price();
            TotalOrderAmount = new Price();
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext content)
        {
            // skip 'TotalOrderAmount' if there wasn't provided anything
            if (TotalOrderAmount != null && TotalOrderAmount.Amount == 0 && TotalOrderAmount.CurrencyCode == null)
            {
                TotalOrderAmount = null;
            }

            // skip 'ChargeAmount' if there wasn't provided anything
            if (ChargeAmount != null && ChargeAmount.Amount == 0 && ChargeAmount.CurrencyCode == null)
            {
                ChargeAmount = null;
            }
        }

        [OnSerialized]
        internal void OnSerialized(StreamingContext content)
        {
            if (TotalOrderAmount == null)
            {
                TotalOrderAmount = new Price();
            }

            if (ChargeAmount == null)
            {
                ChargeAmount = new Price();
            }
        }

        /// <summary>
        /// The total transaction amount that this CheckoutSession is associated with.
        /// </summary>
        [JsonProperty(PropertyName = "chargeAmount")]
        public Price ChargeAmount { get; internal set; }

        /// <summary>
        /// Total order amount, only use this parameter if you need to process additional payments after checkout.
        /// </summary>
        [JsonProperty(PropertyName = "totalOrderAmount")]
        public Price TotalOrderAmount { get; internal set; }
    }
}
