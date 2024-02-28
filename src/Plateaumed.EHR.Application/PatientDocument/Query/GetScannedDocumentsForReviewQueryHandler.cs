using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientDocument.Abstraction;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientDocument.Query;

/// <summary>
/// Get list of scanned documents for review handler
/// </summary>
public class GetScannedDocumentsForReviewQueryHandler : IGetScannedDocumentsForReviewQueryHandler
{
    private readonly IRepository<PatientScanDocument,long> _patientScanDocumentRepository;
    private readonly IRepository<Patient,long> _patientRepository;
    private readonly IAbpSession _abpSession;
    private readonly IRepository<PatientCodeMapping,long> _patientCodeMappingRepository;

    /// <summary>
    /// Constructor for GetScannedDocumentsForReviewQueryHandler
    /// </summary>
    /// <param name="patientScanDocumentRepository"></param>
    /// <param name="patientRepository"></param>
    /// <param name="abpSession"></param>
    /// <param name="patientCodeMappingRepository"></param>
    public GetScannedDocumentsForReviewQueryHandler(
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
    ///  Handler for GetScannedDocumentsForReviewQuery   
    /// </summary>
    /// <param name="showOnlyRejectedDocuments"></param>
    /// <returns></returns>
    public async Task<List<GetScannedDocumentsForReviewResponse>> Handle(bool showOnlyRejectedDocuments = false)
    {
        var query = from d in _patientScanDocumentRepository.GetAll()
                    join pc in _patientCodeMappingRepository.GetAll() on d.PatientCode equals pc.PatientCode
                    join p in _patientRepository.GetAll() on pc.PatientId equals p.Id
                    where d.ReviewerId == _abpSession.UserId 
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
                        IsApproved = d.IsApproved
                        
                    };
        query = showOnlyRejectedDocuments ? 
            query.Where(d => d.IsApproved == false) : 
            query.Where(d => d.IsApproved == null);
        
        return await query.ToListAsync();
                    
                                               
    }
}