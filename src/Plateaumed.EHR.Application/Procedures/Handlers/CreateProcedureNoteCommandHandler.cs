using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class CreateProcedureNoteCommandHandler : ICreateProcedureNoteCommandHandler
{
    private readonly IRepository<ProcedureNote, long> _procedureNoteRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;

    public CreateProcedureNoteCommandHandler(IRepository<ProcedureNote, long> procedureNoteRepository, IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper)
    {
        _procedureNoteRepository = procedureNoteRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
    }
    
    public async Task Handle(CreateProcedureNoteDto requestDto)
    {
        //map request data and set other properties
        var procedureNote = _objectMapper.Map<ProcedureNote>(requestDto);  
        
        // save data
        await _procedureNoteRepository.InsertAsync(procedureNote);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    } 
}