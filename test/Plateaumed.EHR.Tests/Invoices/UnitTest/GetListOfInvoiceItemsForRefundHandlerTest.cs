using System.Linq;
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
public class GetListOfInvoiceItemsForRefundHandlerTest
{
    private readonly IRepository<Invoice,long> _invoiceRepositoryMock = Substitute.For<IRepository<Invoice,long>>();
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepositoryMock = Substitute.For<IRepository<InvoiceItem,long>>();
    const long FacilityId = 1;
    
    [Fact]
    public async Task Handle_When_Filter_Today_Should_Return_Invoice_For_Today()
    {
        //arrange 
        MockDependency();
        var handler = new GetListOfInvoiceItemsForRefundHandler(_invoiceRepositoryMock, _invoiceItemRepositoryMock);
        //act 
        var result = await handler.Handle(new RefundInvoiceQueryRequest
            { DateFilter = WalletRefundFilter.Today, PatientId = 1 , InvoiceIds = new long[]{2,3,4} },FacilityId);
        // assert
        result.Count.ShouldBe(2);
        result.FirstOrDefault(x=>x.Id == 2)?.InvoiceItems.Length.ShouldBe(2);
        
    }

   

    [Fact]
    public async Task Handle_When_Filter_Non_Matching_Invoice_Should_Return_Empty_List()
    {
        //arrange 
       MockDependency();
        var handler = new GetListOfInvoiceItemsForRefundHandler(_invoiceRepositoryMock, _invoiceItemRepositoryMock);
        //act 
        var result = await handler.Handle(new RefundInvoiceQueryRequest
            { DateFilter = WalletRefundFilter.Yesterday, PatientId = 1, InvoiceIds = new long[]{1,5,6} },FacilityId);
        // assert
        result.Count.ShouldBe(0);
    }
    
    private void MockDependency()
    {
        _invoiceRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceForRefundAsQueryable().BuildMock());
        _invoiceItemRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceItemAsQueryable().BuildMock());
    }

}