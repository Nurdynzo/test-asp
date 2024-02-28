using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.InputNotes.Abstractions;

namespace Plateaumed.EHR.InputNotes.Handlers;

public class DeleteInputNotesCommandHandler : IDeleteInputNotesCommandHandler
{
    private readonly IRepository<AllInputs.InputNotes, long> _inputNotesRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager; 


    public DeleteInputNotesCommandHandler(IRepository<AllInputs.InputNotes, long> inputNotesRepository, IUnitOfWorkManager unitOfWorkManager)
    {
        _inputNotesRepository = inputNotesRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(long inputNotesId)
    {
        // get InputNotesId and validate if exist
        var inputNotes = await _inputNotesRepository.GetAsync(inputNotesId);
        if(inputNotes == null)
            throw new UserFriendlyException($"InputNotes id with Id {inputNotesId} does not exist.");

        // persist
        await _inputNotesRepository.DeleteAsync(inputNotes);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}