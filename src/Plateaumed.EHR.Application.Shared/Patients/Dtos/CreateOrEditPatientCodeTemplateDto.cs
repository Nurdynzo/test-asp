using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class CreateOrEditPatientCodeTemplateDto : EntityDto<long?>
    {
        [StringLength(
            PatientCodeTemplateConsts.MaxPrefixLength,
            MinimumLength = PatientCodeTemplateConsts.MinPrefixLength
        )]
        public string Prefix { get; set; }

        public int Length { get; set; }

        [StringLength(
            PatientCodeTemplateConsts.MaxSuffixLength,
            MinimumLength = PatientCodeTemplateConsts.MinSuffixLength
        )]
        public string Suffix { get; set; }

        public int StartingIndex { get; set; }

        public virtual bool IsActive { get; set; }

        public long FacilityId { get; set; }
    }
}
