using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Amazon.Pay.API.WebStore.Types
{
    public class Person
    {
        public Person()
        {
            ResidentialAddress = new AddressInfo();
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext content)
        {
            // skip 'ResidentialAddress' if there wasn't provided anything
            if (ResidentialAddress?.AddressLine1 == null && ResidentialAddress?.City == null && ResidentialAddress?.StateOrRegion == null && ResidentialAddress?.PostalCode == null && ResidentialAddress?.CountryCode == null)
            {
                ResidentialAddress = null;
            }
        }

        [OnSerialized]
        internal void OnSerialized(StreamingContext content)
        {
            if (ResidentialAddress == null)
            {
                ResidentialAddress = new AddressInfo();
            }
        }

        /// <summary>
        /// Gets or sets the person full name.
        /// </summary>
        [JsonProperty(PropertyName = "personFullName")]
        public string PersonFullName { get; set; }

        /// <summary>
        /// Gets or sets the residential address.
        /// </summary>
        [JsonProperty(PropertyName = "residentialAddress")]
        public AddressInfo ResidentialAddress { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        [JsonProperty(PropertyName = "dateOfBirth")]
        public string DateOfBirth { get; set; }
    }
}