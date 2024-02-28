using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.ValueObjects;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;

[Trait("Category", "Unit")]
public class PaymentLandingListQueryHandlerTest
{
    private readonly IRepository<Invoice,long> _invoiceRepositoryMock
        = Substitute.For<IRepository<Invoice,long>>();
    private readonly IRepository<Patient,long> _patientRepositoryMock =
        Substitute.For<IRepository<Patient,long>>();
    private readonly IRepository<InvoiceItem, long> _invoiceItemRepositoryMock =
        Substitute.For<IRepository<InvoiceItem,long>>();
    private readonly IRepository<PatientCodeMapping,long> _patientCodeMappingRepositoryMock =
        Substitute.For<IRepository<PatientCodeMapping,long>>();
    private readonly IRepository<Wallet,long> _walletRepositoryMock =
        Substitute.For<IRepository<Wallet,long>>();
    private readonly IRepository<WalletHistory,long> _walletHistoryRepositoryMock =
        Substitute.For<IRepository<WalletHistory,long>>();
    private readonly IRepository<PatientAppointment,long> _patientAppointmentRepositoryMock =
        Substitute.For<IRepository<PatientAppointment,long>>();

    [Fact]
    public async Task Handle_With_Valid_Request_Returns_PaymentLandingListResponse()
    {
        // Arrange
        MockDependencies();
        var handler = GetPaymentLandingListQueryHandler();
        // Act
        var result = await handler.Handle(new PaymentLandingListFilterRequest(), 1);
        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(1);
        result.Items[0].FirstName.ShouldBe("First1");
        result.Items[0].LastName.ShouldBe("Last1");
        result.Items[0].PatientCode.ShouldBe("PatientCode1");
        result.Items[0].DateOfBirth.ShouldBe(DateTime.Today.AddDays(-30));
        result.Items[0].Gender.ShouldBe("Male");
        result.Items[0].WalletBalance.ShouldBe(new MoneyDto { Amount = 500.00M, Currency = "NGN" });
        result.Items[0].AmountPaid.ShouldBe(new MoneyDto { Amount = 200.00M, Currency = "NGN" });
        result.Items[0].OutstandingAmount.ShouldBe(new MoneyDto() { Amount = 300.00M, Currency = "NGN" });
        result.Items[0].LastPaymentDate.ShouldBe(DateTime.Today.AddDays(-1));
        result.Items[0].ActualInvoiceAmount.ShouldBe(new MoneyDto() { Amount = 500.00M, Currency = "NGN" });
    }

    [Fact]
    public async Task Handle_With_Filter_PatientCode_Should_Return_PaymentLandingListResponse_With_Given_PatientCode()
    {
        // Arrange
        MockDependencies();
        var handler = GetPaymentLandingListQueryHandler();
        // Act
        var result = await handler.Handle(new PaymentLandingListFilterRequest { Filter = "PatientCode1" }, 1);
        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(1);
        result.Items[0].FirstName.ShouldBe("First1");
        result.Items[0].LastName.ShouldBe("Last1");
        result.Items[0].PatientCode.ShouldBe("PatientCode1");
        result.Items[0].DateOfBirth.ShouldBe(DateTime.Today.AddDays(-30));
        result.Items[0].Gender.ShouldBe("Male");
        result.Items[0].WalletBalance.ShouldBe(new MoneyDto { Amount = 500.00M, Currency = "NGN" });
        result.Items[0].AmountPaid.ShouldBe(new MoneyDto { Amount = 200.00M, Currency = "NGN" });
        result.Items[0].OutstandingAmount.ShouldBe(new MoneyDto() { Amount = 300.00M, Currency = "NGN" });
        result.Items[0].LastPaymentDate.ShouldBe(DateTime.Today.AddDays(-1));
        result.Items[0].ActualInvoiceAmount.ShouldBe(new MoneyDto() { Amount = 500.00M, Currency = "NGN" });

    }

    [Fact]
    public async Task Handle_With_Filter_PatientName_Should_Return_PaymentLandingListResponse_With_Given_PatientName()
    {
        // Arrange
        MockDependencies();
        var handler = GetPaymentLandingListQueryHandler();
        // Act
        var result = await handler.Handle(new PaymentLandingListFilterRequest { Filter = "First1" }, 1);
        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(1);
        result.Items[0].FirstName.ShouldBe("First1");
        result.Items[0].LastName.ShouldBe("Last1");
        result.Items[0].PatientCode.ShouldBe("PatientCode1");
        result.Items[0].DateOfBirth.ShouldBe(DateTime.Today.AddDays(-30));
        result.Items[0].Gender.ShouldBe("Male");
        result.Items[0].WalletBalance.ShouldBe(new MoneyDto { Amount = 500.00M, Currency = "NGN" });
        result.Items[0].AmountPaid.ShouldBe(new MoneyDto { Amount = 200.00M, Currency = "NGN" });
        result.Items[0].OutstandingAmount.ShouldBe(new MoneyDto() { Amount = 300.00M, Currency = "NGN" });
        result.Items[0].LastPaymentDate.ShouldBe(DateTime.Today.AddDays(-1));
        result.Items[0].ActualInvoiceAmount.ShouldBe(new MoneyDto() { Amount = 500.00M, Currency = "NGN" });
    }

    private PaymentLandingListQueryHandler GetPaymentLandingListQueryHandler()
    {
        var handler = new PaymentLandingListQueryHandler(_invoiceRepositoryMock,
            _patientRepositoryMock, _invoiceItemRepositoryMock, _patientCodeMappingRepositoryMock,
            _walletRepositoryMock, _walletHistoryRepositoryMock, _patientAppointmentRepositoryMock);
        return handler;
    }

    private void MockDependencies()
    {
        _invoiceRepositoryMock.GetAll().Returns(GetInvoiceAsQueryable().BuildMock());
        _patientRepositoryMock.GetAll().Returns(GetPatientAsQueryable().BuildMock());
        _invoiceItemRepositoryMock.GetAll().Returns(GetInvoiceItemAsQueryable().BuildMock());
        _patientCodeMappingRepositoryMock.GetAll().Returns(GetPatientCodeMappingAsQueryable().BuildMock());
        _walletRepositoryMock.GetAll().Returns(GetWalletAsQueryable().BuildMock());
        _walletHistoryRepositoryMock.GetAll().Returns(GetWalletHistoryAsQueryable().BuildMock());
        _patientAppointmentRepositoryMock.GetAll().Returns(GetPatientAppointmentAsQueryable().BuildMock());
    }

    private IQueryable<PatientAppointment> GetPatientAppointmentAsQueryable()
    {
        return new List<PatientAppointment>()
        {
            new()
            {
                Id = 1,
                PatientId = 1,
                TenantId = 1,
                Duration = 1,
                Notes = "Notes",
                Title = "WalkIn",
                Type = AppointmentType.Walk_In,
                StartTime = DateTime.Today,
                Status = AppointmentStatusType.Awaiting_Admission,
                
            },
            new()
            {
                Id = 2,
                PatientId = 1,
                TenantId = 1,
                Duration = 1,
                Notes = "Notes",
                Title = "WalkIn",
                Type = AppointmentType.Walk_In,
                StartTime = DateTime.Today.AddDays(-2),
                Status = AppointmentStatusType.Seen_Doctor,
                
                
            },
            new()
            {
                Id = 3,
                PatientId = 1,
                TenantId = 1,
                Duration = 1,
                Notes = "Notes",
                Title = "Consultation",
                Type = AppointmentType.Consultation,
                StartTime = DateTime.Today.AddDays(-30),
                Status = AppointmentStatusType.Seen_Doctor,
                
            }
            
        }.AsQueryable();
    }

    private IQueryable<WalletHistory> GetWalletHistoryAsQueryable()
    {
        return new List<WalletHistory>()
        {
            new()
            {
                Id = 1,
                PatientId = 1,
                TenantId = 1,
                FacilityId = 1,
                Status = TransactionStatus.Approved,
                Amount = new Money(200.00M),
                LastModificationTime = DateTime.Today.AddDays(-1),
                WalletId = 1
                
            },
            new()
            {
                Id = 2,
                PatientId = 1,
                TenantId = 1,
                FacilityId = 1,
                Status = TransactionStatus.Approved,
                Amount = new Money(300.00M),
                LastModificationTime = DateTime.Today.AddDays(-10),
                WalletId = 1
            },
            new()
            {
                Id = 3,
                PatientId = 1,
                TenantId = 1,
                FacilityId = 1,
                Status = TransactionStatus.Pending,
                Amount = new Money(100.00M),
                LastModificationTime = DateTime.Today,
                WalletId = 1,
                
                
            }
        }.AsQueryable();
    }

    private IQueryable<Wallet> GetWalletAsQueryable()
    {
        return new List<Wallet>()
        {
            new()
            {
                Id = 1,
                PatientId = 1,
                Balance = new Money(500.00M),
                TenantId = 1,
                LastModificationTime = DateTime.Today.AddDays(-1),
                
            },
            new()
            {
                Id = 2,
                PatientId = 2,
                Balance = new Money(100.00M),
                TenantId = 1,
            }
            
        }.AsQueryable();
    }

    private IQueryable<PatientCodeMapping> GetPatientCodeMappingAsQueryable()
    {
        return new List<PatientCodeMapping>()
        {
            new ()
            {
                FacilityId = 1,
                PatientId = 1,
                PatientCode = "PatientCode1",
            },
            new ()
            {
                FacilityId = 2,
                PatientId = 1,
                PatientCode = "PatientCode2"
            },
            new ()
            {
                FacilityId = 3,
                PatientId = 1,
                PatientCode = "PatientCode3"
                
            }
        }.AsQueryable();
    }

    private IQueryable<InvoiceItem> GetInvoiceItemAsQueryable()
    {
        return new List<InvoiceItem>()
        {
            new()
            {
                Id = 1,
                InvoiceId = 1,
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(100.00M),
                Status = InvoiceItemStatus.Unpaid,
                FacilityId = 1,
                TenantId = 1,
                Name = "Item1",
                UnitPrice = new Money(100.00M),
                Quantity = 1,
                SubTotal = new Money(100.00M),
                LastModificationTime = DateTime.Today.AddDays(-30),
                LastModifierUserId = 1,
                CreationTime = DateTime.Today.AddDays(-30),
                CreatorUserId = 1
                
                },
            new()
            {
                Id = 2,
                InvoiceId = 1,
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(100.00M),
                Status = InvoiceItemStatus.Unpaid,
                FacilityId = 1,
                TenantId = 1,
                Name = "Item2",
                UnitPrice = new Money(100.00M),
                Quantity = 1,
                SubTotal = new Money(100.00M),
                LastModificationTime = DateTime.Today.AddDays(-30),
                LastModifierUserId = 1,
                CreationTime = DateTime.Today.AddDays(-30),
                CreatorUserId = 1
            },
            new()
            {
                Id = 3,
                InvoiceId = 1,
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(100.00M),
                Status = InvoiceItemStatus.Unpaid,
                FacilityId = 1,
                TenantId = 1,
                Name = "Item3",
                UnitPrice = new Money(100.00M),
                Quantity = 1,
                SubTotal = new Money(100.00M),
                LastModificationTime = DateTime.Today.AddDays(-30),
                LastModifierUserId = 1,
                CreationTime = DateTime.Today.AddDays(-30),
                CreatorUserId = 1
            },
            new()
            {
                Id = 4,
                InvoiceId = 1,
                AmountPaid = new Money(100.00M),
                OutstandingAmount = new Money(0.00M),
                Status = InvoiceItemStatus.Paid,
                FacilityId = 1,
                TenantId = 1,
                Name = "Item4",
                UnitPrice = new Money(100.00M),
                Quantity = 1,
                SubTotal = new Money(100.00M),
                LastModificationTime = DateTime.Today.AddDays(-5),
                LastModifierUserId = 1,
                CreationTime = DateTime.Today.AddDays(-30),
                CreatorUserId = 1
            },
            new()
            {
                Id = 5,
                InvoiceId = 1,
                AmountPaid = new Money(100.00M),
                OutstandingAmount = new Money(0.00M),
                Status = InvoiceItemStatus.Paid,
                FacilityId = 1,
                TenantId = 1,
                Name = "Item5",
                UnitPrice = new Money(100.00M),
                Quantity = 1,
                SubTotal = new Money(100.00M),
                LastModificationTime = DateTime.Today.AddDays(-1),
                LastModifierUserId = 1,
                CreationTime = DateTime.Today.AddDays(-30),
                CreatorUserId = 1
            }
            
        }.AsQueryable();
    }

    private IQueryable<Patient> GetPatientAsQueryable()
    {
        return new List<Patient>()
        {
            new()
            {
                Id = 1,
                FirstName = "First1",
                DateOfBirth = DateTime.Today.AddDays(-30),
                GenderType = GenderType.Male,
                EmailAddress = "email@example",
                PhoneNumber = "00000000001",
                LastName = "Last1",
                MiddleName = "Middle1",

            },
            new()
            {
                Id = 2,
                FirstName = "First2",
                DateOfBirth = DateTime.Today.AddDays(-30),
                GenderType = GenderType.Male,
                EmailAddress = "email@example",
                PhoneNumber = "00000000002",
                LastName = "Last2",
                MiddleName = "Middle2",

            },
            new()
            {
                Id = 3,
                FirstName = "First3",
                DateOfBirth = DateTime.Today.AddDays(-30),
                GenderType = GenderType.Male,
                EmailAddress = "email@example",
                PhoneNumber = "00000000003",
                LastName = "Last3",
                MiddleName = "Middle3",
            }
        }.AsQueryable();
    }

    private IQueryable<Invoice> GetInvoiceAsQueryable()
    {
        return new List<Invoice>()
        {
            new ()
            {
                Id = 1,
                PatientId = 1,
                InvoiceId = "0000000001",
                InvoiceType = InvoiceType.InvoiceCreation,
                InvoiceSource = InvoiceSource.OutPatient,
                OutstandingAmount = new Money(300.00M),
                TotalAmount = new Money(500.00M),
                AmountPaid = new Money(200.00M),
                PaymentStatus = PaymentStatus.PartiallyPaid,
                PaymentType = PaymentTypes.Wallet,
                TimeOfInvoicePaid = DateTime.Today.AddDays(-1),
                TenantId = 1,
                FacilityId = 1
            },
            new ()
            {
                Id = 2,
                PatientId = 1,
                InvoiceId = "0000000002",
                InvoiceType = InvoiceType.InvoiceCreation,
                InvoiceSource = InvoiceSource.OutPatient,
                OutstandingAmount = new Money(100.00M),
                TotalAmount = new Money(100.00M),
                AmountPaid = new Money(0.00M),
                PaymentStatus = PaymentStatus.Unpaid,
                PaymentType = PaymentTypes.Wallet,
                TimeOfInvoicePaid = DateTime.Today,
                TenantId = 1,
                FacilityId = 1
            },
            new ()
            {
                Id = 3,
                PatientId = 2,
                InvoiceId = "0000000003",
                InvoiceType = InvoiceType.InvoiceCreation,
                InvoiceSource = InvoiceSource.OutPatient,
                OutstandingAmount = new Money(100.00M),
                TotalAmount = new Money(100.00M),
                AmountPaid = new Money(0.00M),
                PaymentStatus = PaymentStatus.Unpaid,
                PaymentType = PaymentTypes.Wallet,
                TimeOfInvoicePaid = DateTime.Today,
                TenantId = 1,
                FacilityId = 1
            },
            new ()
            {
                Id = 4,
                PatientId = 3,
                InvoiceId = "0000000004",
                InvoiceType = InvoiceType.InvoiceCreation,
                InvoiceSource = InvoiceSource.InPatient,
                OutstandingAmount = new Money(100.00M),
                TotalAmount = new Money(100.00M),
                AmountPaid = new Money(0.00M),
                PaymentStatus = PaymentStatus.Unpaid,
                PaymentType = PaymentTypes.Wallet,
                TimeOfInvoicePaid = DateTime.Today,
                TenantId = 1,
                FacilityId = 1
                
            }
            
        }.AsQueryable();
    }
}