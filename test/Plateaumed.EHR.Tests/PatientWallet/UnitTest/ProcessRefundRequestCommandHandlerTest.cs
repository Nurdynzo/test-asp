using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.PatientWallet.Command;
using Plateaumed.EHR.Tests.PatientWallet.Utils;
using Plateaumed.EHR.ValueObjects;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientWallet.UnitTest;

[Trait("Category", "Unit")]
public class ProcessRefundRequestCommandHandlerTest
{
    private readonly IRepository<InvoiceRefundRequest,long> _invoiceRefundRequestRepositoryMock 
        = Substitute.For<IRepository<InvoiceRefundRequest,long>>();
    private readonly IRepository<WalletHistory,long> _walletHistoryRepositoryMock
        = Substitute.For<IRepository<WalletHistory,long>>();
    private readonly IRepository<Wallet,long> _walletRepositoryMock
        = Substitute.For<IRepository<Wallet,long>>();
    private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepositoryMock
        = Substitute.For<IRepository<PaymentActivityLog,long>>();
    private readonly IAbpSession _abpSession = Substitute.For<IAbpSession>();
    private readonly IUnitOfWorkManager _unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();

    [Fact]
    public async Task Handle_With_ValidValidApprovedRequest_Should__Approved_Refund_Request()
    {
        //arrange
        var request = CommonUtils.GetProcessRefundRequestCommand();
        var handler = GetProcessRefundRequestCommandHandlerInstance();
        //act
        await handler.Handle(request, 1);
        //assert
        _unitOfWorkManager.Received(1).Begin();
        await _invoiceRefundRequestRepositoryMock.Received(3).UpdateAsync(Arg.Any<InvoiceRefundRequest>());
        await _paymentActivityLogRepositoryMock.Received(3).InsertAsync(Arg.Any<PaymentActivityLog>());
        await _walletRepositoryMock.Received(1).UpdateAsync(Arg.Any<Wallet>());
        await _walletHistoryRepositoryMock.Received(1).InsertAsync(Arg.Any<WalletHistory>());
        await _unitOfWorkManager.Received(1).Current.SaveChangesAsync();

    }

  

    [Fact]
    public async Task Handle_With_InValid_Amount_To_Refund_Should_Throw_Exception()
    {
        //arrange
        var request = CommonUtils.GetProcessRefundRequestCommand(100.00M);
        var handler = GetProcessRefundRequestCommandHandlerInstance();
        //act && assert
        var message = await Should.ThrowAsync<UserFriendlyException>(
            async () => await handler.Handle(request, 1));
        message.Message.ShouldBe("Refund amount does not match the total amount to refund");
    }
    
  
    private ProcessRefundRequestCommandHandler GetProcessRefundRequestCommandHandlerInstance()
    {
        _invoiceRefundRequestRepositoryMock.GetAll().Returns(GetInvoiceRefundReqAsQueryable().BuildMock());
        _walletRepositoryMock.GetAll().Returns(GetWalletAsQueryable().BuildMock());
        return new ProcessRefundRequestCommandHandler(
            _invoiceRefundRequestRepositoryMock,
            _walletHistoryRepositoryMock,_walletRepositoryMock,_paymentActivityLogRepositoryMock,
            _abpSession,_unitOfWorkManager);
    }

    private IQueryable<Wallet> GetWalletAsQueryable()
    {
        return new List<Wallet>()
            {
                new(){
                    PatientId = 1,
                    Balance = new Money(100.00M),
                }
            }.AsQueryable()
        ;
    }
    private IQueryable<InvoiceRefundRequest> GetInvoiceRefundReqAsQueryable()
    {
        return new List<InvoiceRefundRequest>
        {
            new ()
            {
                Id = 1,
                PatientId = 1,
                InvoiceId = 1,
                Invoice = new Invoice
                {
                  InvoiceId  = "0000000001",
                  FacilityId = 1,
                  TenantId = 1
                },
                Status = InvoiceRefundStatus.Pending,
                TenantId = 1,
                FacilityId = 1,
                InvoiceItemId = 1,
                InvoiceItem = new InvoiceItem
                {
                    Id = 1,
                    InvoiceId = 1,
                    TenantId = 1,
                    Status = InvoiceItemStatus.Paid,
                    AmountPaid = new Money(100.00M),
                    SubTotal = new Money(100.00M),
                }
                
            },
            new ()
            {
                Id = 2,
                PatientId = 1,
                InvoiceId = 1,
                Invoice = new Invoice
                {
                    InvoiceId  = "0000000001",
                    FacilityId = 1,
                    TenantId = 1
                },
                Status = InvoiceRefundStatus.Pending,
                TenantId = 1,
                FacilityId = 1,
                InvoiceItemId = 2,
                InvoiceItem = new InvoiceItem
                {
                    Id = 2,
                    InvoiceId = 1,
                    TenantId = 1,
                    Status = InvoiceItemStatus.Paid,
                    AmountPaid = new Money(200.00M),
                    SubTotal = new Money(200.00M),
                }
                
            },
            new()
            {
                Id = 3,
                PatientId = 1,
                InvoiceId = 1,
                Invoice = new Invoice
                {
                    InvoiceId  = "0000000001",
                    FacilityId = 1,
                    TenantId = 1
                },
                Status = InvoiceRefundStatus.Pending,
                TenantId = 1,
                FacilityId = 1,
                InvoiceItemId = 3,
                InvoiceItem = new InvoiceItem
                {
                    Id = 3,
                    InvoiceId = 1,
                    TenantId = 1,
                    Status = InvoiceItemStatus.Paid,
                    AmountPaid = new Money(150.00M),
                    SubTotal = new Money(150.00M),
                }
            }
            
        }.AsQueryable();
    }
}

