using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore.Reports
{
     public class GetReportsRequest : ApiRequestBody
     {
          public GetReportsRequest(List<ReportTypes> reportTypes = null, List<ProcessingStatus> processingStatuses = null, DateTime? createdSince = null, DateTime? createdUntil = null, int pageSize = 10, string nextToken = null)
          {
               ReportTypes = reportTypes;
               ProcessingStatuses = processingStatuses;
               if (createdSince != null)
                    CreatedSince = (DateTime)createdSince;
               else
                    CreatedSince = DateTime.UtcNow.AddDays(-90);
               if (createdUntil != null)
                    CreatedUntil = (DateTime)createdUntil;
               else
                    CreatedUntil = DateTime.UtcNow;
               PageSize = pageSize;
               NextToken = nextToken;
          }

          /// <summary>
          /// List of types of reports requested.
          /// </summary>
          [JsonProperty(PropertyName = "reportTypes")]
          public List<ReportTypes> ReportTypes { get; set; }

          /// <summary>
          /// A list of processing statuses used to filter reports.
          /// </summary>
          [JsonProperty(PropertyName = "processingStatuses")]
          public List<ProcessingStatus> ProcessingStatuses { get; set; }

          /// <summary>
          /// The earliest report creation date and time for reports to include in the response, in ISO 8601 date time format. Reports are retained for a maximum of 90 days.
          /// </summary>
          [JsonProperty(PropertyName = "createdSince")]
          public DateTime? CreatedSince { get; set; }

          /// The latest report creation date and time for reports to include in the response, in ISO 8601 date time format. Reports are retained for a maximum of 90 days.
          /// </summary>
          [JsonProperty(PropertyName = "createdUntil")]
          public DateTime? CreatedUntil { get; set; }

          /// The number of reports per page to return.
          /// </summary>
          [JsonProperty(PropertyName = "pageSize")]
          public int PageSize { get; set; }

          /// A string token returned in the response to your previous request. nextToken is returned when the number of results exceeds the specified pageSize value. To get the next page of results, call the getReports operation and include this token as the only parameter. Specifying nextToken with any other parameters will cause the request to fail.
          /// </summary>
          [JsonProperty(PropertyName = "nextToken")]
          public string NextToken { get; set; }
     }
}
