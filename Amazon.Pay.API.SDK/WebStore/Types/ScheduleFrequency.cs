using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// Report Types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ScheduleFrequency
    {
        /// <summary>
        /// Every 5 minutes.
        /// </summary>
        PT5M,

        /// <summary>
        /// Every 15 minutes.
        /// </summary>
        PT15M,

        /// <summary>
        /// Every 30 minutes.
        /// </summary>
        PT30M,

        /// <summary>
        /// Every hour.
        /// </summary>
        PT1H,

        /// <summary>
        /// Every 2 hours.
        /// </summary>
        PT2H,

        /// <summary>
        /// Every 4 hours.
        /// </summary>
        PT4H,

        /// <summary>
        /// Every 8 hours.
        /// </summary>
        PT8H,

        /// <summary>
        /// Every 12 hours.
        /// </summary>
        PT12H,

        /// <summary>
        /// Every 24 hours.
        /// </summary>
        PT24H,
        
        /// <summary>
        /// Every 84 hours.
        /// </summary>
        PT84H,

        /// <summary>
        /// Every day.
        /// </summary>
        P1D,

        /// <summary>
        /// Every 2 days.
        /// </summary>
        P2D,

        /// <summary>
        /// Every 3 days.
        /// </summary>
        P3D,

        /// <summary>
        /// Every 7 days.
        /// </summary>
        P7D,


        /// <summary>
        /// Every 14 days.
        /// </summary>
        P14D,     


        /// <summary>
        /// Every 15 days.
        /// </summary>
        P15D,


        /// <summary>
        /// Every 18 days.
        /// </summary>
        P18D,


        /// <summary>
        /// Every 30 days.
        /// </summary>
        P30D       
    }
}
