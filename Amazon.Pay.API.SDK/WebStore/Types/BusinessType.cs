using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// Type of Business.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BusinessType
    {
        /// <summary>
        /// Corporate Entity
        /// </summary>
        CORPORATE,

        /// <summary>
        /// Indiviual Entity/ Sole Propeitor
        /// </summary>
        INDIVIDUAL
    }
}