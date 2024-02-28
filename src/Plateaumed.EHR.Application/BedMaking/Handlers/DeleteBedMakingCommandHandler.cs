using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.BedMaking.Abstractions;

namespace Plateaumed.EHR.BedMaking.Handlers;

public class DeleteBedMakingCommandHandler : IDeleteBedMakingCommandHandler
{
    private readonly IRepository<AllInputs.BedMaking, long> _bedMakingRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager; 


    public DeleteBedMakingCommandHandler(IRepository<AllInputs.BedMaking, long> bedMakingRepository, IUnitOfWorkManager unitOfWorkManager)
    {
        _bedMakingRepository = bedMakingRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(long bedMakingId, long? userId)
    {
        // get bed making and validate if exist
        var bedMaking = await _bedMakingRepository.GetAll().SingleOrDefaultAsync(v => v.Id == bedMakingId);
        if(bedMaking == null)
            throw new UserFriendlyException($"BedMaking id with Id {bedMakingId} does not exist.");

        // persist
        await _bedMakingRepository.DeleteAsync(bedMaking);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}