using Amazon.Pay.API.WebStore.Reports;
using Amazon.Pay.API.WebStore.Types;
using NUnit.Framework;
using System.Collections.Generic;

namespace Amazon.Pay.API.SDK.Tests.WebStore.Reports
{
    [TestFixture]
    public class ReportAPIsTest
    {
        [Test]
        public void CanConstructGetReportsRequestPayload()
        {
            // act
            GetReportsRequest request = new GetReportsRequest(new List<ReportTypes> {ReportTypes._GET_FLAT_FILE_OFFAMAZONPAYMENTS_ORDER_REFERENCE_DATA_}, new List<ProcessingStatus> {ProcessingStatus.COMPLETED}, null, null, 4);

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.ReportTypes);
            Assert.AreEqual("_GET_FLAT_FILE_OFFAMAZONPAYMENTS_ORDER_REFERENCE_DATA_", string.Join(",", request.ReportTypes));
            Assert.AreEqual("COMPLETED", string.Join(",", request.ProcessingStatuses));
            Assert.AreEqual(4, request.PageSize);
        }

        [Test]
        public void CanConstructGetReportSchedulesRequestPayload()
        {
            // act
            var request = new GetReportSchedulesRequest(new List<ReportTypes> { ReportTypes._GET_FLAT_FILE_OFFAMAZONPAYMENTS_ORDER_REFERENCE_DATA_} );

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.ReportTypes);
            Assert.AreEqual("_GET_FLAT_FILE_OFFAMAZONPAYMENTS_ORDER_REFERENCE_DATA_", string.Join(",", request.ReportTypes));
        }

        [Test]
        public void CanConstructCreateReportPayload()
        {
            // act
            var request = new CreateReportRequest(ReportTypes._GET_FLAT_FILE_OFFAMAZONPAYMENTS_ORDER_REFERENCE_DATA_, "20221225T150630Z", "20230223T111530Z");

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.ReportType);
            Assert.AreEqual(ReportTypes._GET_FLAT_FILE_OFFAMAZONPAYMENTS_ORDER_REFERENCE_DATA_, request.ReportType);
            Assert.AreEqual("20221225T150630Z", request.StartTime);
		    Assert.AreEqual("20230223T111530Z", request.EndTime);
        }

        [Test]
        public void CanConstructCreateReportSchedulePayload()
        {
            // act
            var request = new CreateReportScheduleRequest(ReportTypes._GET_FLAT_FILE_OFFAMAZONPAYMENTS_BILLING_AGREEMENT_DATA_, ScheduleFrequency.P14D, "20221114T074550Z", true);

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.ReportType);
            Assert.AreEqual(ReportTypes._GET_FLAT_FILE_OFFAMAZONPAYMENTS_BILLING_AGREEMENT_DATA_, request.ReportType);
            Assert.AreEqual(ScheduleFrequency.P14D, request.ScheduleFrequency);
            Assert.AreEqual("20221114T074550Z", request.NextReportCreationTime);
            Assert.AreEqual(true, request.DeleteExistingSchedule);
        }
    }
}
