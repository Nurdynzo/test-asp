using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Command;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;
[Trait("Category", "Unit")]
public class UpdateInvoiceCommandHandlerTest
{
    private readonly IRepository<Invoice, long> _invoiceRepository = Substitute.For<IRepository<Invoice, long>>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IObjectMapper _objectMapper = Substitute.For<IObjectMapper>();

    private readonly IRepository<InvoiceItem, long> _invoiceItemRepository =
        Substitute.For<IRepository<InvoiceItem, long>>();

    [Fact]
    public async Task Handle_With_ValidCommand_Should_Update_Invoice()
    {
        //Arrange
        var command = GetUpdateNewInvoiceCommand();
        var handler = GetUpdateInvoiceCommandHandlerInstance();
        //Act
        await handler.Handle(command);
        //Assert
        await _unitOfWork.Received(1).Current.SaveChangesAsync();
    }



    [Fact]
    public async Task Handle_With_WrongTotalAmountCommand_Should_Update_Throw_UserFriendly_Exception()
    {
        //Arrange
        var command = GetUpdateNewInvoiceCommand(CommonUtil.GetMoneyDto(300.00M));

        var handler = GetUpdateInvoiceCommandHandlerInstance();
        //Act && Assert
        var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        message.Message.ShouldBe("Total amount is not equal to the sum of the items to be saved");

    }

    [Fact]
    public async Task Handle_With_WrongInvoiceId_Should_Update_Throw_UserFriendly_Exception()
    {
        //Arrange
        var command = GetUpdateNewInvoiceCommand(id: 10);

        var handler = GetUpdateInvoiceCommandHandlerInstance();
        //Act && Assert
        var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        message.Message.ShouldBe("Invoice not found");
    }

    [Fact]
    public async Task Handle_Invoice_Items_With_Delete_Flag_And_Has_Amount_Paid_Should_Throw_UserFriendly_Exception()
    {
        //Arrange
        var command = GetUpdateNewInvoiceCommand();
        var handler = GetUpdateInvoiceCommandHandlerInstance(2);
        //Act && Assert
        var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        message.Message.ShouldBe("Cannot delete item that has been paid for");
    }

    private static UpdateNewInvoiceRequest GetUpdateNewInvoiceCommand(MoneyDto amount = null, long id = 1)
    {
        var command = new UpdateNewInvoiceRequest()
        {
            Id = id,
            InvoiceNo = "00001",
            AppointmentId = 1,
            PatientId = 1,
            PaymentType = PaymentTypes.Wallet,
            TotalAmount = amount ?? CommonUtil.GetMoneyDto(368.00M),
            IsServiceOnCredit = true,
            Items = new List<InvoiceItemRequest>
            {
                new()
                {
                    Id = 1,
                    Name = "Test",
                    Quantity = 1,
                    UnitPrice = CommonUtil.GetMoneyDto(100),
                    SubTotal = CommonUtil.GetMoneyDto(98),
                    DiscountPercentage = 2,
                    IsGlobal = false,
                    IsDeleted = false
                },
                new()
                {
                    Id=2,
                    Name = "Test2",
                    Quantity = 2,
                    UnitPrice = CommonUtil.GetMoneyDto(100),
                    SubTotal = CommonUtil.GetMoneyDto(196),
                    DiscountPercentage = 2,
                    IsGlobal = false,
                    IsDeleted = true
                },
                new()
                {
                    Id = 3,
                    Name = "Test3",
                    Quantity = 3,
                    UnitPrice = CommonUtil.GetMoneyDto(100),
                    SubTotal = CommonUtil.GetMoneyDto(270),
                    DiscountPercentage = 10,
                    IsGlobal = true,
                    IsDeleted = false
                }
            }
        };
        return command;
    }

  

    private UpdateInvoiceCommandHandler GetUpdateInvoiceCommandHandlerInstance(long itemId=10)
    {

        var invoice = CommonUtil.GetInvoiceWithInvoiceItemsAsQueryable(itemId).BuildMock();

        _invoiceRepository.GetAllIncluding(Arg.Any<Expression<Func<Invoice, object>>>())
        .Returns(invoice);

        var handler = new UpdateInvoiceCommandHandler(_invoiceRepository, _invoiceItemRepository, _unitOfWork, _objectMapper);  
        return handler;
    }
}

