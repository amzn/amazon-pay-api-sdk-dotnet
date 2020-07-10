using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// Specifies whether addresses that match restrictions configuration should or should not be restricted.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RestrictionType
    {
        /// <summary>
        /// 'Allowed' - Mark addresses that don't match restrictions configuration as restricted.
        /// </summary>
        Allowed,

        /// <summary>
        /// Mark addresses that match restrictions configuration as restricted.
        /// </summary>
        NotAllowed
    }
}
