using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Encounters.Dto;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Encounters;

public class EncounterManager : IEncounterManager
{
    private readonly IRepository<PatientEncounter, long> _encounterRepository;
    private readonly IRepository<OrganizationUnitExtended, long> _orgUnitRepository;
    private readonly IRepository<Facility, long> _facilityRepository;
    private readonly IRepository<Patient, long> _patientRepository;
    private readonly IRepository<Ward, long> _wardRepository;
    private readonly IRepository<WardBed, long> _wardBedRepository;
    private readonly IRepository<PatientStability, long> _patientStabilityRepository;
    private readonly IUnitOfWorkManager _unitOfWork;
    private readonly IObjectMapper _objectMapper;
    private readonly IWardBedsManager _wardBedManager;

    public EncounterManager(IRepository<PatientEncounter, long> encounterRepository,
        IRepository<OrganizationUnitExtended, long> orgUnitRepository,
        IRepository<Facility, long> facilityRepository,
        IRepository<Patient, long> patientRepository,
        IRepository<Ward, long> wardRepository,
        IRepository<WardBed, long> wardBedRepository,
        IRepository<PatientStability, long> patientStabilityRepository,
        IUnitOfWorkManager unitOfWork,
        IObjectMapper objectMapper,
        IWardBedsManager wardBedManager)
    {
        _unitOfWork = unitOfWork;
        _objectMapper = objectMapper;
        _encounterRepository = encounterRepository;
        _orgUnitRepository = orgUnitRepository;
        _facilityRepository = facilityRepository;
        _patientRepository = patientRepository;
        _wardRepository = wardRepository;
        _wardBedRepository = wardBedRepository;
        _patientStabilityRepository = patientStabilityRepository;
        _wardBedManager = wardBedManager;
    }

    public async Task CreateAppointmentEncounter(CreateAppointmentEncounterRequest request)
    {
        _ = await _patientRepository.GetAsync(request.PatientId);
        _ = await _facilityRepository.GetAsync(request.FacilityId);
        if (request.UnitId != null) _ = await _orgUnitRepository.GetAsync(request.UnitId.Value);

        var encounter = _objectMapper.Map<PatientEncounter>(request);

        await _encounterRepository.InsertAsync(encounter);
        await _unitOfWork.Current.SaveChangesAsync();
    }

    public async Task AdmitPatient(CreateAdmissionEncounterRequest request)
    {
        _ = await _patientRepository.GetAsync(request.PatientId);
        _ = await _facilityRepository.GetAsync(request.FacilityId);
        if (request.UnitId != null) _ = await _orgUnitRepository.GetAsync(request.UnitId.Value);
        if (request.WardId != null) _ = await _wardRepository.GetAsync(request.WardId.Value);
        if (request.WardBedId != null) _ = await _wardBedRepository.GetAsync(request.WardBedId.Value);

        var encounter = _objectMapper.Map<PatientEncounter>(request);

        await _encounterRepository.InsertAsync(encounter);
        await _unitOfWork.Current.SaveChangesAsync();

        if (encounter.AdmissionId.HasValue && encounter.WardBedId.HasValue)
            await _wardBedManager.OccupyWardBed(encounter.WardBedId.Value, encounter.Id);
    }

    public async Task CheckEncounterExists(long encounterId)
    {
        await _encounterRepository.GetAsync(encounterId);
    }

    public async Task RequestTransferPatient(long encounterId, long wardId, long? wardBedId,
        PatientStabilityStatus status)
    {
        var encounter = await _encounterRepository.GetAsync(encounterId);
        await _wardRepository.GetAsync(wardId);
        if (wardBedId.HasValue) await _wardBedRepository.GetAsync(wardBedId.Value);

        encounter.Status = EncounterStatusType.TransferOutPending;

        var newEncounter = new PatientEncounter
        {
            PatientId = encounter.PatientId,
            Status = EncounterStatusType.TransferInPending,
            ServiceCentre = encounter.ServiceCentre,
            UnitId = encounter.UnitId,
            FacilityId = encounter.FacilityId,
            AdmissionId = encounter.AdmissionId,
            WardId = wardId,
            WardBedId = wardBedId,
        };

        await _encounterRepository.InsertAsync(newEncounter);
        await _encounterRepository.UpdateAsync(encounter);
        await _unitOfWork.Current.SaveChangesAsync();
        await _patientStabilityRepository.InsertAsync(new PatientStability
        {
            EncounterId = newEncounter.Id,
            PatientId = encounter.PatientId,
            TenantId = encounter.TenantId,
            Status = status
        });
        if (wardBedId.HasValue)
            await _wardBedManager.OccupyWardBed(wardBedId.Value, newEncounter.Id);
    }

    public async Task CompleteTransferPatient(long encounterId)
    {
        var encounter = await _encounterRepository.GetAsync(encounterId);
        encounter.Status = EncounterStatusType.Transferred;
        encounter.TimeOut = DateTime.Now;

        var nextEncounter = _encounterRepository.GetAll().FirstOrDefault(x =>
                                x.PatientId == encounter.PatientId && x.Status == EncounterStatusType.TransferInPending)
                            ?? throw new UserFriendlyException("Unable to find encounter to transfer to");

        nextEncounter.Status = EncounterStatusType.InProgress;
        nextEncounter.TimeIn = DateTime.Now;

        await _encounterRepository.UpdateAsync(encounter);
        await _encounterRepository.UpdateAsync(nextEncounter);

        if (encounter.WardBedId.HasValue)
            await _wardBedManager.DeOccupyWardBed(encounter.Id);

        await _unitOfWork.Current.SaveChangesAsync();
    }

    public async Task RequestDischargePatient(long encounterId)
    {
        var encounter = await _encounterRepository.GetAsync(encounterId);
        encounter.Status = encounter.ServiceCentre == ServiceCentreType.OutPatient
            ? EncounterStatusType.Discharged
            : EncounterStatusType.DischargePending;
        await _encounterRepository.UpdateAsync(encounter);
        await _unitOfWork.Current.SaveChangesAsync();
    }

    public async Task CompleteDischargePatient(long encounterId)
    {
        var encounter = await _encounterRepository.GetAsync(encounterId);
        encounter.Status = EncounterStatusType.Discharged;
        encounter.TimeOut = DateTime.Now;

        await _encounterRepository.UpdateAsync(encounter);
        await _unitOfWork.Current.SaveChangesAsync();
        await _wardBedManager.DeOccupyWardBed(encounter.WardBedId);
    }

    public async Task MarkAsDeceased(long encounterId)
    {
        var encounter = await _encounterRepository.GetAsync(encounterId);
        encounter.Status = EncounterStatusType.Deceased;
        encounter.TimeOut = DateTime.Now;

        await _encounterRepository.UpdateAsync(encounter);
        await _unitOfWork.Current.SaveChangesAsync();
    }
}