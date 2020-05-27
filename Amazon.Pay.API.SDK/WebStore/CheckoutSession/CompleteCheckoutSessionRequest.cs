using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.CheckoutSession
{
    public class CompleteCheckoutSessionRequest : ApiRequestBody
    {
        public CompleteCheckoutSessionRequest(decimal amount, Currency currency)
        {
            ChargeAmount = new Price(amount, currency);
        }

        /// <summary>
        /// The total transaction amount that this CheckoutSession is associated with.
        /// </summary>
        [JsonProperty(PropertyName = "chargeAmount")]
        public Price ChargeAmount { get; internal set; }
    }
}
