using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Invoices.Dtos;

public class PaymentLandingListFilterRequest: PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
    public PatientSeenFilter? PatientSeenFilter { get; set; }
    public DateTime? CustomStartDateFilter { get; set; }
    public DateTime? CustomEndDateFilter { get; set; }
    public InvoiceSource? InvoiceSource { get; set; }

    public FilterType FilterType { get; set; }
}