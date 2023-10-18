using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Amazon.Pay.API.WebStore.CheckoutSession
{
    public class FinalizeCheckoutSessionRequest : ApiRequestBody
    {
        /// <summary>
        /// Initializes a new instance of the FinalizeCheckoutSessionRequest class with specified amount and currency.
        /// </summary>
        public FinalizeCheckoutSessionRequest(decimal amount, Currency currency, PaymentIntent paymentIntent)
        {
            ShippingAddress = new Address();
            BillingAddress = new Address();
            ChargeAmount = new Price(amount, currency);
            TotalOrderAmount = new Price();
            PaymentIntent = paymentIntent;
        }
        
        /// <summary>
        /// Initializes a new instance of the FinalizeCheckoutSessionRequest class.
        /// </summary>
        public FinalizeCheckoutSessionRequest(PaymentIntent paymentIntent)
        {
            ShippingAddress = new Address();
            BillingAddress = new Address();
            ChargeAmount = new Price();
            TotalOrderAmount = new Price();
            PaymentIntent = paymentIntent;
        }
        
        [OnSerializing]
        internal void OnSerializing(StreamingContext content)
        {
            // skip 'TotalOrderAmount' if there wasn't provided anything
            if (TotalOrderAmount != null && TotalOrderAmount.Amount == 0 && TotalOrderAmount.CurrencyCode == null)
            {
                TotalOrderAmount = null;
            }
        }

        [OnSerialized]
        internal void OnSerialized(StreamingContext content)
        {
            if (TotalOrderAmount == null)
            {
                TotalOrderAmount = new Price();
            }
        }

        /// <summary>
        /// Gets or sets the payment intent.
        /// </summary>
        [JsonProperty(PropertyName = "paymentIntent")]
        public PaymentIntent? PaymentIntent { get; set; }
        
        /// <summary>
        /// Gets or sets can handle pending authorization.
        /// </summary>
        [JsonProperty(PropertyName = "canHandlePendingAuthorization")]
        public bool? CanHandlePendingAuthorization { get; set; }
        
        /// <summary>
        /// Gets or sets supplementary data.
        /// </summary>
        [JsonProperty(PropertyName = "supplementaryData")]
        public string SupplementaryData { get; set; }
        
        /// <summary>
        /// Gets or sets shipping address.
        /// </summary>
        [JsonProperty(PropertyName = "shippingAddress")]
        public Address ShippingAddress { get; internal set; }

        /// <summary>
        /// Gets or sets billing address.
        /// </summary>
        [JsonProperty(PropertyName = "billingAddress")]
        public Address BillingAddress { get; internal set; }
        
        /// <summary>
        /// Gets or sets charge amount.
        /// </summary>
        [JsonProperty(PropertyName = "chargeAmount")]
        public Price ChargeAmount { get; internal set; }

        /// <summary>
        /// Gets or sets total order amount.
        /// </summary>
        [JsonProperty(PropertyName = "totalOrderAmount")]
        public Price TotalOrderAmount { get; internal set; }
    }
}