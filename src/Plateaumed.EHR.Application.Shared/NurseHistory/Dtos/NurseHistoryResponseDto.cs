using System;

namespace Plateaumed.EHR.NurseHistory.Dtos;

public class NurseHistoryResponseDto
{
    public long PatientId { get; set; } 
    
    public string Note { get; set; }
    
    public DateTime CreationTime { get; set; }
}