using Plateaumed.EHR.Insurance;

using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class PatientInsurerDto : EntityDto<long>
    {
        public long InsuranceProviderId { get; set; }
    }
}
