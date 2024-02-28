using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
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
public class GetPaymentExpandableQueryHandlerTest
{
    private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepositoryMock
        = Substitute.For<IRepository<PaymentActivityLog,long>>();
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepositoryMock 
        = Substitute.For<IRepository<InvoiceItem,long>>();
    private readonly IRepository<Invoice,long> _invoiceRepositoryMock 
        = Substitute.For<IRepository<Invoice,long>>();
    private readonly IRepository<WalletHistory,long> _walletHistoryRepositoryMock
        = Substitute.For<IRepository<WalletHistory,long>>();
    private readonly IRepository<PatientAppointment,long> _patientAppointmentRepositoryMock
        = Substitute.For<IRepository<PatientAppointment,long>>();
    private readonly IRepository<User,long> _userRepositoryMock
        = Substitute.For<IRepository<User,long>>();
    
    [Fact]
    public async Task Handle_With_Valid_Patient_Id_And_Facility_Id_Should_Return_Query_For_Payment_Expandable()
    {
        // Arrange
        var patientId = 1L;
        var facilityId = 1L;
        var request = new GetPaymentExpandableQueryRequest
        {
            PatientId = patientId
        };
        var sut = GetSut();
        // Act
        var result = await sut.Handle(request,facilityId);
        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<PagedResultDto<GetPaymentExpandableQueryResponse>>();
        result.TotalCount.ShouldBe(6);
        result.Items.Count.ShouldBe(6);
        result.Items[0].AmountPaid.ShouldBe(new MoneyDto(){Amount = 100, Currency = "NGN"});
        result.Items[0].InvoiceNo.ShouldBe("INV-0001");
        result.Items[0].InvoiceItemName.ShouldBe("Test1");
        result.Items[0].AppointmentStatus.ShouldBe(AppointmentStatusType.Seen_Doctor.ToString());
        result.Items[0].EditedAmount.ShouldBeNull();
        result.Items[0].LastPaidDateTime.ShouldBe(DateTime.Today);
        result.Items[0].OutstandingAmount.ShouldBe(new MoneyDto(){Amount = 0, Currency = "NGN"});
        
        
    }

    private GetPaymentExpandableQueryHandler GetSut()
    {
        _paymentActivityLogRepositoryMock.GetAll().Returns(GetPaymentActivityLogAsQueryable().BuildMock());
        _invoiceItemRepositoryMock.GetAll().Returns(GetInvoiceItemAsQueryable().BuildMock());
        _invoiceRepositoryMock.GetAll().Returns(GetInvoiceAsQueryable().BuildMock());
        _walletHistoryRepositoryMock.GetAll().Returns(GetWalletHistoryAsQueryable().BuildMock());
        _patientAppointmentRepositoryMock.GetAll().Returns(GetAppointmentAsQueryable().BuildMock());
        _userRepositoryMock.GetAll().Returns(GetUserAsQueryable().BuildMock());
        return new GetPaymentExpandableQueryHandler(
            _paymentActivityLogRepositoryMock,
            _invoiceItemRepositoryMock,
            _invoiceRepositoryMock,
            _walletHistoryRepositoryMock,
            _patientAppointmentRepositoryMock,
            _userRepositoryMock);
    }

    private IQueryable<User> GetUserAsQueryable()
    {
        return new List<User>()
        { new ()
            {
                Id = 1,
                Title = TitleType.Dr,
                Surname = "Ajayi",
                Name = "Adebayo",
                
            }
        }.AsQueryable();
    }

    private IQueryable<PatientAppointment> GetAppointmentAsQueryable()
    {
        return new List<PatientAppointment>()
        {
             new ()
             {
                 Id = 1,
                 PatientId = 1,
                 Status = AppointmentStatusType.Seen_Doctor,
                 CreationTime = DateTime.Today,
                 TenantId = 1,
                 Title = "walk in",
                 Notes = "walk in",
                 CreatorUserId = 1,
                 Type = AppointmentType.Walk_In
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
                WalletId = 1,
                TenantId = 1,
                Status = TransactionStatus.Approved,
                Amount = new Money(100),
                Source = TransactionSource.Indirect,
                TransactionType = TransactionType.Credit,
                FacilityId = 1,
                Narration = "top up",
                CreationTime = DateTime.Today.AddDays(-2),
                LastModificationTime = DateTime.Today.AddDays(-2),
                LastModifierUserId = 1
                
            }
        }.AsQueryable();
    }

    private IQueryable<Invoice> GetInvoiceAsQueryable()
    {
        return new List<Invoice>()
        {
            new()
            {
                Id = 1,
                FacilityId = 1,
                InvoiceId = "INV-0001",
                PatientId = 1,
                InvoiceSource = InvoiceSource.OutPatient,
                InvoiceType = InvoiceType.InvoiceCreation,
                TenantId = 1,
                AmountPaid = new Money(100),
                CreationTime = DateTime.Today.AddDays(-2),
                PatientAppointmentId = 1,
                LastModificationTime = DateTime.Today,
                LastModifierUserId = 1,
                OutstandingAmount = new Money(50),
                TotalAmount = new Money(150),
                PaymentType =  PaymentTypes.Wallet,
                PaymentStatus = PaymentStatus.PartiallyPaid,
                
            },
            new()
            {
                Id = 2,
                FacilityId = 1,
                PatientId = 1,
                InvoiceSource = InvoiceSource.Pharmacy,
                InvoiceType = InvoiceType.Proforma,
                TenantId = 1,
                AmountPaid = new Money(100),
                CreationTime = DateTime.Today.AddDays(-2),
                OutstandingAmount = new Money(100),
                TotalAmount = new Money(100),
                PatientAppointmentId = 1
            }
            
        }.AsQueryable();
    }

    private IQueryable<InvoiceItem> GetInvoiceItemAsQueryable()
    {
        return new List<InvoiceItem>()
        {
            new ()
            {
                Id = 1,
                InvoiceId = 1,
                Name = "Test1",
                SubTotal = new Money(100),
                OutstandingAmount = new Money(0),
                AmountPaid = new Money(100),
                CreationTime = DateTime.Today.AddDays(-2),
                CreatorUserId = 1,
                TenantId = 1,
                LastModificationTime = DateTime.Today,
                LastModifierUserId = 1,
                Status = InvoiceItemStatus.Paid,
                FacilityId = 1,
            },
            new ()
            {
                Id = 2,
                InvoiceId = 1,
                Name = "Test2",
                SubTotal = new Money(50),
                OutstandingAmount = new Money(0),
                AmountPaid = new Money(0),
                CreationTime = DateTime.Today.AddDays(-2),
                CreatorUserId = 1,
                TenantId = 1,
                LastModificationTime = DateTime.Today,
                LastModifierUserId = 1,
                Status = InvoiceItemStatus.ReliefApplied,
                FacilityId = 1,
            },
            new()
            {
                Id = 3,
                InvoiceId = 2,
                Name = "test proforma",
                SubTotal = new Money(200),
                OutstandingAmount = new Money(200),
                AmountPaid = new Money(0),
                CreationTime = DateTime.Today.AddDays(-2),
                CreatorUserId = 1,
                TenantId = 1,
                LastModificationTime = DateTime.Today,
                LastModifierUserId = 1,
                Status = InvoiceItemStatus.Unpaid,
                FacilityId = 1
                
            }
        }.AsQueryable();
    }

    private IQueryable<PaymentActivityLog> GetPaymentActivityLogAsQueryable()
    {
        return new List<PaymentActivityLog>
        {
            new()
            {
                Id = 1,
                PatientId = 1,
                InvoiceId = 1,
                InvoiceItemId = 1,
                TransactionAction = TransactionAction.CreateInvoice, 
                InvoiceNo = "INV-0001",
                ActualAmount = new Money(100),
                FacilityId = 1,
                TenantId = 1,
                OutStandingAmount = new Money(100),
                CreatorUserId = 1,
                CreationTime = DateTime.Today.AddDays(-2)
                
            },
            new()
            {
                Id = 2,
                PatientId = 1,
                InvoiceId = 1,
                InvoiceItemId = 2,
                TransactionAction = TransactionAction.CreateInvoice, 
                InvoiceNo = "INV-0001",
                ActualAmount = new Money(100),
                FacilityId = 1,
                TenantId = 1,
                OutStandingAmount = new Money(100),
                CreatorUserId = 1,
                CreationTime = DateTime.Today.AddDays(-2)
            },
            new()
            {
                Id = 3,
                PatientId = 1,
                InvoiceId = 1,
                InvoiceItemId = 1,
                TransactionAction = TransactionAction.PaidInvoice, 
                InvoiceNo = "INV-0001",
                ActualAmount = new Money(100),
                AmountPaid = new Money(100),
                FacilityId = 1,
                TenantId = 1,
                OutStandingAmount = new Money(0),
                CreatorUserId = 1,
                CreationTime = DateTime.Today
            },
            new()
            {
                Id=4,
                PatientId = 1,
                TransactionAction = TransactionAction.FundWallet,
                ToUpAmount = new Money(100),
                FacilityId = 1,
                TenantId = 1,
                Narration = "Fund Wallet",
                CreatorUserId = 1,
                CreationTime = DateTime.Today.AddDays(-2)
            },
            new()
            {
                Id = 5,
                PatientId = 1,
                InvoiceId = 1,
                InvoiceItemId = 2,
                TransactionAction = TransactionAction.EditInvoice,
                InvoiceNo = "INV-0001",
                ActualAmount = new Money(50),
                EditAmount = new Money(100),
                FacilityId = 1,
                TenantId = 1,
                OutStandingAmount = new Money(50),
                CreatorUserId = 1,
                CreationTime = DateTime.Today.AddDays(-1)
                
            },
            new ()
            {
                Id = 6,
                PatientId = 1,
                TransactionAction = TransactionAction.CreateProforma,
                InvoiceId = 2,
                InvoiceItemId = 3,
                ActualAmount = new Money(100),
                FacilityId = 1,
                TenantId = 1,
                OutStandingAmount = new Money(100),
                CreatorUserId = 1,
                CreationTime = DateTime.Today.AddDays(-1)
            }
        }.AsQueryable();
    }
}