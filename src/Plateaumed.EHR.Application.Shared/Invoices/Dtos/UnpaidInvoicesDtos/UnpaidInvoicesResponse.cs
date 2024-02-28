using System.Collections.Generic;

namespace Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos
{
    public class UnpaidInvoicesResponse
    {
        public MoneyDto TotalAmount { get; init; }

        public IReadOnlyList<UnpaidInvoiceDto> Invoices { get; init; }
    }
}
