using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using MockQueryable.NSubstitute;
using NSubstitute;
using NSubstitute.ClearExtensions;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Command;
using Plateaumed.EHR.Invoices.Dtos.InvoiceRelief;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Tests.Invoices.Util;
using Plateaumed.EHR.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest
{
    public class ApproveDebtReliefCommandHandlerTest
    {
        private readonly IRepository<Invoice, long> _invoiceRepository = Substitute.For<IRepository<Invoice, long>>();
        private readonly IRepository<PaymentActivityLog, long> _logRepository = Substitute.For<IRepository<PaymentActivityLog, long>>();
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepository = Substitute.For<IRepository<InvoiceItem, long>>();
        private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();


        [Fact]
        public async Task ApproveDebtReliefCommandHandler_Should_Update_Discount_Correctly()
        {
            //Arrange
            var request = new ApproveDebtReliefRequestDto
            {
                TenantId = 1,
                FacilityId = 1,
                DiscountPercentage = 5,
                SelectedInvoiceItemIds = new List<long> { 1, 2, 3 }
            };
            _invoiceItemRepository.GetAll().Returns(GetInvoiceItemAsQueryable().BuildMock());
            _invoiceRepository.GetAll().Returns(GetInvoiceAsQueryable().BuildMock());
            _logRepository.GetAll().Returns(CommonUtil.GetPaymentActivityLogAsQueryable().BuildMock());
            var hander = new ApproveDebtReliefCommandHandler(_invoiceRepository, _logRepository, _invoiceItemRepository, _unitOfWork);
            //Act
            await hander.Handle(request);
            //Assert
            _unitOfWork.Received(1).Current.SaveChanges();
        }


        public static IQueryable<InvoiceItem> GetInvoiceItemAsQueryable(long facilityId = 1)
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
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(98),
                Status = InvoiceItemStatus.Unpaid,
                FacilityId = facilityId
            },
            new()
            {
                Id = 2,
                Name = "Test2",
                Quantity = 2,
                UnitPrice = new Money(100),
                SubTotal = new Money(196),
                DiscountPercentage = 2,
                InvoiceId = 1,
                AmountPaid = new Money(0.00M),
                OutstandingAmount = new Money(196),
                Status = InvoiceItemStatus.Unpaid,
                FacilityId = facilityId
            }
            }.AsQueryable();

        }


        public static IQueryable<Invoice> GetInvoiceAsQueryable()
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
                    CreationTime = DateTime.Today.AddDays(-10),
                    CreatorUserId = 1,
                    PaymentType = PaymentTypes.Wallet,
                    PatientAppointmentId = 1,
                
                }
            }.AsQueryable();
        }
    }
}
