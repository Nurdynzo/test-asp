using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Dto;

public class GetPatientTravelHistoryQueryResponse: EntityDto<long>
{
    public string LastCreatedBy { get; set; }

    public DateTime? LastDateCreated { get; set; }

    public List<PatientTravelHistoryResponse> PatientTravelHistory { get; set; }
}