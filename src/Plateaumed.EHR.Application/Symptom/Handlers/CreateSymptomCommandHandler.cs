using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI; 
using Newtonsoft.Json;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Symptom.Abstractions;
using Plateaumed.EHR.Symptom.Dtos;

namespace Plateaumed.EHR.Symptom.Handlers;

public class CreateSymptomCommandHandler : ICreateSymptomCommandHandler
{
    private readonly IRepository<AllInputs.Symptom, long> _symptomRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;
    private readonly IAbpSession _abpSession;

    public CreateSymptomCommandHandler(IRepository<AllInputs.Symptom, long> symptomRepository,
        IUnitOfWorkManager unitOfWorkManager,
        IObjectMapper objectMapper,
        IEncounterManager encounterManager,
        IAbpSession abpSession)
    {
        _symptomRepository = symptomRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
        _abpSession = abpSession;
    }
    
    public async Task<AllInputs.Symptom> Handle(CreateSymptomDto requestDto)
    {
        var tenantId = _abpSession.TenantId ?? throw new UserFriendlyException("Please specify a tenant id.");
        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);
        //map request data and set other properties
        var symptom = _objectMapper.Map<AllInputs.Symptom>(requestDto);
        symptom.PatientId = requestDto.PatientId;
        symptom.TenantId = tenantId;
        
        // serialize the typenote object and save
        var entryType = (SymptomEntryType)Enum.Parse(typeof(SymptomEntryType), requestDto.SymptomEntryType, true);
        if (entryType == SymptomEntryType.TypeNote)
            symptom.JsonData = JsonConvert.SerializeObject(requestDto.TypeNotes);
        
        else if (entryType == SymptomEntryType.Suggestion)
        {
            var suggestions = _objectMapper.Map<List<SymptomSuggestionQuestionDto>>(requestDto.Suggestions);
            symptom.JsonData = JsonConvert.SerializeObject(suggestions);
        }
        
        else
            throw new UserFriendlyException("Invalid symptom entry type.");
        
        await _symptomRepository.InsertAsync(symptom);
        await _unitOfWorkManager.Current.SaveChangesAsync();

        return symptom;
    }
}
