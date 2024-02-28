using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class CreateOrEditJobLevelDto : EntityDto<long?>
    {
        [Required]
        [StringLength(JobLevelConsts.MaxNameLength, MinimumLength = JobLevelConsts.MinNameLength)]
        public string Name { get; set; }

        [Range(JobLevelConsts.MinRankValue, JobLevelConsts.MaxRankValue)]
        public int Rank { get; set; }

        [StringLength(
            JobLevelConsts.MaxShortNameLength,
            MinimumLength = JobLevelConsts.MinShortNameLength
        )]
        public string ShortName { get; set; }

        public long JobTitleId { get; set; }

        public bool? IsActive { get; set; }
    }
}
