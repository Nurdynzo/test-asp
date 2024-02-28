using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetAllFacilityStaffInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}