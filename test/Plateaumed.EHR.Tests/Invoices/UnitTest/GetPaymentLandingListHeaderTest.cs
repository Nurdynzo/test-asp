using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Tests.Invoices.Util;
using Plateaumed.EHR.ValueObjects;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;

[Trait("Category", "Unit")]
public class GetPaymentLandingListHeaderTest
{
    private readonly IRepository<Invoice,long> _invoiceRepositoryMock 
        = Substitute.For<IRepository<Invoice,long>>();
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepositoryMock
        = Substitute.For<IRepository<InvoiceItem,long>>();
    private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepositoryMock
        = Substitute.For<IRepository<PaymentActivityLog,long>>();

    [Fact]
    public async Task Handle_Should_Return_Total_Payment_Summary()
    {
        // Arrange
        _invoiceRepositoryMock.GetAll().Returns(GetInvoiceAsQueryable().BuildMock());
        _invoiceItemRepositoryMock.GetAll().Returns(GetInvoiceItemsQueryable().BuildMock());
        _paymentActivityLogRepositoryMock.GetAll().Returns(GetPaymentActivityLogQueryable().BuildMock());
        
        var handler = new GetPaymentLandingListHeader(
            _invoiceRepositoryMock, 
            _invoiceItemRepositoryMock, 
            _paymentActivityLogRepositoryMock);
        //act
        var result = await handler.Handle(1);
        //assert
        result.ShouldNotBeNull();
        result.ToTalPaid.ShouldBe(100.00M.ToMoneyDto());
        result.TotalOutstanding.ShouldBe(300.00M.ToMoneyDto());
        result.TotalAmount.ShouldBe(400.00M.ToMoneyDto());
        result.ItemsCounts.ShouldBe(1);
        result.TotalTopUp.ShouldBe(300.00M.ToMoneyDto());
        
    }

    private IQueryable<PaymentActivityLog> GetPaymentActivityLogQueryable()
    {
        return new List<PaymentActivityLog>()
        {
            new()
            {
                Id = 1,
                InvoiceId = 1,
                PatientId = 1,
                FacilityId = 1,
                TransactionAction = TransactionAction.FundWallet,
                ToUpAmount = new Money(100),
            },
            new()
            {
                Id = 2,
                InvoiceId = 1,
                PatientId = 1,
                FacilityId = 1,
                TransactionAction = TransactionAction.FundWallet,
                ToUpAmount = new Money(100),
            },
            new()
            {
                Id = 3,
                InvoiceId = 2,
                PatientId = 1,
                FacilityId = 1,
                TransactionAction = TransactionAction.FundWallet,
                ToUpAmount = new Money(100),
            },
            
        }.AsQueryable();
    }

    private IQueryable<InvoiceItem> GetInvoiceItemsQueryable()
    {
        return new List<InvoiceItem>()
        {
            new ()
            {
                Id = 1,
                InvoiceId = 1,
                AmountPaid = new Money(0),
                OutstandingAmount = new Money(100),
                SubTotal = new Money(100),
                DiscountPercentage = 0,
                FacilityId = 1,
                
            },
            new ()
            {
                Id = 2,
                InvoiceId = 1,
                AmountPaid = new Money(0),
                OutstandingAmount = new Money(100),
                SubTotal = new Money(100),
                DiscountPercentage = 0,
                FacilityId = 1,
            },
            new ()
            {
                Id = 3,
                InvoiceId = 2,
                AmountPaid = new Money(0),
                OutstandingAmount = new Money(100),
                SubTotal = new Money(100),
                DiscountPercentage = 0,
                FacilityId = 1,
            },
            new ()
            {
                Id = 4,
                InvoiceId = 3,
                AmountPaid = new Money(100),
                OutstandingAmount = new Money(0),
                SubTotal = new Money(100),
                DiscountPercentage = 0,
                FacilityId = 1,
            },
            
        }.AsQueryable();
    }

    private IQueryable<Invoice> GetInvoiceAsQueryable()
    {
        return new List<Invoice>
        {
            new ()
            {
                Id = 1,
                PatientId = 1,
                FacilityId = 1,
                InvoiceId = "InvoiceId",
                AmountPaid = new Money(0),
                OutstandingAmount = new Money(200),
                
            },
            new ()
            {
                Id = 2,
                PatientId = 1,
                FacilityId = 1,
                InvoiceId = "InvoiceId",
                AmountPaid = new Money(0),
                OutstandingAmount = new Money(100),
                
            },
            new ()
            {
                Id = 3,
                PatientId = 1,
                FacilityId = 1,
                InvoiceId = "InvoiceId",
                AmountPaid = new Money(0),
                OutstandingAmount = new Money(100),
                
            },
            
                
            
        }.AsQueryable();
    }
}