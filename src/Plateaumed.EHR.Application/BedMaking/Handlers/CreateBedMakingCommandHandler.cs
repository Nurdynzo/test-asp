using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Plateaumed.EHR.BedMaking.Abstractions;
using Plateaumed.EHR.BedMaking.Dtos;
using Plateaumed.EHR.Encounters;

namespace Plateaumed.EHR.BedMaking.Handlers;

public class CreateBedMakingCommandHandler : ICreateBedMakingCommandHandler
{
    private readonly IRepository<AllInputs.BedMaking, long> _bedMakingRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;

    public CreateBedMakingCommandHandler(IRepository<AllInputs.BedMaking, long> bedMakingRepository,
        IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper, IEncounterManager encounterManager)
    {
        _bedMakingRepository = bedMakingRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
    }

    public async Task<AllInputs.BedMaking> Handle(CreateBedMakingDto requestDto)
    {
        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);
        //map request data and set other properties
        var bedMaking = _objectMapper.Map<AllInputs.BedMaking>(requestDto);
        bedMaking.PatientId = requestDto.PatientId;

        await _bedMakingRepository.InsertAsync(bedMaking);
        await _unitOfWorkManager.Current.SaveChangesAsync();

        return bedMaking;
    }
}