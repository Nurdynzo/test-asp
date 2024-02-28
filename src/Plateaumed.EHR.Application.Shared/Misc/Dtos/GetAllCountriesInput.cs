using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class GetAllCountriesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
