using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.PatientDocument.Abstraction;
using Plateaumed.EHR.PatientDocument.Dto;
using Abp.IO.Extensions;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.PatientDocument.Query;

namespace Plateaumed.EHR.PatientDocument;

/// <inheritdoc cref="Plateaumed.EHR.EHRAppServiceBase" />
[AbpAuthorize(AppPermissions.Pages_Patients)]
public class PatientDocumentUploadAppService : EHRAppServiceBase, IPatientDocumentUploadAppService
{
    private readonly IUploadReferralLetterFileUploadHandler _uploadReferralLetterFileUploadHandler;
    private readonly IUploadProfilePictureCommandHandler _uploadProfilePictureCommandHandler;
    private readonly IGetFileQueryHandler _getFileQueryHandler;
    private readonly IReviewScannedDocumentCommandHandler _reviewScannedDocumentCommandHandler; 
    private readonly IUploadScanDocumentCommandHandler _uploadScanDocumentCommandHandler;
    private readonly IGetRejectScannedDocumentsForReUploadHandler _getRejectScannedDocumentsForReUploadHandler;
    private readonly ISendUploadedFilesToReviewerCommandHandler _sendUploadedFilesToReviewerCommandHandler;
    private readonly IGetScannedDocumentsForReviewQueryHandler _getScannedDocumentsForReviewQueryHandler;
    private readonly IGetListOfReviewerForScannedDocumentQueryHandler _getListOfReviewerForScannedDocumentQueryHandler;
    private readonly IGetScannedDocumentsByPatientCodeQueryHandler _getScannedDocumentsByPatientCodeQueryHandler;
    private readonly IGetPublicFileQueryHandler _getPublicFileQueryHandler;

    /// <summary>
    /// Upload App Service
    /// </summary>
    /// <param name="uploadReferralLetterFileUploadHandler"></param>
    /// <param name="uploadProfilePictureCommandHandler"></param>
    /// <param name="getFileQueryHandler"></param>
    /// <param name="uploadScanDocumentCommandHandler"></param>
    /// <param name="sendUploadedFilesToReviewerCommandHandler"></param>
    /// <param name="getScannedDocumentsForReviewQueryHandler"></param>
    /// <param name="getListOfReviewerForScannedDocumentQueryHandler"></param>
    /// <param name="getRejectScannedDocumentsForReUploadHandler"></param>
    /// <param name="getScannedDocumentsByPatientCodeQueryHandler"></param>
    /// <param name="reviewScannedDocumentCommandHandler"></param>
    /// <param name="getPublicFileQueryHandler"></param>
    public PatientDocumentUploadAppService(IUploadReferralLetterFileUploadHandler uploadReferralLetterFileUploadHandler, 
        IUploadProfilePictureCommandHandler uploadProfilePictureCommandHandler, 
        IGetFileQueryHandler getFileQueryHandler, 
        IUploadScanDocumentCommandHandler uploadScanDocumentCommandHandler,
        ISendUploadedFilesToReviewerCommandHandler sendUploadedFilesToReviewerCommandHandler, 
        IGetScannedDocumentsForReviewQueryHandler getScannedDocumentsForReviewQueryHandler,
        IGetRejectScannedDocumentsForReUploadHandler getRejectScannedDocumentsForReUploadHandler,
        IGetScannedDocumentsByPatientCodeQueryHandler getScannedDocumentsByPatientCodeQueryHandler,
        IGetListOfReviewerForScannedDocumentQueryHandler getListOfReviewerForScannedDocumentQueryHandler, 
        IReviewScannedDocumentCommandHandler reviewScannedDocumentCommandHandler,
        IGetPublicFileQueryHandler getPublicFileQueryHandler)
    {
        _uploadReferralLetterFileUploadHandler = uploadReferralLetterFileUploadHandler;
        _uploadProfilePictureCommandHandler = uploadProfilePictureCommandHandler;
        _getFileQueryHandler = getFileQueryHandler;
        _sendUploadedFilesToReviewerCommandHandler = sendUploadedFilesToReviewerCommandHandler;
        _getScannedDocumentsForReviewQueryHandler = getScannedDocumentsForReviewQueryHandler;
        _getRejectScannedDocumentsForReUploadHandler = getRejectScannedDocumentsForReUploadHandler;
        _getListOfReviewerForScannedDocumentQueryHandler = getListOfReviewerForScannedDocumentQueryHandler;
        _reviewScannedDocumentCommandHandler = reviewScannedDocumentCommandHandler;
        _getPublicFileQueryHandler = getPublicFileQueryHandler;
        _uploadScanDocumentCommandHandler = uploadScanDocumentCommandHandler;
        _getScannedDocumentsByPatientCodeQueryHandler = getScannedDocumentsByPatientCodeQueryHandler;
    }

    /// <inheritdoc />
    public async Task<ReferralLetterUploadRequest> UploadReferralLetterFile([FromForm] ReferralLetterUploadRequest input)
    {
        return await _uploadReferralLetterFileUploadHandler.Handle(input);
    }
    
    /// <summary>
    /// Uploads profile picture
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public  async Task<FileResult> UploadPicture([FromForm] ProfilePictureUploadRequest request)
    {
        var (stream, fileType) =  await _uploadProfilePictureCommandHandler.Handle(request);
        
        return new FileContentResult(stream.GetAllBytes(), fileType);
        
    }
    
    /// <summary>
    /// Assigns uploaded files to the reviewer
    /// </summary>
    /// <param name="scanDocumentIds"></param>
    /// <param name="reviewerId"></param>
    public async Task AssignUploadedFilesToReviewer(List<long> scanDocumentIds, long reviewerId)
    {
        await _sendUploadedFilesToReviewerCommandHandler.Handle(scanDocumentIds, reviewerId);
    }

    /// <inheritdoc/>
    [AbpAuthorize(AppPermissions.Pages_ScanDocument_Review)]
    public async Task ReviewScannedDocument([FromForm] ReviewedScannedDocumentRequest request)
    {
        await _reviewScannedDocumentCommandHandler.Handle(request);
    }
    
    /// <summary>
    /// Get Rejected Scanned Documents for re-upload
    /// </summary>
    /// <returns></returns>
    public async Task<List<GetScannedDocumentsForReviewResponse>> GetRejectedScannedDocumentsForReview()
    {
        return await _getRejectScannedDocumentsForReUploadHandler.Handle();
    }

    /// <summary>
    /// Get file by Id
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    public  async Task<FileResult> GetDocumentById(Guid fileId)
    {
        var (stream, fileType) =  await _getFileQueryHandler.Handle(fileId);
        
        return new FileContentResult(stream.GetAllBytes(), fileType)
        {
            EnableRangeProcessing = true
        };
        
    }
    
    /// <summary>
    /// Get public file url
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    public async Task<string> GetPublicDocumentUrl(string fileId)
    {
        return await _getPublicFileQueryHandler.Handle(fileId);
    }
    
    /// <summary>
    /// Get file in base 64 string by the document Id
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    public  async Task<string> GetDocumentInBaseStringById(Guid fileId)
    {
        var (stream, _) =  await _getFileQueryHandler.Handle(fileId);

        var byteArray = stream.GetAllBytes(); 
        return Convert.ToBase64String(byteArray);

    }
    
    /// <summary>
    /// Upload Scan Document for a patient with file name pattern
    /// <example>
    /// filename: patientcode#firstname#lastname
    /// </example>
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<PatientScanDocumentUploadRequest> UploadScanDocument([FromForm] PatientScanDocumentUploadRequest request)
    {
        return await _uploadScanDocumentCommandHandler.Handle(request);
    }
    
   /// <summary>
   /// Get list of scanned documents for the current login user for review.
   /// If showOnlyRejectedDocuments is true, only rejected documents will be returned
   /// </summary>
   /// <param name="showOnlyRejectedDocuments"></param>
   /// <returns></returns>
    public async Task<List<GetScannedDocumentsForReviewResponse>> GetScannedDocumentsForReview(bool showOnlyRejectedDocuments = false)
    { 
        return await _getScannedDocumentsForReviewQueryHandler.Handle(showOnlyRejectedDocuments);
    }

    /// <inheritdoc/>
    public async Task<List<Guid>> GetScannedDocumentsByPatientCode(string patientCode)
    {
        return await _getScannedDocumentsByPatientCodeQueryHandler.Handle(patientCode);
    }


   /// <inheritdoc />
   public async Task<List<ScannedDocumentReviewerQueryResponse>> GetListOfReviewerForScannedDocument()
   {
       return await _getListOfReviewerForScannedDocumentQueryHandler.Handle();
   }
    
}