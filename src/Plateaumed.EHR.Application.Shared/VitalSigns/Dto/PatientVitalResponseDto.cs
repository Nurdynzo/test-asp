using System;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.VitalSigns.Dto;

public class PatientVitalResponseDto
{
    public long Id { get; set; }
    
    public long PatientId { get; set; }
    
    public int PainScale { get; set; } 
    
    public long VitalSignId { get; set; } 
    
    public GetSimpleVitalSignsResponse VitalSign { get; set; } 
    
    public long? MeasurementSiteId { get; set; } 
    
    public MeasurementSiteDto MeasurementSite { get; set; } 
    
    public long? MeasurementRangeId { get; set; } 
    
    public MeasurementRangeDto MeasurementRange { get; set; } 
    
    public double VitalReading { get; set; }
    
    public long? ProcedureId { get; set; }
    
    public DateTime CreationTime { get; set; }
    
    public GetStaffMembersSimpleResponseDto CreatorUser { get; set; }
    
    public DateTime? LastModificationTime { get; set; }
    
    public GetStaffMembersSimpleResponseDto LastModifierUser { get; set; }
    
    public string PatientVitalType { get; set; }

    public bool OverThreshold { get; set; }
    
    public string Position { get; set; }
    
    public string ProcedureEntryType { get; set; }

    public bool IsDeleted { get; set; }
    public string DeletedUser { get; set; }
}
