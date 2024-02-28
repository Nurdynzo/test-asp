using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos.PatientInvoicesAndWalletTransactionsDtos
{
    public class PatientInvoicesAndWalletTransactionsRequest : PagedResultRequestDto
    {
        public long PatientId { get; set; }
    }
}
