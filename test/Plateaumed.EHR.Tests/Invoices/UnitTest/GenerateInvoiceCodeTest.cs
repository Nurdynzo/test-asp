using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Command;
using Plateaumed.EHR.Invoices.Dtos;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.UnitTest;
[Trait("Category", "Unit")]
public class GenerateInvoiceCodeTest
{
    private readonly IRepository<Invoice, long> _invoiceRepositoryMock = Substitute.For<IRepository<Invoice,long>>();

    [Fact]
    public async Task GenerateInvoiceCommandHandler_Handle_Should_Return_Next_Invoice_Number()
    {
        //arrange 
        _invoiceRepositoryMock.GetAll().Returns(GetInvoiceAsQueryable().BuildMock());
        var handler = new GenerateInvoiceCommandHandler(_invoiceRepositoryMock);
        //act 
        var invoiceCode = await handler.Handle(new GenerateInvoiceCommand() { TenantId = 1 });
        // assert
       invoiceCode.ShouldBe("0000000004");
        
    }
    
    [Fact]
    public async Task GenerateInvoiceCommandHandler_Handle_For_The_First_InvoiceShould_Return_The_First_Invoice_Number()
    {
        //arrange 
        _invoiceRepositoryMock.GetAll().Returns(new List<Invoice>().AsQueryable().BuildMock());
        var handler = new GenerateInvoiceCommandHandler(_invoiceRepositoryMock);
        //act 
        var invoiceCode = await handler.Handle(new GenerateInvoiceCommand() { TenantId = 1 });
        // assert
        invoiceCode.ShouldBe("0000000001");
        
    }

    private IQueryable<Invoice> GetInvoiceAsQueryable()
    {
        return new List<Invoice>()
        {
            new()
            {
                TenantId = 1,
                PatientId = 1,
                Id = 1,
                InvoiceId = "0000000001",
                
            },
            new()
            {
                TenantId = 1,
                PatientId = 1,
                Id = 2,
                InvoiceId = "0000000002"
            },
            new()
            {
                TenantId = 1,
                PatientId = 1,
                Id = 3,
                InvoiceId = "0000000003"
            },
            
        }.AsQueryable();
    }
    
}