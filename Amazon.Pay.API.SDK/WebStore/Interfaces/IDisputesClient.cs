using System.Collections.Generic;
using Amazon.Pay.API.WebStore.Dispute;
using Amazon.Pay.API.WebStore.File;
using Amazon.Pay.API.WebStore.Types;

namespace Amazon.Pay.API.WebStore.Interfaces
{
    public interface IDisputesClient : IClient
    {
        // ----------------------------------- Dispute APIs -----------------------------------

        /// <summary>
        /// The createDispute operation is used to notify Amazon of a newly created chargeback dispute by a buyer on a transaction processed by the PSP (Payment Service Provider), ensuring the dispute is properly accounted for in the Amazon Pay systems.
        /// </summary>
        /// <param name="request">The payload for creating a dispute.</param>
        /// <param name="headers"></param>
        /// <returns>DisputeResponse response</returns>
        DisputeResponse CreateDispute(CreateDisputeRequest request, Dictionary<string, string> headers = null);

        /// <summary>
        /// The getDispute operation is used to retrieve details of a chargeback dispute associated with a specific order
        /// </summary>
        /// <param name="disputeId"> The unique Dispute ID provided by Amazon Pay for the chargeback case</param>
        /// <param name="headers"></param>
        /// <returns>DisputeResponse response</returns>
        DisputeResponse GetDispute(string disputeId, Dictionary<string, string> headers = null);

        /// <summary>
        /// The updateDispute operation is used to notify Amazon of the closure status of a chargeback dispute initiated by a buyer for orders processed by a partner PSP (Payment Service Provider), ensuring proper accounting within the Amazon systems.
        /// </summary>
        /// <param name="disputeId">Amazon Pay disputeId received in the Create Dispute response</param>
        /// <param name="request">The payload for updating dispute.</param>
        /// <param name="headers"></param>
        /// <returns>DisputeResponse response</returns>
        DisputeResponse UpdateDispute(string disputeId, UpdateDisputeRequest request, Dictionary<string, string> headers = null);

        /// <summary>
        /// The contestDispute operation is used by the partner, on behalf of the merchant, to formally contest a dispute  managed by Amazon, requiring the submission of necessary evidence files within the specified Dispute Window (11 days for Chargeback, 7 days for A-Z Claims).
        /// </summary>
        /// <param name="disputeId">Amazon Pay disputeId received in the Create Dispute response</param>
        /// <param name="request">The payload for updating contest dispute.</param>
        /// <param name="headers"></param>
        /// <returns>DisputeResponse response</returns>
        DisputeResponse ContestDispute(string disputeId, ContestDisputeRequest request, Dictionary<string, string> headers = null);

        // ----------------------------------- File APIs -----------------------------------

        /// <summary>
        /// The uploadFile operation is utilised by PSPs (Payment Service Provider) to upload file-based evidence when a merchant contests a dispute, providing the necessary reference ID to the evidence file as part of the Update Dispute API process
        /// </summary>
        /// <param name="request">The payload for upload file-based evidence.</param>
        /// <param name="headers"></param>
        /// <returns>FileResponse response</returns>
        FileResponse UploadFile(UploadFileRequest request, Dictionary<string, string> headers = null);
    }
}