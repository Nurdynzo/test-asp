using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class ScheduleProcedureCommandHandler : IScheduleProcedureCommandHandler
{
    private readonly IRepository<Procedure, long> _procedureRepository;
    private readonly IRepository<ScheduleProcedure, long> _scheduleProcedureRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;

    public ScheduleProcedureCommandHandler(IRepository<Procedure, long> procedureRepository,
        IRepository<ScheduleProcedure, long> scheduleProcedureRepository, IUnitOfWorkManager unitOfWorkManager,
        IObjectMapper objectMapper)
    {
        _procedureRepository = procedureRepository;
        _scheduleProcedureRepository = scheduleProcedureRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
    }

    public async Task Handle(ScheduleProcedureDto requestDto, IAbpSession abpSession)
    {

        var procedure = await _procedureRepository
            .GetAll().FirstOrDefaultAsync(
                v => v.Id == requestDto.ProcedureId);

        if (procedure == null)
            throw new UserFriendlyException($"Procedure does not exist.");

        if (requestDto.Procedures?.Count <= 0)
            throw new UserFriendlyException($"Add at least one item.");

        foreach (var newScheduledProcedureRequest in requestDto.Procedures)
        {
            // validate that the selected procedure has not been marked as specialized
            if (await _scheduleProcedureRepository
                    .GetAll()
                    .AnyAsync(v => v.SnowmedId == newScheduledProcedureRequest.SnowmedId &&
                                   v.ProcedureId == requestDto.ProcedureId))
            {
                throw new UserFriendlyException(
                    $"'{newScheduledProcedureRequest.ProcedureName}' Has already been scheduled.");
            }

            //map request data and set other properties
            var newScheduledProcedure = _objectMapper.Map<ScheduleProcedure>(requestDto);
            newScheduledProcedure.SnowmedId = newScheduledProcedureRequest.SnowmedId;
            newScheduledProcedure.ProcedureName = newScheduledProcedureRequest.ProcedureName;
            await _scheduleProcedureRepository.InsertAsync(newScheduledProcedure);

            // update procedure as specialised 
            await _procedureRepository.UpdateAsync(procedure);
        }

        // save changes to database
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}