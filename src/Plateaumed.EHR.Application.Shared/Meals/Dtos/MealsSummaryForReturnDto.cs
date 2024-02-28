using System;

namespace Plateaumed.EHR.Meals.Dtos;

public class MealsSummaryForReturnDto
{
    public long Id { get; set; } 
    
    public string Description { get; set; }  
    
    public DateTime CreationTime { get; set; }
    public DateTime? DeletionTime { get; set; }
    
}