using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.PatientDocument.Abstraction;
using Plateaumed.EHR.PatientDocument.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Upload.Abstraction;

namespace Plateaumed.EHR.PatientDocument.Command;

/// <summary>
/// Upload Scan Document Command Handler
/// </summary>
public class UploadScanDocumentCommandHandler : IUploadScanDocumentCommandHandler
{
    private readonly IUploadService _uploadService;
    private readonly IRepository<PatientScanDocument,long> _patientScanDocumentRepository;
    private readonly IAbpSession _abpSession;
    private readonly IUnitOfWorkManager _unitOfWork;
    private const string DocumentType = "ScanDocument";
    private const string AcceptedFileExtensions = ".pdf";
    
    /// <summary>
    /// constructor Upload Scan Document Command Handler
    /// </summary>
    /// <param name="uploadService"></param>
    /// <param name="patientScanDocumentRepository"></param>
    /// <param name="abpSession"></param>
    /// <param name="unitOfWork"></param>
    public UploadScanDocumentCommandHandler(
        IUploadService uploadService, 
        IRepository<PatientScanDocument, long> patientScanDocumentRepository, 
        IAbpSession abpSession, 
        IUnitOfWorkManager unitOfWork)
    {
        _uploadService = uploadService;
        _patientScanDocumentRepository = patientScanDocumentRepository;
        _abpSession = abpSession;
        _unitOfWork = unitOfWork;
    }
    
    
    /// <summary>
    /// Handles the UploadScanDocument
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task<PatientScanDocumentUploadRequest> Handle(PatientScanDocumentUploadRequest request)
    {
        if (!request.IsUpdate)
        {
            await UploadNewFile(request);
        }
        else
        {
            await   UpdateUploadNewFile(request);
        }
       

        return request;
    }

    private async Task UpdateUploadNewFile(PatientScanDocumentUploadRequest request)
    {
        var existingDocument = await GetExistingFile(request);
        existingDocument.CreatorUserId = _abpSession.UserId.GetValueOrDefault();
        existingDocument.IsApproved = null;
        var fileName = request.File.FileName;
        ValidateFileFormat(fileName);
        await _uploadService.UpdateFile(existingDocument.FileId, request.File.OpenReadStream());
        await _patientScanDocumentRepository.UpdateAsync(existingDocument);
        await _unitOfWork.Current.SaveChangesAsync();
        request.Id = existingDocument.Id;
    }

    private async Task<PatientScanDocument> GetExistingFile(PatientScanDocumentUploadRequest request)
    {
        if (request.FileId is null)
        {
            throw new UserFriendlyException("File Id cannot be null");
        }

        var existingDocument = await _patientScanDocumentRepository.FirstOrDefaultAsync(x => x.FileId == request.FileId);
        if (existingDocument is null)
        {
            throw new UserFriendlyException("File does not exist");
        }

        return existingDocument;
    }

    private async Task UploadNewFile(PatientScanDocumentUploadRequest request)
    {
        request.FileId = Guid.NewGuid();
        var fileName = request.File.FileName;
        ValidateFileFormat(fileName);
        var fileMetaData = ExtractMetaDataFromFileName(fileName);

        await _uploadService.UploadFile(request.FileId.GetValueOrDefault(), request.File.OpenReadStream(),
            GetMetaData(request, fileMetaData));
        var patientScanDocument = CreatePatientScanDocumentInstance(request, fileMetaData[0], fileName);
        await _patientScanDocumentRepository.InsertAsync(patientScanDocument);
        await _unitOfWork.Current.SaveChangesAsync();

        request.Id = patientScanDocument.Id;
    }

    private void ValidateFileFormat(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new UserFriendlyException("File name cannot be empty");
        }

        if (!fileName.ToLower().EndsWith(AcceptedFileExtensions))
        {
            throw new UserFriendlyException("Invalid file format. Only PDF files are allowed");
        }
    }

    private static string[] ExtractMetaDataFromFileName(string fileName)
    {
        var fileSplit = fileName.Split("#");
        if (fileSplit.Length is 0 or < 3)
        {
            throw new UserFriendlyException("Invalid file name format, please use the format: PatientCode#FirstName#LastName");
        }

        return fileSplit;
    }

    private PatientScanDocument CreatePatientScanDocumentInstance(PatientScanDocumentUploadRequest request,
        string patientCode, string fileName)
    {
        var patientScanDocument = new PatientScanDocument()
        {
            FileId = request.FileId.GetValueOrDefault(),
            PatientCode = patientCode,
            FileName = fileName,
            TenantId = _abpSession.TenantId.GetValueOrDefault(),
            AssigneeId = _abpSession.UserId.GetValueOrDefault(),
        };
        return patientScanDocument;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="metaData"></param>
    /// <returns></returns>
    private IDictionary<string, string> GetMetaData(PatientScanDocumentUploadRequest request, string[] metaData)
    {
        return new Dictionary<string, string>
        {
            { "FileType", request.File.ContentType },
            { "PatientCode", metaData[0] },
            { "FirstName", metaData[1] },
            { "LastName", metaData[2] },
            { "DocumentType", DocumentType }
        };
    }
}