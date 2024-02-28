using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.PhysicalExaminations;

public class PatientPhysicalExaminationImageFile : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public long PatientPhysicalExaminationId { get; set; }
    
    [ForeignKey("PatientPhysicalExaminationId")]
    public virtual PatientPhysicalExamination PatientPhysicalExamination { get; set; }
    
    public string FileId { get; set; }
 
    public string FileName { get; set; }
    
    public string FileUrl { get; set; }
}