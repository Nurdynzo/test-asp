using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetAllBedTypesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public long FacilityId { get; set; }
    }
}
