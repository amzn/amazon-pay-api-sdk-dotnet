using Amazon.Pay.API.Types;
using Newtonsoft.Json;


namespace Amazon.Pay.API.WebStore.File
{
    public class UploadFileRequest : ApiRequestBody
    {
        public UploadFileRequest(string type, string purpose)
        {
            Type = type;
            Purpose = purpose;
        }

        /// <summary>
        /// Type of the file format. File type can be of format png, jpg, pdf, etc
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Intent of the file being uploaded. File purpose possible values are disputeEvidence
        /// </summary>
        [JsonProperty(PropertyName = "purpose")]
        public string Purpose { get; set; }
    }
}