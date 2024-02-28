using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Plateaumed.EHR.PhysicalExaminations.Dto;

public class PatientPhysicalExaminationResponseDto
{
    public long Id { get; set; } 
    
    public PhysicalExaminationEntryType PhysicalExaminationEntryType { get; set; }
    
    public string PhysicalExaminationEntryTypeName { get; set; } 
    
    public long PhysicalExaminationTypeId { get; set; } 
    
    public GetPhysicalExaminationTypeResponseDto PhysicalExaminationType { get; set; } 
    
    public long PatientId { get; set; } 
    
    public string OtherNote { get; set; } 
    
    public DateTime CreationTime { get; set; }
    
    public DateTime? DeletionTime { get; set; }
    
    public List<PatientPhysicalExamTypeNoteRequestDto> TypeNotes { get; set; }
    
    public List<PatientPhysicalExamSuggestionQuestionDto> Suggestions { get; set; } 
    
    public string ProcedureEntryType { get; set; }
    
    public bool ImageUploaded { get; set; }
    public long EncounterId { get; set; }
    
    public long? ProcedureId { get; set; }
    public bool IsDeleted { get; set; }
    public string DeletedUser { get; set; }

}
