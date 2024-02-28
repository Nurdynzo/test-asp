using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Invoices.Dtos.PatientWithInvoiceItemsDtos
{
    public class PatientsWithInvoiceItemsRequest : PagedResultRequestDto
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        public string Filter { get; set; }
        public PatientSeenFilter? PatientSeenFilter { get; set; }

        public InvoiceSource? InvoiceSource { get; set; }
    }
}
