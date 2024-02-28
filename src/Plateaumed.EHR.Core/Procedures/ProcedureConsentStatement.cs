using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Procedures;

public class ProcedureConsentStatement : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    
    public long ProcedureId { get; set; }
    
    [ForeignKey("ProcedureId")]
    public virtual Procedure Procedure { get; set; }
    
    public long PatientId { get; set; }

    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    
    public string IntendedBenefits { get; set; }
    
    public List<string> FrequentlyOccuringRisks { get; set; }
    
    public List<string> ExtraProcedures { get; set; }
    
    public bool InformationProvidedToPatient { get; set; }
    
    public bool IsRegionalAnaesthesia { get; set; }
    
    public bool IsLocalAnaesthesia { get; set; }
    
    public bool IsSedationAnaesthesia { get; set; }
    
    public string AdditionalProcedures { get; set; }
    
    public bool? UsePatientAuthorizedNextOfKinOrGuardian { get; set; }
    
    public string SignatureOfNextOfKinOrGuardian { get; set; }
    
    public IdentificationType? NextOfKinOrGuardianGovIssuedId { get; set; }
    
    public string NextOfKinOrGuardianGovIssuedIdNumber { get; set; }
    
    public string SignatureOfWitness { get; set; }
    
    public IdentificationType? SignatureOfWitnessGovIssuedId { get; set; }
    
    public string SignatureOfWitnessGovIssuedIdNumber { get; set; }
    
    public string SecondaryLanguageOfInterpretation { get; set; }
    
    public long? InterpretedByStaffUserId { get; set; }
    
    [ForeignKey("InterpretedByStaffUserId")]
    public virtual User InterpretedByStaffUser { get; set; }
    
    public string SecondarySignatureOfNextOfKinOrGuardian { get; set; }
    
    public IdentificationType? SecondaryNextOfKinOrGuardianGovIssuedId { get; set; }
    
    public string SecondaryNextOfKinOrGuardianGovIssuedIdNumber { get; set; }
    
    public string SecondarySignatureOfWitness { get; set; }
    
    public IdentificationType? SecondarySignatureOfWitnessGovIssuedId { get; set; }
    
    public string SecondarySignatureOfWitnessGovIssuedIdNumber { get; set; }
    
    public string ConsultantName { get; set; }
    
    public string PrimarySpecialistName { get; set; }
    
    public string ConfirmedByConsultantName { get; set; }
    
    public string ConfirmedByPrimarySpecialistName { get; set; }
}