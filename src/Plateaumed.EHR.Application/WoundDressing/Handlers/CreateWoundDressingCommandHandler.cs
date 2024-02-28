using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.WoundDressing.Abstractions;
using Plateaumed.EHR.WoundDressing.Dtos;

namespace Plateaumed.EHR.WoundDressing.Handlers;

public class CreateWoundDressingCommandHandler : ICreateWoundDressingCommandHandler
{
    private readonly IRepository<AllInputs.WoundDressing, long> _woundDressingRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;

    public CreateWoundDressingCommandHandler(IRepository<AllInputs.WoundDressing, long> woundDressingRepository, IUnitOfWorkManager unitOfWorkManager, 
        IObjectMapper objectMapper, IEncounterManager encounterManager)
    {
        _woundDressingRepository = woundDressingRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
    }
    
    public async Task<AllInputs.WoundDressing> Handle(CreateWoundDressingDto requestDto)
    {
        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);
        //map request data and set other properties
        var woundDressing = _objectMapper.Map<AllInputs.WoundDressing>(requestDto);
        woundDressing.PatientId = requestDto.PatientId;
        
        await _woundDressingRepository.InsertAsync(woundDressing);
        await _unitOfWorkManager.Current.SaveChangesAsync();
        
        return woundDressing;
    }
}