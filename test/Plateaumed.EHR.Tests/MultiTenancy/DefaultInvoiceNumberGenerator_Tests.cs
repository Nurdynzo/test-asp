using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Timing;
using Plateaumed.EHR.MultiTenancy.Accounting;
using Plateaumed.EHR.Test.Base;
using Shouldly;

namespace Plateaumed.EHR.Tests.MultiTenancy
{
    // ReSharper disable once InconsistentNaming
    public class DefaultInvoiceNumberGenerator_Tests : AppTestBase
    {
        private readonly IInvoiceNumberGenerator _invoiceNumberGenerator;
        private readonly IRepository<TenantInvoice> _invoiceRepository;

        public DefaultInvoiceNumberGenerator_Tests()
        {
            _invoiceNumberGenerator = Resolve<IInvoiceNumberGenerator>();
            _invoiceRepository = Resolve<IRepository<TenantInvoice>>();
        }

        [MultiTenantFact]
        public async Task Should_Not_Generate_Same_Invoice_Number()
        {
            var invoiceCountToGenerate = 100;
            var invoiceNumbers = new List<string>();

            for (int i = 0; i < invoiceCountToGenerate; i++)
            {
                var invoiceNo = await _invoiceNumberGenerator.GetNewInvoiceNumber();
                invoiceNo.ShouldNotBeNullOrEmpty();
                invoiceNo.Length.ShouldBe(12);//Should be YYYYMM00001

                await _invoiceRepository.InsertAsync(new TenantInvoice
                {
                    InvoiceNo = invoiceNo,
                    InvoiceDate = Clock.Now,
                    TenantAddress = "USA",
                    TenantLegalName = "AspNet Zero",
                    TenantTaxNo = "123456789"
                });

                invoiceNumbers.ShouldNotContain(invNo => invNo == invoiceNo);
                invoiceNumbers.Add(invoiceNo);
            }

            invoiceNumbers.Count.ShouldBe(invoiceCountToGenerate);
        }

        [MultiTenantFact]
        public async Task Should_Start_Over_InvoiceNo_When_Month_Changes()
        {
            await _invoiceRepository.InsertAsync(new TenantInvoice
            {
                InvoiceNo = Clock.Now.Year + (Clock.Now.Month - 1).ToString("00") + "55555",
                InvoiceDate = Clock.Now,
                TenantAddress = "USA",
                TenantLegalName = "AspNet Zero",
                TenantTaxNo = "123456789"
            });

            var invoiceNo = await _invoiceNumberGenerator.GetNewInvoiceNumber();
            invoiceNo.ShouldBe(Clock.Now.Year + Clock.Now.Month.ToString("00") + "000001");
        }

        [MultiTenantFact]
        public async Task Should_Start_Over_InvoiceNo_When_Year_Changes()
        {
            await _invoiceRepository.InsertAsync(new TenantInvoice
            {
                InvoiceNo = (Clock.Now.Year - 1) + (Clock.Now.Month).ToString("00") + "55555",
                InvoiceDate = Clock.Now,
                TenantAddress = "USA",
                TenantLegalName = "AspNet Zero",
                TenantTaxNo = "123456789"
            });

            var invoiceNo = await _invoiceNumberGenerator.GetNewInvoiceNumber();
            invoiceNo.ShouldBe(Clock.Now.Year + (Clock.Now.Month).ToString("00") + "000001");
        }

        [MultiTenantFact]
        public async Task Should_Handle_When_Invoice_Number_Exceeds_Defined_Range()
        {
            await _invoiceRepository.InsertAsync(new TenantInvoice
            {
                InvoiceNo = Clock.Now.Year + Clock.Now.Month.ToString("00") + "99999",
                InvoiceDate = Clock.Now,
                TenantAddress = "USA",
                TenantLegalName = "AspNet Zero",
                TenantTaxNo = "123456789"
            });

            var invoiceNo = await _invoiceNumberGenerator.GetNewInvoiceNumber();
            invoiceNo.ShouldBe(Clock.Now.Year + (Clock.Now.Month).ToString("00") + "100000");

            await _invoiceRepository.InsertAsync(new TenantInvoice
            {
                InvoiceNo = invoiceNo,
                InvoiceDate = Clock.Now,
                TenantAddress = "USA",
                TenantLegalName = "AspNet Zero",
                TenantTaxNo = "123456789"
            });

            invoiceNo = await _invoiceNumberGenerator.GetNewInvoiceNumber();
            invoiceNo.ShouldBe(Clock.Now.Year + (Clock.Now.Month).ToString("00") + "100001");
        }
    }
}
