using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amazon.Pay.API.Types
{
    /// <summary>
    /// States available for Amazon Pay objects like ChargePermission, Charge, CheckoutSession and Refund.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum State
    {
        AuthorizationInitiated,
        Authorized,
        Canceled,
        Captured,
        CaptureInitated,
        Chargeable,
        Closed,
        Completed,
        Declined,
        NonChargeable,
        Open,
        Pending,
        RefundInitiated,
        Refunded
    }
}
