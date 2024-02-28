using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.Tests.PatientWallet.Utils
{
    public static class CommonUtils
    {
        public static MoneyDto GetMoneyDto(decimal amount = 100.00M, string currency = "NGN")
        {
            return new MoneyDto { Amount = amount, Currency = currency };
        }

        public static Wallet GetNewWallet(decimal balance = 0.00M, long patientId = 1, int tenantId = 1)
        {

            return new Wallet
            {
                PatientId = patientId,
                Balance = new Money { Amount = balance, Currency = "NGN" },
                TenantId = tenantId
            };
        }

        public static WalletFundingRequestDto GetWalletFundingRequestsDto(decimal totalAmount = 400, decimal amountToBeFunded = 1000.00M)
        {

            return new WalletFundingRequestDto() {

                PatientId = 1,
                TotalAmount = GetMoneyDto(totalAmount),
                AmountToBeFunded = GetMoneyDto(amountToBeFunded),
                InvoiceItems = new List<WalletFundingItem>() {
                    new() {
                        Id = 1,
                        InvoiceId = 1,
                        SubTotal = GetMoneyDto(),
                    },
                    new() {
                        Id = 2,
                        InvoiceId = 1,
                        SubTotal = GetMoneyDto(),
                    },
                    new() {
                        Id = 3,
                        InvoiceId = 1,
                        SubTotal = GetMoneyDto(),
                    },
                    new() {
                        Id = 4,
                        InvoiceId = 1,
                        SubTotal = GetMoneyDto(),
                    }, 
                },
            };
        }
        
        public static IQueryable<InvoiceItem> GetUnpaidInvoiceItems(long facilityId = 1) 
        {

            return new List<InvoiceItem>() {
                new()
                {
                    Id = 1,
                    Name = "Test 1",
                    Quantity = 1,
                    UnitPrice = new Money(100),
                    SubTotal = new Money(100),
                    DiscountPercentage = 0,
                    InvoiceId = 1,
                    AmountPaid = new Money(0.00M),
                    OutstandingAmount = new Money(100),
                    Status = InvoiceItemStatus.Unpaid,
                    FacilityId = facilityId,
                },
                new()
                {
                    Id = 2,
                    Name = "Test 2",
                    Quantity = 1,
                    UnitPrice = new Money(100),
                    SubTotal = new Money(100),
                    DiscountPercentage = 0,
                    InvoiceId = 1,
                    AmountPaid = new Money(0.00M),
                    OutstandingAmount = new Money(100),
                    Status = InvoiceItemStatus.Unpaid,
                    FacilityId = facilityId,
                },
                new()
                {
                    Id = 3,
                    Name = "Test 3",
                    Quantity = 1,
                    UnitPrice = new Money(100),
                    SubTotal = new Money(100),
                    DiscountPercentage = 0,
                    InvoiceId = 1,
                    AmountPaid = new Money(0.00M),
                    OutstandingAmount = new Money(100),
                    Status = InvoiceItemStatus.Unpaid,
                    FacilityId = facilityId,
                },
                new()
                {
                    Id = 4,
                    Name = "Test 4",
                    Quantity = 1,
                    UnitPrice = new Money(100),
                    SubTotal = new Money(100),
                    DiscountPercentage = 0,
                    InvoiceId = 1,
                    AmountPaid = new Money(0.00M),
                    OutstandingAmount = new Money(100),
                    Status = InvoiceItemStatus.Unpaid,
                    FacilityId = facilityId,
                },
            }.AsQueryable();
        }

        public static IQueryable<WalletHistory> GetWalletHistories(
            long patientId = 1, 
            long facilityId = 1,
            decimal walletBalance = 3000, 
            int tenantId = 1) {

            return new List<WalletHistory>() {

                new(){
                    WalletId = 1,
                    PatientId = patientId,
                    FacilityId = facilityId,
                    Amount = new Money(1000),
                    CurrentBalance = new Money(walletBalance),
                    TransactionType = TransactionType.Credit,
                    Source = TransactionSource.Indirect,
                    Status = TransactionStatus.Pending,
                    TenantId = tenantId,
                },
                new(){
                    WalletId = 1,
                    PatientId = patientId,
                    FacilityId = facilityId,
                    Amount = new Money(1000),
                    CurrentBalance = new Money(walletBalance),
                    TransactionType = TransactionType.Credit,
                    Source = TransactionSource.Indirect,
                    Status = TransactionStatus.Pending,
                    TenantId = tenantId
                },
            }.AsQueryable();
        }

        public static WalletFundingRequestDto GetWalletFundingRequestsForInvoiceItemsAwaitingApproval(long patientId = 1, decimal totalAmount = 598, decimal amountToBeFunded = 2000.00M)
        {

            return new WalletFundingRequestDto()
            {

                PatientId = patientId,
                TotalAmount = GetMoneyDto(totalAmount),
                AmountToBeFunded = GetMoneyDto(amountToBeFunded),
                InvoiceItems = new List<WalletFundingItem>() {
                    new() {
                        Id = 1,
                        InvoiceId = 1,
                        SubTotal = GetMoneyDto(98),
                    },
                    new() {
                        Id = 4,
                        InvoiceId = 2,
                        SubTotal = GetMoneyDto(500),
                    },
                },
            };
        }

        public static IQueryable<Invoice> GetInvoicesWithItemsAwaitingApprovalAsQueryable(int tenantId = 1, long patientId = 1, long appointmentId = 1)
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
                    AmountPaid = new Money(0.00M),
                    OutstandingAmount = new Money(100.00M),
                    TotalAmount = new Money(100.00M),
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
                            UnitPrice = new Money(100),
                            SubTotal = new Money(98),
                            DiscountPercentage = 2,
                            AmountPaid = new Money(0.00M),
                            OutstandingAmount = new Money(0.00M),
                            Status = InvoiceItemStatus.AwaitingApproval,
                        },
                        new()
                         {
                            Id = 2,
                            TenantId = tenantId,
                            FacilityId = 1,
                            Name = "Test 2",
                            InvoiceId = 1,
                            Quantity = 1,
                            UnitPrice = new Money(2),
                            SubTotal = new Money(2),
                            DiscountPercentage = 0,
                            AmountPaid = new Money(0.00M),
                            OutstandingAmount = new Money(0.00M),
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
                    AmountPaid = new Money(500.00M),
                    OutstandingAmount = new Money(500.00M),
                    TotalAmount = new Money(1000),
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
                            UnitPrice = new Money(500),
                            SubTotal = new Money(500),
                            DiscountPercentage = 0,
                            AmountPaid = new Money(500),
                            OutstandingAmount = new Money(500),
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
                            UnitPrice = new Money(500),
                            SubTotal = new Money(500),
                            DiscountPercentage = 0,
                            AmountPaid = new Money(0.00M),
                            OutstandingAmount = new Money(0.00M),
                            Status = InvoiceItemStatus.AwaitingApproval,
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
                    AmountPaid = new Money(1000.00M),
                    OutstandingAmount = new Money(1000.00M),
                    TotalAmount = new Money(1000),
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
                            UnitPrice = new Money(500),
                            SubTotal = new Money(500),
                            DiscountPercentage = 0,
                            AmountPaid = new Money(500),
                            OutstandingAmount = new Money(500),
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
                            UnitPrice = new Money(500),
                            SubTotal = new Money(500),
                            DiscountPercentage = 0,
                            AmountPaid = new Money(500),
                            OutstandingAmount = new Money(500),
                            Status = InvoiceItemStatus.Paid,
                        },
                    },
                },
            }.AsQueryable();
        }

        public static IQueryable<Invoice> GetInvoiceWithItemsAwaitingApprovalAsQueryable()
        {
            return new List<Invoice>
        {
            new()
            {
                Id = 1,
                PatientId = 1,
                InvoiceId = "0000000001",
                TotalAmount = new Money(681.58m),
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(681.58m),
                PaymentStatus = PaymentStatus.Unpaid,
                CreationTime = DateTime.Today,
                CreatorUserId = 1,
                PaymentType = PaymentTypes.Wallet,
                PatientAppointmentId = 1,
                TenantId = 1
            },
            new()
            {
                Id = 2,
                PatientId = 1,
                InvoiceId = "0000000002",
                TotalAmount = new Money(109.48m),
                AmountPaid = new Money(0),
                OutstandingAmount = new Money(109.48m),
                PaymentStatus = PaymentStatus.Unpaid,
                CreationTime = DateTime.Today,
                CreatorUserId = 1,
                PaymentType = PaymentTypes.Wallet,
                PatientAppointmentId = 1,
                TenantId = 1
            },

        }.AsQueryable();
        }

        public static IQueryable<InvoiceItem> GetInvoiceItemsAwaitingApprovalAsQueryable(long facilityId = 1)
        {
            return new List<InvoiceItem>
        {
            new(){
                Id = 1,
                Name = "Test1",
                Quantity = 1,
                UnitPrice = new Money(572.1m),
                SubTotal = new Money(572.1m),
                DiscountPercentage = 0,
                InvoiceId = 1,
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(572.1m),
                FacilityId = facilityId,
                Status = InvoiceItemStatus.AwaitingApproval,
                TenantId = 1
            },
            new(){
                Id = 2,
                Name = "Test2",
                Quantity = 1,
                UnitPrice = new Money(109.48m),
                SubTotal = new Money(109.48m),
                DiscountPercentage = 0,
                InvoiceId = 1,
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(109.48m),
                FacilityId = facilityId,
                Status = InvoiceItemStatus.AwaitingApproval,
                TenantId = 1
            },
            new(){
                Id = 3,
                Name = "Test3",
                Quantity = 1,
                UnitPrice = new Money(100),
                SubTotal = new Money(100),
                DiscountPercentage = 0,
                InvoiceId = 2,
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(100),
                FacilityId = facilityId,
                Status = InvoiceItemStatus.Unpaid,
            },
            new(){
                Id = 4,
                Name = "Test4",
                Quantity = 1,
                UnitPrice = new Money(100),
                SubTotal = new Money(98),
                DiscountPercentage = 2,
                InvoiceId = 2,
                AmountPaid = new Money(98),
                OutstandingAmount = new Money(0.00M),
                FacilityId = facilityId,
                Status = InvoiceItemStatus.Paid,
            },
            new(){
                Id = 5,
                Name = "Test5",
                Quantity = 1,
                UnitPrice = new Money(100),
                SubTotal = new Money(100),
                DiscountPercentage = 0,
                InvoiceId = 2,
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(100),
                FacilityId = facilityId,
                Status = InvoiceItemStatus.Unpaid,
            },
            }.AsQueryable();

        }
        public static ProcessRefundRequestCommand GetProcessRefundRequestCommand(decimal amount = 378.00M, long patientId = 1, bool isApproved = true)
        {
            var request = new ProcessRefundRequestCommand
            {
                IsApproved = isApproved,
                PatientId = patientId,
                TotalAmountToRefund = new MoneyDto
                {
                    Amount = amount,
                    Currency = "NGN"
                }
            };
            return request;
        }

        public static IQueryable<Invoice> GetInvoices()
        {
            return new List<Invoice>
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    InvoiceId = "0000000001",
                    TotalAmount = new Money(681.58m),
                    AmountPaid = new Money(0.00M),
                    OutstandingAmount = new Money(0),
                    PaymentStatus = PaymentStatus.Unpaid,
                    CreationTime = DateTime.Today,
                    CreatorUserId = 1,
                    PaymentType = PaymentTypes.Wallet,
                    PatientAppointmentId = 1
                    
                }
            }.AsQueryable();
        }
    }
}
