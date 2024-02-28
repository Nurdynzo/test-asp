using Plateaumed.EHR.Insurance;
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class CreateOrEditPatientInsurerDto : EntityDto<long?>
    {
        public InsuranceProviderType Type { get; set; }

        public InsuranceBenefiaryType BenefiaryType { get; set; }

        [StringLength(
            PatientInsurerConsts.MaxCoverageLength,
            MinimumLength = PatientInsurerConsts.MinCoverageLength
        )]
        public string Coverage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [StringLength(
            PatientInsurerConsts.MaxInsuranceCodeLength,
            MinimumLength = PatientInsurerConsts.MinInsuranceCodeLength
        )]
        public string InsuranceCode { get; set; }

        public long InsuranceProviderId { get; set; }
    }
}
