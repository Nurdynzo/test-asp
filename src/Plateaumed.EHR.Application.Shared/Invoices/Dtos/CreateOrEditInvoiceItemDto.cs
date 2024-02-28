using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class CreateOrEditInvoiceItemDto : EntityDto<long?>
    {

        [Required]
        [StringLength(InvoiceItemConsts.MaxNameLength, MinimumLength = InvoiceItemConsts.MinNameLength)]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? DiscountAmount { get; set; }

        [Range(InvoiceItemConsts.MinDiscountPercentageValue, InvoiceItemConsts.MaxDiscountPercentageValue)]
        public decimal? DiscountPercentage { get; set; }

        [StringLength(InvoiceItemConsts.MaxNotesLength, MinimumLength = InvoiceItemConsts.MinNotesLength)]
        public string Notes { get; set; }

    }
}