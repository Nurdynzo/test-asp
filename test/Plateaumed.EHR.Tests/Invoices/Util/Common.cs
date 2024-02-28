using System;
using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.Tests.Invoices.Util;

public static class CommonUtil
{

    public static MoneyDto ToMoneyDto(this decimal amount)
        => new MoneyDto{ Amount = amount, Currency = "NGN" };
    
    public static CreateNewInvoiceCommand GetCreatNewInvoiceCommand(
        MoneyDto totalAmount = null,
        PaymentTypes paymentType = PaymentTypes.Wallet,
        long patientId = 1,
        long appointmentId = 1,
        bool isServiceOnCredit = true)
    {
        return new CreateNewInvoiceCommand
        {
            InvoiceNo = "00001",
            AppointmentId = patientId,
            PatientId = appointmentId,
            PaymentType = paymentType,
            TotalAmount = totalAmount ?? GetMoneyDto(),
            IsServiceOnCredit = isServiceOnCredit,
            Items = new List<InvoiceItemRequest>
            {
                new()
                {
                    Name = "Test",
                    Quantity = 1,
                    UnitPrice =  GetMoneyDto(100),
                    SubTotal = GetMoneyDto(98),
                    DiscountPercentage = 2,
                    IsGlobal = false,
                    IsDeleted = false
                },
                new()
                {
                    Name = "Test2",
                    Quantity = 2,
                    UnitPrice = GetMoneyDto(100),
                    SubTotal = GetMoneyDto(196),
                    DiscountPercentage = 2,
                    IsGlobal = false,
                    IsDeleted = true
                },
                new()
                {
                    Name = "Test3",
                    Quantity = 3,
                    UnitPrice = GetMoneyDto(100),
                    SubTotal = GetMoneyDto(270),
                    DiscountPercentage = 10,
                    IsGlobal = true,
                    IsDeleted = false
                }
            }
        };
    }

    public static CreateNewInvestigationInvoiceCommand GetCreatNewInvestigationInvoiceCommand(
        MoneyDto totalAmount = null,
        PaymentTypes paymentType = PaymentTypes.Wallet,
        long patientId = 1,
        long appointmentId = 1,
        bool isServiceOnCredit = true)
    {
        return new CreateNewInvestigationInvoiceCommand
        {
            InvoiceNo = "00001",
            AppointmentId = patientId,
            PatientId = appointmentId,
            PaymentType = paymentType,
            TotalAmount = totalAmount ?? GetMoneyDto(),
            IsServiceOnCredit = isServiceOnCredit,
            Items = new List<InvoiceItemRequest>
            {
                new()
                {
                    Name = "Test",
                    Quantity = 1,
                    UnitPrice =  GetMoneyDto(100),
                    SubTotal = GetMoneyDto(98),
                    DiscountPercentage = 2,
                    IsGlobal = false,
                    IsDeleted = false
                },
                new()
                {
                    Name = "Test2",
                    Quantity = 2,
                    UnitPrice = GetMoneyDto(100),
                    SubTotal = GetMoneyDto(196),
                    DiscountPercentage = 2,
                    IsGlobal = false,
                    IsDeleted = true
                },
                new()
                {
                    Name = "Test3",
                    Quantity = 3,
                    UnitPrice = GetMoneyDto(100),
                    SubTotal = GetMoneyDto(270),
                    DiscountPercentage = 10,
                    IsGlobal = true,
                    IsDeleted = false
                }
            }
        };
    }

    public static MoneyDto GetMoneyDto(decimal amount = 564, string currency = "NGN")
    {
        return new MoneyDto { Amount = amount, Currency = currency };
    }

    public static Money GetMoney(decimal amount = 100, string currency = "NGN")
    {
        return new Money { Amount = amount, Currency = currency };
    }

    public static IQueryable<InvoiceItem> GetInvoiceItemAsQueryable(long facilityId = 1)
    {
        return new List<InvoiceItem>
        {
            new()
            {
                Id = 1,
                Name = "Test",
                Quantity = 1,
                UnitPrice = new Money(100),
                SubTotal = new Money(98),
                DiscountPercentage = 2,
                InvoiceId = 2,
                AmountPaid = new Money(98),
                DebtReliefAmount = new Money(20),
                OutstandingAmount = new Money(0.00M),
                Status = InvoiceItemStatus.Paid,
                FacilityId = facilityId
            },
            new()
            {
                Id = 2,
                Name = "Test2",
                Quantity = 2,
                UnitPrice = new Money(100),
                SubTotal = new Money(196),
                DiscountPercentage = 2,
                InvoiceId = 2,
                AmountPaid = new Money(196),
                OutstandingAmount = new Money(0.00M),
                Status = InvoiceItemStatus.Paid,
                FacilityId = facilityId
            },
            new()
            {
                Id = 3,
                Name = "Test3",
                Quantity = 2,
                UnitPrice = new Money(100),
                SubTotal = new Money(196),
                DiscountPercentage = 2,
                InvoiceId = 1,
                AmountPaid = new Money(196),
                DebtReliefAmount = new Money(20),
                OutstandingAmount = new Money(0.00M),
                Status = InvoiceItemStatus.Paid,
            },
            new(){
                Name = "Test4",
                Quantity = 1,
                UnitPrice = new Money(100),
                SubTotal = new Money(98),
                DiscountPercentage = 2,
                InvoiceId = 1,
                AmountPaid = new Money(0.00M),
                DebtReliefAmount = new Money(20),
                OutstandingAmount = new Money(98.00M),
                FacilityId = facilityId
            },
            new()
            {
                Name = "Test5",
                Quantity = 2,
                UnitPrice = new Money(100),
                SubTotal = new Money(196),
                DiscountPercentage = 2,
                InvoiceId = 3,
                AmountPaid = new Money(196.00M),
                DebtReliefAmount = new Money(20),
                OutstandingAmount = new Money(0.00M),
                Status = InvoiceItemStatus.Paid,
                FacilityId = facilityId
            },
            new(){
                Name = "Test6",
                Quantity = 1,
                UnitPrice = new Money(100),
                SubTotal = new Money(98),
                DiscountPercentage = 2,
                InvoiceId = 3,
                AmountPaid = new Money(0.00M),
                DebtReliefAmount = new Money(20),
                OutstandingAmount = new Money(98.00M),
                FacilityId = facilityId
            },
            new()
            {
                Name = "Test3",
                Quantity = 2,
                UnitPrice = new Money(100),
                SubTotal = new Money(196),
                DiscountPercentage = 2,
                InvoiceId = 1,
                DiscountAmount = new Money(20),
                DebtReliefAmount = new Money(20),
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(196.00M),
                FacilityId = facilityId
            },
            new()
            {
                Id = 4,
                Name = "Test9",
                Quantity = 1,
                UnitPrice = new Money(564),
                SubTotal = new Money(564),
                DiscountPercentage = 0,
                InvoiceId = 4,
                AmountPaid = new Money(564),
                DebtReliefAmount = new Money(20),
                OutstandingAmount = new Money(0.00M),
                Status = InvoiceItemStatus.Paid,
                FacilityId = facilityId
            },
            }.AsQueryable();

    }

    public static IQueryable<User> GetUserAsQueryable()
    {
        return new List<User>
        {
            new()
            {
                Id = 1,
                UserName = "jnash",
                Name = "John",
                Surname = "Nash",
                EmailAddress = "j@j.com",
                Title = TitleType.Dr

        }}.AsQueryable();
    }

    public static IQueryable<Invoice> GetInvoiceAsQueryable()
    {
        return new List<Invoice>
        {
            new()
            {
                Id = 1,
                PatientId = 1,
                InvoiceId = "0000000001",
                TotalAmount = new Money(100),
                PaymentStatus = PaymentStatus.Unpaid,
                AmountPaid = new Money(0.00M),
                FacilityId = 1,
                CreationTime = DateTime.Today.AddDays(-10),
                CreatorUserId = 1,
                PaymentType = PaymentTypes.Wallet,
                PatientAppointmentId = 1,
                InvoiceItems = new List<InvoiceItem>()
                {

                }
            },
            new()
            {
                Id = 2,
                PatientId = 1,
                InvoiceId = "0000000002",
                TotalAmount = new Money(564),
                PaymentStatus = PaymentStatus.Paid,
                CreationTime = DateTime.Today.AddDays(-1),
                CreatorUserId = 1,
                PaymentType = PaymentTypes.Wallet,
                PatientAppointmentId = 1,
            },
            new()
            {
                Id = 3,
                PatientId = 1,
                InvoiceId = "0000000003",
                TotalAmount = new Money(564),
                PaymentStatus = PaymentStatus.PartiallyPaid,
                CreationTime = DateTime.Today.AddDays(-1),
                CreatorUserId = 1,
                PaymentType = PaymentTypes.Wallet,
                PatientAppointmentId = 1

            },
            new()
            {
                Id = 4,
                PatientId = 3,
                InvoiceId = "0000000004",
                TotalAmount = new Money(564),
                PaymentStatus = PaymentStatus.Paid,
                CreationTime = DateTime.Today.AddDays(-1),
                CreatorUserId = 1,
                PaymentType = PaymentTypes.Wallet,
                PatientAppointmentId = 1,
            },

        }.AsQueryable();
    }

    public static IQueryable<PatientAppointment> GetPatientAppointmentAsQueryable()
    {
        return new List<PatientAppointment>
        {
            new()
            {
                Id = 1,
                Status = AppointmentStatusType.Not_Arrived,
                PatientId = 1

            }
        }.AsQueryable();
    }

    public static IQueryable<Invoice> GetInvoiceWithInvoiceItemsAsQueryable(long itemId = 10)
    {
        var invoice = GetInvoiceAsQueryable();
        foreach (var item in invoice)
        {
            item.InvoiceItems = GetInvoiceItemAsQueryable().Where(x => x.InvoiceId == item.Id).ToList();
        }
        invoice.First(x => x.Id == 1).InvoiceItems.Add(new InvoiceItem()
        {
            Id = itemId,
            Name = "Test4",
            Quantity = 3,
            UnitPrice = new Money(100),
            SubTotal = new Money(270.00M),
            DiscountPercentage = 10,
            InvoiceId = 1,
            AmountPaid = new Money(200.00M),
            OutstandingAmount = new Money(270.00M),
        });
        return invoice;
    }

    public static IQueryable<PaymentActivityLog> GetPaymentActivityLogAsQueryableForIntegrationTest(
          int tenantId = 1, long invoiceId = 1, long patientId = 1)
    {
        return new List<PaymentActivityLog>()
        {
            new()
            {
                Id = 1,
                AmountPaid = new Money(100),
                ActualAmount = new Money(100),
                PatientId = patientId,
                TransactionAction = TransactionAction.FundWallet,
                TransactionType = TransactionType.Credit,
                ToUpAmount = new Money(100.00M),
                TenantId = tenantId,

            },
            new()
            {
                Id = 2,
                AmountPaid = new Money(100),
                ActualAmount = new Money(100),
                PatientId = patientId,
                TransactionAction = TransactionAction.FundWallet,
                TransactionType = TransactionType.Credit,
                ToUpAmount = new Money(300.00M),
                TenantId = tenantId,

            },
            new()
            {
                Id = 3,
                AmountPaid = new Money(0.00M),
                ActualAmount = new Money(100),
                OutStandingAmount = new Money(100.00M),
                InvoiceItemId = 1,
                PatientId = patientId,
                TransactionAction = TransactionAction.PaidInvoice,
                TransactionType = TransactionType.Other,
                InvoiceId = invoiceId,
                InvoiceNo = "Test",
                Narration = "Test",
                TenantId = tenantId,
            },
            new ()
            {
            Id = 4,
            AmountPaid = new Money(0.00M),
            ActualAmount = new Money(100),
            OutStandingAmount = new Money(100.00M),
            InvoiceItemId = 2,
            PatientId = 1,
            CreationTime = DateTime.Today,
            CreatorUserId = 1,
            TransactionAction = TransactionAction.CreateProforma,
            Narration = "Test 4",
            TenantId = tenantId,

        },
        new()
        {
            Id = 5,
            AmountPaid = new Money(0.00M),
            ActualAmount = new Money(100),
            OutStandingAmount = new Money(100.00M),
            InvoiceId = invoiceId,
            InvoiceItemId = 3,
            PatientId = patientId,
            TenantId = tenantId,
            CreationTime = DateTime.Today,
            CreatorUserId = 1,
            TransactionAction = TransactionAction.CreateInvoice,
            Narration = "Test 5",

        }

        }.AsQueryable();
    }
    public static IQueryable<PaymentActivityLog> GetPaymentActivityLogAsQueryable(int tenantId = 1)
    {
        return new List<PaymentActivityLog>()
        {
            new()
            {
                Id = 1,
                AmountPaid = new Money(100),
                ActualAmount = new Money(100),
                InvoiceId = 1,
                InvoiceItemId = 1,
                PatientId = 1,
                TransactionAction = TransactionAction.FundWallet,
                TransactionType = TransactionType.Credit,
                ToUpAmount = new Money(100.00M),
                TenantId = tenantId,

            },
            new()
            {
                Id = 2,
                AmountPaid = new Money(100),
                ActualAmount = new Money(100),
                InvoiceId = 1,
                InvoiceItemId = 2,
                PatientId = 1,
                TransactionAction = TransactionAction.FundWallet,
                TransactionType = TransactionType.Credit,
                ToUpAmount = new Money(300.00M),
                TenantId = tenantId,

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
                TenantId = tenantId,
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
            TenantId = tenantId,
            CreationTime = DateTime.Today,
            CreatorUserId = 1,
            TransactionAction = TransactionAction.CreateProforma,
            Narration = "Test 4",

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
            TenantId = tenantId,
            CreationTime = DateTime.Today,
            CreatorUserId = 1,
            TransactionAction = TransactionAction.CreateInvoice,
            Narration = "Test 5",

        }

        }.AsQueryable();
    }

    public static IQueryable<Invoice> GetInvoicesForPaymentSummaryAsQueryable(
        long patientId = 1,
        long appointmentId = 1,
        int tenantId = 1)
    {
        return new List<Invoice>()
         {
             new()
             {
                 Id = 1,
                 PatientId = patientId,
                 PatientAppointmentId = appointmentId,
                 TotalAmount = new Money(100.00M),
                 InvoiceSource = InvoiceSource.OutPatient,
                 PaymentType = PaymentTypes.SplitPayment,
                 InvoiceId = "Test00001",
                 PaymentStatus = PaymentStatus.Unpaid,
                 TenantId = tenantId,

             }
         }.AsQueryable();
    }

    public static IQueryable<InvoiceItem> GetInvoiceItemsForPaymentSummaryAsQueryable(
        bool flagDifferentCurrency = false,
        long invoiceId = 1, int tenantId = 1, long facilityId = 1)
    {
        return new List<InvoiceItem>()
        {
            new()
            {
                TenantId = tenantId,
                Id = 1,
                InvoiceId = invoiceId,
                Name = "Test",
                Quantity = 1,
                UnitPrice = new Money(100.00M),
                SubTotal = new Money(100.00M),
                OutstandingAmount = new Money(100.00M),
                AmountPaid = new Money(0.00M),
                FacilityId = facilityId

            },
            new()
            {
                Id = 2,
                InvoiceId = invoiceId,
                TenantId = tenantId,
                Name = "Test2",
                Quantity = 2,
                UnitPrice = new Money(100.00M),
                SubTotal = new Money(200.00M, flagDifferentCurrency ? "USD" : "NGN"),
                OutstandingAmount = new Money(200.00M),
                AmountPaid = new Money(0.00M),
                FacilityId = facilityId
            },
            new()
            {
                Id = 3,
                InvoiceId = invoiceId,
                TenantId = tenantId,
                Name = "Test3",
                Quantity = 2,
                UnitPrice = new Money(100.00M),
                SubTotal = new Money(200.00M),
                OutstandingAmount = new Money(0.00M),
                AmountPaid = new Money(200.00M),
                FacilityId = facilityId

            },
            new()
            {
                Id = 4,
                InvoiceId = invoiceId,
                TenantId = tenantId,
                Name = "Test4",
                Quantity = 1,
                UnitPrice = new Money(100.00M),
                SubTotal = new Money(100.00M),
                OutstandingAmount = new Money(0.00M),
                AmountPaid = new Money(100.00M),
                FacilityId = facilityId
            }
        }.AsQueryable();
    }

    public static IQueryable<Facility> GetFacilities(int tenantId = 1)
    {
        return new List<Facility>
        {
            new ()
            {
                Id = 1,
                Name = "TestFacility",
                TenantId = tenantId
            }

        }.AsQueryable();
    }

    public static IQueryable<Invoice> GetInvoiceForRefundAsQueryable()
    {
        return new List<Invoice>
        {
            new()
            {
                TenantId = 1,
                PatientId = 1,
                Id = 1,
                InvoiceId = "0000000001",
                CreationTime = DateTime.Today,
                TimeOfInvoicePaid = DateTime.Today,
                TotalAmount = new Money(100.00M),
                PaymentStatus = PaymentStatus.Paid
            },
            new ()
            {
                TenantId = 1,
                PatientId = 1,
                Id = 2,
                InvoiceId = "0000000002",
                CreationTime = DateTime.Today,
                TimeOfInvoicePaid = DateTime.Today,
                TotalAmount = new Money(294.00M),
                PaymentStatus = PaymentStatus.Paid

            },
            new()
            {
                TenantId = 1,
                PatientId = 1,
                Id = 3,
                InvoiceId = "0000000003",
                CreationTime = DateTime.Today,
                TimeOfInvoicePaid = DateTime.Today,
                TotalAmount = new Money(294.00M),
                PaymentStatus = PaymentStatus.Paid

            },
            new ()
            {
                TenantId = 1,
                PatientId = 1,
                Id = 4,
                InvoiceId = "0000000004",
                CreationTime = DateTime.Today.AddDays(-30),
                TimeOfInvoicePaid = DateTime.Today.AddDays(-20),
                TotalAmount = new Money(294.00M),
                PaymentStatus = PaymentStatus.Paid


            },
            new ()
            {
                TenantId = 1,
                PatientId = 1,
                Id = 5,
                InvoiceId = "0000000005",
                CreationTime = DateTime.Today.AddDays(-7),
                TimeOfInvoicePaid = DateTime.Today.AddDays(-4),
                TotalAmount = new Money(294.00M),
                PaymentStatus = PaymentStatus.Paid
            },
            new ()
            {
                TenantId = 1,
                PatientId = 1,
                Id = 6,
                InvoiceId = "0000000006",
                CreationTime = DateTime.Today.AddDays(-14),
                TimeOfInvoicePaid = DateTime.Today.AddDays(-7),
                TotalAmount = new Money(294.00M),
                PaymentStatus = PaymentStatus.Paid

            },
            new ()
            {
                TenantId = 1,
                PatientId = 1,
                Id = 7,
                InvoiceId = "0000000007",
                CreationTime = DateTime.Today.AddDays(-1),
                TimeOfInvoicePaid = DateTime.Today,
                TotalAmount = new Money(294.00M),
                PaymentStatus = PaymentStatus.Paid

            },
            new()
            {
                TenantId = 1,
                PatientId = 1,
                Id = 8,
                InvoiceId = "0000000008",
                CreationTime = DateTime.Today.AddDays(-365),
                TimeOfInvoicePaid = DateTime.Today.AddDays(-365),
                TotalAmount = new Money(294.00M),
                PaymentStatus = PaymentStatus.Paid

            }
        }.AsQueryable();
    }

    public static Wallet GetNewWallet(decimal balance = 0.00M, long patientId = 1, int tenantId = 1)
    {

        return new Wallet
        {
            Id = 1,
            PatientId = patientId,
            Balance = new Money { Amount = balance, Currency = "NGN" },
            TenantId = tenantId
        };
    }

    public static WalletFundingRequestDto GetWalletFundingRequestsDto(long patientId = 1, decimal totalAmount = 2100)
    {

        return new WalletFundingRequestDto()
        {

            PatientId = patientId,
            TotalAmount = GetMoneyDto(totalAmount),
            InvoiceItems = new List<WalletFundingItem>() {
                new() {
                    Id = 1,
                    InvoiceId = 1,
                    SubTotal = GetMoneyDto(98),
                },
                new() {
                    Id = 2,
                    InvoiceId = 1,
                    SubTotal = GetMoneyDto(2),
                },
                new() {
                    Id = 3,
                    InvoiceId = 2,
                    SubTotal = GetMoneyDto(500),
                },
                new() {
                    Id = 4,
                    InvoiceId = 2,
                    SubTotal = GetMoneyDto(500),
                },
                new() {
                    Id = 5,
                    InvoiceId = 3,
                    SubTotal = GetMoneyDto(500),
                },
                new() {
                    Id = 6,
                    InvoiceId = 3,
                    SubTotal = GetMoneyDto(500),
                },
            },
        };
    }

    public static WalletFundingRequestDto GetInvalidWalletFundingRequestsDto()
    {

        return new WalletFundingRequestDto()
        {

            PatientId = 1,
            TotalAmount = GetMoneyDto(1100),
            InvoiceItems = new List<WalletFundingItem>() {
                new() {
                    Id = 1,
                    InvoiceId = 10,
                    SubTotal = GetMoneyDto(1100),
                },
            },
        };
    }

    public static IQueryable<Invoice> GetInvoicesToBePaidForAsQueryable(int tenantId = 1, long patientId = 1, long appointmentId = 1)
    {
        return new List<Invoice>()
        {
                new()
                {
                    Id = 1,
                    PatientId = patientId,
                    TenantId = tenantId,
                    FacilityId = 1,
                    InvoiceId = "0000000001",
                    AmountPaid = GetMoney(0.00M),
                    OutstandingAmount = GetMoney(100.00M),
                    TotalAmount = GetMoney(),
                    PaymentStatus = PaymentStatus.Unpaid,
                    CreationTime = DateTime.Today,
                    CreatorUserId = 1,
                    PaymentType = PaymentTypes.Wallet,
                    PatientAppointmentId = appointmentId,
                    InvoiceItems = new List<InvoiceItem>()
                    {
                         new()
                         {
                            Id = 1,
                            TenantId = tenantId,
                            FacilityId = 1,
                            Name = "Test 1",
                            InvoiceId = 1,
                            Quantity = 1,
                            UnitPrice = GetMoney(),
                            SubTotal = GetMoney(98),
                            DiscountPercentage = 2,
                            AmountPaid = GetMoney(0.00M),
                            OutstandingAmount = GetMoney(0.00M),
                            Status = InvoiceItemStatus.Unpaid,
                        },
                        new()
                         {
                            Id = 2,
                            TenantId = tenantId,
                            FacilityId = 1,
                            Name = "Test 2",
                            InvoiceId = 1,
                            Quantity = 1,
                            UnitPrice = GetMoney(2),
                            SubTotal = GetMoney(2),
                            DiscountPercentage = 0,
                            AmountPaid = GetMoney(0.00M),
                            OutstandingAmount = GetMoney(0.00M),
                            Status = InvoiceItemStatus.Unpaid,
                        },
                    },
                },
                new()
                {
                    Id = 2,
                    PatientId = patientId,
                    TenantId = tenantId,
                    FacilityId = 1,
                    InvoiceId = "0000000002",
                    AmountPaid = GetMoney(500.00M),
                    OutstandingAmount = GetMoney(500.00M),
                    TotalAmount = GetMoney(1000),
                    PaymentStatus = PaymentStatus.PartiallyPaid,
                    CreationTime = DateTime.Today,
                    CreatorUserId = 1,
                    PaymentType = PaymentTypes.Wallet,
                    PatientAppointmentId = appointmentId,
                    InvoiceItems = new List<InvoiceItem>()
                    {
                         new()
                         {
                            Id = 3,
                            TenantId = tenantId,
                            FacilityId = 1,
                            Name = "Test 1",
                            InvoiceId = 2,
                            Quantity = 1,
                            UnitPrice = GetMoney(500),
                            SubTotal = GetMoney(500),
                            DiscountPercentage = 0,
                            AmountPaid = GetMoney(500),
                            OutstandingAmount = GetMoney(500),
                            Status = InvoiceItemStatus.Paid,
                        },
                        new()
                         {
                            Id = 4,
                            TenantId = tenantId,
                            FacilityId = 1,
                            Name = "Test 2",
                            InvoiceId = 2,
                            Quantity = 1,
                            UnitPrice = GetMoney(500),
                            SubTotal = GetMoney(500),
                            DiscountPercentage = 0,
                            AmountPaid = GetMoney(0.00M),
                            OutstandingAmount = GetMoney(0.00M),
                            Status = InvoiceItemStatus.Unpaid,
                        },
                    },
                },
                new()
                {
                    Id = 3,
                    PatientId = patientId,
                    TenantId = tenantId,
                    FacilityId = 1,
                    InvoiceId = "0000000003",
                    AmountPaid = GetMoney(1000.00M),
                    OutstandingAmount = GetMoney(1000.00M),
                    TotalAmount = GetMoney(1000),
                    PaymentStatus = PaymentStatus.Paid,
                    CreationTime = DateTime.Today,
                    CreatorUserId = 1,
                    PaymentType = PaymentTypes.Wallet,
                    PatientAppointmentId = appointmentId,
                    InvoiceItems = new List<InvoiceItem>()
                    {
                         new()
                         {
                            Id = 5,
                            TenantId = tenantId,
                            FacilityId = 1,
                            Name = "Test 1",
                            InvoiceId = 3,
                            Quantity = 1,
                            UnitPrice = GetMoney(500),
                            SubTotal = GetMoney(500),
                            DiscountPercentage = 0,
                            AmountPaid = GetMoney(500),
                            OutstandingAmount = GetMoney(500),
                            Status = InvoiceItemStatus.Paid,
                        },
                         new()
                         {
                            Id = 6,
                            TenantId = tenantId,
                            FacilityId = 1,
                            Name = "Test 2",
                            InvoiceId = 3,
                            Quantity = 1,
                            UnitPrice = GetMoney(500),
                            SubTotal = GetMoney(500),
                            DiscountPercentage = 0,
                            AmountPaid = GetMoney(500),
                            OutstandingAmount = GetMoney(500),
                            Status = InvoiceItemStatus.Paid,
                        },
                    },
                },
            }.AsQueryable();
    }

    public static IQueryable<InvoiceRefundRequest> GetInvoiceRefundRequestList()
    {
        return new List<InvoiceRefundRequest>()
         {
             new()
             {
                 Id = 1,
                 PatientId = 1,
                 InvoiceId = 1,
                 InvoiceItemId = 1,
                 FacilityId = 1,
                 Status = InvoiceRefundStatus.Pending,
                 TenantId = 1

             },
             new()
             {
                 Id = 2,
                 PatientId = 1,
                 InvoiceId = 1,
                 InvoiceItemId = 2,
                 FacilityId = 1,
                 Status = InvoiceRefundStatus.Pending,
                 TenantId = 1
             },
             new()
             {
                 Id = 3,
                 PatientId = 1,
                 InvoiceId = 1,
                 InvoiceItemId = 3,
                 FacilityId = 1,
                 Status = InvoiceRefundStatus.Approved,
                 TenantId = 1
             }

         }.AsQueryable();
     }


    public static IQueryable<Invoice> GetProformaInvoiceAsQueryable(
        long facilityId = 1,
        long patientId = 1,
        int tenantId = 1,
        long appointmentId = 1)
    {
        return new List<Invoice>()
        {
            new ()
            {
                Id = 1,
                PatientId = patientId,
                TenantId = tenantId,
                FacilityId = facilityId,
                InvoiceType = InvoiceType.Proforma,
                PaymentStatus = PaymentStatus.Unpaid,
                CreationTime = DateTime.Today,
                PatientAppointmentId = appointmentId,
                CreatorUserId = 1,
                TotalAmount = GetMoney(178.00M),
                InvoiceItems = new List<InvoiceItem>
                {
                    new()
                    {
                        Id = 1,
                        TenantId = tenantId,
                        FacilityId = facilityId,
                        Name = "Test 1",
                        Quantity = 1,
                        UnitPrice = GetMoney(),
                        SubTotal = GetMoney(98),
                        DiscountPercentage = 2,
                        AmountPaid = GetMoney(0.00M),
                        OutstandingAmount = GetMoney(98.00M),
                        Status = InvoiceItemStatus.Unpaid,
                        
                    },
                    new()
                    {
                        Id = 2,
                        TenantId = tenantId,
                        FacilityId = facilityId,
                        Name = "Test 2",
                        Quantity = 1,
                        UnitPrice = GetMoney(80),
                        SubTotal = GetMoney(80),
                        DiscountPercentage = 0,
                        AmountPaid = GetMoney(0.00M),
                        OutstandingAmount = GetMoney(80.00M),
                        Status = InvoiceItemStatus.Unpaid,
                        
                    }
                }
            }
        }.AsQueryable();
    }

    public static IQueryable<Patient> GetPatientList() 
    {
        return new List<Patient>
        {
            new()
            {
                Id = 1,
                UuId = new Guid(),
                GenderType = GenderType.Male,
                FirstName = "Test firstname",
                LastName = "Test Lastname",
                DateOfBirth = DateTime.Parse("1995/07/26")
            },
            new()
            {
                Id = 2,
                UuId = new Guid(),
                GenderType = GenderType.Female,
                FirstName = "Test firstname2",
                LastName = "Test Lastname",
                DateOfBirth = DateTime.Parse("1995/07/26")
            }
        }.AsQueryable();
    }



    public static IQueryable<PaymentActivityLog> GetPaymentActivityLogForEditedInvoiceAsQueryable(int tenantId = 1)
    {
        return new List<PaymentActivityLog>
        {
            new()
            {
                Id = 1,
                AmountPaid = new Money(60),
                ActualAmount = new Money(100),
                EditAmount = new Money(20),
                OutStandingAmount = new Money(20),
                InvoiceId = 1,
                InvoiceItemId = 1,
                PatientId = 1,
                TransactionAction = TransactionAction.EditInvoice,
                TransactionType = TransactionType.Credit,
                ToUpAmount = new Money(100.00M),
                TenantId = tenantId,
                FacilityId = 1,
                CreationTime = DateTime.Today

            },
            new()
            {
                Id = 2,
                AmountPaid = new Money(60),
                ActualAmount = new Money(100),
                EditAmount = new Money(20),
                OutStandingAmount = new Money(20),
                InvoiceId = 1,
                InvoiceItemId = 2,
                PatientId = 1,
                TransactionAction = TransactionAction.EditInvoice,
                TransactionType = TransactionType.Credit,
                ToUpAmount = new Money(300.00M),
                TenantId = tenantId,
                FacilityId = 1,
                CreationTime = DateTime.Today
            },
            new()
            {
                Id = 3,
                AmountPaid = new Money(60),
                ActualAmount = new Money(100),
                EditAmount = new Money(20),
                OutStandingAmount = new Money(20),
                InvoiceItemId = 3,
                PatientId = 1,
                TransactionAction = TransactionAction.EditInvoice,
                TransactionType = TransactionType.Other,
                InvoiceId = 2,
                InvoiceNo = "Test",
                Narration = "Test",
                TenantId = tenantId,
                FacilityId = 1,
                CreationTime = DateTime.Today
            },
            new ()
            {
            Id = 4,
            AmountPaid = new Money(60),
            ActualAmount = new Money(100),
            EditAmount = new Money(20),
            OutStandingAmount = new Money(20),
            InvoiceId = 2,
            InvoiceItemId = 1,
            PatientId = 1,
            TenantId = tenantId,
            CreationTime = DateTime.Today,
            CreatorUserId = 1,
            TransactionAction = TransactionAction.EditInvoice,
            Narration = "Test 4",
            FacilityId = 1,
            
            
        },
        new()
        {
            Id = 5,
            AmountPaid = new Money(60),
            ActualAmount = new Money(100),
            EditAmount = new Money(20),
            OutStandingAmount = new Money(20),
            InvoiceId = 3,
            InvoiceItemId = 2,
            PatientId = 1,
            TenantId = tenantId,
            CreationTime = DateTime.Today,
            CreatorUserId = 1,
            TransactionAction = TransactionAction.EditInvoice,
            Narration = "Test 5",
            FacilityId = 1
        }

        }.AsQueryable();
    }
    public static IQueryable<PatientCodeMapping> GetPatientCodeMappingList()
    {
        return new List<PatientCodeMapping>()
        {
            new()
            {
                Id = 1,
                PatientId = 1,
                PatientCode = "Test",
                FacilityId = 1
                
            }
        }.AsQueryable();
    }
}
