using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.InputNotes.Abstractions;
using Plateaumed.EHR.InputNotes.Dtos;

namespace Plateaumed.EHR.InputNotes.Handlers;

public class CreateInputNotesCommandHandler : ICreateInputNotesCommandHandler
{
    private readonly IRepository<AllInputs.InputNotes, long> _inputNotesRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;

    public CreateInputNotesCommandHandler(IRepository<AllInputs.InputNotes, long> inputNotesRepository,
        IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper, IEncounterManager encounterManager)
    {
        _inputNotesRepository = inputNotesRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
    }

    public async Task<AllInputs.InputNotes> Handle(CreateInputNotesDto requestDto)
    {
        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);

        //map request data and set other properties
        var inputNotes = _objectMapper.Map<AllInputs.InputNotes>(requestDto) ??
                         throw new UserFriendlyException("Missing fields in Request");
        inputNotes.PatientId = requestDto.PatientId;

        await _inputNotesRepository.InsertAsync(inputNotes);
        await _unitOfWorkManager.Current.SaveChangesAsync();

        return inputNotes;
    }
}