using System;
using Newtonsoft.Json;
using Amazon.Pay.API.Types;

namespace Amazon.Pay.API.WebStore.Types
{
    public class ReportSchedule : AmazonPayResponse
    {
        /// <summary>
        /// The report schedule identifier.
        /// </summary>
        [JsonProperty(PropertyName = "reportScheduleId")]
        public string ReportScheduleId { get; internal set; }

        /// <summary>
        /// The type of the scheduled report.
        /// </summary>
        [JsonProperty(PropertyName = "reportType")]
        public ReportTypes ReportType { get; internal set; }

        /// <summary>
        /// Frequency defining the interval between report creations.
        /// </summary>
        [JsonProperty(PropertyName = "scheduleFrequency")]
        public ScheduleFrequency ScheduleFrequency { get; internal set; }

        /// <summary>
        /// Time when the next report will be created.
        /// </summary>
        [JsonProperty(PropertyName = "nextReportCreationTime")]
        public DateTime NextReportCreationTime { get; internal set; }
    }
}