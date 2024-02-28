using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Organizations.Dtos
{
    public class GetAllOrganizationUnitTimesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int? DayOfTheWeekFilter { get; set; }

        public DateTime? MaxOpeningTimeFilter { get; set; }
        public DateTime? MinOpeningTimeFilter { get; set; }

        public DateTime? MaxClosingTimeFilter { get; set; }
        public DateTime? MinClosingTimeFilter { get; set; }

        public int? IsActiveFilter { get; set; }

        public string OrganizationUnitDisplayNameFilter { get; set; }

    }
}