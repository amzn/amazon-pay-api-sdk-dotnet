namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// An address restriction inside a specific country such as a state, region or postal code.
    /// </summary>
    public class InnerCountryAddressRestriction
    {
        private AddressRestrictions parent;
        private string countryCode;

        internal InnerCountryAddressRestriction(string countryCode, AddressRestrictions parent)
        {
            this.parent = parent;
            this.countryCode = countryCode;
        }

        /// <summary>
        /// Add restriction for a state or region of this country.
        /// </summary>
        /// <param name="stateOrRegion">State or region code, for instance CA for California.</param>
        /// <returns>The same InnerCountryAddressRestriction object so that additional state, region or postal code can be added.</returns>
        public InnerCountryAddressRestriction AddStateOrRegionRestriction(string stateOrRegion)
        {
            Restriction existingRestriction = GetExistingRestriction();

            existingRestriction.StatesOrRegions.Add(stateOrRegion);

            return this;
        }

        /// <summary>
        /// Add restriction for a ZIP code of this country.
        /// </summary>
        /// <param name="zipCode">ZIP code, for instance 12345.</param>
        /// <returns>The same InnerCountryAddressRestriction object so that additional state, region or postal code can be added.</returns>
        public InnerCountryAddressRestriction AddZipCodesRestriction(string zipCode)
        {
            Restriction existingRestriction = GetExistingRestriction();

            existingRestriction.ZipCodes.Add(zipCode);

            return this;
        }

        private Restriction GetExistingRestriction()
        {
            var existingRestriction = parent.Restrictions[this.countryCode];

            if (existingRestriction == null)
            {
                existingRestriction = new Restriction();
            }

            return existingRestriction;
        }
    }
}
