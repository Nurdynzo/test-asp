using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientInsurerForEditOutput
    {
        public CreateOrEditPatientInsurerDto PatientInsurer { get; set; }

        public string InsuranceProviderName { get; set; }
    }
}
