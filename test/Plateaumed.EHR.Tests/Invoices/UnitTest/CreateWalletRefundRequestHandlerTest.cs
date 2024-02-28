using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PatientWallet.Command;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;

[Trait("Category", "Unit")]
public class CreateWalletRefundRequestHandlerTest
{
    private readonly IRepository<InvoiceRefundRequest, long> _invoiceRequestRepository
        = Substitute.For<IRepository<InvoiceRefundRequest, long>>();

    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();

    private readonly IRepository<InvoiceItem, long> _invoiceItemRepositoryMock
        = Substitute.For<IRepository<InvoiceItem, long>>();

    private readonly IUnitOfWorkManager _unitOfWorkManagerMock = Substitute.For<IUnitOfWorkManager>();

    private readonly IRepository<PaymentActivityLog, long> _paymentActivityLogRepositoryMock
        = Substitute.For<IRepository<PaymentActivityLog, long>>();

    [Fact]
    public async Task Handle_With_ValidRequest_Should_Create_WalletRefund()
    {
        //Arrange
        var request = GetCreateWalletRefundRequest(1,2,3);
        var handler = GetCreateWalletRefundRequestHandlerInstance();
        //Act
        await handler.Handle(request, 1);
        //Assert
        _unitOfWorkManagerMock.Received(1).Begin();
        await _invoiceRequestRepository.Received(3).InsertAsync(Arg.Any<InvoiceRefundRequest>());
        await _paymentActivityLogRepositoryMock.Received(3).InsertAsync(Arg.Any<PaymentActivityLog>());
        await _unitOfWorkManagerMock.Received(1).Current.SaveChangesAsync();
    }

    [Fact]
    public async Task Handle_With_No_MatchingItemsRequest_Should_Throw_UserFriendlyException()
    {
        //Arrange
        var request = GetCreateWalletRefundRequest(5,6,7);
        var handler = GetCreateWalletRefundRequestHandlerInstance();
        //Act && Assert
        var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request, 1));
        message.Message.ShouldBe("No matching items to refund");
    }

    [Fact]
    public async Task Handle_With_InvalidRequest_Should_Throw_UserFriendlyException()
    {
        //Arrange
        var request = GetCreateWalletRefundRequest();
        var handler = GetCreateWalletRefundRequestHandlerInstance();
        //Act && Assert
        var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request, 2));
        message.Message.ShouldBe("invoice items cannot be empty");
    }



    private CreateWalletRefundCommandHandler GetCreateWalletRefundRequestHandlerInstance()
    {
        _invoiceItemRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceItemAsQueryable().BuildMock());
        _abpSessionMock.TenantId.Returns(1);
        return new CreateWalletRefundCommandHandler(
            _invoiceRequestRepository,
            _abpSessionMock,
            _invoiceItemRepositoryMock,
            _unitOfWorkManagerMock,
            _paymentActivityLogRepositoryMock);
    }

    private InvoiceWalletRefundRequest GetCreateWalletRefundRequest(params long[] invoiceItemIds)
    {
        return new InvoiceWalletRefundRequest
        {
            InvoiceItemsIds = invoiceItemIds,
            PatientId = 1
        };
    }
}