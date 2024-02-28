using System;

namespace Plateaumed.EHR.Invoices.Dtos.PatientInvoicesAndWalletTransactionsDtos
{
    public class PatientInvoicesAndWalletTransactionsResponse
    {
        public long? InvoiceId { get; set; }

        public string InvoiceNo { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string InvoiceItemName { get; set; }  

        public string InvoiceItemStatus { get; set; } 

        public string GeneralStatus { get; set; }

        public bool IsProformaInvoice { get; set; }

        public MoneyDto TopUpAmount { get; set; }

        public MoneyDto InvoiceAmount { get; set; }

        public MoneyDto EditedAmount { get; set; } 

        public DateTime? ModifiedAt { get; set; }

        public string ModifiedBy { get; set; }

        public string Type { get; set; }
    }
}
