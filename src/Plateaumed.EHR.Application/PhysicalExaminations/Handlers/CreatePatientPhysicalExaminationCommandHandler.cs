using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class CreatePatientPhysicalExaminationCommandHandler : ICreatePatientPhysicalExaminationCommandHandler
{
    private readonly IRepository<PatientPhysicalExamination, long> _repository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;

    public CreatePatientPhysicalExaminationCommandHandler(IRepository<PatientPhysicalExamination, long> repository, IUnitOfWorkManager unitOfWorkManager, 
        IObjectMapper objectMapper, IEncounterManager encounterManager)
    {
        _repository = repository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
    }
    
    public async Task<long> Handle(CreatePatientPhysicalExaminationDto requestDto)
    {
        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);
        //map request data and set other properties
        var patientPhysicalExamination = _objectMapper.Map<PatientPhysicalExamination>(requestDto); 
        
        // serialize the typenote object and save
        var entryType = (PhysicalExaminationEntryType)Enum.Parse(typeof(PhysicalExaminationEntryType), requestDto.PhysicalExaminationEntryType, true);
        patientPhysicalExamination.PhysicalExaminationEntryType = entryType;
        

        patientPhysicalExamination.ProcedureEntryType = requestDto.ProcedureEntryType;
        patientPhysicalExamination.ProcedureId = requestDto.ProcedureId;
        
        // save the patient examination doc
        await _repository.InsertAsync(patientPhysicalExamination);
        await _unitOfWorkManager.Current.SaveChangesAsync();

        return patientPhysicalExamination.Id;
    } 
}