using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.WebStore.Reports
{
     public class CreateReportRequest : ApiRequestBody
     {

          string isoFormat = "yyyyMMddTHHmmssZ";

          public CreateReportRequest(ReportTypes reportType, DateTime startTime, DateTime endTime)
          {
               ReportType = reportType;
               StartTime = startTime.ToString(isoFormat);
               EndTime = endTime.ToString(isoFormat);
          }

          public CreateReportRequest(ReportTypes reportType, string startTime, string endTime)
          {
               ReportType = reportType;
               StartTime = startTime;
               EndTime = endTime;
          }

          /// <summary>
          /// Type of report to be created.
          /// </summary>
          [JsonProperty(PropertyName = "reportType")]
          public ReportTypes ReportType { get; set; }

          /// <summary>
          /// Time from which the transactions are included in the report.
          /// </summary>
          [JsonProperty(PropertyName = "startTime")]
          public string StartTime { get; set; }

          /// <summary>
          /// Time until which the transactions are included in the report.
          /// </summary>
          [JsonProperty(PropertyName = "endTime")]
          public string EndTime { get; set; }

     }
}
