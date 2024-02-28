using System;
using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Procedures.Dtos;

public class StatementOfPatientOrNextOfKinOrGuardianResponseDto : CreateStatementOfPatientOrNextOfKinOrGuardianDto
{
    public long Id { get; set; }
    
    public SimplePatientInfoResponseDto Patient { get; set; }
    
    public GetStaffMembersSimpleResponseDto InterpretedByStaffUser { get; set; }
    
    public string FacilityName { get; set; }
    
    public string FacilityLevel { get; set; }
     
    public DateTime CreationTime { get; set; }
}

public class StatementOfHealthProfessionalResponseDto : CreateStatementOfHealthProfessionalDto
{
    public long Id { get; set; }
    
    public SimplePatientInfoResponseDto Patient { get; set; }
     
    public DateTime CreationTime { get; set; }
}