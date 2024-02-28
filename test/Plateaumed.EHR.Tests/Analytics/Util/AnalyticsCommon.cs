using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Tests.Analytics.Util
{
    public static class AnalyticsCommon
    {
        public static IQueryable<Invoice> GetInvoicForAnalyticsAsQueryable(long facilityId = 1)
        {
            return new List<Invoice>
        {
            new()
            {
                InvoiceId = "0000001",
                TotalAmount = new Money(20000),
                FacilityId = 1,
                CreationTime = DateTime.Now.AddDays(-1)
            },
            new()
            {
                InvoiceId = "0000002",
                TotalAmount = new Money(40000),
                FacilityId = 1,
                CreationTime = DateTime.Today.AddDays(-7)
            },
            new()
            {
                InvoiceId = "0000003",
                TotalAmount = new Money(5000),
                FacilityId = 1,
                CreationTime = DateTime.Today.AddDays(-1)
            },
            new(){
                InvoiceId = "0000004",
                TotalAmount = new Money(10000),
                FacilityId = 1,
                CreationTime = DateTime.Today
            },
            new()
            {
                InvoiceId = "0000005",
                TotalAmount = new Money(20000),
                FacilityId = 1,
                CreationTime = DateTime.Today.AddDays(-7)
            },
            new(){
                InvoiceId = "0000006",
                TotalAmount = new Money(1000),
                FacilityId = 1,
                CreationTime = DateTime.Now.AddDays(-7)
            },
            }.AsQueryable();

        }


        public static IQueryable<PaymentActivityLog> GetLogActivitiesForAnalyticsAsQueryable(long facilityId = 1)
        {
            return new List<PaymentActivityLog>
            {

                    new()
            {
                Id = 1,
                AmountPaid = new Money(100),
                ActualAmount = new Money(100),
                OutStandingAmount = new Money(0.00M),
                InvoiceId = 1,
                InvoiceItemId = 1,
                PatientId = 1,
                TransactionAction = TransactionAction.FundWallet,
                TransactionType = TransactionType.Credit,
                ToUpAmount = new Money(100.00M),
                FacilityId = facilityId

            },
            new()
            {
                Id = 2,
                AmountPaid = new Money(100),
                ActualAmount = new Money(100),
                 OutStandingAmount = new Money(0.00M),
                InvoiceId = 1,
                InvoiceItemId = 2,
                PatientId = 1,
                TransactionAction = TransactionAction.FundWallet,
                TransactionType = TransactionType.Credit,
                ToUpAmount = new Money(300.00M),
                FacilityId = facilityId

            },
            new()
            {
                Id = 3,
                AmountPaid = new Money(0.00M),
                ActualAmount = new Money(100),
                OutStandingAmount = new Money(100.00M),
                InvoiceItemId = 3,
                PatientId = 1,
                TransactionAction = TransactionAction.PaidInvoice,
                TransactionType = TransactionType.Other,
                InvoiceId = 1,
                InvoiceNo = "Test",
                Narration = "Test",
                FacilityId = facilityId
            },
            new ()
            {
            Id = 4,
            AmountPaid = new Money(0.00M),
            ActualAmount = new Money(100),
            OutStandingAmount = new Money(100.00M),
            InvoiceId = 2,
            InvoiceItemId = 1,
            PatientId = 1,
            CreationTime = DateTime.Today.AddDays(-1),
            CreatorUserId = 1,
            TransactionAction = TransactionAction.FundWallet,
            Narration = "Test 4",
            ToUpAmount = new Money(300.00M),
            FacilityId = facilityId

        },
        new()
        {
            Id = 5,
            AmountPaid = new Money(0.00M),
            ActualAmount = new Money(100),
            OutStandingAmount = new Money(100.00M),
            InvoiceId = 2,
            InvoiceItemId = 2,
            PatientId = 1,
            CreationTime = DateTime.Today,
            CreatorUserId = 1,
            TransactionAction = TransactionAction.FundWallet,
            Narration = "Test 5",
            ToUpAmount = new Money(300.00M),
            FacilityId = facilityId

        },
        new()
        {
            Id = 6,
            AmountPaid = new Money(0.00M),
            ActualAmount = new Money(100),
            OutStandingAmount = new Money(100.00M),
            InvoiceId = 2,
            InvoiceItemId = 2,
            PatientId = 1,
            CreationTime = DateTime.Today,
            CreatorUserId = 1,
            TransactionAction = TransactionAction.ClearInvoice,
            Narration = "Test 5",
            ToUpAmount = new Money(300.00M),
            FacilityId = facilityId

        }

            }.AsQueryable();
        }

        public static IQueryable<InvoiceCancelRequest> GetInvoicesCancelForAnalyticsAsQueryable(long facilityId = 1)
        {
            return new List<InvoiceCancelRequest>
            {
                new()
                {
                    TenantId = 1,
                    PatientId = 1,
                    FacilityId = 1,
                    InvoiceId = 1,
                    Status = InvoiceCancelStatus.Approved,
                    CreationTime = DateTime.Now.AddDays(-1)
                },
                new()
                {
                    TenantId = 1,
                    PatientId = 1,
                    FacilityId = 1,
                    InvoiceId = 1,
                    Status = InvoiceCancelStatus.Rejected,
                    CreationTime = DateTime.Now
                },
                new()
                {
                    TenantId = 1,
                    PatientId = 1,
                    FacilityId = 1,
                    InvoiceId = 1,
                    Status = InvoiceCancelStatus.Approved,
                    CreationTime = DateTime.Now
                },
                new()
                {
                    TenantId = 1,
                    PatientId = 1,
                    FacilityId = 1,
                    InvoiceId = 1,
                    Status = InvoiceCancelStatus.Pending,
                    CreationTime = DateTime.Now
                }
            }.AsQueryable();

        }
    }
}
