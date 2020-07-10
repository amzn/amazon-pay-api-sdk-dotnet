using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// The type of Charge Permission requested.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChargePermissionType
    {
        OneTime,
        Recurring
    }
}
