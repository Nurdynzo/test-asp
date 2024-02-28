using System;
using Abp.Domain.Entities;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.PatientWallet.Dtos.WalletRefund
{
    public class RefundRequestListQueryResponse: Entity<long>
    {
        public string PatientName { get; set; }
        public string Ward { get; set; }
        public string PatientCode { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public MoneyDto TotalReceiptAmount { get; set; }
        public MoneyDto TotalRefundAmount { get; set; }
        public bool IsApprove { get; set; }
        public bool IsReject { get; set; }
        public bool IsPendingApproval { get; set; }
        public InvoiceSource InvoiceSource { get; set; }

        public DateTime CreationDate { get; set; }
    }

}