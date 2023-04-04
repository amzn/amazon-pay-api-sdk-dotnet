using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Reports
{
     public class CreateReportScheduleResponse : AmazonPayResponse
     {
          /// <summary>
          /// Report Schedule Id Identifier.
          /// </summary>
          [JsonProperty(PropertyName = "reportScheduleId")]
          public string ReportScheduleId { get; internal set; }
     }
}