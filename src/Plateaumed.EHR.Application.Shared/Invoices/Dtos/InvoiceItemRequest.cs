using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class InvoiceItemRequest : EntityDto<long?>
    {
        public string Name { get; set; }
        
        public int Quantity { get; set; }
        
        public MoneyDto UnitPrice { get; set; }
        
        public MoneyDto SubTotal { get; set; }

        [Range(InvoiceItemConsts.MinDiscountPercentageValue, InvoiceItemConsts.MaxDiscountPercentageValue)]
        public decimal DiscountPercentage { get; set; }

        public bool IsGlobal { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
