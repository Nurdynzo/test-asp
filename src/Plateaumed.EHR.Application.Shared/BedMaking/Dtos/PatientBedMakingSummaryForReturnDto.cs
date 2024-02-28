using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Plateaumed.EHR.BedMaking.Dtos;

public class PatientBedMakingSummaryForReturnDto
{
    public long Id { get; set; } 
    
    public string Note { get; set; }  
    
    public List<string> BedMakingSnowmedIds { get; set; }

    public DateTime CreationTime { get; set; }
    public DateTime? DeletionTime { get; set; }
    
}