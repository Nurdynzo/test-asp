using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;

[Trait("Category", "Unit")]
public class GetInvoicePaymentBillListQueryHandlerTest
{
    private readonly IRepository<Invoice, long> _invoiceRepositoryMock =  Substitute.For<IRepository<Invoice,long>>();
    private readonly IRepository<User,long> _userRepositoryMock = Substitute.For<IRepository<User,long>>();
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepositoryMock = Substitute.For<IRepository<InvoiceItem,long>>();
    private readonly IRepository<PatientAppointment, long> _patientAppointmentRepositoryMock = Substitute.For<IRepository<PatientAppointment, long>>();

    [Fact]
    public async Task Handle_Should_Return_Payment_Bills_For_A_Given_Patient()
    {
        //arrange
        _invoiceRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceAsQueryable().BuildMock());
        _userRepositoryMock.GetAll().Returns(CommonUtil.GetUserAsQueryable().BuildMock());
        _invoiceItemRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceItemAsQueryable().BuildMock());
        _patientAppointmentRepositoryMock.GetAll().Returns(CommonUtil.GetPatientAppointmentAsQueryable().BuildMock());
        
        var handler = new GetInvoicePaymentBillListQueryHandler( _invoiceRepositoryMock,_invoiceItemRepositoryMock,_userRepositoryMock,_patientAppointmentRepositoryMock) ;
        var request = new GetInvoicePaymentBillListQueryRequest { PatientId = 1 };
        //act
        var result = await handler.Handle(request);
        
        //assert
        result.ShouldNotBeNull();
        result.InvoiceItems.Count.ShouldBe(2);
        result.ReceiptItems.Count.ShouldBe(3);
        result.ReceiptItems.ShouldContain(x=>x.InvoiceNo == "0000000003");
        result.InvoiceItems.ShouldContain(x=>x.InvoiceNo == "0000000003");
        
    }
}