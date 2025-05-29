using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class MerchantEvidence
    {
        /// <summary>
        /// Identifies the type of the evidence
        /// </summary>
        [JsonProperty("evidenceType")]
        public string EvidenceType { get; set; }

        /// <summary>
        /// File ID of the evidence file that has already been uploaded
        /// </summary>
        [JsonProperty("fileId")]
        public string FileId { get; set; }

        /// <summary>
        /// Raw text evidence
        /// </summary>
        [JsonProperty("evidenceText")]
        public string EvidenceText { get; set; }
    }
}
