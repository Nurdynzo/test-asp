using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetInvoiceItemForEditOutput
    {
        public CreateOrEditInvoiceItemDto InvoiceItem { get; set; }

    }
}