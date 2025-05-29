using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.WebStore.File
{
    public class FileResponse : AmazonPayResponse
    {
        /// <summary>
        /// Amazon Pay File ID generated from request.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; internal set; }

        /// <summary>
        /// File type (e.g., "jpg").
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; internal set; }

        /// <summary>
        /// Purpose of the file (e.g., "disputeEvidence").
        /// </summary>
        [JsonProperty(PropertyName = "purpose")]
        public string Purpose { get; internal set; }

        /// <summary>
        /// Timestamp when the file was uploaded (ISO 8601 format).
        /// </summary>
        [JsonProperty(PropertyName = "uploadTimestamp")]
        public DateTime UploadTimestamp { get; internal set; }

        /// <summary>
        /// URL to access the uploaded file.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string URL { get; internal set; }

        /// <summary>
        /// Expiration timestamp of the URL (ISO 8601 format).
        /// </summary>
        [JsonProperty(PropertyName = "urlExpirationTimeStamp")]
        public DateTime UrlExpirationTimestamp { get; internal set; }
    }

}