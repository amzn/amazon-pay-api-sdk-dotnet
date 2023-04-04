using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore.Reports
{
     public class GetReportSchedulesRequest : ApiRequestBody
     {

          public GetReportSchedulesRequest(List<ReportTypes> reportTypes = null)
          {
               ReportTypes = reportTypes;
          }

          public GetReportSchedulesRequest(ReportTypes? reportTypes = null)
          {
               if (reportTypes != null)
                    ReportTypes = new List<ReportTypes> { (ReportTypes)reportTypes };
          }

          /// <summary>
          /// List of report types
          /// </summary>
          [JsonProperty(PropertyName = "reportTypes")]
          public List<ReportTypes> ReportTypes { get; set; }
     }
}
