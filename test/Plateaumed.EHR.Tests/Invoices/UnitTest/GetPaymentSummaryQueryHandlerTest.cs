using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;

[Trait("Category", "Unit")]
public class GetPaymentSummaryQueryHandlerTest
{
    private readonly IRepository<Invoice, long> _invoiceRepository = Substitute.For<IRepository<Invoice, long>>();
    private readonly IRepository<InvoiceItem, long> _invoiceItemRepository = Substitute.For<IRepository<InvoiceItem, long>>();
    private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository = Substitute.For<IRepository<PatientAppointment, long>>();
    private readonly IRepository<PaymentActivityLog, long> _paymentActivityLogMock =
        Substitute.For<IRepository<PaymentActivityLog, long>>();

    [Fact]
    public async Task Handle_Should_Return_PaymentSummary_For_The_Given_Patient()
    {
        // arrange 
        var request = new GetPaymentSummaryQueryRequest { PatientId = 1 };
        _invoiceRepository.GetAll().Returns(CommonUtil.GetInvoiceAsQueryable().BuildMock());
        _invoiceItemRepository.GetAll().Returns(CommonUtil.GetInvoiceItemAsQueryable().BuildMock());
        _patientAppointmentRepository.GetAll().Returns(CommonUtil.GetPatientAppointmentAsQueryable().BuildMock());
        _paymentActivityLogMock.GetAll().Returns(CommonUtil.GetPaymentActivityLogAsQueryable().BuildMock());
        var handler = new GetPaymentSummaryQueryHandler(_invoiceRepository, _invoiceItemRepository, _patientAppointmentRepository,_paymentActivityLogMock);
        // act
        PagedResultDto<GetPaymentSummaryQueryResponse> result = await handler.Handle(request);
        
        // assert
        result.Items.ShouldNotBeNull();
        result.Items.Count.ShouldBe(5);
        
    }
}