using Amazon.Pay.API.Attributes;

namespace Amazon.Pay.API.Types
{
    /// <summary>
    /// Payment regions that Amazon Pay is available at.
    /// </summary>
    public enum Region
    {
        /// <summary>
        /// United Kingdom (GBP) and all countries in the EUR region.
        /// </summary>
        [Region(shortform: "eu",  "eu")]
        Europe,

        /// <summary>
        /// Japan (JPY)
        /// </summary>
        [Region(shortform: "jp",  "jp")]
        Japan,

        /// <summary>
        /// United States (USD)
        /// </summary>
        [Region(shortform: "na", "com")]
        UnitedStates
    }
}
