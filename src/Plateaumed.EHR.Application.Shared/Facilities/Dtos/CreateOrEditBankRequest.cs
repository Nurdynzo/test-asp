using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditBankRequest : EntityDto<long?>
    {
        public long FacilityId { get; set; }

        [Required]
        [StringLength(FacilityConsts.MaxBankNameLength, MinimumLength = FacilityConsts.MinBankNameLength)]
        public string BankName { get; set; }

        [Required]
        [StringLength(FacilityConsts.MaxBankAccountHolderLength, MinimumLength = FacilityConsts.MinBankAccountHolderLength)]
        public string BankAccountHolder { get; set; }

        [Required]
        [StringLength(FacilityConsts.MaxBankAccountNumberLength, MinimumLength = FacilityConsts.MinBankAccountNumberLength)]
        public string BankAccountNumber { get; set; }

        public bool IsActive { get; set; }

        public  bool IsDefault { get; set; } 
    }
}
