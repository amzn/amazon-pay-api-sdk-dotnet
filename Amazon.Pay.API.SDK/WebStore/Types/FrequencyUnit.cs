using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// Frequency Unit for Recurring Charge Permissions
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FrequencyUnit
    {
        Year,
        Month,
        Week,
        Day,
        Variable
    }
}
