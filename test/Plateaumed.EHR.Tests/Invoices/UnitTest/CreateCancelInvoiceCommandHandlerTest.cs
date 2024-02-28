using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Command;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.ValueObjects;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;

[Trait("Category", "Unit")]
public class CreateCancelInvoiceCommandHandlerTest
{
    private readonly IRepository<InvoiceCancelRequest,long> _invoiceCancelRequestRepositoryMock
        = Substitute.For<IRepository<InvoiceCancelRequest, long>>();
    private readonly IRepository<InvoiceItem,long> _invoiceItemRepositoryMock
        = Substitute.For<IRepository<InvoiceItem, long>>();
    private readonly IUnitOfWorkManager _unitOfWorkManagerMock = Substitute.For<IUnitOfWorkManager>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();

    [Fact]
    public async Task Handle_With_ValidRequest_Should_Create_CancelInvoice()
    {
        //Arrange
        var request = GetCreateCancelInvoiceCommand(1,1,2,3);
        var handler = GetCreateCancelInvoiceCommandHandlerInstance();
        //Act
        await handler.Handle(request, 1);
        //Assert
        _unitOfWorkManagerMock.Received(1).Begin(Arg.Any<TransactionScopeOption>());
        await _invoiceCancelRequestRepositoryMock.Received(2).InsertAsync(Arg.Any<InvoiceCancelRequest>());
    }
    [Fact]
    public async Task Handle_With_Non_Matching_InvoiceItem_Should_Throw_UserFriendlyException()
    {
        //Arrange
        var request = GetCreateCancelInvoiceCommand(1,5,6,7);
        var handler = GetCreateCancelInvoiceCommandHandlerInstance();
        //Act && Assert
        var message = await  Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request, 1));
        message.Message.ShouldBe("No matching invoice items to cancel");
        
    }

    private ICreateCancelInvoiceCommandHandler GetCreateCancelInvoiceCommandHandlerInstance()
    {
        _invoiceItemRepositoryMock.GetAll().Returns(GetInvoiceItemAsQueryable().BuildMock());
        _invoiceCancelRequestRepositoryMock.GetAll().Returns(GetInvoiceCancelRequestAsQueryable().BuildMock());
        _abpSessionMock.TenantId.Returns(1);
        
        return new CreateCancelInvoiceCommandHandler(
            _invoiceCancelRequestRepositoryMock,
            _invoiceItemRepositoryMock,
            _unitOfWorkManagerMock, 
            _abpSessionMock);
    }

    private IQueryable<InvoiceCancelRequest> GetInvoiceCancelRequestAsQueryable()
    {
        return new List<InvoiceCancelRequest>()
        {
            new ()
            {
                Id = 1,
                PatientId = 1,
                InvoiceId = 1,
                Status = InvoiceCancelStatus.Pending,
                TenantId = 1,
                FacilityId = 1,
                InvoiceItemId = 3
            },
            
        }.AsQueryable();
    }

    private IQueryable<InvoiceItem> GetInvoiceItemAsQueryable(long facilityId = 1, int tenantId = 1)
    {
        return new List<InvoiceItem>()
        {
            new ()
            {
                Id = 1,
                InvoiceId = 1,
                Name = "test",
                Status = InvoiceItemStatus.Unpaid,
                SubTotal = new Money(100.00M),
                FacilityId = facilityId,
                TenantId = tenantId
            },
            new ()
            {
                Id = 2,
                InvoiceId = 1,
                Name = "test2",
                Status = InvoiceItemStatus.Unpaid,
                SubTotal = new Money(100.00M),
                FacilityId = facilityId,
                TenantId = tenantId
            },
            new ()
            {
                Id = 3,
                InvoiceId = 1,
                Name = "test3",
                Status = InvoiceItemStatus.Unpaid,
                SubTotal = new Money(100.00M),
                FacilityId = facilityId,
                TenantId = tenantId
            },
        }.AsQueryable();
    }

    private CreateCancelInvoiceCommand GetCreateCancelInvoiceCommand(long patientId =1, params long[] invoiceItemsIds)
    {
        return new CreateCancelInvoiceCommand
        {
            PatientId = patientId,
            InvoiceItemsIds = invoiceItemsIds

        };
    }
}