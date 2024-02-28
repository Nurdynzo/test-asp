using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class InvoicePatientLookupTableDto
    {
        public long Id { get; set; }

        public string DisplayName { get; set; }
    }
}