namespace Plateaumed.EHR.VitalSigns.Dto;

public class RecheckPatientVitalDto
{
    public bool DeleteMostRecentRecord { get; set; }
    
    public long PatientId { get; set; }
    
    public long? ProcedureId { get; set; }
    
    public long Id { get; set; }
    
    public long? VitalSignId { get; set; }
    
    public long? MeasurementSiteId { get; set; } 
    
    public long? MeasurementRangeId { get; set; } 
    
    public double VitalReading { get; set; }
    
    public string Position { get; set; }

    public long EncounterId { get; set; }
}