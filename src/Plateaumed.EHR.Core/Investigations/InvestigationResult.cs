using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.Investigations;

public class InvestigationResult : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    public long PatientId { get; set; }

    public long InvestigationId { get; set; }

    [ForeignKey(nameof(InvestigationId))]
    public Investigation Investigation { get; set; }

    public long InvestigationRequestId { get; set; }

    [ForeignKey(nameof(InvestigationRequestId))]
    public InvestigationRequest InvestigationRequest { get; set; }

    public string Name { get; set; }

    public string Reference { get; set; }

    public DateOnly SampleCollectionDate { get; set; }

    public DateOnly ResultDate { get; set; }

    public TimeOnly SampleTime { get; set; }

    public TimeOnly ResultTime { get; set; }

    public string Specimen { get; set; }

    public string Conclusion { get; set; }

    public string SpecificOrganism { get; set; }

    public string View { get; set; }

    public string Notes { get; set; }

    public List<InvestigationComponentResult> InvestigationComponentResults { get; set; }

    public long? EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public PatientEncounter PatientEncounter { get; set; }

    public long? ProcedureId { get; set; }
    
    [ForeignKey("ProcedureId")]
    public virtual Procedure Procedure { get; set; }
}
