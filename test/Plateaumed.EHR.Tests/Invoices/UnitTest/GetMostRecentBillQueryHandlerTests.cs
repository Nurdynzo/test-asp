using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Tests.Invoices.Util;
using Plateaumed.EHR.ValueObjects;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;

[Trait("Category", "Unit")]
public class GetMostRecentBillQueryHandlerTests
{
    private readonly IRepository<Invoice, long> _invoiceRepositoryMock =  Substitute.For<IRepository<Invoice,long>>();
    private readonly IRepository<User,long> _userRepositoryMock = Substitute.For<IRepository<User,long>>();
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepositoryMock = Substitute.For<IRepository<InvoiceItem,long>>();

    [Fact]
    public async Task Handle_Should_Return_MostRecent_Invoice()
    {
        //arrange
        _invoiceRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceAsQueryable().BuildMock());
        _userRepositoryMock.GetAll().Returns(CommonUtil.GetUserAsQueryable().BuildMock());
        _invoiceItemRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceItemAsQueryable().BuildMock());
        var handler = new GetMostRecentBillQueryHandler(_userRepositoryMock,_invoiceItemRepositoryMock, _invoiceRepositoryMock) ;
        
        //act
        var result = await handler.Handle(1);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(2);
        result.TotalAmount.Amount.ShouldBe(CommonUtil.GetMoneyDto().Amount);
        result.TotalAmount.Currency.ShouldBe(CommonUtil.GetMoneyDto().Currency);
        result.IssuedBy.ShouldBe("John Nash");
        result.PaymentStatus.ShouldBe(PaymentStatus.Paid.ToString());
        result.Items.Count.ShouldBe(2);
    }
}