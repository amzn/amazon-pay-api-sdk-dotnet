using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Amazon.Pay.API.WebStore.Types
{
    public class DeliverySpecifications
    {
        public DeliverySpecifications()
        {
            SpecialRestrictions = new List<SpecialRestriction>();
            AddressRestrictions = new AddressRestrictions();
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext content)
        {
            // skip 'specialRestrictions' if there weren't any provided
            if (SpecialRestrictions.Count == 0)
            {
                SpecialRestrictions = null;
            }

            // skip 'addressRestrictions' if there weren't any provided
            if (AddressRestrictions.Restrictions.Count == 0)
            {
                AddressRestrictions = null;
            }
        }

        /// <summary>
        /// Rule-based restrictions.
        /// </summary>
        [JsonProperty(PropertyName = "specialRestrictions")]
        public List<SpecialRestriction> SpecialRestrictions { get; internal set; }

        /// <summary>
        /// Country-based restrictions.
        /// </summary>
        [JsonProperty(PropertyName = "addressRestrictions")]
        public AddressRestrictions AddressRestrictions { get; internal set; }
    }
}
