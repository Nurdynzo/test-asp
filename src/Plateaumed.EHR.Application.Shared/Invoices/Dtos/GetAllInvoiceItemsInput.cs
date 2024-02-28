using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetAllInvoiceItemsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string NameFilter { get; set; }
        public int? MaxQuantityFilter { get; set; }
        public int? MinQuantityFilter { get; set; }
        public decimal? MaxUnitPriceFilter { get; set; }
        public decimal? MinUnitPriceFilter { get; set; }
        public string NotesFilter { get; set; }

    }
}