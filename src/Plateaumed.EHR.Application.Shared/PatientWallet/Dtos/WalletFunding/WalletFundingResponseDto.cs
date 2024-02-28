using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using System.Collections.Generic;

namespace Plateaumed.EHR.PatientWallet.Dtos.WalletFunding
{
    public class WalletFundingResponseDto
    {
        public MoneyDto TotalAmount { set; get; }

        public MoneyDto AmountToBeFunded { set; get; }

        public List<UnpaidInvoiceDto> Invoices { get; set; }
    }
}
