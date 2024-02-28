using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Insurance.Dtos
{
    public class GetAllInsuranceProvidersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public InsuranceProviderType? TypeFilter { get; set; }

        public int? CountryIdFilter { get; set; }
    }
}