using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Investigations.Handlers;

public class RequestInvestigationCommandHandler : IRequestInvestigationCommandHandler
{
    private readonly IRepository<InvestigationRequest, long> _investigationRequestRepository;
    private readonly IRepository<Patient, long> _patientRepository;
    private readonly IRepository<Investigation, long> _investigationRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IEncounterManager _encounterManager;

    public RequestInvestigationCommandHandler(IRepository<InvestigationRequest, long> investigationRequestRepository,
        IRepository<Patient, long> patientRepository, IRepository<Investigation, long> investigationRepository,
        IUnitOfWorkManager unitOfWorkManager, IEncounterManager encounterManager)
    {
        _investigationRequestRepository = investigationRequestRepository;
        _patientRepository = patientRepository;
        _investigationRepository = investigationRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _encounterManager = encounterManager;
    }

    public async Task Handle(List<RequestInvestigationRequest> requests)
    {
        foreach(var request in requests)
        {
            await ValidateRequest(request);
            await _investigationRequestRepository.InsertAsync(new InvestigationRequest
            {
                PatientId = request.PatientId,
                InvestigationId = request.InvestigationId,
                Urgent = request.Urgent,
                Notes = request.Notes,
                WithContrast = request.WithContrast,
                InvestigationStatus = InvestigationStatus.Requested,
                PatientEncounterId = request.EncounterId,
                ProcedureId = request.ProcedureId
            });
        }
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }

    private async Task ValidateRequest(RequestInvestigationRequest request)
    {
        if (request.EncounterId.HasValue)
            await _encounterManager.CheckEncounterExists(request.EncounterId.Value);

        _ = await _patientRepository.GetAsync(request.PatientId) ??
            throw new UserFriendlyException("Patient not found");

        _ = await _investigationRepository.GetAsync(request.InvestigationId) ??
            throw new UserFriendlyException("Investigation not found");
    }
}