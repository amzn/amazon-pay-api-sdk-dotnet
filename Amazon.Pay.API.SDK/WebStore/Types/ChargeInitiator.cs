using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// Represents who initiated the payment.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChargeInitiator
    {
        CITU,
        MITU,
        CITR,
        MITR
    }
}
