using Newtonsoft.Json;
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using System.Runtime.Serialization;

namespace Amazon.Pay.API.WebStore.AccountManagement
{
    public class UpdateAmazonPayAccountRequest : ApiRequestBody
    {
        // Initializes a new instance of the UpdateAmazonPayAccountRequest class.
        public UpdateAmazonPayAccountRequest()
        {
            BusinessInfo = new BusinessInfo();
            PrimaryContactPerson = new Person();
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext content)
        {
            // skip 'PrimaryContactPerson' if there wasn't provided anything
            if (PrimaryContactPerson?.PersonFullName == null)
            {
                PrimaryContactPerson = null;
            }
        }

        [OnSerialized]
        internal void OnSerialized(StreamingContext content)
        {
            if (PrimaryContactPerson == null)
            {
                PrimaryContactPerson = new Person();
            }
        }

        /// <summary>
        /// Gets or sets the business details.
        /// </summary>
        [JsonProperty(PropertyName = "businessInfo")]
        public BusinessInfo BusinessInfo { get; internal set; }

        /// <summary>
        /// Gets or sets the primary contact person.
        /// </summary>
        [JsonProperty(PropertyName = "primaryContactPerson")]
        public Person PrimaryContactPerson { get; internal set; }
    }
}