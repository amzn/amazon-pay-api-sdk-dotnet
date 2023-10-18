using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore;
using Amazon.Pay.API.WebStore.AccountManagement;
using Amazon.Pay.API.WebStore.Buyer;
using Amazon.Pay.API.WebStore.Charge;
using Amazon.Pay.API.WebStore.ChargePermission;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Refund;
using Amazon.Pay.API.WebStore.Types;
using Amazon.Pay.API.WebStore.Reports;
using Moq;
using NUnit.Framework;
using Environment = Amazon.Pay.API.Types.Environment;
using Moq.Protected;

namespace Amazon.Pay.API.SDK.Tests.WebStore
{
    [TestFixture]
    public class WebStoreClientTests
    {
        private Mock<WebStoreClient> mockClient;
        private ApiConfiguration payConfig;
        Mock<ISignatureHelper> mockSignatureHelper;
        private readonly string checkoutSessionIdToTest = "123456789";
        private readonly string merchantAccountIdToTest = "ABCD12345";

        [OneTimeSetUp]
        public void Init()
        {
            payConfig = new ApiConfiguration
            (
                region: Region.UnitedStates,
                environment: Environment.Sandbox,
                publicKeyId: "foo",
                privateKey: "-----BEGIN RSA PRIVATE KEY-----" // fake a private key ..);
            );
            mockClient = new Mock<WebStoreClient>(MockBehavior.Strict, payConfig) { CallBase = true };
            mockSignatureHelper = new Mock<ISignatureHelper>(MockBehavior.Strict);
        }

        [SetUp]
        public void Setup()
        {
            mockClient.Reset();
            mockSignatureHelper.Reset();
            mockSignatureHelper.SetupAllProperties();
            mockSignatureHelper.Setup<Dictionary<string, List<string>>>(x => x.CreateDefaultHeaders(It.IsAny<Uri>()))
                .Returns(CreateHeaders(new Uri("http://pay-api.amazon.eu/")));
            mockSignatureHelper.Setup<string>(x => x.CreateCanonicalRequest(It.IsAny<ApiRequest>(), It.IsAny<Dictionary<string, List<string>>>()))
                .Returns("canonicalstring");
            mockSignatureHelper.Setup<string>(x => x.CreateStringToSign(It.IsAny<string>())).Returns("bar");
            mockSignatureHelper.Setup<string>(x => x.GenerateSignature(It.IsAny<string>())).Returns("baz");
            mockClient.Protected().As<IClientMapping>()
                .Setup<ISignatureHelper>(c => c.signatureHelper).Returns(mockSignatureHelper.Object);
        }

        [Test]
        public void CanInstantiateWebStoreClient()
        {
            var client = new WebStoreClient(payConfig);
            Assert.NotNull(client);
        }

        [Test]
        public void CanCreateCheckoutSession()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<CheckoutSessionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<CheckoutSessionResponse>(request, headers, HttpMethod.POST, Constants.Resources.WebStore.CheckoutSessions));

            var testRequest = new CreateCheckoutSessionRequest(checkoutReviewReturnUrl: "http://localhost:5000/checkout",
                storeId: "testStoreId");
            var result = mockClient.Object.CreateCheckoutSession(testRequest);
        }

        public T AssertPreProcessRequestFlow<T>(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders, HttpMethod method, string pathContains) where T : AmazonPayResponse, new()
        {
            var expectedHeaderCount = method.Equals(HttpMethod.POST) ? 8 : 7;
            // assert values setup to this point
            Assert.NotNull(postSignedHeaders);
            // Reporting APIs have 9 headers so commenting out to serve other API Tests as well
            // Assert.AreEqual(expectedHeaderCount, postSignedHeaders.Count);
            Assert.True(postSignedHeaders.ContainsKey("user-agent"));
            Assert.True(postSignedHeaders.ContainsKey("authorization"));
            if (method.Equals(HttpMethod.POST))
            {
                Assert.True(postSignedHeaders.ContainsKey("x-amz-pay-idempotency-key"));
            }

            var authHeader = postSignedHeaders["authorization"];
            var authHeaderValues = authHeader.Split(',').ToList();
            Assert.AreEqual(3, authHeaderValues.Count);
            Assert.True(authHeaderValues[0].Contains("PublicKeyId=foo"));
            Assert.True(authHeaderValues[1].Contains("SignedHeaders="));
            Assert.True(authHeaderValues[2].Contains("Signature=baz"));

            Assert.AreEqual(apiRequest.HttpMethod, method);
            Assert.True(apiRequest.Path.ToString().Contains(pathContains));

            return new T();
        }

        [Test]
        public void CanCreateCheckoutSessionWithCustomHeader()
        {
            var myHeaderKey = "my-header-key";
            var myHeaderValue = "some string";

            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<CheckoutSessionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlowWithCustomHeader<CheckoutSessionResponse>(request, headers, myHeaderKey, HttpMethod.POST));

            var headers = new Dictionary<string, string> { { myHeaderKey, myHeaderValue } };

            var request = new CreateCheckoutSessionRequest(checkoutReviewReturnUrl: "http://localhost:5000/checkout",
                storeId: "testStoreId");

            var result = mockClient.Object.CreateCheckoutSession(request, headers);
        }

        public T AssertPreProcessRequestFlowWithCustomHeader<T>(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders, string myHeaderKey, HttpMethod method)
            where T : AmazonPayResponse, new()
        {
            var expectedHeaderCount = method.Equals(HttpMethod.POST) ? 9 : 8;
            // assert values setup to this point
            Assert.NotNull(postSignedHeaders);
            Assert.AreEqual(expectedHeaderCount, postSignedHeaders.Count);
            Assert.True(postSignedHeaders.ContainsKey("user-agent"));
            Assert.True(postSignedHeaders.ContainsKey("authorization"));

            Assert.True(postSignedHeaders.ContainsKey(myHeaderKey));
            Assert.AreEqual(apiRequest.HttpMethod, method);
            return new T();
        }

        [Test]
        public void CanGetCheckoutSession()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<CheckoutSessionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<CheckoutSessionResponse>(request, headers, HttpMethod.GET, Constants.Resources.WebStore.CheckoutSessions));
            var result = mockClient.Object.GetCheckoutSession(checkoutSessionIdToTest);
        }

        [Test]
        public void CanGetCheckoutSessionWithCustomHeader()
        {
            var myHeaderKey = "my-header-key";
            var myHeaderValue = "some string";

            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<CheckoutSessionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlowWithCustomHeader<CheckoutSessionResponse>(request, headers, myHeaderKey, HttpMethod.GET));

            var headers = new Dictionary<string, string> { { myHeaderKey, myHeaderValue } };
            var result = mockClient.Object.GetCheckoutSession(checkoutSessionIdToTest, headers);
        }

        [Test]
        public void CanUpdateCheckoutSession()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<CheckoutSessionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<CheckoutSessionResponse>(request, headers, HttpMethod.PATCH,
                    $"{Constants.Resources.WebStore.CheckoutSessions}/{checkoutSessionIdToTest}/"));

            var testRequest = new UpdateCheckoutSessionRequest()
            {
                WebCheckoutDetails = { CheckoutResultReturnUrl = "http://localhost:5000/checkout" },
                PaymentDetails =
                {
                    PaymentIntent = PaymentIntent.Authorize,
                    // todo: this indicates asynchronous processing, so need to separate indication of Auth with sync/async 
                    CanHandlePendingAuthorization = true,
                    AllowOvercharge = false,
                    ChargeAmount = {Amount = 10, CurrencyCode = Currency.USD},
                    ExtendExpiration = false,
                },
                MerchantMetadata = { MerchantReferenceId = "123456", MerchantStoreName = "MerchantStoreName", NoteToBuyer = "NoteToBuyer" }
            }; ;

            var result = mockClient.Object.UpdateCheckoutSession(checkoutSessionIdToTest, testRequest);

        }

        [Test]
        public void CanUpdateCheckoutSessionWithCancelUrl()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<CheckoutSessionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<CheckoutSessionResponse>(request, headers, HttpMethod.PATCH,
                    $"{Constants.Resources.WebStore.CheckoutSessions}/{checkoutSessionIdToTest}/"));

            var testRequest = new UpdateCheckoutSessionRequest()
            {
                WebCheckoutDetails = { CheckoutCancelUrl = "http://localhost:5000/cancel" },
                PaymentDetails =
                {
                    PaymentIntent = PaymentIntent.Authorize,
                    // todo: this indicates asynchronous processing, so need to separate indication of Auth with sync/async 
                    CanHandlePendingAuthorization = true,
                    AllowOvercharge = false,
                    ChargeAmount = {Amount = 10, CurrencyCode = Currency.USD},
                    ExtendExpiration = false,
                },
                MerchantMetadata = { MerchantReferenceId = "123456", MerchantStoreName = "MerchantStoreName", NoteToBuyer = "NoteToBuyer" }
            }; ;

            var result = mockClient.Object.UpdateCheckoutSession(checkoutSessionIdToTest, testRequest);

        }

        [Test]
        public void CanCompleteCheckoutSession()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<CheckoutSessionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<CheckoutSessionResponse>(request, headers, HttpMethod.POST,
                    $"{Constants.Resources.WebStore.CheckoutSessions}/{checkoutSessionIdToTest}/complete"));
            var testRequest = new CompleteCheckoutSessionRequest(10, Currency.USD);
            var result = mockClient.Object.CompleteCheckoutSession(checkoutSessionIdToTest, testRequest);
        }

        [Test]
        public void CanFinalizeCheckoutSession()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<CheckoutSessionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<CheckoutSessionResponse>(request, headers, HttpMethod.POST,
                    $"{Constants.Resources.WebStore.CheckoutSessions}/{checkoutSessionIdToTest}/finalize"));
            var testRequest = new FinalizeCheckoutSessionRequest(10, Currency.USD, PaymentIntent.Confirm);
            var finalizeCheckoutSessionResponse = mockClient.Object.FinalizeCheckoutSession(checkoutSessionIdToTest, testRequest);
        }

        [Test]
        public void CanGetChargePermission()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<ChargePermissionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<ChargePermissionResponse>(request, headers, HttpMethod.GET, Constants.Resources.WebStore.ChargePermissions));
            var result = mockClient.Object.GetChargePermission(checkoutSessionIdToTest);
        }

        [Test]
        public void CanGetChargePermissionWithCustomHeader()
        {
            var myHeaderKey = "my-header-key";
            var myHeaderValue = "some string";

            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<ChargePermissionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlowWithCustomHeader<ChargePermissionResponse>(request, headers, myHeaderKey, HttpMethod.GET));

            var headers = new Dictionary<string, string> { { myHeaderKey, myHeaderValue } };
            var result = mockClient.Object.GetChargePermission(checkoutSessionIdToTest, headers);
        }

        [Test]
        public void CanUpdateChargePermission()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<ChargePermissionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<ChargePermissionResponse>(request, headers, HttpMethod.PATCH,
                    $"{Constants.Resources.WebStore.ChargePermissions}/{checkoutSessionIdToTest}/"));
            var testRequest = new UpdateChargePermissionRequest()
            {
                MerchantMetadata = { MerchantReferenceId = "123456", MerchantStoreName = "MerchantStoreName", NoteToBuyer = "NoteToBuyer", CustomInformation = "CustomInformation" }
            };
            var result = mockClient.Object.UpdateChargePermission(checkoutSessionIdToTest, testRequest);
        }

        [Test]
        public void CanCloseChargePermission()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<ChargePermissionResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<ChargePermissionResponse>(request, headers, HttpMethod.DELETE,
                    $"{Constants.Resources.WebStore.ChargePermissions}/{checkoutSessionIdToTest}/close"));
            var testRequest = new CloseChargePermissionRequest("ClosureReason")
            {
                CancelPendingCharges = true
            };
            var result = mockClient.Object.CloseChargePermission(checkoutSessionIdToTest, testRequest);
        }

        [Test]
        public void CanCreateCharge()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<ChargeResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<ChargeResponse>(request, headers, HttpMethod.POST,
                    $"{Constants.Resources.WebStore.Charges}"));
            var testRequest = new Amazon.Pay.API.WebStore.Charge.CreateChargeRequest(checkoutSessionIdToTest, 10, Currency.USD)
            {
                CaptureNow = false,
                SoftDescriptor = "_softDescriptor",
                CanHandlePendingAuthorization = true
            };
            var result = mockClient.Object.CreateCharge(testRequest);
        }

        [Test]
        public void CanCaptureCharge()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<ChargeResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<ChargeResponse>(request, headers, HttpMethod.POST,
                    $"{Constants.Resources.WebStore.Charges}/{checkoutSessionIdToTest}/capture"));
            var testRequest = new CaptureChargeRequest(10, Currency.USD);
            var result = mockClient.Object.CaptureCharge(checkoutSessionIdToTest, testRequest);

        }


        [Test]
        public void CanGetCharge()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<ChargeResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<ChargeResponse>(request, headers, HttpMethod.GET, Constants.Resources.WebStore.Charges));
            var result = mockClient.Object.GetCharge(checkoutSessionIdToTest);

        }

        [Test]
        public void CanCancelCharge()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<ChargeResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<ChargeResponse>(request, headers, HttpMethod.DELETE,
                    $"{Constants.Resources.WebStore.Charges}/{checkoutSessionIdToTest}/cancel"));
            var testRequest = new CancelChargeRequest("my reason");
            var result = mockClient.Object.CancelCharge(checkoutSessionIdToTest, testRequest);

        }

        [Test]
        public void CanCreateRefund()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<RefundResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<RefundResponse>(request, headers, HttpMethod.POST,
                    $"{Constants.Resources.WebStore.Refunds}"));
            var testRequest = new Amazon.Pay.API.WebStore.Refund.CreateRefundRequest(checkoutSessionIdToTest, 10, Currency.USD)
            {
                SoftDescriptor = "_softDescriptor"
            };
            var result = mockClient.Object.CreateRefund(testRequest);
        }

        [Test]
        public void CanGetRefund()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<RefundResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<RefundResponse>(request, headers, HttpMethod.GET, Constants.Resources.WebStore.Refunds));
            var result = mockClient.Object.GetRefund(checkoutSessionIdToTest);

        }

        [Test]
        public void CanGetBuyer()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<BuyerResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<BuyerResponse>(request, headers, HttpMethod.GET, Constants.Resources.WebStore.Buyer));
            var result = mockClient.Object.GetBuyer(checkoutSessionIdToTest);

        }

        [Test]
        public void CanGenerateButtonSignatureFromCreateCheckoutSessionRequest()
        {
            var testRequest = new CreateCheckoutSessionRequest(checkoutReviewReturnUrl: "http://localhost:5000/checkout",
                storeId: "testStoreId");
            var result = mockClient.Object.GenerateButtonSignature(testRequest);

            Assert.NotNull(result);
            Assert.AreEqual("baz", result);

            // TODO: Add additional asserts - Potential refactor needed to properly mock and test signature helper
        }

        [Test]
        public void CanGenerateButtonSignatureFromSignInRequest()
        {
            var testRequest = new SignInRequest("http://localhost:5000/signin", checkoutSessionIdToTest)
            {
                SignInScopes = new SignInScope[]
                {
                    SignInScope.Email
                }
            };
            var result = mockClient.Object.GenerateButtonSignature(testRequest);

            Assert.NotNull(result);
            Assert.AreEqual("baz", result);

            // TODO: Add additional asserts - Potential refactor needed to properly mock and test signature helper
        }


        private Dictionary<string, List<string>> CreateHeaders(Uri uri)
        {
            Dictionary<string, List<string>> headers = new Dictionary<string, List<string>>();

            List<string> acceptHeaderValue = new List<string>
            {
                "application/json"
            };
            headers.Add("accept", acceptHeaderValue);

            List<string> contentHeaderValue = new List<string>
            {
                "application/json"
            };
            headers.Add("content-type", contentHeaderValue);

            List<string> regionHeaderValue = new List<string>
            {
                payConfig.Region.ToShortform()
            };
            headers.Add(Constants.Headers.Region, regionHeaderValue);

            List<string> dateHeaderValue = new List<string>
            {
                "20181013T004102Z"
            };
            headers.Add(Constants.Headers.Date, dateHeaderValue);

            List<string> hostHeaderValue = new List<string>
            {
                uri.Host
            };
            headers.Add(Constants.Headers.Host, hostHeaderValue);

            return headers;
        }

        public interface IClientMapping
        {
            ISignatureHelper signatureHelper { get; }
            Dictionary<string, string> SignRequest(ApiRequest request);

            T ProcessRequest<T>(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders)
                where T : AmazonPayResponse, new();

            HttpWebResponse SendRequest(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders);

            string BuildAuthorizationHeader(Dictionary<string, List<string>> preSignedHeaders, string signature);
        }

        // ------------ Testing the CV2 Reporting APIs ---------------
        [Test]
        public void CanGetReportsAPI()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<GetReportsResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<GetReportsResponse>(request, headers, HttpMethod.GET, Constants.Resources.WebStore.Reports));

            var result = mockClient.Object.GetReports();
        }

        [Test]
        public void CanGetReportById()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<Report>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<Report>(request, headers, HttpMethod.GET, Constants.Resources.WebStore.Reports));
            
            string reportId = "1234567890";
            var result = mockClient.Object.GetReportById(reportId);
        }

        [Test]
        public void CanGetReportDocument()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<GetReportDocumentResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<GetReportDocumentResponse>(request, headers, HttpMethod.GET, Constants.Resources.WebStore.ReportDocuments));
            
            string reportDocumentId = "1234567890";
            var result = mockClient.Object.GetReportDocument(reportDocumentId);
        }

        [Test]
        public void CanGetReportSchedules()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<GetReportSchedulesResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<GetReportSchedulesResponse>(request, headers, HttpMethod.GET, Constants.Resources.WebStore.ReportSchedules));
            
            var result = mockClient.Object.GetReportSchedules();
        }

        [Test]
        public void CanGetReportScheduleById()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<ReportSchedule>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<ReportSchedule>(request, headers, HttpMethod.GET, Constants.Resources.WebStore.ReportSchedules));
            
            string reportScheduleId = "1234567890";
            var result = mockClient.Object.GetReportScheduleById(reportScheduleId);
        }

        [Test]
        public void CanCreateReport()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<CreateReportResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<CreateReportResponse>(request, headers, HttpMethod.POST, Constants.Resources.WebStore.Reports));
            
            CreateReportRequest requestPayload = new CreateReportRequest(
                reportType: ReportTypes._GET_FLAT_FILE_OFFAMAZONPAYMENTS_ORDER_REFERENCE_DATA_,
                startTime: "20221225T150630Z",
                endTime: "20230223T111530Z"
            );

            var myHeaderKey = "my-header-key";
            var myHeaderValue = "some string";
            var headers = new Dictionary<string, string> { { myHeaderKey, myHeaderValue } };

            var result = mockClient.Object.CreateReport(requestPayload, headers);
        }

        [Test]
        public void CanCreateReportSchedule()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<CreateReportScheduleResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<CreateReportScheduleResponse>(request, headers, HttpMethod.POST, Constants.Resources.WebStore.ReportSchedules));
            
            CreateReportScheduleRequest requestPayload = new CreateReportScheduleRequest(
                reportType: ReportTypes._GET_FLAT_FILE_OFFAMAZONPAYMENTS_BILLING_AGREEMENT_DATA_,
                scheduleFrequency: ScheduleFrequency.P14D,
                nextReportCreationTime: "20221114T074550Z",
                deleteExistingSchedule: true
            );

            var myHeaderKey = "my-header-key";
            var myHeaderValue = "some string";
            var headers = new Dictionary<string, string> { { myHeaderKey, myHeaderValue } };

            var result = mockClient.Object.CreateReportSchedule(requestPayload, headers);
        }

        [Test]
        public void CanCancelReportSchedule()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<CancelReportScheduleResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<CancelReportScheduleResponse>(request, headers, HttpMethod.DELETE, Constants.Resources.WebStore.ReportSchedules));
            
            string reportScheduleId = "1234567890";
            var result = mockClient.Object.CancelReportSchedule(reportScheduleId);
        }

        // ----------- Testing the Merchant Onboarding & Account Management APIs ---------

        [Test]
        public void GivenValidRegistrationRequest_CanRegisterAmazonPayAccountSuccessfully()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<RegisterAmazonPayAccountResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<RegisterAmazonPayAccountResponse>(request, headers, HttpMethod.POST,
                    $"{Constants.Resources.WebStore.AccountManagement}"));
            var request = new RegisterAmazonPayAccountRequest(uniqueReferenceId: "ABCD1234", ledgerCurrency: LedgerCurrency.JPY)
            {
                BusinessInfo =
                {
                    BusinessType = BusinessType.INDIVIDUAL,
                    CountryOfEstablishment = "JP",
                    BusinessLegalName = "TestingSubmerchant",
                    BusinessAddress =
                    {
                        AddressLine1 = "450 Noda",
                        City = "香取市",
                        StateOrRegion = "千葉県",
                        PostalCode = "289-0314",
                        CountryCode = "JP"
                    }
                }
            };
            var result = mockClient.Object.RegisterAmazonPayAccount(request);
        }

        [Test]
        public void GivenValidUpdationRequest_CanUpdateAmazonPayAccountSuccessfully()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<UpdateAmazonPayAccountResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<UpdateAmazonPayAccountResponse>(request, headers, HttpMethod.PATCH,
                    $"{Constants.Resources.WebStore.AccountManagement}/{merchantAccountIdToTest}/"));

            var request = new UpdateAmazonPayAccountRequest()
            {
                BusinessInfo =
                {
                    BusinessType = BusinessType.CORPORATE,
                    CountryOfEstablishment = "JP",
                    BusinessLegalName = "TestingSubmerchant",
                    BusinessAddress =
                    {
                        AddressLine1 = "450 Noda",
                        City = "香取市",
                        StateOrRegion = "千葉県",
                        PostalCode = "289-0314",
                        CountryCode = "JP"
                    }
                },
                PrimaryContactPerson =
                {
                    DateOfBirth = "19971111",
                    PersonFullName = "Testing"
                }
            };

            var result = mockClient.Object.UpdateAmazonPayAccount(merchantAccountIdToTest, request);
        }

        [Test]
        public void GivenValidDeletionRequest_CanDeleteAmazonPayAccountSuccessfully()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<DeleteAmazonPayAccountResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<DeleteAmazonPayAccountResponse>(request, headers, HttpMethod.DELETE, Constants.Resources.WebStore.AccountManagement));

            var result = mockClient.Object.DeleteAmazonPayAccount(merchantAccountIdToTest);
        }
    }
}