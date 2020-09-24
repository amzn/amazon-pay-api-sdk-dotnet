namespace Amazon.Pay.API.Types
{
    /// <summary>
    /// States available for Amazon Pay objects like ChargePermission, Charge, CheckoutSession and Refund.
    /// </summary>
    public static class State
    {
        public const string AuthorizationInitiated = nameof(AuthorizationInitiated);
        public const string Authorized = nameof(Authorized);
        public const string Canceled = nameof(Canceled);
        public const string Captured = nameof(Captured);
        public const string CaptureInitiated = nameof(CaptureInitiated);
        public const string Chargeable = nameof(Chargeable);
        public const string Closed = nameof(Closed);
        public const string Completed = nameof(Completed);
        public const string Declined = nameof(Declined);
        public const string NonChargeable = nameof(NonChargeable);
        public const string Open = nameof(Open);
        public const string Pending = nameof(Pending);
        public const string RefundInitiated = nameof(RefundInitiated);
        public const string Refunded = nameof(Refunded);
    }
}
