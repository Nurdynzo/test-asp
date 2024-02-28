

using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos
{
    public class UnpaidInvoiceItemDto : EntityDto<long>
    {
        public string Name { get; set; }

        public bool IsGlobal { get; set; }

        [Range(InvoiceItemConsts.MinDiscountPercentageValue, InvoiceItemConsts.MaxDiscountPercentageValue)]
        public decimal DiscountPercentage { get; set; }

        public MoneyDto SubTotal { get; set; }
    } 
}
