using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos
{
    public class DownloadPaymentActivityRequest : PagedAndSortedResultRequestDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
