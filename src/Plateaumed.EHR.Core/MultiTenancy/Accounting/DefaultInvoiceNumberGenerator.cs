using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Timing;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.MultiTenancy.Accounting
{
    public class DefaultInvoiceNumberGenerator : IInvoiceNumberGenerator
    {
        private readonly IRepository<TenantInvoice> _tenantInvoiceRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        
        public DefaultInvoiceNumberGenerator(
            IRepository<TenantInvoice> tenantInvoiceRepository, 
            IUnitOfWorkManager unitOfWorkManager)
        {
            _tenantInvoiceRepository = tenantInvoiceRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        
        public async Task<string> GetNewInvoiceNumber()
        {
            return await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                var lastTenantInvoice = await _tenantInvoiceRepository.GetAll().OrderByDescending(i => i.Id).FirstOrDefaultAsync();
                if (lastTenantInvoice == null)
                {
                    return Clock.Now.Year + "" + (Clock.Now.Month).ToString("00") + "000001";
                }

                var year = Convert.ToInt32(lastTenantInvoice.InvoiceNo.Substring(0, 4));
                var month = Convert.ToInt32(lastTenantInvoice.InvoiceNo.Substring(4, 2));

                var invoiceNumberToIncrease = lastTenantInvoice.InvoiceNo.Substring(6, lastTenantInvoice.InvoiceNo.Length - 6);
                if (year != Clock.Now.Year || month != Clock.Now.Month)
                {
                    invoiceNumberToIncrease = "0";
                }

                var invoiceNumberPostfix = Convert.ToInt32(invoiceNumberToIncrease) + 1;
                return Clock.Now.Year + (Clock.Now.Month).ToString("00") + invoiceNumberPostfix.ToString("000000");
            });
        }
    }
}
