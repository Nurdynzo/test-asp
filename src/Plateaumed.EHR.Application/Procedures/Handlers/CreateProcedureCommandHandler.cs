using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Newtonsoft.Json;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class CreateProcedureCommandHandler : ICreateProcedureCommandHandler
{
    private readonly IRepository<Procedure, long> _procedureRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;

    public CreateProcedureCommandHandler(IRepository<Procedure, long> procedureRepository,
        IUnitOfWorkManager unitOfWorkManager,
        IObjectMapper objectMapper, IEncounterManager encounterManager)
    {
        _procedureRepository = procedureRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
    }

    public async Task Handle(CreateProcedureDto requestDto)
    {
        if (requestDto.SelectedProcedures?.Count <= 0)
            throw new UserFriendlyException("Add at least one item.");

        var type = (ProcedureType)Enum.Parse(typeof(ProcedureType), requestDto.ProcedureType, true);
        if (type == ProcedureType.RequestProcedure && requestDto.SnowmedId.HasValue)
            throw new UserFriendlyException("Snowmed id is only required for record procedure.");

        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);

        //map request data and set other properties
        var procedure = _objectMapper.Map<Procedure>(requestDto);
        procedure.ProcedureType = type;
        procedure.SelectedProcedures = JsonConvert.SerializeObject(requestDto.SelectedProcedures);
        procedure.PatientId = requestDto.PatientId;
        procedure.ProcedureEntryType = requestDto.ProcedureEntryType;
        procedure.EncounterId = requestDto.EncounterId;
        
        // add to list 
        await _procedureRepository.InsertAsync(procedure);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}