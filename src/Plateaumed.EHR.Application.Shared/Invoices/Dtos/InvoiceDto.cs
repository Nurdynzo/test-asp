using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class InvoiceDto : EntityDto<long>
    {
        public string InvoiceId { get; set; }

        public DateTime TimeOfInvoicePaid { get; set; }

        public long PatientId { get; set; }

        public long? PatientAppointmentId { get; set; }

    }
}