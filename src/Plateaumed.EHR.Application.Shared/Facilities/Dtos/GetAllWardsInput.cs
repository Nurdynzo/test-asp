using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetAllWardsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string FacilityNameFilter { get; set; }

        public List<long> FacilityIds { get; set; }
    }
}
