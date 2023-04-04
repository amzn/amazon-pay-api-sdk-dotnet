using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Reports
{
     public class CreateReportResponse : AmazonPayResponse
     {
          /// <summary>
          /// Report Id Identifier.
          /// </summary>
          [JsonProperty(PropertyName = "reportId")]
          public string ReportId { get; internal set; }
     }
}