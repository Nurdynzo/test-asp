using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetEditedInvoiceItemResponseDto
    {
        public string InvoiceNumber { get; set; }
        public string ItemName { get; set; }
        public MoneyDto AmountBeforeEdit { get; set; }
        public MoneyDto EditedInvoice { get; set; }
        public MoneyDto AmountPaid { get; set; }
        public MoneyDto Outstanding { get; set; }
        public string EditedBy { get; set; }
        public DateTime DateEdited { get; set; }
        public string PaymentType { get; set; }
    }
}
