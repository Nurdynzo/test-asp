using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Plateaumed.EHR.CsvUpload.Abstraction;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
using Plateaumed.EHR.Utility;
namespace Plateaumed.EHR.PriceSettings.Command
{
    public class UploadPriceSettingsCommandHandler : IUploadPriceSettingsCommandHandler
    {
        private const int AllowedFileSize = 3 * 1024 * 1024;
        private readonly IRepository<ItemPricing ,long> _itemPricingRepository;
        private readonly IAbpSession _abpSession;
        private readonly ICsvService _csvService;
        public UploadPriceSettingsCommandHandler(
            IRepository<ItemPricing, long> itemPricingRepository,
            IAbpSession abpSession,
            ICsvService csvService)
        {
            _itemPricingRepository = itemPricingRepository;
            _abpSession = abpSession;
            _csvService = csvService;
        }

        public async Task Handle(UpdatePricingCommandRequest request)
        {
            ValidateFile(request.FormFile);
            var priceItems =
                _csvService.ProcessCsvFile<PricingCsvDto>(request.FormFile);
            foreach (var priceItem in priceItems)
            {
                var itemPricing = new ItemPricing
                {
                    TenantId = _abpSession.GetTenantId(),
                    Name = priceItem.Name,
                    Amount = priceItem.Amount.ToMoney(priceItem.Currency.Trim()),
                    ItemId = priceItem.ItemId,
                    PricingType = Enum.Parse<PricingType>(priceItem.PricingType.Trim()),
                    SubCategory = priceItem.SubCategory,
                    PricingCategory = Enum.Parse<PricingCategory>(priceItem.PricingCategory.Trim()),
                    FacilityId = request.FacilityId,
                    IsActive = true
                };
                await _itemPricingRepository.InsertAsync(itemPricing);
            }


        }

        private void ValidateFile(IFormFile requestFormFile)
        {
            if (requestFormFile == null)
            {
                throw new UserFriendlyException("File is required");
            }
            if (!requestFormFile.FileName.ToLower().EndsWith(".csv"))
            {
                throw new UserFriendlyException("Invalid file format. Only CSV files are allowed");
            }
            if (requestFormFile.Length > AllowedFileSize)
            {
                throw new UserFriendlyException("File size must be less than 3MB");
            }
        }
    }
}
