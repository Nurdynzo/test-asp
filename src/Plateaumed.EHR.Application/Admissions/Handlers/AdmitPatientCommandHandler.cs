using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Admissions.Dto;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Encounters.Dto;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Admissions.Handlers;

public class AdmitPatientCommandHandler : IAdmitPatientCommandHandler
{
    private readonly IRepository<Admission, long> _admissionRepository;
    private readonly IRepository<Facility, long> _facilityRepository;
    private readonly IRepository<Patient, long> _patientRepository;
    private readonly IRepository<StaffMember, long> _staffRepository;
    private readonly IRepository<Ward, long> _wardRepository;
    private readonly IRepository<WardBed, long> _wardBedRepository;
    private readonly IEncounterManager _encounterManager;
    private readonly IUnitOfWorkManager _unitOfWork;
    
    public AdmitPatientCommandHandler(IRepository<Admission, long> admissionRepository,
        IRepository<Facility, long> facilityRepository,
        IRepository<Patient, long> patientRepository,
        IRepository<StaffMember, long> staffRepository,
        IEncounterManager  encounterManager,
        IUnitOfWorkManager unitOfWork, IRepository<Ward, long> wardRepository, IRepository<WardBed, long> wardBedRepository)
    {
        _admissionRepository = admissionRepository;
        _facilityRepository = facilityRepository;
        _patientRepository = patientRepository;
        _staffRepository = staffRepository;
        _encounterManager = encounterManager;
        _unitOfWork = unitOfWork;
        _wardRepository = wardRepository;
        _wardBedRepository = wardBedRepository;
    }

    public async Task Handle(AdmitPatientRequest request)
    {
        await ValidateRequest(request);

        var admission = new Admission
        {
            AttendingPhysicianId = request.AttendingPhysicianId,
            PatientId = request.PatientId,
            FacilityId = request.FacilityId,
            AdmittingEncounterId = request.EncounterId,
        };

        await _admissionRepository.InsertAsync(admission);
        await _unitOfWork.Current.SaveChangesAsync();

        await _encounterManager.AdmitPatient(new CreateAdmissionEncounterRequest
        {
            AdmissionId = admission.Id,
            FacilityId = request.FacilityId,
            PatientId = request.PatientId,
            ServiceCentre = request.ServiceCentre,
            UnitId = request.UnitId,
            WardId = request.WardId,
            WardBedId = request.WardBedId,
            Status = EncounterStatusType.InProgress
        });
    }

    private async Task ValidateRequest(AdmitPatientRequest request)
    {
        if (request.ServiceCentre is not ServiceCentreType.InPatient and not ServiceCentreType.AccidentAndEmergency)
        {
            throw new UserFriendlyException("Invalid Service Centre");
        }

        _ = await _patientRepository.GetAsync(request.PatientId);
        _ = await _facilityRepository.GetAsync(request.FacilityId);
        if (request.AttendingPhysicianId != null) _ = await _staffRepository.GetAsync(request.AttendingPhysicianId.Value);
        if (request.WardId != null) _ = await _wardRepository.GetAsync(request.WardId.Value);
        if (request.WardBedId != null) _ = await _wardBedRepository.GetAsync(request.WardBedId.Value);
        if (request.EncounterId != null) await _encounterManager.CheckEncounterExists(request.EncounterId.Value);

        var existingAdmission = await _admissionRepository.FirstOrDefaultAsync(x => x.AdmittingEncounterId == request.EncounterId);
        if (existingAdmission != null)
        {
            throw new UserFriendlyException("Patient has already been admitted");
        }
    }
}