using System.Collections.Generic;

namespace Plateaumed.EHR.WardEmergencies.Dto;

public class CreatePatientInterventionRequest
{
    public long PatientId { get; set; }
    public long EncounterId { get; set; }
    public long? EventId { get; set; }
    public List<long> InterventionIds { get; set; } = new();
    public string EventText { get; set;}
    public List<string> InterventionTexts { get; set; }
}