using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Invoices.Dtos.InvoiceRelief
{
    public class ApproveDebtReliefRequestDto
    {
        public int TenantId { get; set; }
        public long FacilityId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public List<long> SelectedInvoiceItemIds { get; set; }
    }
}
