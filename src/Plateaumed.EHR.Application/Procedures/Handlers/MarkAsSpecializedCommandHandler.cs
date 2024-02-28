using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class MarkAsSpecializedCommandHandler : IMarkAsSpecializedCommandHandler
{
    private readonly IRepository<Procedure, long> _procedureRepository;  
    private readonly IRepository<SpecializedProcedure, long> _specializedProcedureRepository;  
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;

    public MarkAsSpecializedCommandHandler(IRepository<Procedure, long> procedureRepository, IRepository<SpecializedProcedure, long> specializedProcedureRepository, IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper)
    {
        _procedureRepository = procedureRepository;
        _specializedProcedureRepository = specializedProcedureRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
    }
    
    public async Task Handle(CreateSpecializedProcedureDto requestDto, IAbpSession abpSession)
    {
        if(!abpSession.TenantId.HasValue)
            throw new UserFriendlyException($"Tenant Id is required."); 

        var procedure = await _procedureRepository
            .GetAll().FirstOrDefaultAsync(v => v.Id == requestDto.ProcedureId && v.TenantId == abpSession.TenantId.Value);

        if(procedure == null)
            throw new UserFriendlyException($"Procedure does not exist."); 
        
        if(requestDto.Procedures?.Count <= 0)
            throw new UserFriendlyException($"Add at least one item.");

        foreach (var newSpecializedProcedureRequest in requestDto.Procedures)
        {
            // validate that the selected procedure has not been marked as specialized
            if (await _specializedProcedureRepository
                    .GetAll()
                    .AnyAsync(v => v.SnowmedId == newSpecializedProcedureRequest.SnowmedId &&
                                   v.TenantId == abpSession.TenantId.Value &&
                                   v.ProcedureId == requestDto.ProcedureId))
            {
                throw new UserFriendlyException($"'{newSpecializedProcedureRequest.ProcedureName}' Has already been marked as specialized.");
            }
            
            //map request data and set other properties
            var newSpecializedProcedure = _objectMapper.Map<SpecializedProcedure>(requestDto);
            newSpecializedProcedure.SnowmedId = newSpecializedProcedureRequest.SnowmedId;
            newSpecializedProcedure.ProcedureName = newSpecializedProcedureRequest.ProcedureName;
            newSpecializedProcedure.TenantId = abpSession.TenantId.Value; 
            await _specializedProcedureRepository.InsertAsync(newSpecializedProcedure);
            
            // update procedure as specialised 
            await _procedureRepository.UpdateAsync(procedure);
        } 
        
        // save changes to database
        await _unitOfWorkManager.Current.SaveChangesAsync(); 
    }
}