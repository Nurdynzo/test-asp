using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Investigations.Handlers;

public class RecordInvestigationCommandHandler : IRecordInvestigationCommandHandler
{
    private readonly IRepository<InvestigationResult, long> _repository;
    private readonly IObjectMapper _objectMapper;
    private readonly IRepository<InvestigationRequest, long> _investigationRequestRepository;
    private readonly IRepository<Investigation, long> _investigationRepository;
    private readonly IRepository<Patient, long> _patientRepository;
    private readonly IEncounterManager _encounterManager;
    private readonly IRepository<InvestigationResultReviewer, long> _reviewer;
    private readonly IRepository<InvestigationComponentResult, long> _investigationComponents;
    private readonly IAbpSession _abpSession;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IRepository<StaffMember, long> _staffMember;

    public RecordInvestigationCommandHandler(IRepository<InvestigationResult, long> repository,
        IObjectMapper objectMapper, IRepository<InvestigationRequest, long> investigationRequestRepository,
        IRepository<Patient, long> patientRepository, IRepository<Investigation, long> investigationRepository,
        IEncounterManager encounterManager,
        IRepository<InvestigationResultReviewer, long> reviewer,
        IRepository<InvestigationComponentResult, long> investigationComponents,
        IAbpSession abpSession,
        IUnitOfWorkManager unitOfWorkManager,
        IRepository<StaffMember, long> staffMember)
    {
        _repository = repository;
        _objectMapper = objectMapper;
        _investigationRequestRepository = investigationRequestRepository;
        _patientRepository = patientRepository;
        _encounterManager = encounterManager;
        _investigationRepository = investigationRepository;
        _reviewer = reviewer;
        _investigationComponents = investigationComponents;
        _abpSession = abpSession;
        _unitOfWorkManager = unitOfWorkManager;
        _staffMember = staffMember;
    }

    public async Task Handle(RecordInvestigationRequest request, long facilityId)
    {
        var investigationRequest = await ValidateInputsAndReturnInvestigationRequest(request);

        var tenantId = _abpSession.TenantId ?? throw new UserFriendlyException("Tenant not found");

        var mappedRequest = _objectMapper.Map<InvestigationResult>(request);

        var savedResult = await _repository.InsertAsync(mappedRequest);

        await _unitOfWorkManager.Current.SaveChangesAsync();     

        await InsertInvestigationReviewer(new InvestigationResultReviewer
        {
            TenantId = tenantId,
            ReviewerId = request.ReviewerId,
            CreatorUserId = _abpSession.UserId,
            FacilityId = facilityId,
            InvestigationResultId = savedResult.Id
        });

        var investigation = await GetInvestigation(request.InvestigationId);

        await UpdateInvestigationRequest(investigationRequest, investigation);
    }

    private async Task UpdateInvestigationRequest(InvestigationRequest investigationRequest, Investigation investigation)
    {
        if (!string.IsNullOrWhiteSpace(investigationRequest.Investigation.Type) &&
           investigation.Type.Contains(InvestigationTypes.Electrophysiology) || investigation.Type.Contains(InvestigationTypes.RadiologyAndPulm))
            investigationRequest.InvestigationStatus = Misc.InvestigationStatus.ImageReady;
        else
            investigationRequest.InvestigationStatus = Misc.InvestigationStatus.ResultReady;

        await _investigationRequestRepository.UpdateAsync(investigationRequest);
    }

    private async Task InsertInvestigationReviewer(InvestigationResultReviewer request) =>
        await _reviewer.InsertAsync(request);

    private async Task<InvestigationRequest> ValidateInputsAndReturnInvestigationRequest(RecordInvestigationRequest request)
    {
        var investigationRequest = await _investigationRequestRepository.GetAsync(request.InvestigationRequestId) ??
           throw new UserFriendlyException("Investigation request not found");

        _ = await _patientRepository.GetAsync(request.PatientId) ?? throw new UserFriendlyException("Patient not found");

        if (request.ReviewerId.HasValue)
            _ = await _staffMember.GetAsync(request.ReviewerId.Value) ?? throw new UserFriendlyException("Staff Member not found");

        await _encounterManager.CheckEncounterExists(request.EncounterId);

        return investigationRequest;
    }

    private async Task<Investigation> GetInvestigation(long investigationId)
        => await _investigationRepository.GetAsync(investigationId) ?? throw new UserFriendlyException("Investigation not found");
}