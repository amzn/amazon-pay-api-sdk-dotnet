using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.WebStore.Types
{
    /// <summary>
    /// Report Types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReportTypes
    {
        /// <summary>
        /// Amazon Pay Settlement Report. Automatically created with each settlement.
        /// </summary>
        _GET_FLAT_FILE_OFFAMAZONPAYMENTS_SETTLEMENT_DATA_,

        /// <summary>
        ///Amazon Pay Settlement Report based on sandbox transactions. Needs to be manually created.
        /// </summary>
        _GET_FLAT_FILE_OFFAMAZONPAYMENTS_SANDBOX_SETTLEMENT_DATA_,

        /// <summary>
        /// Transaction report listing all orders of a period.
        /// </summary>
        _GET_FLAT_FILE_OFFAMAZONPAYMENTS_ORDER_REFERENCE_DATA_,

        /// <summary>
        /// Transaction report listing all recurring contracts of a period.
        /// </summary>
        _GET_FLAT_FILE_OFFAMAZONPAYMENTS_BILLING_AGREEMENT_DATA_,

        /// <summary>
        /// Transaction report listing all payment authorizations of a period.
        /// </summary>
        _GET_FLAT_FILE_OFFAMAZONPAYMENTS_AUTHORIZATION_DATA_,

        /// <summary>
        /// Transaction report listing all charges of a period.
        /// Note: This report type is currently not available via API.
        /// </summary>
        _GET_FLAT_FILE_OFFAMAZONPAYMENTS_CAPTURE_DATA_,

        /// <summary>
        /// Transaction report listing all refunds of a period.
        /// </summary>
        _GET_FLAT_FILE_OFFAMAZONPAYMENTS_REFUND_DATA_
    }
}
