using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.IntakeOutputs;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Dtos;

namespace Plateaumed.EHR.IntakeOutputs.Handlers;
public class EditIntakeOutputCommandHandler : IEditIntakeOutputCommandHandler
{
    private readonly IRepository<IntakeOutputCharting, long> _intakeOutputRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IBaseQuery _baseQuery;
    private readonly IAbpSession _abpSession;


    public EditIntakeOutputCommandHandler(IRepository<IntakeOutputCharting, long> intakeOutputRepository,
            IUnitOfWorkManager unitOfWorkManager, IBaseQuery baseQuery, IAbpSession abpSession)
    {
        _intakeOutputRepository = intakeOutputRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _baseQuery = baseQuery;
        _abpSession = abpSession;
    }
    public async Task<IntakeOutputReturnDto> Handle(CreateIntakeOutputDto requestDto)
    {
        if (requestDto.Type != ChartingType.INTAKE && requestDto.Type != ChartingType.OUTPUT)
            throw new UserFriendlyException($"Invalid Type.");

        if (requestDto.PatientId <= 0)
            throw new UserFriendlyException($"PatientId is required.");

        if (requestDto.Id == 0 || requestDto.Id == null)
            throw new UserFriendlyException($"Id is required.");

        if (string.IsNullOrEmpty(requestDto.SuggestedText))
            throw new UserFriendlyException($"Please enter the type of fluid.");

        if (requestDto.VolumnInMls <= 0)
            throw new UserFriendlyException($"Please enter the volume of the fluid.");

        //Get assigned Intakes
        var model = await _baseQuery.GetIntakeOutputById(requestDto.Id.GetValueOrDefault()) ?? throw new UserFriendlyException($"Wrong request id or invalid type.");

        model.SuggestedText = requestDto.SuggestedText;
        model.VolumnInMls = requestDto.VolumnInMls;
        if (requestDto.PatientId != model.PatientId)
            throw new UserFriendlyException($"Wrong Patient.");

        //map request data and set other properties
        var mappedModel = MapObject(model);
        mappedModel.LastModifierUserId = _abpSession.UserId.GetValueOrDefault();
        mappedModel.LastModificationTime = DateTime.UtcNow;

        await _intakeOutputRepository.UpdateAsync(mappedModel);
        await _unitOfWorkManager.Current.SaveChangesAsync();
        return ReturnObject(mappedModel);
    }
    private IntakeOutputCharting MapObject(IntakeOutputReturnDto requestDto)
    {
        return requestDto == null
            ? new IntakeOutputCharting()
            : new IntakeOutputCharting
            {
                Id = requestDto.Id,
                PatientId = requestDto.PatientId,
                Type = requestDto.Type,
                SuggestedText = requestDto.SuggestedText,
                VolumnInMls = requestDto.VolumnInMls,
                ProcedureId = requestDto.ProcedureId,
                ProcedureEntryType = requestDto.ProcedureEntryType
            };
    }


    private IntakeOutputReturnDto ReturnObject(IntakeOutputCharting model)
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
