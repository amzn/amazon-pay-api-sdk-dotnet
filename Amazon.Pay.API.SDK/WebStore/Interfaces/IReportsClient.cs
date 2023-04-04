using System.Collections.Generic;
using Amazon.Pay.API.WebStore.Reports;
using Amazon.Pay.API.WebStore.Types;

namespace Amazon.Pay.API.WebStore.Interfaces
{
     public interface IReportsClient : IClient
     {
          // ----------------------------------- CV2 REPORTING APIS -----------------------------------

          /// <summary>
          /// Returns report details for the reports that match the filters that you specify.
          /// </summary>
          /// <param name="queryParamters">The queryParameters for the request.</param>
          /// <param name="headers"></param>
          /// <returns>Details for the reports that match the filters that you specify.</returns>
          GetReportsResponse GetReports(GetReportsRequest queryParamters = null, Dictionary<string, string> headers = null);

          /// <summary>
          /// Returns report details for the given reportId.
          /// </summary>
          /// <param name="reportId">The Report Id for filtering.</param>
          /// /// <param name="headers"></param>
          /// <returns>Report details for the given reportId.</returns>
          Report GetReportById(string reportId, Dictionary<string, string> headers = null);

          /// <summary>
          /// Returns the pre-signed S3 URL for the report. The report can be downloaded using this URL.
          /// </summary>
          /// <param name="reportDocumentId">The Report Document Id for filtering.</param>
          /// <param name="headers"></param>
          /// <returns>Pre-signed S3 URL for the report.</returns>
          GetReportDocumentResponse GetReportDocument(string reportDocumentId, Dictionary<string, string> headers = null);

          /// <summary>
          /// Returns report schedule details that match the filters criteria specified.
          /// </summary>
          /// <param name="reportTypes">The Report Types for filtering.</param>
          /// <param name="headers"></param>
          /// <returns>Report schedule details that match the filters criteria specified.</returns>
          GetReportSchedulesResponse GetReportSchedules(GetReportSchedulesRequest reportTypes = null, Dictionary<string, string> headers = null);

          /// <summary>
          /// Returns the report schedule details that match the given ID.
          /// </summary>
          /// <param name="reportScheduleId">The Report Schedule Id for filtering</param>
          /// <param name="headers"></param>
          /// <returns>Report schedule details that match the given ID.</returns>
          ReportSchedule GetReportScheduleById(string reportScheduleId, Dictionary<string, string> headers = null);

          /// <summary>
          /// Submits a request to generate a report based on the reportType and date range specified.
          /// </summary>
          /// <param name="requestPayload">The payload for creating a Report.</param>
          /// <param name="headers"></param>
          /// <returns>Report ID which is created via the Request Payload</returns>
          CreateReportResponse CreateReport(CreateReportRequest requestPayload, Dictionary<string, string> headers);

          /// <summary>
          /// Creates a report schedule for the given reportType. Only one schedule per report type allowed.
          /// </summary>
          /// <param name="requestPayload">The payload for creating a Report Schedule.</param>
          /// <param name="headers"></param>
          /// <returns>Report Schedule ID which is created via the Request Payload</returns>
          CreateReportScheduleResponse CreateReportSchedule(CreateReportScheduleRequest requestPayload, Dictionary<string, string> headers);

          /// <summary>
          /// Cancels the report schedule with the given reportScheduleId.
          /// </summary>
          /// <param name="reportScheduleId">The Report Schedule ID for cancelling a Report.</param>
          /// <param name="headers"></param>
          /// <returns>HTTP Response after cancelling the Report Schedule mentioned</returns>
          CancelReportScheduleResponse CancelReportSchedule(string reportScheduleId, Dictionary<string, string> headers = null);
     }
}