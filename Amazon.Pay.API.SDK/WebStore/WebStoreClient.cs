using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.AccountManagement;
using Amazon.Pay.API.WebStore.Buyer;
using Amazon.Pay.API.WebStore.Charge;
using Amazon.Pay.API.WebStore.ChargePermission;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Interfaces;
using Amazon.Pay.API.WebStore.Refund;
using Amazon.Pay.API.WebStore.Reports;
using Amazon.Pay.API.WebStore.Types;

using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore
{
    public class WebStoreClient : Client, IWebStoreClient, IReportsClient, IMerchantOnboardingClient
    {
        public WebStoreClient(ApiConfiguration payConfiguration) : base(payConfiguration)
        {
        }

        /// <summary>
        /// Creates a new Checkout Session.
        /// </summary>
        public CheckoutSessionResponse CreateCheckoutSession(CreateCheckoutSessionRequest body, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.CheckoutSessions);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, body, headers);

            var result = CallAPI<CheckoutSessionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Gets a Checkout Session.
        /// </summary>
        public CheckoutSessionResponse GetCheckoutSession(string checkoutSessionId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.CheckoutSessions, checkoutSessionId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<CheckoutSessionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Updates a Checkout Session.
        /// </summary>
        public CheckoutSessionResponse UpdateCheckoutSession(string checkoutSessionId, UpdateCheckoutSessionRequest body, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.CheckoutSessions, checkoutSessionId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.PATCH, body, headers);

            var result = CallAPI<CheckoutSessionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Completes a Checkout Session.
        /// </summary>
        public CheckoutSessionResponse CompleteCheckoutSession(string checkoutSessionId, CompleteCheckoutSessionRequest completeRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.CheckoutSessions, checkoutSessionId, Constants.Methods.Complete);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, completeRequest, headers);

            var result = CallAPI<CheckoutSessionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Helps the solution provider get details of a chargePermission
        /// </summary>
        /// <param name="chargePermissionId"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public ChargePermissionResponse GetChargePermission(string chargePermissionId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.ChargePermissions, chargePermissionId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<ChargePermissionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Updates a Charge Permission.
        /// </summary>
        public ChargePermissionResponse UpdateChargePermission(string chargePermissionId, UpdateChargePermissionRequest request, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.ChargePermissions, chargePermissionId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.PATCH, request, headers);

            var result = CallAPI<ChargePermissionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Closes a Charge Permission.
        /// </summary>
        public ChargePermissionResponse CloseChargePermission(string chargePermissionId, CloseChargePermissionRequest request, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.ChargePermissions, chargePermissionId, Constants.Methods.Close);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.DELETE, request, headers);

            var result = CallAPI<ChargePermissionResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Create a Charge to authorize payment.
        /// </summary>
        /// <returns>ChargeResponse response</returns>
        public ChargeResponse CreateCharge(CreateChargeRequest createChargeRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Charges);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, createChargeRequest, headers);

            var result = CallAPI<ChargeResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Capture payment on a Charge in the Authorized state.
        /// </summary>
        /// <returns>ChargeResponse response</returns>
        public ChargeResponse CaptureCharge(string chargeId, CaptureChargeRequest captureRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Charges, chargeId, Constants.Methods.Capture);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, captureRequest, headers);

            var result = CallAPI<ChargeResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Gets a Charge object.
        /// </summary>
        /// <returns>ChargeResponse response</returns>
        public ChargeResponse GetCharge(string chargeId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Charges, chargeId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<ChargeResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Moves Charge to Canceled state and releases any authorized payments.
        /// </summary>
        /// <returns>AmazonPayResponse response</returns>
        /// <remarks>
        /// You can call this operation until Capture is initiated while Charge is in an AuthorizationInitiated or Authorized state.
        /// </remarks>
        public ChargeResponse CancelCharge(string chargeId, CancelChargeRequest cancelRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Charges, chargeId, Constants.Methods.Cancel);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.DELETE, cancelRequest, headers);

            var result = CallAPI<ChargeResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Initiate a full or partial refund for a charge.
        /// </summary>
        /// <param name="refundRequest"></param>
        /// <param name="headers"></param>
        /// <returns>RefundResponse response</returns>
        public RefundResponse CreateRefund(CreateRefundRequest refundRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Refunds);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, refundRequest, headers);

            var result = CallAPI<RefundResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Get refund details.
        /// </summary>
        /// <param name="refundId"></param>
        /// <param name="headers"></param>
        /// <returns>AmazonPayResponse response</returns>
        public RefundResponse GetRefund(string refundId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Refunds, refundId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<RefundResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Get buyer details.
        /// </summary>
        /// <param name="buyerToken">Token used to retrieve buyer details. This value is appended as a query parameter to signInReturnUrl.</param>
        /// <param name="headers"></param>
        /// <returns>Object with buyer details.</returns>
        /// <remarks>
        /// Get Buyer will only return buyerId by default. You must explicitly request access to additional buyer details using the button signInScopes parameter.
        /// Amazon Pay will only provide the token required to retrieve buyer details after the buyer signs in. It will be appended to the signInReturnUrl as a query parameter and expires after 24 hours.
        /// </remarks>
        public BuyerResponse GetBuyer(string buyerToken, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Buyer, buyerToken);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<BuyerResponse>(apiRequest);

            return result;
        }

        /// <summary>
        /// Generates the signature string for the Amazon Pay front-end button.
        /// </summary>
        /// <param name="jsonString">The payload for generating a CheckoutSession as JSON string.</param>
        /// <returns>Signature string that can be assigned to the front-end button's "signature" parameter.</returns>
        public string GenerateButtonSignature(string jsonString)
        {
            var stringToSign = signatureHelper.CreateStringToSign(jsonString);
            var signature = signatureHelper.GenerateSignature(stringToSign);

            return signature;
        }

        /// <summary>
        /// Generates the signature string for the Amazon Pay Checkout button.
        /// </summary>
        /// <param name="request">The payload for generating a CheckoutSession as CreateCheckoutSessionRequest object.</param>
        /// <returns>Signature string that can be assigned to the front-end button's "signature" parameter.</returns>
        public string GenerateButtonSignature(CreateCheckoutSessionRequest request)
        {
            var signature = GenerateButtonSignature(request.ToJson());

            return signature;
        }

        /// <summary>
        /// Generates the signature string for the Amazon Pay Login button.
        /// </summary>
        /// <param name="request">The payload for generating a SignIn Request.</param>
        /// <returns>Signature string that can be assigned to the front-end button's "signature" parameter.</returns>
        public string GenerateButtonSignature(SignInRequest request)
        {
            var signature = GenerateButtonSignature(request.ToJson());

            return signature;
        }


        // ----------------------------------- CV2 REPORTING APIS -----------------------------------

        /// <summary>
        /// Returns report details for the reports that match the filters that you specify.
        /// </summary>
        /// <param name="queryParamters">The queryParameters for the request.</param>
        /// <param name="headers"></param>
        /// <returns>Details for the reports that match the filters that you specify.</returns>
        public GetReportsResponse GetReports(GetReportsRequest getReportsRequest = null, Dictionary<string, string> headers = null)
        {
            // Utility function to convert GetReportsRequest to a Dictionary:
            var queryParameters = new Dictionary<string, List<string>>();
            string isoFormat = "yyyyMMddTHHmmssZ";

            if(getReportsRequest != null) 
            {
                if(getReportsRequest.ReportTypes != null)
                    queryParameters.Add("reportTypes", new List<string>() { string.Join(",", getReportsRequest.ReportTypes) });
                    
                if(getReportsRequest.ProcessingStatuses !=null)
                    queryParameters.Add("processingStatuses", new List<string>() { string.Join(",", getReportsRequest.ProcessingStatuses) });
                    
                if(getReportsRequest.CreatedSince != null)
                    queryParameters.Add("createdSince", new List<string>() { getReportsRequest.CreatedSince.Value.ToString(isoFormat) });

                if(getReportsRequest.CreatedUntil != null)
                    queryParameters.Add("createdUntil", new List<string>() { getReportsRequest.CreatedUntil.Value.ToString(isoFormat) });
                    
                queryParameters.Add("pageSize", new List<string>() { getReportsRequest.PageSize.ToString() });
                if(getReportsRequest.NextToken != null)
                {
                    queryParameters.Add("nextToken", new List<string>() { getReportsRequest.NextToken });
                    queryParameters.Remove("createdSince");
                    queryParameters.Remove("createdUntil");
                    queryParameters.Remove("pageSize");
                }
            }

            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Reports);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                QueryParameters = queryParameters,
                Headers = headers
            };

            var result = CallAPI<GetReportsResponse>(apiRequest);

            return result;
        }


        /// <summary>
        /// Returns report details for the given reportId.
        /// </summary>
        /// <param name="reportId">The Report Id for filtering.</param>
        /// /// <param name="headers"></param>
        /// <returns>Report details for the given reportId.</returns>
        public Report GetReportById(string reportId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Reports, reportId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<Report>(apiRequest);

            return result;
        }


        /// <summary>
        /// Returns the pre-signed S3 URL for the report. The report can be downloaded using this URL.
        /// </summary>
        /// <param name="reportDocumentId">The Report Document Id for filtering.</param>
        /// <param name="headers"></param>
        /// <returns>Pre-signed S3 URL for the report.</returns>
        public GetReportDocumentResponse GetReportDocument(string reportDocumentId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.ReportDocuments, reportDocumentId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<GetReportDocumentResponse>(apiRequest);

            return result;
        }


        /// <summary>
        /// Returns report schedule details that match the filters criteria specified.
        /// </summary>
        /// <param name="reportTypes">The Report Types for filtering.</param>
        /// <param name="headers"></param>
        /// <returns>Report schedule details that match the filters criteria specified.</returns>
        public GetReportSchedulesResponse GetReportSchedules(GetReportSchedulesRequest getReportSchedulesRequest = null, Dictionary<string, string> headers = null)
        {
            // Utility function to convert GetReportScheduleRequest to Dictionary for QueryParameters:
            var queryParameters = new Dictionary<string, List<string>>();
            if(getReportSchedulesRequest != null) 
                queryParameters.Add("reportTypes", new List<string>() { string.Join(",", getReportSchedulesRequest.ReportTypes) });

            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.ReportSchedules);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                QueryParameters = queryParameters,
                Headers = headers
            };

            var result = CallAPI<GetReportSchedulesResponse>(apiRequest);

            return result;
        }
        

        /// <summary>
        /// Returns the report schedule details that match the given ID.
        /// </summary>
        /// <param name="reportScheduleId">The Report Schedule Id for filtering</param>
        /// <param name="headers"></param>
        /// <returns>Report schedule details that match the given ID.</returns>
        public ReportSchedule GetReportScheduleById(string reportScheduleId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.ReportSchedules, reportScheduleId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.GET)
            {
                Headers = headers
            };

            var result = CallAPI<ReportSchedule>(apiRequest);

            return result;
        }

        /// <summary>
        /// Submits a request to generate a report based on the reportType and date range specified.
        /// </summary>
        /// <param name="requestPayload">The payload for creating a Report.</param>
        /// <param name="headers"></param>
        /// <returns>Report ID which is created via the Request Payload</returns>
        public CreateReportResponse CreateReport(CreateReportRequest requestPayload, Dictionary<string, string> headers)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.Reports);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, requestPayload, headers);

            var result = CallAPI<CreateReportResponse>(apiRequest);

            return result;
        }


        /// <summary>
        /// Creates a report schedule for the given reportType. Only one schedule per report type allowed.
        /// </summary>
        /// <param name="requestPayload">The payload for creating a Report Schedule.</param>
        /// <param name="headers"></param>
        /// <returns>Report Schedule ID which is created via the Request Payload</returns>
        public CreateReportScheduleResponse CreateReportSchedule(CreateReportScheduleRequest requestPayload, Dictionary<string, string> headers)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.ReportSchedules);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, requestPayload, headers);

            var result = CallAPI<CreateReportScheduleResponse>(apiRequest);

            return result;
        }


        /// <summary>
        /// Cancels the report schedule with the given reportScheduleId.
        /// </summary>
        /// <param name="reportScheduleId">The Report Schedule ID for cancelling a Report.</param>
        /// <param name="headers"></param>
        /// <returns>HTTP Response after cancelling the Report Schedule mentioned</returns>
        public CancelReportScheduleResponse CancelReportSchedule(string reportScheduleId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.ReportSchedules, reportScheduleId);
            CancelReportScheduleRequest cancelReportScheduleRequest = new CancelReportScheduleRequest();
            var apiRequest = new ApiRequest(apiPath, HttpMethod.DELETE, cancelReportScheduleRequest, headers);

            var result = CallAPI<CancelReportScheduleResponse>(apiRequest);

            return result;
        }

        // ----------------------------------- Merchant Onboarding & Account Management APIs --------------------

        /// <summary>
        /// Creates a non-logginable account for your merchant partners. These would be special accounts through which Merchants would not be able to login to Amazon or access Seller Central.
        /// </summary>
        /// <param name="registerAmazonPayAccountRequest">The payload for creating a amazon pay account.</param>
        /// <param name="headers"></param>
        /// <returns>Merchant Account ID which is created via the Request Payload</returns>
        public RegisterAmazonPayAccountResponse RegisterAmazonPayAccount(RegisterAmazonPayAccountRequest registerAmazonPayAccountRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.AccountManagement);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, registerAmazonPayAccountRequest, headers);

            return CallAPI<RegisterAmazonPayAccountResponse>(apiRequest);
        }

        /// <summary>
        /// Updates a merchant account for the given Merchant Account ID. We would be allowing our partners to update only a certain set of fields which won’t change the legal business entity itself.
        /// </summary>
        /// <param name="merchantAccountId">Internal Merchant Account ID for Updating the Amazon Pay Account</param>
        /// <param name="updateAmazonPayAccountRequest">The payload for updating amazon pay account.</param>
        /// <param name="headers"></param>
        /// <returns>HTTP Response after Updating the Merchant Account ID mentioned</returns>
        public UpdateAmazonPayAccountResponse UpdateAmazonPayAccount(string merchantAccountId, UpdateAmazonPayAccountRequest updateAmazonPayAccountRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.AccountManagement, merchantAccountId);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.PATCH, updateAmazonPayAccountRequest, headers);

            return CallAPI<UpdateAmazonPayAccountResponse>(apiRequest);
        }

        /// <summary>
        /// Deletes the Merchant account for the given Merchant Account ID. Partners can close the merchant accounts created for their merchant partners.
        /// </summary>
        /// <param name="merchantAccountId">Internal Merchant Account ID for Deleting the Amazon Pay Account</param>
        /// <param name="headers"></param>
        /// <returns>HTTP Response after Deleting the Merchant Account ID mentioned</returns>
        public DeleteAmazonPayAccountResponse DeleteAmazonPayAccount(string merchantAccountId, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.AccountManagement, merchantAccountId);
            DeleteAmazonPayAccountRequest deleteAmazonPayAccountRequest = new DeleteAmazonPayAccountRequest();
            var apiRequest = new ApiRequest(apiPath, HttpMethod.DELETE, deleteAmazonPayAccountRequest, headers);

            return CallAPI<DeleteAmazonPayAccountResponse>(apiRequest);
        }
        
        /// <summary>
        /// FinalizeCheckoutSession API which enables Pay to validate payment critical attributes and also update book-keeping attributes present in merchantMetadata
        /// </summary>
        /// <param name="checkoutSessionId">The checkout session Id</param>
        /// <param name="finalizeCheckoutSessionRequest">The payload for finalize checkout session</param>
        /// <param name="headers"></param>
        public CheckoutSessionResponse FinalizeCheckoutSession(string checkoutSessionId, FinalizeCheckoutSessionRequest finalizeCheckoutSessionRequest, Dictionary<string, string> headers = null)
        {
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.WebStore.CheckoutSessions, checkoutSessionId, Constants.Methods.Finalize);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, finalizeCheckoutSessionRequest, headers);

            var result = CallAPI<CheckoutSessionResponse>(apiRequest);

            return result;
        }
    }
}
