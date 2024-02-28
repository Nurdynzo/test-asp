using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos.InvoiceRelief;
using Plateaumed.EHR.Invoices.Query;
using Plateaumed.EHR.Tests.Invoices.Util;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Plateaumed.EHR.Authorization.Users;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest
{
    public class GetItemsToApplyReliefQueryHandlerTest
    {
        private readonly IRepository<Invoice, long> _invoiceRepositoryMock = Substitute.For<IRepository<Invoice, long>>();
        private readonly IRepository<InvoiceItem, long> _invoiceItemRepositoryMock = Substitute.For<IRepository<InvoiceItem, long>>();
        private readonly IRepository<User,long> _userRepositoryMock = Substitute.For<IRepository<User, long>>();
        private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();


        [Fact]
        public async Task GetItemsToApplyReliefQueryHandler_Should_Return_InvoiceWithCorrespondingInvoiceItems()
        {
            //Arrange
            _invoiceItemRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceItemAsQueryable().BuildMock());
            _invoiceRepositoryMock.GetAll().Returns(CommonUtil.GetInvoiceAsQueryable().BuildMock());
            _userRepositoryMock.GetAll().Returns(CommonUtil.GetUserAsQueryable().BuildMock());
            _abpSessionMock.UserId.Returns(1);
            var handler = new GetItemsToApplyReliefQueryHandler(_invoiceItemRepositoryMock, _invoiceRepositoryMock, _userRepositoryMock, _abpSessionMock);
            //Act
            var result = await handler.Handle(1);
            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<ApplyReliefInvoiceViewDto>>();
            result.Count.ShouldBeGreaterThan(0);
            result[0].InitiatedBy.ShouldBe("Dr. John");
        }
    }
}
