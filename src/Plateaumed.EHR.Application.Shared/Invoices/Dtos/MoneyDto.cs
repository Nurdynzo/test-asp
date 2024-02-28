using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Invoices.Dtos;

public record  MoneyDto
{
    
    
    [Required]
    [DataType(DataType.Currency)]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Please enter a valid price with at most 2 decimal places.")]
    public decimal Amount { get; set; }
    [Required]
    public string Currency { get; set; }
}