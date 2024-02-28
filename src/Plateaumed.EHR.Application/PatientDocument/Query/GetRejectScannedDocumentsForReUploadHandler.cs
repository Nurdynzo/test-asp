using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Plateaumed.EHR.Patients;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientDocument.Abstraction;

namespace Plateaumed.EHR.PatientDocument.Query;

public class GetRejectScannedDocumentsForReUploadHandler : IGetRejectScannedDocumentsForReUploadHandler
{
    private readonly IRepository<PatientScanDocument,long> _patientScanDocumentRepository;
    private readonly IRepository<Patient,long> _patientRepository;
    private readonly IAbpSession _abpSession;
    private readonly IRepository<PatientCodeMapping,long> _patientCodeMappingRepository;

    public GetRejectScannedDocumentsForReUploadHandler(
        IRepository<PatientScanDocument, long> patientScanDocumentRepository,
        IRepository<Patient, long> patientRepository,
        IAbpSession abpSession, 
        IRepository<PatientCodeMapping, long> patientCodeMappingRepository)
    {
        _patientScanDocumentRepository = patientScanDocumentRepository;
        _patientRepository = patientRepository;
        _abpSession = abpSession;
        _patientCodeMappingRepository = patientCodeMappingRepository;
    }

    /// <summary>
    /// Get reject scanned documents for re-upload handler
    /// </summary>
    /// <returns></returns>
    public async Task<List<GetScannedDocumentsForReviewResponse>> Handle()
    {
        var query = from d in _patientScanDocumentRepository.GetAll()
            join pc in _patientCodeMappingRepository.GetAll() on d.PatientCode equals pc.PatientCode
            join p in _patientRepository.GetAll() on pc.PatientId equals p.Id
            where d.CreatorUserId == _abpSession.UserId  && d.IsApproved == false
            select new GetScannedDocumentsForReviewResponse
            {
                Id = d.Id,
                PatientFullName = $"{p.FirstName} {p.LastName}",
                PatientCode = pc.PatientCode,
                PictureId = p.ProfilePictureId,
                PictureUrl = p.PictureUrl,
                Gender = p.GenderType.ToString(),
                DateOfBirth = p.DateOfBirth,
                FileId = d.FileId,
                IsApproved = d.IsApproved,
                ReviewerNote = d.ReviewNote
            };
        
        return await query.ToListAsync();
                    
                                               
    }
}