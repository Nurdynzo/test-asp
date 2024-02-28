using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;
using Abp.Domain.Entities;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.ReferAndConsults;

namespace Plateaumed.EHR.AllInputs;

[Table("PatientReferralOrConsultLetters")]
[Audited]
public class PatientReferralOrConsultLetter : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    public InputType Type { get; set; }
    public string ReceivingHospital { get; set; }
    public string ReceivingUnit { get; set; }
    public string ReceivingConsultant { get; set; }
    public string OtherNote { get; set; }
    public string JsonData { get; set; }

    public long? EncounterId { get; set; }

    [ForeignKey("EncounterId")]
    public PatientEncounter PatientEncounter { get; set; }

}