namespace Amazon.Pay.API.Types
{
    public static class ReasonCode
    {
        public const string AmazonCanceled = nameof(AmazonCanceled);
        public const string AmazonClosed = nameof(AmazonClosed);
        public const string AmazonRejected = nameof(AmazonRejected);
        public const string AmountNotSet = nameof(AmountNotSet);
        public const string BillingAddressDeleted = nameof(BillingAddressDeleted);
        public const string BuyerCanceled = nameof(BuyerCanceled);
        public const string ChargeInProgress = nameof(ChargeInProgress);
        public const string ChargePermissionCanceled = nameof(ChargePermissionCanceled);
        public const string Declined = nameof(Declined);
        public const string Expired = nameof(Expired);
        public const string ExpiredUnused = nameof(ExpiredUnused);
        public const string HardDeclined = nameof(HardDeclined);
        public const string MerchantCanceled = nameof(MerchantCanceled);
        public const string MerchantClosed = nameof(MerchantClosed);
        public const string MFAFailed = nameof(MFAFailed);
        public const string SoftDeclined = nameof(SoftDeclined);
        public const string PaymentMethodDeleted = nameof(PaymentMethodDeleted);
        public const string PaymentMethodExpired = nameof(PaymentMethodExpired);
        public const string PaymentMethodInvalid = nameof(PaymentMethodInvalid);
        public const string PaymentMethodNotAllowed = nameof(PaymentMethodNotAllowed);
        public const string PaymentMethodNotSet = nameof(PaymentMethodNotSet);
        public const string ProcessingFailure = nameof(ProcessingFailure);
        public const string TransactionAmountExceeded = nameof(TransactionAmountExceeded);
        public const string TransactionTimedOut = nameof(TransactionTimedOut);
        public const string BuyerClosed = nameof(BuyerClosed);
    }
}
