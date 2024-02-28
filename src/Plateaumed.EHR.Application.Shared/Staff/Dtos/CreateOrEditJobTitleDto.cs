using System.Collections.Generic;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class CreateOrEditJobTitleDto : EntityDto<long?>
    {
        [Required]
        [StringLength(JobTitleConsts.MaxNameLength, MinimumLength = JobTitleConsts.MinNameLength)]
        public string Name { get; set; }

        [StringLength(
            JobTitleConsts.MaxShortNameLength,
            MinimumLength = JobTitleConsts.MinShortNameLength
        )]
        public string ShortName { get; set; }

        public bool? IsActive { get; set; }

        public long FacilityId { get; set; }

        public List<CreateOrEditJobLevelDto> JobLevels { get; set; }
    }
}
