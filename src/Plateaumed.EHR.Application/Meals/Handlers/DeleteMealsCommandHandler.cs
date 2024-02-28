using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Meals.Abstractions;

namespace Plateaumed.EHR.Meals.Handlers;

public class DeleteMealsCommandHandler : IDeleteMealsCommandHandler
{
    private readonly IRepository<AllInputs.Meals, long> _mealsRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;


    public DeleteMealsCommandHandler(IRepository<AllInputs.Meals, long> mealsRepository,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _mealsRepository = mealsRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(long mealsId)
    {
        // get mealsId and validate if exist
        var meals = await _mealsRepository.GetAsync(mealsId);
        if (meals == null)
            throw new UserFriendlyException($"Meals id with Id {mealsId} does not exist.");

        // persist
        await _mealsRepository.DeleteAsync(meals);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}