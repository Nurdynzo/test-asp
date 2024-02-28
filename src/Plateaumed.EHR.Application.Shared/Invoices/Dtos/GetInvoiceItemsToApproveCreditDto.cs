using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetInvoiceItemsToApproveCreditDto
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public MoneyDto ItemAmount { get; set; }
        public long InvoiceId { get; set; }
    }
}
