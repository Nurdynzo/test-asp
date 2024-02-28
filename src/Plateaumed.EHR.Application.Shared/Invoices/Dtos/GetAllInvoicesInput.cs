using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetAllInvoicesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string InvoiceIdFilter { get; set; }

        public DateTime? MaxTimeOfInvoicePaidFilter { get; set; }
        public DateTime? MinTimeOfInvoicePaidFilter { get; set; }

        public string PatientPatientCodeFilter { get; set; }

        public string PatientAppointmentTitleFilter { get; set; }

    }
}