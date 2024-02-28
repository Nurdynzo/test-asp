using System;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetEditedInvoiceResponseDto
    {
        public long InvoiceId { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime EditDate { get; set; }
        public string Gender { get; set; }
        public string Ward { get; set; }
        public MoneyDto TotalAmountBeforeEdit { get; set; }
        public MoneyDto TotalEditedInvoiceAmount { get; set; }
        public MoneyDto TotalAmountPaid { get; set; }
        public MoneyDto TotalOutstanding { get; set; }
        public DateTime CreationTime { get; set; }
        public string PatientCode { get; set; }
        public InvoiceSource InvoiceSource { get; set; }
    }
}
