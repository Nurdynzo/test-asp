using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class JobTitleDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public bool IsActive { get; set; }

        public long FacilityId { get; set; }

        public List<JobLevelDto> JobLevels { get; set; }

        public bool IsStatic { get; set; }
    }
}
