using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Handlers;

public class GetNoteTemplatesQueryHandler : IGetNoteTemplatesQueryHandler
{
    private readonly IRepository<NoteTemplate, long> _noteTemplateRepository; 
    private readonly IObjectMapper _objectMapper;

    public GetNoteTemplatesQueryHandler(IRepository<NoteTemplate, long> noteTemplateRepository, IObjectMapper objectMapper)
    {
        _noteTemplateRepository = noteTemplateRepository;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<NoteTemplateResponseDto>> Handle(string noteRequestType)
    {
        var type = Enum.TryParse(noteRequestType, out NoteType noteType) ? noteType :
            throw new UserFriendlyException($"Requested value '{noteRequestType}' was not found.");
        
        return await _noteTemplateRepository.GetAll().Where(v => v.NoteType == type)
            .Select(v => _objectMapper.Map<NoteTemplateResponseDto>(v))
            .ToListAsync();
    }
}
