using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class InvoiceItemDto : EntityDto<long>
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public decimal SubTotal { get; set; }

        public string Notes { get; set; }

    }
    public class InvoiceItemResponse : EntityDto<long>
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public MoneyDto UnitPrice { get; set; }

        public MoneyDto DiscountAmount { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public MoneyDto SubTotal { get; set; }

        public string Notes { get; set; }

    }
}