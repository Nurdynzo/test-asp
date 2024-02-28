using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos
{
    public class UnpaidInvoicesRequest
    {
        [Required]
        public long PatientId { get; set; }
    }
}
