using Newtonsoft.Json;
using Amazon.Pay.API.Types; 
using System;

namespace Amazon.Pay.API.WebStore.Types
{
    public class Report : AmazonPayResponse
    {
        /// <summary>
        /// The report identifier.
        /// </summary>
        [JsonProperty(PropertyName = "reportId")]
        public string ReportId { get; internal set; }

        /// <summary>
        /// The type of the report.
        /// </summary>
        [JsonProperty(PropertyName = "reportType")]
        public ReportTypes ReportType { get; internal set; }

        /// <summary>
        /// Time from which the transactions are included in the report.
        /// </summary>
        [JsonProperty(PropertyName = "startTime")]
        public DateTime StartTime { get; internal set; }

        /// <summary>
        /// Time to which the transactions are included in the report.
        /// </summary>
        [JsonProperty(PropertyName = "endTime")]
        public DateTime EndTime { get; internal set; }

        /// <summary>
        /// Time at which the request to create the report was received.
        /// </summary>
        [JsonProperty(PropertyName = "createdTime")]
        public DateTime CreatedTime { get; internal set; }

        /// <summary>
        /// The processing status of the report.
        /// </summary>
        [JsonProperty(PropertyName = "processingStatus")]
        public ProcessingStatus ProcessingStatus { get; internal set; }

        /// <summary>
        /// Time at which the request to create report was processed (started processing).
        /// </summary>
        [JsonProperty(PropertyName = "processingStartTime")]
        public DateTime ProcessingStartTime { get; internal set; }


        /// <summary>
        /// Time at which the report request was completed and the report was generated.
        /// </summary>
        [JsonProperty(PropertyName = "processingEndTime")]
        public DateTime ProcessingEndTime { get; internal set; }

        /// <summary>
        /// The report document identifier.
        /// </summary>
        [JsonProperty(PropertyName = "reportDocumentId")]
        public string ReportDocumentId { get; internal set; }
    }
}
