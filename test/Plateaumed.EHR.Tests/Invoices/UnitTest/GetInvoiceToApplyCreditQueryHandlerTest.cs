using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.InvoiceRelief;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Tests.Invoices.Util;
using Plateaumed.EHR.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetInvoiceToApplyCreditQueryHandlerTest
    {
        private readonly IRepository<Invoice, long> _invoiceRepositoryMock = Substitute.For<IRepository<Invoice, long>>();
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepositoryMock = Substitute.For<IRepository<InvoiceItem, long>>();


        [Fact]
        public async Task GetInvoiceToApplyCreditQueryHandler_Should_Return_InvoiceWithCorrespondingInvoiceItems()
        {
            //Arrange
            _invoiceItemRepositoryMock.GetAll().Returns(GetInvoiceItemsAsQueryable().BuildMock());
            _invoiceRepositoryMock.GetAll().Returns(GetInvoicesAsQueryable().BuildMock());
            var handler = new GetInvoicesToApplyCreditRequestHandler(_invoiceRepositoryMock, _invoiceItemRepositoryMock);
            //Act
            var result = await handler.Handle(1);
    
            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<GetInvoiceToApproveCrediteDto>>();
            result.Count.ShouldBeGreaterThan(0);
            result[0].Items.ShouldNotBeNull();
            result[0].Items.ShouldBeOfType<List<GetInvoiceItemsToApproveCreditDto>>();
        }

        public static IQueryable<Invoice> GetInvoicesAsQueryable()
        {
            return new List<Invoice>
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    InvoiceId = "0000000001",
                    TotalAmount = new Money(100),
                    PaymentStatus = PaymentStatus.Unpaid,
                    AmountPaid = new Money(0.00M),
                    FacilityId = 1,
                    CreationTime = DateTime.Today.AddDays(-10),
                    CreatorUserId = 1,
                    PaymentType = PaymentTypes.Wallet,
                    PatientAppointmentId = 1,
                    InvoiceItems = new List<InvoiceItem>
                    {
                        new()
                        {
                            Id = 1,
                            Name = "Test",
                            Quantity = 1,
                            UnitPrice = new Money(100),
                            SubTotal = new Money(98),
                            DiscountPercentage = 2,
                            InvoiceId = 1,
                            AmountPaid = new Money(98),
                            DebtReliefAmount = new Money(20),
                            OutstandingAmount = new Money(0.00M),
                            Status = InvoiceItemStatus.Unpaid
                        }
                    }
                }
            }.AsQueryable();
        }

        public static IQueryable<InvoiceItem> GetInvoiceItemsAsQueryable()
        {
            return new List<InvoiceItem>
            {
                new()
                {
                    Id = 1,
                    Name = "Test",
                    Quantity = 1,
                    UnitPrice = new Money(100),
                    SubTotal = new Money(98),
                    DiscountPercentage = 2,
                    InvoiceId = 1,
                    AmountPaid = new Money(98),
                    DebtReliefAmount = new Money(20),
                    OutstandingAmount = new Money(0.00M),
                    Status = InvoiceItemStatus.Unpaid
                }
            }.AsQueryable();
        }
    }
}
