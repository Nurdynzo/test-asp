using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Meals.Abstractions;
using Plateaumed.EHR.Meals.Dtos;

namespace Plateaumed.EHR.Meals.Handlers;

public class CreateMealsCommandHandler : ICreateMealsCommandHandler
{
    private readonly IRepository<AllInputs.Meals, long> _mealsRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;

    public CreateMealsCommandHandler(IRepository<AllInputs.Meals, long> mealsRepository, IUnitOfWorkManager unitOfWorkManager, 
        IObjectMapper objectMapper, IEncounterManager encounterManager)
    {
        _mealsRepository = mealsRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
    }
    
    public async Task<AllInputs.Meals> Handle(CreateMealsDto requestDto)
    {
        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);
        //map request data and set other properties
        var meals = _objectMapper.Map<AllInputs.Meals>(requestDto);
        meals.PatientId = requestDto.PatientId;
        
        await _mealsRepository.InsertAsync(meals);
        await _unitOfWorkManager.Current.SaveChangesAsync();
        
        return meals;
    }
}