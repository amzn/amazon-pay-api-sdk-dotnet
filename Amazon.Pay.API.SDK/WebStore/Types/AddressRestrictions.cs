using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore.Types
{
    // TODO: replace property Type with a boolean property "IsAllowed"

    /// <summary>
    /// Country-based address restrictions.
    /// </summary>
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

        public InnerCountryAddressRestriction AddCountryRestriction(string countryCode)
        {
            Restrictions.Add(countryCode, new Restriction());

            return new InnerCountryAddressRestriction(countryCode, this);
        }
    }
}
