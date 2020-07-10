using Amazon.Pay.API.Converters;
using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class Price
    {
        internal Price()
        {

        }

        public Price(decimal amount, Currency currencyCode)
        {
            Amount = amount;
            CurrencyCode = currencyCode;
        }

        /// <summary>
        /// Transaction amount.
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal Amount { get; set; }

        /// <summary>
        /// Transaction currency code in ISO 4217 format. Example: USD.
        /// </summary>
        [JsonProperty(PropertyName = "currencyCode")]
        public Currency? CurrencyCode { get; set; }
    }
}
