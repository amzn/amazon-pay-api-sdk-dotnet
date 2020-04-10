using Newtonsoft.Json;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore.Types
{
    public class AddressRestrictions
    {
        public AddressRestrictions()
        {
            Restrictions = new Dictionary<string, Restriction>();
        }

        /// <summary>
        /// Specifies whether addresses that match restrictions configuration should or should not be restricted. Supported values: Allowed / NotAllowed.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public RestrictionType? Type { get; set; }

        /// <summary>
        /// Hash of country-level restrictions that determine which addresses should or should not be restricted.
        /// </summary>
        [JsonProperty(PropertyName = "restrictions")]
        public Dictionary<string, Restriction> Restrictions { get; internal set; }
    }
}
