using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Invoices.Dtos.InvoiceRelief
{
    public class ApplyReliefInvoiceViewDto
    {
        public long Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<ApplyReliefItemsViewDto> GroupedInvoiceItems { get; set; }
        public string InitiatedBy { get; set; }
    }
}
