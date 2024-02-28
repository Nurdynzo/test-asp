using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Invoices.Dtos.InvoiceRelief
{
    public class ApplyReliefItemsViewDto
    {
        public long Id { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public MoneyDto Price { get; set; }
        public MoneyDto ReliefAmount { get; set; }
        public bool IsReliefApplied { get; set; }
    }
}
