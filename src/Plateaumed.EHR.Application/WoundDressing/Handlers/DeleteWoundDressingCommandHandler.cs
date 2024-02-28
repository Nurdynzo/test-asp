using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.WoundDressing.Abstractions;

namespace Plateaumed.EHR.WoundDressing.Handlers;

public class DeleteWoundDressingCommandHandler : IDeleteWoundDressingCommandHandler
{
    private readonly IRepository<AllInputs.WoundDressing, long> _woundDressingRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager; 


    public DeleteWoundDressingCommandHandler(IRepository<AllInputs.WoundDressing, long> woundDressingRepository, IUnitOfWorkManager unitOfWorkManager)
    {
        _woundDressingRepository = woundDressingRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(long woundDressingId)
    {
        // get WoundDressingId and validate if exist
        var woundDressing = await _woundDressingRepository.GetAsync(woundDressingId);
        if(woundDressing == null)
            throw new UserFriendlyException($"WoundDressing id with Id {woundDressingId} does not exist.");

        // persist
        await _woundDressingRepository.DeleteAsync(woundDressing);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}