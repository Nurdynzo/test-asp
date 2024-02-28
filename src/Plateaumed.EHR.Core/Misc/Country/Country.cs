using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;
using System.Collections.Generic;

namespace Plateaumed.EHR.Misc.Country
{
    [Table("Countries")]
    [Audited]
    public class Country : FullAuditedEntity<int>
    {
        [Required]
        [StringLength(CountryConsts.MaxNameLength, MinimumLength = CountryConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            CountryConsts.MaxNationalityLength,
            MinimumLength = CountryConsts.MinNationalityLength
        )]
        public string Nationality { get; set; }

        [Required]
        [StringLength(CountryConsts.MaxCodeLength, MinimumLength = CountryConsts.MinCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(
            CountryConsts.MaxPhoneCodeLength,
            MinimumLength = CountryConsts.MinPhoneCodeLength
        )]
        public string PhoneCode { get; set; }

        [Required]
        [StringLength(CountryConsts.MaxCurrencyLength, MinimumLength = CountryConsts.MinCurrencyLength)]
        public string Currency { get; set; }

        [Required]
        [StringLength(CountryConsts.MaxCurrencyCodeLength, MinimumLength = CountryConsts.MinCurrencyCodeLength)]
        public string CurrencyCode { get; set; }

        public ICollection<Region> Regions {get; set;}

        [Required]
        [StringLength(CountryConsts.MaxCurrencySymbolLength, MinimumLength = CountryConsts.MinCurrencySymbolLength)]
        public string CurrencySymbol { get; set; }

        public Country(string name, string nationality, string code, string phoneCode, string currency, string currencyCode, string currencySymbol)
        {
            Name = name;
            Nationality = nationality;
            Code = code;
            PhoneCode = phoneCode;
            Currency = currency;
            CurrencyCode = currencyCode;
            CurrencySymbol = currencySymbol;
        }
    }
}