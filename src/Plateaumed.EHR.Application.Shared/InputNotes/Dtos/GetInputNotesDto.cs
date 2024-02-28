using Plateaumed.EHR.Procedures;
namespace Plateaumed.EHR.InputNotes.Dtos;

public class GetInputNotesDto
{
    public long PatientId { get; set; }
    public long EncounterId { get; set; }
    public ProcedureEntryType? EntryType { get; set; }
    public long? ProcedureId { get; set; }
}
