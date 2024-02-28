using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.Patients;

public class PatientScanDocument : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public Guid FileId { get; set; }

    [StringLength(maximumLength: PatientScanDocumentConstant.MaxFileNameLength)]
    public string FileName { get; set; }
    
    [Required]
    public string PatientCode { get; set; }

    [Required]
    public long AssigneeId { get; set; }

    public long? ReviewerId { get; set; }

    public bool? IsApproved { get; set; }

    public String ReviewNote { get; set; }
}