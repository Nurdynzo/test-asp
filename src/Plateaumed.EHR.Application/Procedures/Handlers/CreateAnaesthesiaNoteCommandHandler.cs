using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class CreateAnaesthesiaNoteCommandHandler : ICreateAnaesthesiaNoteCommandHandler
{
    private readonly IRepository<AnaesthesiaNote, long> _anaesthesiaNoteRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;

    public CreateAnaesthesiaNoteCommandHandler(IRepository<AnaesthesiaNote, long> anaesthesiaNoteRepository, IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper)
    {
        _anaesthesiaNoteRepository = anaesthesiaNoteRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
    }
    
    public async Task Handle(CreateAnaesthesiaNoteDto requestDto)
    {
        //map request data and set other properties
        var procedureNote = _objectMapper.Map<AnaesthesiaNote>(requestDto);  
        
        // save data
        await _anaesthesiaNoteRepository.InsertAsync(procedureNote);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    } 
}