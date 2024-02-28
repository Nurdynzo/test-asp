using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Encounters.Dto;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Command;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Tests.Invoices.Util;
using Plateaumed.EHR.ValueObjects;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;

[Trait("Category", "Unit")]
public class CreateInvoiceHandlerTest
{
    private readonly IRepository<Invoice, long> _invoiceRepository = Substitute.For<IRepository<Invoice, long>>();
    private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository = Substitute.For<IRepository<PatientAppointment, long>>();
    private readonly IObjectMapper _objectMapper = Substitute.For<IObjectMapper>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IEncounterManager _encounterManager = Substitute.For<IEncounterManager>();
    private readonly IAbpSession _abpSession = Substitute.For<IAbpSession>();

    [Fact]
    public async Task CreateNewInvoiceCommandHandler_Handle_With_No_Invoice_Item_Should_Throw_Exception()
    {
        //arrange
        var command = GetCreatNewInvoiceCommand(CommonUtil.GetMoneyDto(0));
        command.Items = new List<InvoiceItemRequest>();
        MockDependencies(command);
        //act & assert
        var handler = CreateNewInvoiceCommandHandler();
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command,  1));
        result.Message.ShouldBe("Invoice items cannot be empty");
        
    }
    [Fact]
    public async Task CreateNewInvoiceCommandHandler_Handle_With_ValidCommand_Should_Return_Created_Invoice()
    {
        //Arrange
        var command= GetCreatNewInvoiceCommand();
        MockDependencies(command);
        var handler = CreateNewInvoiceCommandHandler();
        //Act
        var result = await handler.Handle(command, 1);
        //Assert
        result.ShouldBeAssignableTo<CreateNewInvoiceCommand>();
        result.InvoiceNo.ShouldBe("00001");
        result.Id.ShouldBe(1);
        result.TotalAmount.Amount.ShouldBe(CommonUtil.GetMoneyDto().Amount);
        result.TotalAmount.Currency.ShouldBe(CommonUtil.GetMoneyDto().Currency);
        
    }
    private void MockDependencies(CreateNewInvoiceCommand command)
    {
        _objectMapper.Map<Invoice>(Arg.Any<CreateNewInvoiceCommand>()).Returns(GetInvoiceInstance(command));
        _patientAppointmentRepository.GetAll().Returns(GetPatientAppointmentAsQueryable().BuildMock());
        _invoiceRepository.InsertAsync(Arg.Any<Invoice>())
            .ReturnsForAnyArgs(GetInvoiceInstance(GetCreatNewInvoiceCommand()));
        _abpSession.TenantId.Returns(1);
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _encounterManager.CreateAppointmentEncounter(Arg.Any<CreateAppointmentEncounterRequest>()).Returns(Task.CompletedTask);
    }
    private static IQueryable<PatientAppointment> GetPatientAppointmentAsQueryable()
    {
        return new List<PatientAppointment>
        {
            new() {
                Id = 1,
                Status = AppointmentStatusType.Not_Arrived,
                PatientId = 1,
            }
        }.AsQueryable();
    }

    private Invoice GetInvoiceInstance(CreateNewInvoiceCommand command)
    {
        return new Invoice
        {
            Id = 1,
            PaymentType = command.PaymentType,
            TotalAmount = new Money(command.TotalAmount.Amount, command.TotalAmount.Currency),
            InvoiceId = command.InvoiceNo,
            PatientId = command.PatientId,
            PatientAppointmentId = command.AppointmentId,
            InvoiceItems = command.Items.Select(x => new InvoiceItem()
            {
                Name = x.Name,
                Quantity = x.Quantity,
                UnitPrice = new Money(x.UnitPrice.Amount, x.UnitPrice.Currency),
                SubTotal = new Money(x.SubTotal.Amount, x.SubTotal.Currency),
                DiscountPercentage = x.DiscountPercentage

            }).ToList()

        };
    }

    private static CreateNewInvoiceCommand  GetCreatNewInvoiceCommand(
        MoneyDto totalAmount = null,
        PaymentTypes paymentType = PaymentTypes.Wallet)
    {
       return CommonUtil.GetCreatNewInvoiceCommand(totalAmount ?? CommonUtil.GetMoneyDto(),paymentType);
    }
    private CreateNewInvoiceCommandHandler CreateNewInvoiceCommandHandler()
    {
        var handler = new CreateNewInvoiceCommandHandler(_invoiceRepository, _objectMapper, _patientAppointmentRepository, _unitOfWork, _encounterManager,_abpSession);
        return handler;
    }
}
