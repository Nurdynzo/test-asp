using Abp.Application.Services.Dto;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.PatientWallet.Dtos.WalletRefund
{
    public class RefundRequestListQueryRequest: PagedAndSortedResultRequestDto
    {
        public InvoiceSource? InvoiceSource { get; set; }
        public PatientSeenFilter DateFilter { get; set; }
    }
}