using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Reports
{
     public class GetReportDocumentResponse : AmazonPayResponse
     {
          /// <summary>
          /// Report Document Id Identifier.
          /// </summary>
          [JsonProperty(PropertyName = "reportDocumentId")]
          public string ReportDocumentId { get; internal set; }

          /// <summary>
          /// URL Identifier.
          /// </summary>
          [JsonProperty(PropertyName = "url")]
          public string URL { get; internal set; }

          /// <summary>
          /// Compression Algorithm Identifier.
          /// </summary>
          [JsonProperty(PropertyName = "compressionAlgorithm")]
          public string CompressionAlgorithm { get; internal set; }
     }
}