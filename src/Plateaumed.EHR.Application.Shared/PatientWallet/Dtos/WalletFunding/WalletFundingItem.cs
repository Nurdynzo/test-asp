using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.PatientWallet.Dtos.WalletFunding
{
    public class WalletFundingItem : EntityDto<long>
    {
        [Required(ErrorMessage ="InvoiceId is required")]
        public long InvoiceId { get; set; }

        [Required(ErrorMessage = "SubTotal is required")]
        public MoneyDto SubTotal { get; set; }
    }
}
