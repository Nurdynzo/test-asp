using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;
using Plateaumed.EHR.PatientWallet.Query;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;

[Trait("Category", "Unit")]
public class GetInvoicesForRefundQueryHandlerTest
{
    private readonly IRepository<Invoice,long> _invoiceRepositoryMock = Substitute.For<IRepository<Invoice,long>>();
    
    [Fact]
    public async Task Handle_When_Filter_Today_Should_Return_Invoice_For_Today()
    {
        //arrange 
        _invoiceRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceForRefundAsQueryable().BuildMock());
        var handler = new GetInvoicesForRefundQueryHandler(_invoiceRepositoryMock);
        //act 
        var result = await handler.Handle(new GetInvoicesForRefundQueryRequest
            { Filter = WalletRefundFilter.Today, PatientId = 1 });
        // assert
        result.Count.ShouldBe(3);
        
    }
    [Fact]
    public async Task Handle_When_Filter_Yesterday_Should_Return_Invoice_For_Yesterday()
    {
        //arrange 
        _invoiceRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceForRefundAsQueryable().BuildMock());
        var handler = new GetInvoicesForRefundQueryHandler(_invoiceRepositoryMock);
        //act 
        var result = await handler.Handle(new GetInvoicesForRefundQueryRequest 
            { Filter = WalletRefundFilter.Yesterday,PatientId = 1});
        // assert
        result.Count.ShouldBe(4);
        
    }

    [Fact]
    public async Task Handle_When_Filter_ThisWeek_Should_Return_Invoice_For_ThisWeek()
    {
        //arrange 
        _invoiceRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceForRefundAsQueryable().BuildMock());
        var handler = new GetInvoicesForRefundQueryHandler(_invoiceRepositoryMock);
        //act 
        var result = await handler.Handle(new GetInvoicesForRefundQueryRequest 
            { Filter = WalletRefundFilter.ThisWeek,PatientId = 1 });
        // assert
        result.Count.ShouldBe(5);
    }
    [Fact]
    public async Task Handle_When_Filter_ThisMonth_Should_Return_Invoice_For_ThisMonth()
    {
        //arrange 
        _invoiceRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceForRefundAsQueryable().BuildMock());
        var handler = new GetInvoicesForRefundQueryHandler(_invoiceRepositoryMock);
        //act 
        var result = await handler.Handle(new GetInvoicesForRefundQueryRequest 
            { Filter = WalletRefundFilter.ThisMonth,PatientId = 1 });
        // assert
        result.Count.ShouldBe(7);
    }

    [Fact]
    public async Task Handle_When_Filter_ThisYear_Should_Return_Invoice_For_ThisYear()
    {
        //arrange 
        _invoiceRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceForRefundAsQueryable().BuildMock());
        var handler = new GetInvoicesForRefundQueryHandler(_invoiceRepositoryMock);
        //act 
        var result = await handler.Handle(new GetInvoicesForRefundQueryRequest 
            { Filter = WalletRefundFilter.ThisYear,PatientId = 1 });
        // assert
        result.Count.ShouldBe(8);
    }


   

}