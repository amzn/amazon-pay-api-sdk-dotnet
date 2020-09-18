namespace Amazon.Pay.API.Types
{
    /// <summary>
    /// States available for Amazon Pay objects like ChargePermission, Charge, CheckoutSession and Refund.
    /// </summary>
    public class State
    {
        public static string AuthorizationInitiated = nameof(AuthorizationInitiated);
        public static string Authorized = nameof(Authorized);
        public static string Canceled = nameof(Canceled);
        public static string Captured = nameof(Captured);
        public static string CaptureInitiated = nameof(CaptureInitiated);
        public static string Chargeable = nameof(Chargeable);
        public static string Closed = nameof(Closed);
        public static string Completed = nameof(Completed);
        public static string Declined = nameof(Declined);
        public static string NonChargeable = nameof(NonChargeable);
        public static string Open = nameof(Open);
        public static string Pending = nameof(Pending);
        public static string RefundInitiated = nameof(RefundInitiated);
        public static string Refunde = nameof(Refunde);
    }
}
