using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Symptom.Abstractions;

namespace Plateaumed.EHR.Symptom.Handlers;

public class DeleteSymptomCommandHandler : IDeleteSymptomCommandHandler
{
    private readonly IRepository<AllInputs.Symptom, long> _symptomRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager; 


    public DeleteSymptomCommandHandler(IRepository<AllInputs.Symptom, long> symptomRepository, IUnitOfWorkManager unitOfWorkManager)
    {
        _symptomRepository = symptomRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task<string> Handle(long symptomId, long? userId)
    {
        // get symptom and validate if exist
        var symptom = await _symptomRepository.GetAll().SingleOrDefaultAsync(v => v.Id == symptomId);
        if(symptom == null)
            throw new UserFriendlyException($"Symptom with Id {symptomId} does not exist.");

        // update the symtom details
        symptom.DeleterUserId = userId;
        symptom.IsDeleted = true;

        // persist
        await _symptomRepository.UpdateAsync(symptom);
        await _unitOfWorkManager.Current.SaveChangesAsync();

        // return response
        return "Deleted successfully.";
    }
}