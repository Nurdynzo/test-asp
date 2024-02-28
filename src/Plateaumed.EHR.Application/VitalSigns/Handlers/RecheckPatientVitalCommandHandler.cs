using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.VitalSigns.Abstraction;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns.Handlers;

public class RecheckPatientVitalCommandHandler : IRecheckPatientVitalCommandHandler
{
    private readonly IRepository<PatientVital, long> _patientVitalRepository; 
    private readonly IRepository<VitalSign, long> _vitalSignRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IVitalSignValidator _vitalSignValidator;

    public RecheckPatientVitalCommandHandler(IRepository<PatientVital, long> patientVitalRepository, IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper, IRepository<VitalSign, long> vitalSignRepository, IVitalSignValidator vitalSignValidator)
    {
        _patientVitalRepository = patientVitalRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _vitalSignRepository = vitalSignRepository;
        _vitalSignValidator = vitalSignValidator;
    }

    public async Task Handle(RecheckPatientVitalDto requestDto)
    {
        // validates the value
        if(requestDto.VitalReading <= 0)
            throw new UserFriendlyException($"Vital reading is required.");
        
        // get the most recent specific patient vital based on the vital id
        var patientVital = await _patientVitalRepository.GetAll()
            .OrderByDescending(v => v.Id)
            .FirstOrDefaultAsync(v => v.Id == requestDto.Id && v.PatientId == requestDto.PatientId);
        
        if(patientVital == null) 
            throw new UserFriendlyException($"Patient vital is not found. Only existing vitals can be rechecked.");
        requestDto.VitalSignId ??= patientVital.VitalSignId;

        // check if the most recent needs to be deleted
        if (requestDto.DeleteMostRecentRecord)
        {

            // update record
            await _patientVitalRepository.UpdateAsync(patientVital);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            // update the previous vital and set to an old vital
            patientVital.PatientVitalType = PatientVitalType.OldVital;
                    
            // add new record
            await InsertNewPatientVital(requestDto);
        }
        else
        {
            // just add new record
            await InsertNewPatientVital(requestDto);
        }
    }

    private async Task InsertNewPatientVital(RecheckPatientVitalDto requestDto)
    {
        PatientVitalPosition? position = requestDto.Position != null
            ? (PatientVitalPosition)Enum.Parse(typeof(PatientVitalPosition), requestDto.Position, true)
            : null;
        
        // validate the vital
        var vitalSign = await _vitalSignRepository.GetAsync(requestDto.VitalSignId.Value) ?? throw new UserFriendlyException("Vital not found");
        await _vitalSignValidator.ValidateRequest(requestDto.VitalReading, vitalSign, requestDto.MeasurementRangeId);
        
        // initiate new vital from the old and set the readings
        var newPatientVital = _objectMapper.Map<PatientVital>(requestDto);
        newPatientVital.PatientVitalType = PatientVitalType.NewRecheckedVital;
        newPatientVital.VitalPosition = position;
        // add new record
        await _patientVitalRepository.InsertAsync(newPatientVital);
        await _unitOfWorkManager.Current.SaveChangesAsync(); 
    }
}