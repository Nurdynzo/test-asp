using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class UpdateStatusCommandHandler : IUpdateStatusCommandHandler
{
    private readonly IRepository<Procedure, long> _procedureRepository;   
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public UpdateStatusCommandHandler(IRepository<Procedure, long> procedureRepository, IUnitOfWorkManager unitOfWorkManager)
    {
        _procedureRepository = procedureRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(UpdateProcedureStatusDto requestDto)
    {
        var procedure = await _procedureRepository.GetAsync(requestDto.ProcedureId) ??
                        throw new UserFriendlyException("Procedure not found");
        
        // update status
        procedure.ProcedureStatus = requestDto.ProcedureStatus;
        
        await _procedureRepository.UpdateAsync(procedure);  
        await _unitOfWorkManager.Current.SaveChangesAsync(); 
    }
}