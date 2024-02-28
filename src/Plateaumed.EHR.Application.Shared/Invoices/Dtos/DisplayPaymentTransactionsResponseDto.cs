using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class DisplayPaymentTransactionsResponseDto
    {
        public string TenantName { get; set; }
        public int Index { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string ServiceCenter { get; set; }
        public decimal InvoiceTotal { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal OutStandingAmount { get; set; }
        public decimal WalletTopUpTotal { get; set; }
        public string InvoiceNo { get; set; }
        public string TransactionType { get; set; }
        public string TransactionAction { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
