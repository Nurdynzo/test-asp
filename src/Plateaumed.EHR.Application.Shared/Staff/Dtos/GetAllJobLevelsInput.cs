using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetAllJobLevelsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string ShortNameFilter { get; set; }

        public string JobTitleNameFilter { get; set; }
    }
}
