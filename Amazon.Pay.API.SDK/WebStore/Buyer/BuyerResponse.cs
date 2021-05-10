using System;
using Amazon.Pay.API.Types;
using System.Collections.Generic;
using System.Linq;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Buyer
{
    public class BuyerResponse : AmazonPayResponse
    {
        /// <summary>
        /// Unique Amazon Pay buyer identifier.
        /// </summary>
        [JsonProperty(PropertyName = "buyerId")]
        public string BuyerId { get; internal set; }

        /// <summary>
        /// Buyer default shipping address country.
        /// </summary>
        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; internal set; }

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
        /// Buyer default shipping address postal code.
        /// </summary>
        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; internal set; }

        /// <summary>
        /// Shipping address selected by the buyer.
        /// </summary>
        [JsonProperty(PropertyName = "shippingAddress")]
        public Address ShippingAddress { get; internal set; }

        /// <summary>
        ///  Billing address for buyer-selected payment instrument.
        /// </summary>
        [JsonProperty(PropertyName = "billingAddress")]
        public Address BillingAddress { get; internal set; }

        /// <summary>
        ///  PrimeMembershipTypes of the buyer, available by allow list only.  If merchant account is not part of allow list, the value will be null.  Empty list is returned if buyer is not eligible for any of the benefits.
        /// </summary>
        [JsonProperty(PropertyName = "primeMembershipTypes")]
        public IList<string> PrimeMembershipTypes { get; internal set; }

        /// <summary>
        /// Evaluates PrimeMembershipTypes of the buyer by PrimeMembershipType.  PrimeMembershipType.NONE returns true if merchant account is allow listed and buyer has no Prime Membership. UnauthorizedAccessException will be thrown if merchant account has not been allow listed for PrimeMembershipTypes access.
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        public bool HasPrimeMembershipType(PrimeMembershipType membership)
        {
            if (PrimeMembershipTypes == null) throw new UnauthorizedAccessException("Merchant account has not been allow listed for PrimeMembershipTypes access");
            // return true if the ask is if the buyer has no membership
            if (membership == PrimeMembershipType.NONE && !PrimeMembershipTypes.Any()) return true;
            return (PrimeMembershipTypes.Any()) && PrimeMembershipTypes.Contains(Enum.GetName(typeof(PrimeMembershipType), membership), StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
