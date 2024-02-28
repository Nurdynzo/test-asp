using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class GetAllRegionsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string CountryCodeFilter {get; set;}
    }
}