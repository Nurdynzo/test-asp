
namespace Plateaumed.EHR.Invoices.Dtos.PatientWithUnpaidInvoiceItemsDtos
{
    public class InvoiceItemSummary
    {
        public long Id { get; set; }    

        public string Name { get; set; }

        public string Status { get; set; }

        public MoneyDto SubTotal { get; set; }
    }
}
