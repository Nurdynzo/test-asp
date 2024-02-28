using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Feeding.Abstractions;

namespace Plateaumed.EHR.Feeding.Handlers;

public class DeleteFeedingCommandHandler : IDeleteFeedingCommandHandler
{
    private readonly IRepository<AllInputs.Feeding, long> _feedingRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;


    public DeleteFeedingCommandHandler(IRepository<AllInputs.Feeding, long> feedingRepository,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _feedingRepository = feedingRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(long feedingId)
    {
        // get feedingId and validate if exist
        var feeding = await _feedingRepository.GetAsync(feedingId);
        if (feeding == null)
            throw new UserFriendlyException($"Feeding id with Id {feedingId} does not exist.");

        // persist
        await _feedingRepository.DeleteAsync(feeding);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}