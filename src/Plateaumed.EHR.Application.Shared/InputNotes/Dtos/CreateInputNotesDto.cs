using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.InputNotes.Dtos;

public class CreateInputNotesDto
{
    public long PatientId { get; set; }
    public long? Stamp { get; set; }
    public List<string> InputNotesSnowmedIds { get; set; }
    public long EncounterId { get; set; }

    [StringLength(UserConsts.MaxDescriptionLength, MinimumLength = UserConsts.MinDescriptionLength)]
    public string Description { get; set; }
    public ProcedureEntryType? EntryType { get; set; }
    public long? ProcedureId { get; set; }
}
