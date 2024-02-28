using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.PatientDocument.Dto;

namespace Plateaumed.EHR.PatientDocument.Abstraction;

/// <inheritdoc />
public interface IPatientDocumentUploadAppService : IApplicationService
{
    /// <summary>
    /// Uploads referral letter
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ReferralLetterUploadRequest> UploadReferralLetterFile(ReferralLetterUploadRequest input);

    /// <summary>
    /// Uploads profile picture
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<FileResult> UploadPicture(ProfilePictureUploadRequest request);

    /// <summary>
    /// Get rejected scanned documents for re-upload
    /// </summary>
    /// <returns></returns>
    Task<List<GetScannedDocumentsForReviewResponse>> GetRejectedScannedDocumentsForReview();

    /// <summary>
    /// Get file by Id
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    Task<FileResult> GetDocumentById(Guid fileId);

    /// <summary>
    /// Accept scanned document review from a reviewer
    /// </summary>
    /// <param name="request"></param>
    Task ReviewScannedDocument([FromForm] ReviewedScannedDocumentRequest request);
    
    /// <summary>
    /// Uploads scan document
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<PatientScanDocumentUploadRequest> UploadScanDocument([FromForm] PatientScanDocumentUploadRequest request);

    /// <summary>
    /// Assigns uploaded files to the reviewer
    /// </summary>
    /// <param name="scanDocumentIds"></param>
    /// <param name="reviewerId"></param>
    /// <returns></returns>
    Task AssignUploadedFilesToReviewer(List<long> scanDocumentIds, long reviewerId);

    /// <summary>
    /// Get file in base 64 string by the document Id
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    Task<string> GetDocumentInBaseStringById(Guid fileId);

    /// <summary>
    /// Get list of scanned documents for the current login user for review.
    /// If showOnlyRejectedDocuments is true, only rejected documents will be returned
    /// </summary>
    /// <param name="showOnlyRejectedDocuments"></param>
    /// <returns></returns>
    Task<List<GetScannedDocumentsForReviewResponse>> GetScannedDocumentsForReview(bool showOnlyRejectedDocuments = false);
    
    /// <summary>
    /// Get all approved scanned documents by patentCode.
    /// </summary>
    /// <param name="patientCode"></param>
    /// <returns></returns>
    Task<List<Guid>> GetScannedDocumentsByPatientCode(string patientCode); 
   
    /// <summary>
    /// Get list of reviewers for scanned documents
    /// </summary>
    /// <returns></returns>
    Task<List<ScannedDocumentReviewerQueryResponse>> GetListOfReviewerForScannedDocument();

    Task<string> GetPublicDocumentUrl(string fileId);
}