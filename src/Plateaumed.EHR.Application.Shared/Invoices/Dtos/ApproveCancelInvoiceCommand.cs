using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Invoices.Dtos;

public class ApproveCancelInvoiceCommand
{
    [Required]
    public long PatientId { get; set; }

    public bool IsApproved { get; set; }
}