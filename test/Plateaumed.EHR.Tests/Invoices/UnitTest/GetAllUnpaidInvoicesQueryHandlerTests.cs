
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Invoices.Query.BaseQueryHelper;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using System.Threading.Tasks;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.ValueObjects;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetAllUnpaidInvoicesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenPatientHasNoInvoice_ShouldReturnEmptyListWithAmountNull()
        {

            // Arrange
            var request = new UnpaidInvoicesRequest { PatientId = 10 };
            var handler = GetHandler();

            // Act
            var response = await handler.Handle(request);

            // Assert
            response.TotalAmount.ShouldBe(null);
            response.Invoices.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Handle_GivenPatient_ShouldReturnAllPartiallyAndUnpaidInvoices()
        {

            // Arrange
            var request = new UnpaidInvoicesRequest { PatientId = 1 };
            var handler = GetHandler();

            // Act
            var response = await handler.Handle(request);

            // Assert
            response.TotalAmount.ShouldNotBe(null);
            response.TotalAmount.Amount.ShouldBe(500);
            response.TotalAmount.Currency.ShouldBe(CommonUtil.GetMoneyDto().Currency);
            response.Invoices.Count.ShouldBe(1);
            response.Invoices[0].InvoiceItems.Count.ShouldBe(2);
        }

        private GetAllUnpaidInvoicesQueryHandler GetHandler()
        {
            var invoicesBaseQuery = GetBaseQuery();
            var handler = new GetAllUnpaidInvoicesQueryHandler(invoicesBaseQuery);
            return handler;
        }

        private static InvoicesBaseQuery GetBaseQuery()
        {
            var invoiceRepository = Substitute.For<IRepository<Invoice, long>>();
            invoiceRepository.GetAll().Returns(GetUnpaidInvoiceAsQueryable().BuildMock());
            return new InvoicesBaseQuery(invoiceRepository);
        }
        private static IQueryable<Invoice> GetUnpaidInvoiceAsQueryable()
        {
            return new List<Invoice>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    InvoiceId = "000000001",
                    AmountPaid = new Money(100),
                    FacilityId = 1,
                    PaymentType = PaymentTypes.Wallet,
                    InvoiceType = InvoiceType.InvoiceCreation,
                    CreationTime = DateTime.Now.AddDays(-5),
                    OutstandingAmount = new Money(400),
                    InvoiceSource = InvoiceSource.OutPatient,
                    TotalAmount = new Money(500),
                    TenantId = 1,
                    PaymentStatus = PaymentStatus.PartiallyPaid,
                    InvoiceItems = new List<InvoiceItem>()
                    {
                        new ()
                        {
                            Id = 1,
                            InvoiceId = 1,
                            FacilityId = 1,
                            Name = "Consultation",
                            Quantity = 1,
                            UnitPrice = new Money(200),
                            OutstandingAmount = new Money(200),
                            SubTotal = new Money(200),
                            Status = InvoiceItemStatus.Unpaid,
                            DiscountAmount = new Money(0),
                            DiscountPercentage = 0,
                            TenantId = 1,
                            CreationTime = DateTime.Now.AddDays(-5),
                        },
                        new ()
                        {
                            Id = 2,
                            InvoiceId = 1,
                            FacilityId = 1,
                            Name = "Drugs",
                            Quantity = 1,
                            UnitPrice = new Money(300),
                            OutstandingAmount = new Money(200),
                            AmountPaid = new Money(100),
                            SubTotal = new Money(300),
                            Status = InvoiceItemStatus.Unpaid,
                            DiscountAmount = new Money(0),
                            DiscountPercentage = 0,
                            TenantId = 1,
                            CreationTime = DateTime.Now.AddDays(-5),
                        }
                    }
                }
            }.AsQueryable();
        }

    }
}
