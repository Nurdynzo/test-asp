using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Tests.Invoices.Util
{
    public static class GetPatientsWithInvoiceItemsTestData
    {

        public static IQueryable<Patient> GetPatients(long patientId = 1)
        {

            return new List<Patient> {

            new()
            {
                Id = patientId,
                FirstName = "Sam",
                LastName = "Ade",
                DateOfBirth = DateTime.Now.AddYears(-20).Date,
                GenderType = GenderType.Male,
            }
            }.AsQueryable();
        }

        public static IQueryable<PatientCodeMapping> GetPatientCodeMappings(long patientId = 1, long facilityId = 1)
        {

            return new List<PatientCodeMapping> {
            new()
            {
                Id = 1,
                PatientId = patientId,
                FacilityId = facilityId,
                PatientCode = "000001",
            },
            }.AsQueryable();
        }

        public static IQueryable<Wallet> GetWallets(decimal balance = 0.00M, long patientId = 1, int tenantId = 1)
        {

            return new List<Wallet> {
            new Wallet
            {
                Id = 1,
                PatientId = patientId,
                Balance = new Money { Amount = balance, Currency = "NGN" },
                TenantId = tenantId,
            }
            }.AsQueryable();
        }

        public static IQueryable<Invoice> GetInvoiceAsQueryable(long patientId = 1, long facilityId = 1)
        {
            return new List<Invoice>
        {
            new()
            {
                Id = 1,
                PatientId = patientId,
                InvoiceId = "0000000001",
                TotalAmount = new Money(100),
                PaymentStatus = PaymentStatus.Unpaid,
                CreationTime = DateTime.Today,
                CreatorUserId = 1,
                PaymentType = PaymentTypes.Wallet,
                PatientAppointmentId = 1,
                FacilityId = facilityId,
            },
            new()
            {
                Id = 2,
                PatientId = patientId,
                InvoiceId = "0000000002",
                TotalAmount = new Money(100),
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
                Name = "Test1",
                Quantity = 1,
                UnitPrice = new Money(50),
                SubTotal = new Money(50),
                DiscountPercentage = 0,
                InvoiceId = 1,
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(50),
                Status = InvoiceItemStatus.Unpaid,
                FacilityId = facilityId
            },
            new()
            {
                Id = 2,
                Name = "Test2",
                Quantity = 1,
                UnitPrice = new Money(50),
                SubTotal = new Money(50),
                DiscountPercentage = 0,
                InvoiceId = 1,
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(50),
                Status = InvoiceItemStatus.Unpaid,
                FacilityId = facilityId
            },
                new()
            {
                Id = 3,
                Name = "Test3",
                Quantity = 1,
                UnitPrice = new Money(100),
                SubTotal = new Money(100),
                DiscountPercentage = 0,
                InvoiceId = 2,
                AmountPaid = new Money(100),
                OutstandingAmount = new Money(0.00M),
                Status = InvoiceItemStatus.Paid,
                FacilityId = facilityId,
                LastModificationTime = DateTime.Today,
                LastModifierUserId = 1,
            },
            }.AsQueryable();

        }

        public static IQueryable<PatientAppointment> GetPatientAppointments()
        {
            return new List<PatientAppointment>()
            {
                new ()
                {
                    Id = 1,
                    PatientId = 1,
                    Status = AppointmentStatusType.Seen_Doctor,
                    Type = AppointmentType.Walk_In,
                    TenantId = 1,
                    CreationTime = DateTime.Today,
                }
            }.AsQueryable();
        }
    }
}
