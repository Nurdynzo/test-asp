namespace Plateaumed.EHR.Invoices.Dtos;

public record GetPatientTotalSummaryQueryResponse
{
    public int ItemsCounts { get; set; }
    public MoneyDto TotalTopUp { get; set; }
    public MoneyDto TotalAmount { get; set; }
    public MoneyDto ToTalPaid { get; set; }
    public MoneyDto TotalOutstanding { get; set; }

    public bool IsDiscountApplied { get; set; }

    public bool IsReliefApplied { get; set; }
}