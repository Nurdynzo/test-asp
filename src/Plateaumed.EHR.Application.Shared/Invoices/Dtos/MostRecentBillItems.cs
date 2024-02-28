namespace Plateaumed.EHR.Invoices.Dtos;

public class MostRecentBillItems
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public MoneyDto SubTotal { get; set; }
    public MoneyDto AmountPaid { get; set; }
    public MoneyDto OutstandingAmount { get; set; }
    public string Notes { get; set; }
}