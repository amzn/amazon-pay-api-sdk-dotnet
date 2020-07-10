using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// Rule-based restrictions.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpecialRestriction
    {
        /// <summary>
        /// Marks PO box addresses in US, CA, GB, FR, DE, ES, PT, IT, AU as restricted
        /// </summary>
        RestrictPOBoxes,

        /// <summary>
        /// Marks packstation addresses in DE as restricted
        /// </summary>
        RestrictPackstations
    }
}
