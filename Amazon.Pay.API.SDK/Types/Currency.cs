using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.Types
{
    /// <summary>
    /// Currencies available for Amazon Pay.
    /// </summary>
    /// <remarks>
    /// Specifying a currenty other than the ledger currenc is only supported in the EU region.
    /// </remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        /// <summary>
        /// Australian Dollar
        /// </summary>
        AUD,

        /// <summary>
        /// British Pound
        /// </summary>
        GBP,

        /// <summary>
        /// Danish Krone
        /// </summary>
        DKK,

        /// <summary>
        /// Euro
        /// </summary>
        EUR,

        /// <summary>
        /// Hong Kong Dollar
        /// </summary>
        HKD,

        /// <summary>
        /// Japanese Yen
        /// </summary>
        JPY,

        /// <summary>
        /// New Zealand Dollar
        /// </summary>
        NZD,

        /// <summary>
        /// Norwegian Krone
        /// </summary>
        NOK,

        /// <summary>
        /// South African Rand
        /// </summary>
        ZAR,

        /// <summary>
        /// Swedish Krone
        /// </summary>
        SEK,

        /// <summary>
        /// Swiss Franc
        /// </summary>
        CHF,

        /// <summary>
        /// United States Dollar
        /// </summary>
        USD
    }
}
