using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization.Users;
namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetEditedInvoiceForDownloadResponse: EntityDto<long>
    {
        public string PatientCode { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public GenderType Gender { get; set; }
        public string ServiceCentre { get; set; }
        public MoneyDto Total { get; set; }
        public MoneyDto OutStanding { get; set; }
        public string InvoiceNo { get; set; }
        public string ItemName { get; set; }
        public MoneyDto ActualAmount { get; set; }
        public MoneyDto EditedAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}