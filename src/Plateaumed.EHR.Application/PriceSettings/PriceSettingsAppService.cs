using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Misc.Dtos;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;

namespace Plateaumed.EHR.PriceSettings
{
    public class PriceSettingsAppService : EHRAppServiceBase, IPriceSettingsAppService
    {
        private readonly IGetUnifyPriceItemSearchQueryHandler _getUnifyPriceItemSearchQueryHandler;
        private readonly IActivateDeactivatePriceCommandHandler _activateDeactivatePriceCommandHandler;
        private readonly ISaveExtendedPriceSettingsCommandHandler _saveExtendedPriceSettingsCommandHandler;
        private readonly ICreateNewPricingCommandHandler _createNewPricingCommandHandler;
        private readonly IGetPriceListQueryHandler _getPriceListQueryHandler;
        private readonly IEditPricingCommandHandler _editPricingCommandHandler;
        private readonly IUploadPriceSettingsCommandHandler _uploadPriceSettingsCommandHandler;
        private readonly IGetPriceSampleCsvFileForDownloadHandler _getPriceSampleCsvFileForDownloadHandler;
        private readonly IGetConsultationPriceSettingsHandler _getConsultationPriceSettingsHandler;
        private readonly IGetWardAdmissionPriceSettingsHandler _getWardAdmissionPriceSettingsHandler;
        private readonly IGetPriceMealSettingsHandler _getPriceMealSettingsHandler;
        private readonly IGetDiscountPriceSettingsHandler _getDiscountPriceSettingsHandler;
        private readonly ICreateInvestigationPricingCommandHandler _createInvestivationPricingHandler;
        private readonly IUpdateInvestigationPricingCommandHandler _updateInvestigationPricingCommmandHandler;
        private readonly IGetInvestigationPricesQueryHandler _getInvestigationPricesQueryHandler;

        public PriceSettingsAppService(
            IGetUnifyPriceItemSearchQueryHandler getUnifyPriceItemSearchQueryHandler,
            IActivateDeactivatePriceCommandHandler activateDeactivatePriceCommandHandler,
            ISaveExtendedPriceSettingsCommandHandler saveExtendedPriceSettingsCommandHandler,
            ICreateNewPricingCommandHandler createNewPricingCommandHandler,
            IGetPriceListQueryHandler getPriceListQueryHandler,
            IGetPriceSampleCsvFileForDownloadHandler getPriceSampleCsvFileForDownloadHandler,
            IEditPricingCommandHandler editPricingCommandHandler,
            IUploadPriceSettingsCommandHandler uploadPriceSettingsCommandHandler,
            IGetConsultationPriceSettingsHandler getConsultationPriceSettingsHandler,
            IGetWardAdmissionPriceSettingsHandler getWardAdmissionPriceSettingsHandler,
            IGetPriceMealSettingsHandler getPriceMealSettingsHandler,
            IGetDiscountPriceSettingsHandler getDiscountPriceSettingsHandler,
            ICreateInvestigationPricingCommandHandler createInvestigationPricingHandler,
            IUpdateInvestigationPricingCommandHandler updateInvestigationPricingCommmandHandler,
            IGetInvestigationPricesQueryHandler getInvestigationPricesQueryHandler)
        {
            _getUnifyPriceItemSearchQueryHandler = getUnifyPriceItemSearchQueryHandler;
            _activateDeactivatePriceCommandHandler = activateDeactivatePriceCommandHandler;
            _saveExtendedPriceSettingsCommandHandler = saveExtendedPriceSettingsCommandHandler;
            _createNewPricingCommandHandler = createNewPricingCommandHandler;
            _getPriceListQueryHandler = getPriceListQueryHandler;
            _editPricingCommandHandler = editPricingCommandHandler;
            _getPriceSampleCsvFileForDownloadHandler = getPriceSampleCsvFileForDownloadHandler;
            _uploadPriceSettingsCommandHandler = uploadPriceSettingsCommandHandler;
            _getConsultationPriceSettingsHandler = getConsultationPriceSettingsHandler;
            _getWardAdmissionPriceSettingsHandler = getWardAdmissionPriceSettingsHandler;
            _getPriceMealSettingsHandler = getPriceMealSettingsHandler;
            _getDiscountPriceSettingsHandler = getDiscountPriceSettingsHandler;
            _createInvestivationPricingHandler = createInvestigationPricingHandler;
            _updateInvestigationPricingCommmandHandler = updateInvestigationPricingCommmandHandler;
            _getInvestigationPricesQueryHandler = getInvestigationPricesQueryHandler;
        }
        public List<string> GetPriceTypes() =>
            Enum.GetValues<PricingType>()
                .AsEnumerable()
                .Select(x => x.ToString())
                .ToList();
        public List<string> GetPriceCategories() =>
            Enum.GetValues<PricingCategory>()
                .AsEnumerable()
                .Select(x => x.ToString())
                .ToList();
        public async Task<List<PriceItemsSearchResponse>> GetUnifyPriceItemSearch(PriceItemsSearchRequest request) =>
            await _getUnifyPriceItemSearchQueryHandler.Handle(request);
        public async Task ActivateDeactivatePrice(ActivateDeactivatePriceCommandRequest request) =>
            await _activateDeactivatePriceCommandHandler.Handle(request);

        public async Task CreateNewPricing(List<CreateNewPricingCommandRequest> request) =>
            await _createNewPricingCommandHandler.Handle(request);
        public async Task<PagedResultDto<GetPriceListQueryResponse>> GetPriceList(GetPriceListQueryRequest request) =>
            await _getPriceListQueryHandler.Handle(request);
        public async Task UploadPriceSettings([FromForm] UpdatePricingCommandRequest request) =>
            await _uploadPriceSettingsCommandHandler.Handle(request);
        public async Task EditPricing(EditPricingCommandRequest request) =>
            await _editPricingCommandHandler.Handle(request);
        public async Task<FileResult> DownloadPriceSampleCsvFileForUpload()
        {
            var result = await _getPriceSampleCsvFileForDownloadHandler.Handle();
            return new FileContentResult(result,"application/octet-stream")
            {
                FileDownloadName = "price_sample.csv"
            };
        }
        public async Task SaveExtendedPriceSettings(SaveExtendedPriceSettingsCommand request) =>
            await _saveExtendedPriceSettingsCommandHandler.Handle(request);

        public async Task<GetConsultationPriceSettingsResponse> GetConsultationPriceSettings(long facilityId) =>
            await _getConsultationPriceSettingsHandler.Handle(facilityId);

        public async Task<GetWardAdmissionPriceSettingsResponse> GetWardAdmissionPriceSettings(long facilityId) =>
            await _getWardAdmissionPriceSettingsHandler.Handle(facilityId);
        public async Task<GetPriceMealSettingsResponse> GetPriceMealSettings(long facilityId) =>
            await _getPriceMealSettingsHandler.Handle(facilityId);
        public async Task<GetDiscountPriceSettingsResponse> GetDiscountPriceSettings(long facilityId) =>
            await _getDiscountPriceSettingsHandler.Handle(facilityId);

        public async Task AddInvestigationPricing(List<CreateInvestigationPricingDto> request) => await _createInvestivationPricingHandler.Handle(request);

        public async Task UpdateInvestigationPricing(UpdateInvestigationPricingRequestCommand request)
             => await _updateInvestigationPricingCommmandHandler.Handle(request);

        public async Task<PagedResultDto<GetInvestigationPricingResponseDto>> GetInvestigationPricing(GetInvestigationPricingRequestDto request)
            => await _getInvestigationPricesQueryHandler.Handle(request);

        public List<IdentificationTypeDto> GetInvestigationPricingSortItems() =>
                    Enum.GetValues<InvestigationPricingSortFields>()
                    .AsEnumerable()
                    .Select(x => new IdentificationTypeDto()
                    {
                        Value = x.ToString(),
                        Label = InvestigationPricingSortFieldsDictionary.GetInvestigationSortFieldValue(x.ToString())
                    }).ToList();
        
    }
}
