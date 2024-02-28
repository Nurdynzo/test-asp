using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PlanItems.Abstractions;

namespace Plateaumed.EHR.PlanItems.Handlers;

public class DeletePlanItemsCommandHandler : IDeletePlanItemsCommandHandler
{
    private readonly IRepository<AllInputs.PlanItems, long> _planItemsRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager; 


    public DeletePlanItemsCommandHandler(IRepository<AllInputs.PlanItems, long> planItemsRepository, IUnitOfWorkManager unitOfWorkManager)
    {
        _planItemsRepository = planItemsRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(long planItemsId, long? userId)
    {
        // get planItemsId and validate if exist
        var planItems = await _planItemsRepository.GetAll().SingleOrDefaultAsync(v => v.Id == planItemsId);
        if(planItems == null)
            throw new UserFriendlyException($"PlanItems id with Id {planItemsId} does not exist.");

        // persist
        await _planItemsRepository.DeleteAsync(planItems);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}