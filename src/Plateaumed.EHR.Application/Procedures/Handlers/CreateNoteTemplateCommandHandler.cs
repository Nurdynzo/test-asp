using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class CreateNoteTemplateCommandHandler : ICreateNoteTemplateCommandHandler
{
    private readonly IRepository<NoteTemplate, long> _noteTemplateRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;

    public CreateNoteTemplateCommandHandler(IRepository<NoteTemplate, long> noteTemplateRepository, IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper)
    {
        _noteTemplateRepository = noteTemplateRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
    }
    
    public async Task Handle(CreateNoteTemplateDto requestDto)
    {
        var noteType = Enum.TryParse(requestDto.NoteType, out NoteType type) ? type :
            throw new UserFriendlyException($"Requested value '{requestDto.NoteType}' was not found.");
        
        //map request data and set other properties
        var noteTemplate = _objectMapper.Map<NoteTemplate>(requestDto);
        noteTemplate.NoteType = noteType;
        
        // save data
        await _noteTemplateRepository.InsertAsync(noteTemplate);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    } 
}
