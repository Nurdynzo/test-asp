using System;

namespace Plateaumed.EHR.PhysicalExaminations.Dto;

public class PatientPhysicalExaminationImageFileResponseDto
{ 
    public long Id { get; set; } 
    
    public long PatientPhysicalExaminationId { get; set; } 
    
    public string FileId { get; set; }
 
    public string FileName { get; set; }
    
    public string FileUrl { get; set; }
    
    public DateTime CreationTime { get; set; }
}