using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Procedures.Dtos;


public class CreateStatementOfHealthProfessionalDto
{
    public long ProcedureId { get; set; } 
    
    public long PatientId { get; set; } 
    
    public string IntendedBenefits { get; set; }
    
    public List<string> FrequentlyOccuringRisks { get; set; }
    
    public List<string> ExtraProcedures { get; set; }
    
    public bool InformationProvidedToPatient { get; set; }
    
    public bool IsRegionalAnaesthesia { get; set; }
    
    public bool IsLocalAnaesthesia { get; set; }
    
    public bool IsSedationAnaesthesia { get; set; }
    
    public string ConsultantName { get; set; }
    
    public string PrimarySpecialistName { get; set; }
}

public class CreateStatementOfPatientOrNextOfKinOrGuardianDto
{
    public long ProcedureId { get; set; } 
    
    public long PatientId { get; set; } 
    
    public List<AddtionalProcedure> AdditionalProcedures { get; set; }
    
    public bool UsePatientAuthorizedNextOfKinOrGuardian { get; set; }
    
    public string SignatureOfNextOfKinOrGuardian { get; set; }
    
    public IdentificationType NextOfKinOrGuardianGovIssuedId { get; set; }
    
    public string NextOfKinOrGuardianGovIssuedIdNumber { get; set; }
    
    public string SignatureOfWitness { get; set; }
    
    public IdentificationType SignatureOfWitnessGovIssuedId { get; set; }
    
    public string SignatureOfWitnessGovIssuedIdNumber { get; set; }
    
    public string SecondaryLanguageOfInterpretation { get; set; }
    
    public long? InterpretedByStaffUserId { get; set; }
    
    public string SecondarySignatureOfNextOfKinOrGuardian { get; set; }
    
    public IdentificationType SecondaryNextOfKinOrGuardianGovIssuedId { get; set; }
    
    public string SecondaryNextOfKinOrGuardianGovIssuedIdNumber { get; set; }
    
    public string SecondarySignatureOfWitness { get; set; }
    
    public IdentificationType SecondarySignatureOfWitnessGovIssuedId { get; set; }
    
    public string SecondarySignatureOfWitnessGovIssuedIdNumber { get; set; }
}

public class EmailStatementDto
{
    public long ProcedureId { get; set; } 
    
    public long PatientId { get; set; } 
    
    public List<string> recipientEmails { get; set; } 
}

public class SignConfirmationOfConsentDto
{
    public long ProcedureId { get; set; } 
    
    public long PatientId { get; set; } 
    
    public string ConfirmedByConsultantName { get; set; }
    
    public string ConfirmedByPrimarySpecialistName { get; set; }
}

public class AddtionalProcedure
{
    public string Name { get; set; }
    
    public bool Requested { get; set; }
}