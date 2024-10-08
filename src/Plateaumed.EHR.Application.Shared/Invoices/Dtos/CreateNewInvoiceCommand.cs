﻿using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class CreateNewInvoiceCommand : EntityDto<long?>
    {
        [Required(ErrorMessage = "InvoiceNo is required")]
        public string InvoiceNo { set; get; }

        [Required(ErrorMessage = "AppointmentId is required")]
        public long AppointmentId { set; get; }

        [Required(ErrorMessage = "PatientId is required")]
        public long PatientId { set; get; }

        [Required(ErrorMessage = "PaymentType is required")]
        public PaymentTypes PaymentType { set; get; }
        
        public MoneyDto TotalAmount { set; get; }

        public bool IsServiceOnCredit { get; set; }

        public long? EncounterId { get; set; }

        public ICollection<InvoiceItemRequest> Items { get; set; }
    }
}
