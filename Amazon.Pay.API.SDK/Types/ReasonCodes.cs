using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.Types
{
    /// <summary>
    /// Reason codes for Amazon Pay objects like ChargePermission, Charge, CheckoutSession and Refund.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReasonCode
    {
        AmazonCanceled,
        AmazonClosed,
        AmazonRejected,
        BillingAddressDeleted,
        BuyerCanceled,
        ChargeInProgress,
        ChargePermissionCanceled,
        Declined,
        Expired,
        ExpiredUnused,
        HardDeclined,
        MerchantCanceled,
        MerchantClosed,
        MFAFailed,
        SoftDeclined,
        PaymentMethodDeleted,
        PaymentMethodExpired,
        PaymentMethodInvalid,
        PaymentMethodNotAllowed,
        PaymentMethodNotSet,
        ProcessingFailure,
        TransactionTimedOut
    }
}