using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PatientWallet.Query;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;

[Trait("Category", "Unit")]
public class GetListOfRequestedRefundInvoiceItemForApprovalQueryHandlerTest
{
    private readonly IRepository<InvoiceRefundRequest,long> _invoiceRefundRequestRepositoryMock 
        = Substitute.For<IRepository<InvoiceRefundRequest,long>>();
    private readonly IRepository<Invoice,long> _invoiceRepository 
        = Substitute.For<IRepository<Invoice,long>>();
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepository 
        = Substitute.For<IRepository<InvoiceItem,long>>();

    [Fact]
    public async Task Handle_WhenCalled_Should_ReturnListOfRequestedRefundInvoiceItemForApprovalQueryResponse()
    {
        // Arrange
        MockDependency();
        var handler = new GetListOfRequestedRefundInvoiceItemForApprovalQueryHandler(
            _invoiceRefundRequestRepositoryMock, _invoiceRepository, _invoiceItemRepository);
        var patientId = 1;
        var facilityId = 1;
        // Act
        var result =
            await handler.Handle(patientId,facilityId);
        // Assert
        result.ShouldNotBeEmpty();
        result.Count.ShouldBe(1);
        result[0].InvoiceItems.Length.ShouldBe(2);
        
    }

    private void MockDependency()
    {
        _invoiceRefundRequestRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceRefundRequestList().BuildMock());
        _invoiceRepository.GetAll().Returns(CommonUtil.GetInvoiceForRefundAsQueryable().BuildMock());
        _invoiceItemRepository.GetAll().Returns(CommonUtil.GetInvoiceItemAsQueryable().BuildMock());
    }
}