using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// Report Types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProcessingStatus
    {
        /// <summary>
        /// The report is being processed.
        /// </summary>
        IN_PROGRESS,

        /// <summary>
        /// The report has completed processing.
        /// </summary>
        COMPLETED,

        /// <summary>
        /// The report was aborted due to a fatal error.
        /// </summary>
        FAILED,

        /// <summary>
        /// The report was cancelled. There are two ways a report can be cancelled: an explicit cancellation request before the report starts processing, or an automatic cancellation if there is no data to return.
        /// </summary>
        CANCELLED
    }
}
