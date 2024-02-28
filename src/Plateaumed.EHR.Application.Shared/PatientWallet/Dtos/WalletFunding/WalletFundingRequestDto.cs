using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.PatientWallet.Dtos.WalletFunding
{
    public class WalletFundingRequestDto
    {
        [Required(ErrorMessage = "PatientId is required")]
        public long PatientId { set; get; }

        public MoneyDto TotalAmount { set; get; }

        public MoneyDto AmountToBeFunded { set; get; }

        public ICollection<WalletFundingItem> InvoiceItems { get; set; }
    }
}
