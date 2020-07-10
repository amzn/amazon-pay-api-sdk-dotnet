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
            if (SpecialRestrictions != null && SpecialRestrictions.Count == 0)
            {
                SpecialRestrictions = null;
            }

            // skip 'addressRestrictions' if there weren't any provided
            if (AddressRestrictions != null && AddressRestrictions.Restrictions != null && AddressRestrictions.Restrictions.Count == 0)
            {
                AddressRestrictions = null;
            }
        }

        [OnSerialized]
        internal void OnSerialized(StreamingContext content)
        {
            if (SpecialRestrictions == null)
            {
                SpecialRestrictions = new List<SpecialRestriction>();
            }

            if (AddressRestrictions == null)
            {
                AddressRestrictions = new AddressRestrictions();
            }
        }

        /// <summary>
        /// Special restrictions, for example to prohibit buyers from selecting PO boxes.
        /// </summary>
        [JsonProperty(PropertyName = "specialRestrictions")]
        public List<SpecialRestriction> SpecialRestrictions { get; internal set; }

        /// <summary>
        /// Country-based address restrictions.
        /// </summary>
        [JsonProperty(PropertyName = "addressRestrictions")]
        public AddressRestrictions AddressRestrictions { get; internal set; }
    }
}
