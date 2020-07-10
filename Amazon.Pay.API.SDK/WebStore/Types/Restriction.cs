using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Amazon.Pay.API.WebStore.Types
{
    public class Restriction
    {
        public Restriction()
        {
            StatesOrRegions = new List<string>();
            ZipCodes = new List<string>();
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext content)
        {
            // skip 'statesOrRegions' if there weren't any provided
            if (StatesOrRegions != null && StatesOrRegions.Count == 0)
            {
                StatesOrRegions = null;
            }

            // skip 'zipCodes' if there weren't any provided
            if (ZipCodes != null && ZipCodes.Count == 0)
            {
                ZipCodes = null;
            }
        }

        [OnSerialized]
        internal void OnSerialized(StreamingContext content)
        {
            if (StatesOrRegions == null)
            {
                StatesOrRegions = new List<string>();
            }

            if (ZipCodes == null)
            {
                ZipCodes = new List<string>();
            }
        }

        /// <summary>
        /// List of country-specific states that should or should not be restricted based on addressRestrictions.type parameter.
        /// </summary>
        [JsonProperty(PropertyName = "statesOrRegions")]
        public List<string> StatesOrRegions { get; internal set; }

        /// <summary>
        /// List of country-specific zip codes that should or should not be restricted based on addressRestrictions.type parameter
        /// </summary>
        [JsonProperty(PropertyName = "zipCodes")]
        public List<string> ZipCodes { get; internal set; }
    }
}
