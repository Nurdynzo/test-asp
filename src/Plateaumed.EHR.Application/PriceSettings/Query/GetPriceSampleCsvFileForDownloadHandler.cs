using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
namespace Plateaumed.EHR.PriceSettings.Query
{
    public class GetPriceSampleCsvFileForDownloadHandler : IGetPriceSampleCsvFileForDownloadHandler
    {


        public async Task<byte[]> Handle()
        {
            var memoryStream = new MemoryStream();
            await using var writer = new StreamWriter(memoryStream);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(GetSamples());
            await writer.FlushAsync();
            memoryStream.Position = 0;

            return memoryStream.ToArray();
        }

        private List<PricingCsvDto> GetSamples()
        {
            return new List<PricingCsvDto>()
            {
                new()
                {
                    Currency = "NGN",
                    PricingCategory = PricingCategory.Consultation.ToString(),
                    PricingType = PricingType.GeneralPricing.ToString(),
                    Name = "SampleName1",
                    ItemId = "ItemId1",
                    SubCategory = "SampleSubCategory1",
                    Amount = 1000
                },
                new()
                {
                    Currency = "NGN",
                    PricingCategory = PricingCategory.Laboratory.ToString(),
                    PricingType = PricingType.GeneralPricing.ToString(),
                    Name = "SampleName2",
                    ItemId = "ItemId2",
                    SubCategory = "SampleSubCategory2",
                    Amount = 2000
                },
                new()
                {
                    Currency = "NGN",
                    PricingCategory = PricingCategory.Procedure.ToString(),
                    PricingType = PricingType.GeneralPricing.ToString(),
                    Name = "SampleName3",
                    ItemId = "ItemId3",
                    SubCategory = "SampleSubCategory3",
                    Amount = 3000
                },
                new()
                {
                    Currency = "NGN",
                    PricingCategory = PricingCategory.WardAdmission.ToString(),
                    PricingType = PricingType.GeneralPricing.ToString(),
                    Name = "SampleName4",
                    ItemId = "ItemId4",
                    SubCategory = "SampleSubCategory4",
                    Amount = 4000
                },
                new()
                {
                    Currency = "NGN",
                    PricingCategory = PricingCategory.WardAdmission.ToString(),
                    PricingType = PricingType.GeneralPricing.ToString(),
                    Name = "SampleName5",
                    ItemId = "ItemId5",
                    SubCategory = "SampleSubCategory5",
                    Amount = 5000
                },
                new()
                {
                    Currency = "NGN",
                    PricingCategory = PricingCategory.Others.ToString(),
                    PricingType = PricingType.GeneralPricing.ToString(),
                    Name = "SampleName6",
                    ItemId = "ItemId6",
                    SubCategory = "SampleSubCategory6",
                    Amount = 6000
                }
            };
        }

    }
}
