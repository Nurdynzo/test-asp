
namespace Plateaumed.EHR.Invoices.Dtos;

public class GetInvoiceItemPricingResponse
{
  public long Id { get; set; }
  public string Name { get; set; }
  public string DiscountName { get; set; }
  public MoneyDto Amount { get; set; }
  public decimal DiscountPercentage { get; set; }
  public bool IsGlobal { get; set; }
}