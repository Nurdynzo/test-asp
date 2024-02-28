using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Patients;
using System.Linq;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientDocument.Abstraction;

namespace Plateaumed.EHR.PatientDocument.Command;

/// <summary>
/// Assigning Uploaded Files to Reviewer
/// </summary>
public class SendUploadedFilesToReviewerCommandHandler : ISendUploadedFilesToReviewerCommandHandler
{
    private readonly IRepository<PatientScanDocument,long> _patientScanDocumentRepository;
    private readonly IUnitOfWorkManager _unitOfWork;

    /// <summary>
    ///  constructor for Assigning Uploaded Files to Reviewer
    /// </summary>
    /// <param name="patientScanDocumentRepository"></param>
    /// <param name="unitOfWork"></param>
    public SendUploadedFilesToReviewerCommandHandler(IRepository<PatientScanDocument, long> patientScanDocumentRepository,
        IUnitOfWorkManager unitOfWork)
    {
        _patientScanDocumentRepository = patientScanDocumentRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the Assigning Uploaded Files to Reviewer
    /// </summary>
    /// <param name="scanDocumentIds"></param>
    /// <param name="reviewerId"></param>
    public async Task Handle(List<long> scanDocumentIds, long reviewerId)
    {
        var scanDocuments = await _patientScanDocumentRepository.GetAll()
            .Where(x => scanDocumentIds.Contains(x.Id)).ToListAsync();
        scanDocuments.ForEach(x=>x.ReviewerId = reviewerId);
        await _unitOfWork.Current.SaveChangesAsync();
        
    }
}