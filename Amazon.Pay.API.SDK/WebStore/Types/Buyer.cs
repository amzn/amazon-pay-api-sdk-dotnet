using System.Collections.Generic;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class Buyer
    {
        /// <summary>
        /// Unique Amazon Pay buyer identifier.
        /// </summary>
        [JsonProperty(PropertyName = "buyerId")]
        public string BuyerId { get; internal set; }

        /// <summary>
        /// Buyer name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; internal set; }

        /// <summary>
        /// Buyer email address.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; internal set; }

        /// <summary>
        /// Buyer Phone Number.
        /// </summary>
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; internal set; }

        /// <summary>
        /// Buyer PrimeMembershipTypes.
        /// </summary>
        [JsonProperty(PropertyName = "primeMembershipTypes")]
        public IList<string> PrimeMembershipTypes { get; internal set; }
    }
}
