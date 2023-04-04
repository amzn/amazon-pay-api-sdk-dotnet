using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore.Reports
{
     public class GetReportsResponse : AmazonPayResponse
     {
          /// <summary>
          /// A list of report objects matching the search criteria.
          /// </summary>
          [JsonProperty(PropertyName = "reports")]
          public List<Report> Reports { get; internal set; }

          /// <summary>
          /// Returned when the number of results exceeds pageSize. To get the next page of results, call getReports with this token as the only parameter.
          /// </summary>
          [JsonProperty(PropertyName = "nextToken")]
          public string NextToken { get; internal set; }
     }
}