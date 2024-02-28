using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Command;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Tests.Invoices.Util;
using Plateaumed.EHR.ValueObjects;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest
{
    [Trait("Category", "Unit")]
    public class CreateInvestigationInvoiceHandlerTest
    {
        private readonly IRepository<Invoice, long> _invoiceRepository = Substitute.For<IRepository<Invoice, long>>();
        private readonly IRepository<InvestigationRequest, long> _investigationRequest = Substitute.For<IRepository<InvestigationRequest, long>>();
        private readonly IRepository<PatientEncounter, long> _patientEncounter = Substitute.For<IRepository<PatientEncounter, long>>();
        private readonly IObjectMapper _objectMapper = Substitute.For<IObjectMapper>();
        private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
        
        [Fact]
        public async Task CreateNewInvoiceCommandHandler_Handle_With_No_Invoice_Item_Should_Throw_Exception()
        {
            //arrange
            var command = GetCreatNewInvestigationInvoiceCommand(CommonUtil.GetMoneyDto(0));
            command.Items = new List<InvoiceItemRequest>();
            MockDependencies(command);
            //act & assert
            var handler = CreateInvestigationInvoiceCommandHandler();
            var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command, 1));
            result.Message.ShouldBe("Invoice items cannot be empty");
        }
        [Fact]
        public async Task CreateNewInvoiceCommandHandler_Handle_With_ValidCommand_Should_Return_Created_Invoice()
        {
            //Arrange
            var command = GetCreatNewInvestigationInvoiceCommand();
            MockDependencies(command);
            var handler = CreateInvestigationInvoiceCommandHandler();
            //Act
            var result = await handler.Handle(command, 1);
            //Assert
            result.ShouldBeAssignableTo<CreateNewInvestigationInvoiceCommand>();
            result.InvoiceNo.ShouldBe("00001");
            result.Id.ShouldBe(1);
            result.TotalAmount.Amount.ShouldBe(CommonUtil.GetMoneyDto().Amount);
            result.TotalAmount.Currency.ShouldBe(CommonUtil.GetMoneyDto().Currency);

        }
        private void MockDependencies(CreateNewInvestigationInvoiceCommand command)
        {
            _objectMapper.Map<Invoice>(Arg.Any<CreateNewInvestigationInvoiceCommand>()).Returns(GetInvoiceInstance(command));
            _invoiceRepository.InsertAsync(Arg.Any<Invoice>())
                .ReturnsForAnyArgs(GetInvoiceInstance(GetCreatNewInvestigationInvoiceCommand()));
            _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
            _patientEncounter.GetAll().Returns(GetPatientEncounterAsQueryable().BuildMock());
            _investigationRequest.GetAll().Returns(GetInvestigationRequestAsQueryable().BuildMock());
            
        }
        private static IQueryable<PatientEncounter> GetPatientEncounterAsQueryable()
        {
            return new List<PatientEncounter>
            {
                new()
                {
                    Id = 1,
                    Status = EncounterStatusType.InProgress,
                    PatientId = 1,
                }
            }.AsQueryable();
        }

        private static IQueryable<InvestigationRequest> GetInvestigationRequestAsQueryable()
        {
            return new List<InvestigationRequest>
            {
                new()
                {
                    Id = 1,
                    InvestigationId = 1,
                    InvestigationStatus = InvestigationStatus.Requested,
                    PatientId = 1
                }
            }.AsQueryable();
        }

        private Invoice GetInvoiceInstance(CreateNewInvestigationInvoiceCommand command)
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

        private static CreateNewInvestigationInvoiceCommand GetCreatNewInvestigationInvoiceCommand(
            MoneyDto totalAmount = null,
            PaymentTypes paymentType = PaymentTypes.Wallet)
        {
            return CommonUtil.GetCreatNewInvestigationInvoiceCommand(totalAmount ?? CommonUtil.GetMoneyDto(), paymentType);
        }
        private CreateInvestigationInvoiceCommandHandler CreateInvestigationInvoiceCommandHandler()
        {
            var handler = new CreateInvestigationInvoiceCommandHandler(_invoiceRepository, _objectMapper, _patientEncounter, _unitOfWork, _investigationRequest);
            return handler;
        }
    }
}

