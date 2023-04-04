using System.Collections.Generic;
using Amazon.Pay.API.WebStore.Reports;
using Amazon.Pay.API.WebStore.Types;
using Amazon.Pay.API.WebStore.Interfaces;
using Moq;
using NUnit.Framework;

namespace Amazon.Pay.API.SDK.Tests.WebStore.Interfaces
{
     // This is a test fixture to ensure that a method can be mocked and helps ensure it is on the interface.
     [TestFixture]
     public class IReportingClientTests
     {
          private Mock<IReportsClient> mockReportingClient;

          [OneTimeSetUp]
          public void Init()
          {
               this.mockReportingClient = new Mock<IReportsClient>(MockBehavior.Strict);
          }

          [SetUp]
          public void Setup()
          {
               this.mockReportingClient.Reset();
          }


          // ------------ Testing the CV2 Reporting APIs ---------------

          [Test]
          public void GetReportCanBeMocked()
          {
               var response = new GetReportsResponse();
               this.mockReportingClient.Setup(mwsc => mwsc.GetReports(It.IsAny<GetReportsRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(response);

               var result = this.mockReportingClient.Object.GetReports(new GetReportsRequest(), new Dictionary<string, string>());

               Assert.That(result, Is.EqualTo(response));
               this.mockReportingClient.Verify(mwsc => mwsc.GetReports(It.IsAny<GetReportsRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
          }

          [Test]
          public void GetReportbyIDCanBeMocked()
          {
               var response = new Report();
               this.mockReportingClient.Setup(mwsc => mwsc.GetReportById(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(response);

               var result = this.mockReportingClient.Object.GetReportById("1234567890", new Dictionary<string, string>());

               Assert.That(result, Is.EqualTo(response));
               this.mockReportingClient.Verify(mwsc => mwsc.GetReportById(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
          }

          [Test]
          public void GetReportDocumentCanBeMocked()
          {
               var response = new GetReportDocumentResponse();
               this.mockReportingClient.Setup(mwsc => mwsc.GetReportDocument(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(response);

               var result = this.mockReportingClient.Object.GetReportDocument("1234567890", new Dictionary<string, string>());

               Assert.That(result, Is.EqualTo(response));
               this.mockReportingClient.Verify(mwsc => mwsc.GetReportDocument(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
          }

          [Test]
          public void GetReportSchedulesCanBeMocked()
          {
               var response = new GetReportSchedulesResponse();
               this.mockReportingClient.Setup(mwsc => mwsc.GetReportSchedules(It.IsAny<GetReportSchedulesRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(response);

               var result = this.mockReportingClient.Object.GetReportSchedules(new GetReportSchedulesRequest(new List<ReportTypes>()), new Dictionary<string, string>());

               Assert.That(result, Is.EqualTo(response));
               this.mockReportingClient.Verify(mwsc => mwsc.GetReportSchedules(It.IsAny<GetReportSchedulesRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
          }

          [Test]
          public void GetReportScheduleByIdCanBeMocked()
          {
               var response = new ReportSchedule();
               this.mockReportingClient.Setup(mwsc => mwsc.GetReportScheduleById(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(response);

               var result = this.mockReportingClient.Object.GetReportScheduleById("1234567890", new Dictionary<string, string>());

               Assert.That(result, Is.EqualTo(response));
               this.mockReportingClient.Verify(mwsc => mwsc.GetReportScheduleById(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
          }

          [Test]
          public void CreateReportCanBeMocked()
          {
               var response = new CreateReportResponse();
               this.mockReportingClient.Setup(mwsc => mwsc.CreateReport(It.IsAny<CreateReportRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(response);

               var result = this.mockReportingClient.Object.CreateReport(new CreateReportRequest(ReportTypes._GET_FLAT_FILE_OFFAMAZONPAYMENTS_ORDER_REFERENCE_DATA_, "startTime", "endTime"), new Dictionary<string, string>());

               Assert.That(result, Is.EqualTo(response));
               this.mockReportingClient.Verify(mwsc => mwsc.CreateReport(It.IsAny<CreateReportRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
          }

          [Test]
          public void CreateReportScheduleCanBeMocked()
          {
               var response = new CreateReportScheduleResponse();
               this.mockReportingClient.Setup(mwsc => mwsc.CreateReportSchedule(It.IsAny<CreateReportScheduleRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(response);

               var result = this.mockReportingClient.Object.CreateReportSchedule(new CreateReportScheduleRequest(ReportTypes._GET_FLAT_FILE_OFFAMAZONPAYMENTS_ORDER_REFERENCE_DATA_, ScheduleFrequency.P14D, "nextReportCreationTime"), new Dictionary<string, string>());

               Assert.That(result, Is.EqualTo(response));
               this.mockReportingClient.Verify(mwsc => mwsc.CreateReportSchedule(It.IsAny<CreateReportScheduleRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
          }

          [Test]
          public void CancelReportScheduleCanBeMocked()
          {
               var response = new CancelReportScheduleResponse();
               this.mockReportingClient.Setup(mwsc => mwsc.CancelReportSchedule(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(response);

               var result = this.mockReportingClient.Object.CancelReportSchedule("1234567890", new Dictionary<string, string>());

               Assert.That(result, Is.EqualTo(response));
               this.mockReportingClient.Verify(mwsc => mwsc.CancelReportSchedule(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
          }
     }

}