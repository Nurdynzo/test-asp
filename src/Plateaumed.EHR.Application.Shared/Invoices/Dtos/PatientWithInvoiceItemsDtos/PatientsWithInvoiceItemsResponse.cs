using Plateaumed.EHR.Authorization.Users;
using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.Invoices.Dtos.PatientWithUnpaidInvoiceItemsDtos
{
    public class PatientsWithInvoiceItemsResponse
    {
        public long PatientId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PatientCode { get; set; }

        public DateTime DateOfBirth { get; set; }

        public GenderType GenderType { get; set; }

        public MoneyDto WalletBalance { get; set; }

        public MoneyDto TotalOutstanding { get; set; } 

        public DateTime? LastPaymentDate { get; set; }

        public bool HasPendingWalletFundingRequest { get; set; }

        public string AppointmentStatus { get; set; }

        public IEnumerable<InvoiceItemSummary> InvoiceItems { get; set; }
    }
}
