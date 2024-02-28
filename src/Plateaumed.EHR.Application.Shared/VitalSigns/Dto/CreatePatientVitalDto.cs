using System.Collections.Generic;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.VitalSigns.Dto;

public class CreateMultiplePatientVitalDto
{
    public long PatientId { get; set; }

    public long? ProcedureId { get; set; } = null;
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }
    
    public long EncounterId { get; set; }

    public List<CreatePatientVitalDto> PatientVitals { get; set; }
}

public class CreatePatientVitalDto
{ 
    public long VitalSignId { get; set; } 
    
    public long? MeasurementSiteId { get; set; } 
    
    public long? MeasurementRangeId { get; set; } 
    
    public double VitalReading { get; set; }
    
    public string Position { get; set; }
}