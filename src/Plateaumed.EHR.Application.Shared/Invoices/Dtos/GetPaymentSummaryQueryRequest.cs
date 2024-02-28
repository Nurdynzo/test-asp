using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Invoices.Dtos;

public class GetPaymentSummaryQueryRequest : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }

    public PaymentTypes? PaymentType { get; set; }

    public decimal OutStandingAmount { get; set; }

    public decimal AmountPaid { get; set; }

    public decimal Amount { get; set; }
    
    
    public PatientSeenFilter? DateFilter { get; set; }

    [Required]
    public long PatientId { get; set; }
}