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

public class GetNoteListQueryHandler : IGetNoteListQueryHandler
{
    private readonly IRepository<ProcedureNote, long> _procedureNoteRepository; 
    private readonly IRepository<AnaesthesiaNote, long> _anaesthesiaNoteRepository; 
    private readonly IObjectMapper _objectMapper;

    public GetNoteListQueryHandler(IRepository<ProcedureNote, long> procedureNoteRepository, IRepository<AnaesthesiaNote, long> anaesthesiaNoteRepository, IObjectMapper objectMapper)
    {
        _procedureNoteRepository = procedureNoteRepository;
        _anaesthesiaNoteRepository = anaesthesiaNoteRepository;
        _objectMapper = objectMapper;
    }

    public async Task<List<NoteResponseDto>> Handle(long procedureId, string noteRequestType)
    {
        var type = Enum.TryParse(noteRequestType, out NoteType noteType) ? noteType : throw new UserFriendlyException($"Requested value '{noteRequestType}' was not found.");

        switch (type)
        {
            case NoteType.ProcedureNote:
            case NoteType.NurseNote:
                return await _procedureNoteRepository.GetAll().Where(v => v.ProcedureId == procedureId && v.NoteType == type)
                    .Select(v => _objectMapper.Map<NoteResponseDto>(v))
                    .ToListAsync();
            case NoteType.AnaesthesiaNote:
                return await _anaesthesiaNoteRepository.GetAll().Where(v => v.ProcedureId == procedureId)
                    .Select(v => _objectMapper.Map<NoteResponseDto>(v))
                    .ToListAsync();
            default:
                throw new UserFriendlyException($"Requested value '{noteRequestType}' was not found.");
        }
    }
}
