using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore.Reports
{
     public class GetReportSchedulesResponse : AmazonPayResponse
     {
          /// <summary>
          /// A list of report schedule objects matching the search criteria.
          /// </summary>
          [JsonProperty(PropertyName = "reportSchedules")]
          public List<ReportSchedule> ReportSchedules { get; internal set; }
     }
}