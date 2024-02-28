using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class CreateOrEditInvoiceDto : EntityDto<long?>
    {

        [Required]
        [StringLength(InvoiceConsts.MaxInvoiceIdLength, MinimumLength = InvoiceConsts.MinInvoiceIdLength)]
        public string InvoiceId { get; set; }

        public DateTime TimeOfInvoicePaid { get; set; }

        public long PatientId { get; set; }

        public long? PatientAppointmentId { get; set; }

    }
}