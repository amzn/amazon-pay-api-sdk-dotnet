using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Amazon.Pay.API.WebStore.Types
{

    /// <summary>
    /// Frequency at which the buyer will be charged using a recurring Charge Permission.
    /// </summary>
    public class Frequency
    {
        /// <summary>
        /// Frequency unit for each billing cycle. Supported values: 'Year', 'Month', 'Week', 'Day'.
        /// </summary>
        [JsonProperty(PropertyName = "unit")]
        public FrequencyUnit? Unit { get; set; }

        /// <summary>
        /// Number of frequency units per billing cycle. For example, to specify a weekly cycle set unit to Week and value to 1.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public int? Value { get; set; }
    }
}
