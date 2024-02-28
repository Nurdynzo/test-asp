using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class GetAllDistrictsInput : PagedAndSortedResultRequestDto
    {
        public string Filter {get; set;}
        public long RegionIdFilter {get; set;}
    }
}