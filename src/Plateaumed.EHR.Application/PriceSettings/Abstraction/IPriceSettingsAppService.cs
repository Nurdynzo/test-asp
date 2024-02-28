using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.Misc.Dtos;
using Plateaumed.EHR.PriceSettings.Dto;
namespace Plateaumed.EHR.PriceSettings.Abstraction
{
    public interface IPriceSettingsAppService : IApplicationService
    {
        List<string> GetPriceTypes();

        List<string> GetPriceCategories();
        Task<List<PriceItemsSearchResponse>> GetUnifyPriceItemSearch(PriceItemsSearchRequest request);

        Task CreateNewPricing(List<CreateNewPricingCommandRequest> request);

        Task ActivateDeactivatePrice(ActivateDeactivatePriceCommandRequest request);

        Task<PagedResultDto<GetPriceListQueryResponse>> GetPriceList(GetPriceListQueryRequest request);

        Task UploadPriceSettings(UpdatePricingCommandRequest request);

        Task<FileResult> DownloadPriceSampleCsvFileForUpload();

        Task EditPricing(EditPricingCommandRequest request);

        Task SaveExtendedPriceSettings(SaveExtendedPriceSettingsCommand request);

        Task<GetConsultationPriceSettingsResponse> GetConsultationPriceSettings(long facilityId);

        Task<GetWardAdmissionPriceSettingsResponse> GetWardAdmissionPriceSettings(long facilityId);

        Task<GetPriceMealSettingsResponse> GetPriceMealSettings(long facilityId);

        Task<GetDiscountPriceSettingsResponse> GetDiscountPriceSettings(long facilityId);

        Task<PagedResultDto<GetInvestigationPricingResponseDto>> GetInvestigationPricing(GetInvestigationPricingRequestDto request);

        Task AddInvestigationPricing(List<CreateInvestigationPricingDto> request);

        List<IdentificationTypeDto> GetInvestigationPricingSortItems();

    }
}
