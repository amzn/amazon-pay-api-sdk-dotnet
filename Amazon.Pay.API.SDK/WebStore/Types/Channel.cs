using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// Channel of transaction.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Channel
    {
        Web,
        Phone,
        App,
        Alexa,
        PointOfSale,
        FireTv,
        Offline,
        Amazon
    }
}
