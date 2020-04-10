using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// Payment flow for charging the buyer.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentIntent
    {
        Confirm,
        Authorize,
        AuthorizeWithCapture
    }
}