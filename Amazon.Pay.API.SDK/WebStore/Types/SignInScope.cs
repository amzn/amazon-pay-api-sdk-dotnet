using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// The buyer details that you're requesting access for.
    /// </summary>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum SignInScope
    {
        /// <summary>
        /// Request access to buyer name.
        /// </summary>
        [EnumMember(Value = "name")]
        Name,

        /// <summary>
        /// Request access to buyer email address.
        /// </summary>
        [EnumMember(Value = "email")]
        Email,

        /// <summary>
        /// Request access to buyer default shipping address postal code and country code.
        /// </summary>
        [EnumMember(Value = "postalCode")]
        PostalCode
    }
}
