using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.PlanItems.Abstractions;
using Plateaumed.EHR.PlanItems.Dtos;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.PlanItems.Handlers;

public class CreatePlanItemsCommandHandler : ICreatePlanItemsCommandHandler
{
    private readonly IRepository<AllInputs.PlanItems, long> _planItemsRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;

    public CreatePlanItemsCommandHandler(IRepository<AllInputs.PlanItems, long> planItemsRepository, IUnitOfWorkManager unitOfWorkManager, 
        IObjectMapper objectMapper, IEncounterManager encounterManager)
    {
        _planItemsRepository = planItemsRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
    }
    
    public async Task<AllInputs.PlanItems> Handle(CreatePlanItemsDto requestDto)
    {
        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);
        
        //map request data and set other properties
        var planItems = _objectMapper.Map<AllInputs.PlanItems>(requestDto);
        planItems.PatientId = requestDto.PatientId;
        planItems.ProcedureId = requestDto.ProcedureId;
        planItems.ProcedureEntryType = requestDto.ProcedureEntryType;
        planItems.EncounterId = requestDto.EncounterId;
        
        await _planItemsRepository.InsertAsync(planItems);
        await _unitOfWorkManager.Current.SaveChangesAsync();
        
        return planItems;
    }
}