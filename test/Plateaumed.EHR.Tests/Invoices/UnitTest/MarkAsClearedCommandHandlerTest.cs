using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Command;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest
{
    [Trait("Category", "Unit")]
    public class MarkAsClearedCommandHandlerTest
    {
        private readonly IRepository<Invoice, long> _invoiceRepository = Substitute.For<IRepository<Invoice, long>>();
        private readonly IRepository<PaymentActivityLog, long> _logRepository = Substitute.For<IRepository<PaymentActivityLog, long>>();
        private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();


        [Fact]
        public async Task MarkAsClearedCommandHadler_Should_Calculate_Correctly()
        {
            //Arrange
            _invoiceRepository.GetAll().Returns(CommonUtil.GetInvoiceWithInvoiceItemsAsQueryable().BuildMock());
            _logRepository.GetAll().Returns(CommonUtil.GetPaymentActivityLogAsQueryable().BuildMock());
            //Act
            var handler = new MarkAsClearedCommandHandler(_invoiceRepository, _logRepository, _unitOfWork);
            await handler.Handle(1);
            var invoice = _invoiceRepository.GetAll().SingleOrDefault(x => x.Id == 1);
            //Assert
            invoice.AmountPaid.Amount.ShouldBe(100M);
        }
    }
}
