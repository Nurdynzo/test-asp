using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetAllFacilityGroupsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
