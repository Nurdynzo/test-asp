using System;

namespace Plateaumed.EHR.Feeding.Dtos;

public class FeedingSummaryForReturnDto
{
    public long Id { get; set; } 
    public int Volume { get; set; }
    public string Description { get; set; }  
    
    public DateTime CreationTime { get; set; }
    public DateTime? DeletionTime { get; set; }
    
}