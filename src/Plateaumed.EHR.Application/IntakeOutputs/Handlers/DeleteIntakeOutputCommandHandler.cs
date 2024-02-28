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
public class DeleteIntakeOutputCommandHandler : IDeleteIntakeOutputCommandHandler
{
    private readonly IRepository<IntakeOutputCharting, long> _intakeOutputRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IBaseQuery _baseQuery;
    private readonly IAbpSession _abpSession;


    public DeleteIntakeOutputCommandHandler(IRepository<IntakeOutputCharting, long> intakeOutputRepository,
            IUnitOfWorkManager unitOfWorkManager, IBaseQuery baseQuery, IAbpSession abpSession)
    {
        _intakeOutputRepository = intakeOutputRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _baseQuery = baseQuery;
        _abpSession = abpSession;
    }
    public async Task<bool> Handle(long id)
    {
        if (id == 0)
            throw new UserFriendlyException($"Id is required.");

        //Get assigned Intakes
        var model = await _baseQuery.GetIntakeOutputById(id);


        //map request data and set other properties
        var mappedModel = MapObject(model);
        mappedModel.LastModifierUserId = _abpSession.UserId.GetValueOrDefault();
        mappedModel.IsDeleted = true;
        mappedModel.DeleterUserId = _abpSession.UserId.GetValueOrDefault();
        mappedModel.DeletionTime = DateTime.UtcNow;

        await _intakeOutputRepository.UpdateAsync(mappedModel);
        await _unitOfWorkManager.Current.SaveChangesAsync();
        return true;
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
                VolumnInMls = requestDto.VolumnInMls
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
                Pannel = model.Type == ChartingType.INTAKE ? "Intake" : "Output"
            };
    }
}
