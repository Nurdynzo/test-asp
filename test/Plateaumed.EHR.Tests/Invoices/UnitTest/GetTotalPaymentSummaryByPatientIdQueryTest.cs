using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;

[Trait("Category", "Unit")]
public class GetTotalPaymentSummaryByPatientIdQueryTest
{
    private readonly IRepository<Invoice,long> _invoiceRepositoryMock 
        = Substitute.For<IRepository<Invoice,long>>();
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepositoryMock 
        = Substitute.For<IRepository<InvoiceItem,long>>();
    private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepositoryMock 
        = Substitute.For<IRepository<PaymentActivityLog,long>>();
    

    [Fact]
    public async Task Handle_Should_Return_Total_Payment_Summary_For_A_Given_PatientId()
    {
        //Arrange
        var handler = GetTotalPaymentSummaryByPatientIdQueryInstance();
        //Act
        var result = await handler.Handle(1,1);
        //Assert
        result.ShouldNotBeNull();
        result.TotalAmount.ShouldBe(new MoneyDto(){ Amount = 600.00M, Currency = "NGN" });
        result.ToTalPaid.ShouldBe(new MoneyDto(){ Amount = 300.00M, Currency = "NGN" });
        result.TotalOutstanding.ShouldBe(new MoneyDto(){ Amount = 300.00M, Currency = "NGN"});
        result.TotalTopUp.ShouldBe(new MoneyDto(){ Amount = 400.00M, Currency = "NGN"});
        result.ItemsCounts.ShouldBe(1);

    }
    
    [Fact]
    public async Task Handle_Should_Throw_If_Money_Has_Different_Currency()
    {
        //Arrange
        var handler = GetTotalPaymentSummaryByPatientIdQueryInstance(true);
       
        //Act && Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.Handle(1,1));
        exception.Message.ShouldBe("Cannot add two different currencies");
    }

    private IGetTotalPaymentSummaryByPatientIdQuery GetTotalPaymentSummaryByPatientIdQueryInstance(bool flagDifferentCurrency=false)
    {
        _invoiceRepositoryMock.GetAll().Returns(CommonUtil.GetInvoicesForPaymentSummaryAsQueryable().BuildMock());
        _invoiceItemRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceItemsForPaymentSummaryAsQueryable(flagDifferentCurrency).BuildMock());
        _paymentActivityLogRepositoryMock.GetAll().Returns(CommonUtil.GetPaymentActivityLogAsQueryable().BuildMock());
      
        return new GetTotalPaymentSummaryByPatientIdQuery(_invoiceRepositoryMock,_invoiceItemRepositoryMock,_paymentActivityLogRepositoryMock);
    }
   

   
}

