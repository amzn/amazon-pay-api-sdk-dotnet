using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// Ledger Currencies available for Amazon Pay.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LedgerCurrency
    {
        /// <summary>
        /// British Pound
        /// </summary>
        GBP,

        /// <summary>
        /// Euro
        /// </summary>
        EUR,

        /// <summary>
        /// Japanese Yen
        /// </summary>
        JPY,

        /// <summary>
        /// United States Dollar
        /// </summary>
        USD
    }
}