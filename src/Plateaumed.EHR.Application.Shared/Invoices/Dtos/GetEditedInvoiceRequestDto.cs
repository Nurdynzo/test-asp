using Abp.Application.Services.Dto;
using System;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetEditedInvoiceRequestDto : PagedAndSortedResultRequestDto
    {
        public string SearchText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndTime { get; set; }
        public PatientSeenFilter FilterDate { get; set; }

        public InvoiceSource? InvoiceSource { get; set; }
    }
}
