using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetInvoiceToApproveCrediteDto
    {
        public long InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<GetInvoiceItemsToApproveCreditDto> Items { get; set; }
    }
}
