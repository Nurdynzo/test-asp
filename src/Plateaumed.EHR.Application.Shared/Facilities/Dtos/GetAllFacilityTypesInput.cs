using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetAllFacilityTypesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }
    }
}
