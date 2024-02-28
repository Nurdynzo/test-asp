using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Plateaumed.EHR.Symptom.Dtos;

public class PatientSymptomSummaryForReturnDto
{
    public long Id { get; set; } 
    
    public string SymptomEntryTypeName { get; set; }
    public SymptomEntryType SymptomEntryType { get; set; }
    public string Description { get; set; }
    public string SuggestionSummary { get; set; }
    public List<SymptomTypeNoteRequestDto> TypeNotes { get; set; }
    public long CreatorUserId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsDeleted { get; set; }
    public string DeletedUser { get; set; }
    public long? EncounterId { get; set; }
    public long? AppointmentId { get; set; }
    [JsonIgnore]
    public string OtherNote { get; set; }  
    
    [JsonIgnore]
    public string JsonData { get; set; } 
}
