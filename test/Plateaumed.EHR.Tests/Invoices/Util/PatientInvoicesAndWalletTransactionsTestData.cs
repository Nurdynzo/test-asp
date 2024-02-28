using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Tests.Invoices.Util
{
    public static class PatientInvoicesAndWalletTransactionsTestData
    {

        public static IQueryable<User> GetUsers()
        {
            return new List<User> {

                new(){
                    Id = 1,
                    TenantId = 1,
                    Title = TitleType.Mr,
                    Surname = "Sam",
                },
            }.AsQueryable();
        }

        public static IQueryable<PatientAppointment> GetPatientAppointmentsAsQueryable(long facilityId = 1)
        {
            return new List<PatientAppointment>
        {
            new(){
                Id = 1,
                TenantId = 1,
                PatientId = 1,
                Status = AppointmentStatusType.Awaiting_Vitals,
                StartTime = DateTime.Today,
                AttendingClinicId = 1,
                CreatorUserId = 1,
            },
            }.AsQueryable();

        }

        public static IQueryable<Invoice> GetInvoiceAsQueryable(long patientId = 1, long facilityId = 1)
        {
            return new List<Invoice>
        {
            new()
            {
                Id = 1,
                TenantId = 1,
                PatientId = patientId,
                InvoiceId = "0000000001",
                TotalAmount = new Money(100),
                AmountPaid = new Money(100),
                OutstandingAmount = new Money(0),
                PaymentStatus = PaymentStatus.Paid,
                CreationTime = DateTime.Today,
                CreatorUserId = 1,
                PaymentType = PaymentTypes.Wallet,
                PatientAppointmentId = 1,
                FacilityId = facilityId,
            },
        }.AsQueryable();
        }

        public static IQueryable<InvoiceItem> GetInvoiceItemAsQueryable(long facilityId = 1)
        {
            return new List<InvoiceItem>
        {
            new()
            {
                Id = 1,
                TenantId = 1,
                Name = "Test1",
                Quantity = 1,
                UnitPrice = new Money(100),
                SubTotal = new Money(100),
                DiscountPercentage = 0,
                InvoiceId = 1,
                AmountPaid = new Money(100.00M),
                OutstandingAmount = new Money(0.00M),
                Status = InvoiceItemStatus.Paid,
                FacilityId = facilityId,
                CreatorUserId = 1,
            },
            }.AsQueryable();

        }


        public static IQueryable<PaymentActivityLog> GetPaymentActivityLogAsQueryable(long facilityId = 1)
        {
            return new List<PaymentActivityLog> {

                new(){

                    Id = 1,
                    PatientId = 1,
                    TenantId = 1,
                    TransactionType = TransactionType.Credit,
                    CreationTime = DateTime.Today.AddMinutes(-100),
                    CreatorUserId = 1,
                    FacilityId = facilityId,
                    TransactionAction = TransactionAction.RequestWalletFunding,
                    Narration = "Wallet funding request",
                    ToUpAmount = new Money(2000),
                },
                
                new(){

                    Id = 2,
                    PatientId = 1,
                    TenantId = 1,
                    CreationTime = DateTime.Today.AddMinutes(-20),
                    CreatorUserId = 1,
                    FacilityId = facilityId,
                    TransactionType = TransactionType.Credit,
                    TransactionAction = TransactionAction.ApproveWalletFunding,
                    Narration = "Wallet fund approved",
                    ToUpAmount = new Money(2000),
                },


                new(){

                    Id = 3,
                    InvoiceId = 1,
                    PatientId = 1,
                    TenantId = 1,
                    InvoiceItemId = 1,
                    InvoiceNo =  "0000000001",
                    CreationTime = DateTime.Today,
                    CreatorUserId = 1,
                    FacilityId = facilityId,
                    TransactionAction = TransactionAction.PaidInvoiceItem,
                    Narration = "Test1",
                    AmountPaid = new Money(100),
                    OutStandingAmount = new Money(0.00M), 
                },
                
                new(){

                    Id = 4,
                    InvoiceId = 1,
                    PatientId = 1,
                    TenantId = 1,
                    InvoiceNo =  "0000000001",
                    CreationTime = DateTime.Today.AddMinutes(2),
                    CreatorUserId = 1,
                    FacilityId = facilityId,
                    TransactionType = TransactionType.Debit,
                    TransactionAction = TransactionAction.PaidInvoice,
                    AmountPaid = new Money(100),
                    OutStandingAmount = new Money(0.00M),
                },
            }.AsQueryable();

        }


    }
}
