using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.WardEmergencies.Dto;

public class GetPatientInterventionsResponse
{
    public long Id { get; set; }
    public string Event { get; set; }
    public List<string> Interventions { get; set; }
    public DateTime Time { get; set; }
    public bool IsDeleted { get; set; }
    public string DeletedUser { get; set; }
}
