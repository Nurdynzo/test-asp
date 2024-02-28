using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class CountryDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }
        public string CurrencySymbol {get; set;}
        public string Currency { get; set; }
        public string CurrencyCode { get; set; }
    }
}
