namespace Amazon.Pay.API.Types
{
    // TODO: change to static string fields for avoiding that parsing of JSON response from API leads to converter exceptions

    public class ReasonCode
    {
        public static string AmazonCanceled = nameof(AmazonCanceled);
        public static string AmazonClosed = nameof(AmazonClosed);
        public static string AmazonRejected = nameof(AmazonRejected);
        public static string AmountNotSet = nameof(AmountNotSet);
        public static string BillingAddressDeleted = nameof(BillingAddressDeleted);
        public static string BuyerCanceled = nameof(BuyerCanceled);
        public static string ChargeInProgress = nameof(ChargeInProgress);
        public static string ChargePermissionCanceled = nameof(ChargePermissionCanceled);
        public static string Declined = nameof(Declined);
        public static string Expired = nameof(Expired);
        public static string ExpiredUnused = nameof(ExpiredUnused);
        public static string HardDeclined = nameof(HardDeclined);
        public static string MerchantCanceled = nameof(MerchantCanceled);
        public static string MerchantClosed = nameof(MerchantClosed);
        public static string MFAFailed = nameof(MFAFailed);
        public static string SoftDeclined = nameof(SoftDeclined);
        public static string PaymentMethodDeleted = nameof(PaymentMethodDeleted);
        public static string PaymentMethodExpired = nameof(PaymentMethodExpired);
        public static string PaymentMethodInvalid = nameof(PaymentMethodInvalid);
        public static string PaymentMethodNotAllowed = nameof(PaymentMethodNotAllowed);
        public static string PaymentMethodNotSet = nameof(PaymentMethodNotSet);
        public static string ProcessingFailure = nameof(ProcessingFailure);
        public static string TransactionAmountExceeded = nameof(TransactionAmountExceeded);
        public static string TransactionTimedOut = nameof(TransactionTimedOut);
    }
}
