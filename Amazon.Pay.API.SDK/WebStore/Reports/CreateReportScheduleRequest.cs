using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.WebStore.Reports
{
    public class CreateReportScheduleRequest : ApiRequestBody
    {
        string isoFormat = "yyyyMMddTHHmmssZ";

        public CreateReportScheduleRequest(ReportTypes reportType, ScheduleFrequency scheduleFrequency, DateTime nextReportCreationTime, bool deleteExistingSchedule = false)
        {
            ReportType = reportType;
            ScheduleFrequency = scheduleFrequency;
            NextReportCreationTime = nextReportCreationTime.ToString(isoFormat);
            DeleteExistingSchedule = deleteExistingSchedule;
        }   

        public CreateReportScheduleRequest(ReportTypes reportType, ScheduleFrequency scheduleFrequency, string nextReportCreationTime, bool deleteExistingSchedule = false)
        { 
            ReportType = reportType;
            ScheduleFrequency = scheduleFrequency;
            NextReportCreationTime = nextReportCreationTime;
            DeleteExistingSchedule = deleteExistingSchedule;
        }

        /// <summary>
        /// Type of the report for the schedule
        /// </summary>
        [JsonProperty(PropertyName = "reportType")]
        public ReportTypes ReportType { get; set; }

        /// <summary>
        /// Frequency in which the report shall be created.
        /// </summary>
        [JsonProperty(PropertyName = "scheduleFrequency")]
        public ScheduleFrequency ScheduleFrequency { get; set; }

        /// <summary>
        /// ISO 8601 time defining the next report creation time.
        /// </summary>
        [JsonProperty(PropertyName = "nextReportCreationTime")]
        public string NextReportCreationTime { get; set; }

        /// <summary>
        /// If true deletes an existing report schedule for the given report type. The API returns an array, if a schedule for the given report type already exists and set to false.
        /// </summary>
        [JsonProperty(PropertyName = "deleteExistingSchedule")]
        public bool DeleteExistingSchedule { get; set; }
    }
}
