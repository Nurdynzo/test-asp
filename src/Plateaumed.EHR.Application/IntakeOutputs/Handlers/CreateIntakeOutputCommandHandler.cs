using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Dtos;

namespace Plateaumed.EHR.IntakeOutputs.Handlers;
public class CreateIntakeOutputCommandHandler : ICreateIntakeOutputCommandHandler
{
    private readonly IRepository<AllInputs.IntakeOutputCharting, long> _intakeOutputRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IAbpSession _abpSession;
    private readonly IBaseQuery _baseQuery;
    private readonly IEncounterManager _encounterManager;
    
    public CreateIntakeOutputCommandHandler(IRepository<AllInputs.IntakeOutputCharting, long> intakeOutputRepository,
            IUnitOfWorkManager unitOfWorkManager, IAbpSession abpSession, IBaseQuery baseQuery, IEncounterManager encounterManager)
    {
        _intakeOutputRepository = intakeOutputRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _abpSession = abpSession;
        _baseQuery = baseQuery;
        _encounterManager = encounterManager;
    }

    public async Task<IntakeOutputReturnDto> Handle(CreateIntakeOutputDto requestDto)
    {
        if (requestDto.Type != ChartingType.INTAKE && requestDto.Type != ChartingType.OUTPUT)
            throw new UserFriendlyException($"Invalid Type.");

        if (requestDto.PatientId <= 0)
            throw new UserFriendlyException($"PatientId is required.");

        if (string.IsNullOrEmpty(requestDto.SuggestedText))
            throw new UserFriendlyException($"Please enter the type of fluid.");

        if (requestDto.VolumnInMls <= 0)
            throw new UserFriendlyException($"Please enter the volume of the fluid.");

        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);

        //map request data and set other properties
        var mappedModel = MapObject(requestDto);
        mappedModel.CreatorUserId = _abpSession.UserId.GetValueOrDefault();
        mappedModel.CreationTime = DateTime.UtcNow;

        await _intakeOutputRepository.InsertAndGetIdAsync(mappedModel);
        await _unitOfWorkManager.Current.SaveChangesAsync();
        return ReturnObject(mappedModel);
    }

    private AllInputs.IntakeOutputCharting MapObject(CreateIntakeOutputDto requestDto)
    {
        return new AllInputs.IntakeOutputCharting
            {
                Id = requestDto.Id.GetValueOrDefault(),
                PatientId = requestDto.PatientId,
                Type = requestDto.Type,
                SuggestedText = requestDto.SuggestedText,
                VolumnInMls = requestDto.VolumnInMls,
                EncounterId = requestDto.EncounterId,
                ProcedureId = requestDto.ProcedureId,
                ProcedureEntryType = requestDto.ProcedureEntryType
            };
    }

    private IntakeOutputReturnDto ReturnObject(AllInputs.IntakeOutputCharting model)
    {
        return model == null
            ? new IntakeOutputReturnDto()
            : new IntakeOutputReturnDto
            {
                Id = model.Id,
                PatientId = model.PatientId,
                Type = model.Type,
                SuggestedText = model.SuggestedText,
                VolumnInMls = model.VolumnInMls,
                Pannel = model.Type == ChartingType.INTAKE ? "Intake" : "Output",
                ProcedureId = model.ProcedureId,
                ProcedureEntryType = model.ProcedureEntryType
            };
    }
}
