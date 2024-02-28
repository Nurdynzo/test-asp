using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CsvHelper.Configuration.Attributes;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class PaymentTransactionDisplayResponseDto : EntityDto<long>
    {
        [Name("S/N")]
        public int Index { get; set; }

        [Name("PATIENT ID")]
        public string PatientId { get; set; }

        [Name("PATIENT NAME")]
        public string PatientName { get; set; }

        [Name("AGE")]
        public string Age { get; set; }

        [Name("GENDER")]
        public string Gender { get; set; }

        [Name("SERVICE CENTER")]
        public string ServiceCenter { get; set; }

        [Name("INVOICE TOTAL (#)")]
        public decimal InvoiceTotal { get; set; }

        [Name("AMOUNT PAID (₦)")]
        public decimal AmountPaid { get; set; }

        [Name("OUTSTANDING AMOUNT (₦)")]
        public decimal OutStandingAmount { get; set; }

        [Name("WALLET TOP-UPS TOTAL")]
        public decimal WalletTopUpTotal { get; set; }

        [Name("INVOICE NO")]
        public string InvoiceNo { get; set; }

        [Name("TRANSACTION TYPE")]
        public string TransactionType { get; set; }

        [Name("TRANSACTION ACTION")]
        public string TransactionAction { get; set; }

        [Name("DATE")]
        public DateTime TransactionDate { get; set; }
    }
}
