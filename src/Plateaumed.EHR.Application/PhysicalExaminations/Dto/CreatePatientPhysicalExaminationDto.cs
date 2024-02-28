using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.PhysicalExaminations.Dto;

public class CreatePatientPhysicalExaminationDto
{
    [Required]
    public string PhysicalExaminationEntryType { get; set; }
    [Required]
    public long PhysicalExaminationTypeId { get; set; }
    [Required]
    public long PatientId { get; set; }
    public List<PatientPhysicalExamTypeNoteRequestDto> TypeNotes { get; set; }
    public List<PatientPhysicalExamSuggestionQuestionDto> Suggestions { get; set; }
    public string OtherNote { get; set; }
    public long EncounterId { get; set; }
    
    public long? ProcedureId { get; set; } = null;
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }
}