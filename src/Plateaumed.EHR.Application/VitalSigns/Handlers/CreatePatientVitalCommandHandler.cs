using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.VitalSigns.Abstraction;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns.Handlers;

public class CreatePatientVitalCommandHandler : ICreatePatientVitalCommandHandler
{
    private readonly IRepository<PatientVital, long> _patientVitalRepository;
    private readonly IRepository<VitalSign, long> _vitalSignRepository; 
    private readonly IRepository<Patient, long> _patientRepository;
    private readonly IEncounterManager _encounterManager;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IVitalSignValidator _vitalSignValidator;
    private readonly IUpdateAppointmentStatusFromAwaitingVitalsCommandHandler _updateAppointmentStatusFromAwaiting;

    public CreatePatientVitalCommandHandler(IRepository<PatientVital, long> patientVitalRepository, IUnitOfWorkManager unitOfWorkManager, 
        IObjectMapper objectMapper, IRepository<Patient, long> patientRepository, IEncounterManager encounterManager, IRepository<VitalSign, long> vitalSignRepository, IVitalSignValidator vitalSignValidator,
        IUpdateAppointmentStatusFromAwaitingVitalsCommandHandler updateAppointmentStatusFromAwaiting)
    {
        _patientVitalRepository = patientVitalRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _patientRepository = patientRepository;
        _encounterManager = encounterManager;
        _vitalSignRepository = vitalSignRepository;
        _vitalSignValidator = vitalSignValidator;
        _updateAppointmentStatusFromAwaiting = updateAppointmentStatusFromAwaiting;
    }
    
    public async Task Handle(CreateMultiplePatientVitalDto requestDto)
    {
        var patient = await _patientRepository.GetAsync(requestDto.PatientId) ??
                      throw new UserFriendlyException("Patient not found");
        
        if(requestDto.PatientVitals.Count <= 0)
            throw new UserFriendlyException("At least one vital data is required."); 

        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);
        
        foreach (var patientVitalReq in requestDto.PatientVitals)
        {
            PatientVitalPosition? position =
                patientVitalReq.Position != null
                    ? (PatientVitalPosition)Enum.Parse(typeof(PatientVitalPosition), patientVitalReq.Position, true)
                    : null;
            
            // validate the vital
            var vitalSign = await _vitalSignRepository.GetAsync(patientVitalReq.VitalSignId) ?? throw new UserFriendlyException("Vital not found");
            await _vitalSignValidator.ValidateRequest(patientVitalReq.VitalReading, vitalSign, patientVitalReq.MeasurementRangeId);
            
            //map request data and set other properties
            var patientVital = _objectMapper.Map<PatientVital>(patientVitalReq);
            patientVital.PatientVitalType = PatientVitalType.NormalVital;
            patientVital.PatientId = patient.Id;
            patientVital.VitalPosition = position;
            patientVital.ProcedureId = requestDto.ProcedureId;
            patientVital.ProcedureEntryType = requestDto.ProcedureEntryType; 
            patientVital.EncounterId = requestDto.EncounterId;
            
            // add to list
            await _patientVitalRepository.InsertAsync(patientVital);
        }
        
        // save changes
        await _unitOfWorkManager.Current.SaveChangesAsync();
        await _updateAppointmentStatusFromAwaiting.Handle(requestDto.EncounterId);
    }
}