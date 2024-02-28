using System;

namespace Plateaumed.EHR.WoundDressing.Dtos;

public class WoundDressingSummaryForReturnDto
{
    public long Id { get; set; } 
    
    public string Description { get; set; }  
    
    public DateTime CreationTime { get; set; }
    public DateTime? DeletionTime { get; set; }
    
}