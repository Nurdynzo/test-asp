using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class CreateOrEditStaffCodeTemplateDto : EntityDto<long?>
    {
        public int Length { get; set; }

        public int StartingIndex { get; set; }

        [StringLength(
            StaffCodeTemplateConsts.MaxPrefixLength,
            MinimumLength = StaffCodeTemplateConsts.MinPrefixLength
        )]
        public string Prefix { get; set; }

        [StringLength(
            StaffCodeTemplateConsts.MaxSuffixLength,
            MinimumLength = StaffCodeTemplateConsts.MinSuffixLength
        )]
        public string Suffix { get; set; }
    }
}
