using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class Constraint
    {
        /// <summary>
        /// Code for any Checkout Session constraint(s).
        /// </summary>
        [JsonProperty(PropertyName = "constraintId")]
        public string ConstraintId { get; internal set; }

        /// <summary>
        /// Description of the Checkout Session constraint(s).
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; internal set; }
    }
}
