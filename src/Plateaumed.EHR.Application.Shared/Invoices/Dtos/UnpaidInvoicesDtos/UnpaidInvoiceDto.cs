using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos
{
    public class UnpaidInvoiceDto : EntityDto<long>
    {
        public string InvoiceNo { set; get; }

        public MoneyDto TotalAmount { set; get; }

        public DateTime IssuedOn { get; set; }

        public IReadOnlyList<UnpaidInvoiceItemDto> InvoiceItems { get; set; }
    }
}
