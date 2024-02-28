using System;
using System.Collections.Generic;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Dto;
public class GetPatientPastMedicalConditionQueryResponse
{
    public DateTime? LastEnteredByDate { get; set; }
    
    public string LastEnteredBy { get; set; }
    
    public List<GetPatientPastMedicalCondition> PastMedicalConditions { get; set; }
}