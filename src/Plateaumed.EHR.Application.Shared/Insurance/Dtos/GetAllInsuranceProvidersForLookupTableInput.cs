using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Insurance.Dtos
{
    public class GetAllInsuranceProvidersForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public InsuranceProviderType? TypeFilter { get; set; }

        public int? CountryIdFilter { get; set; }
    }
}