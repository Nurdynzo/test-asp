using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.NurseHistory.Abstractions;
using Plateaumed.EHR.NurseHistory.Dtos; 

namespace Plateaumed.EHR.NurseHistory.Handlers;

public class CreateNurseHistoryCommandHandler : ICreateNurseHistoryCommandHandler
{
    private readonly IRepository<NursingHistory, long> _nurseHistoryRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;

    public CreateNurseHistoryCommandHandler(IRepository<NursingHistory, long> nurseHistoryRepository, IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper)
    {
        _nurseHistoryRepository = nurseHistoryRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
    }
    
    public async Task Handle(CreateNurseHistoryDto requestDto)
    { 
        //map request data and set other properties
        var nursingHistory = _objectMapper.Map<NursingHistory>(requestDto); 
        
        // save data
        await _nurseHistoryRepository.InsertAsync(nursingHistory);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    } 
}