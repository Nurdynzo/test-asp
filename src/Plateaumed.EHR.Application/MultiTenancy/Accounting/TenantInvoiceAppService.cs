using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using Plateaumed.EHR.Configuration;
using Plateaumed.EHR.Editions;
using Plateaumed.EHR.MultiTenancy.Accounting.Dto;
using Plateaumed.EHR.MultiTenancy.Payments;

namespace Plateaumed.EHR.MultiTenancy.Accounting
{
    public class TenantInvoiceAppService : EHRAppServiceBase, ITenantInvoiceAppService
    {
        private readonly ISubscriptionPaymentRepository _subscriptionPaymentRepository;
        private readonly IInvoiceNumberGenerator _invoiceNumberGenerator;
        private readonly EditionManager _editionManager;
        private readonly IRepository<TenantInvoice> _appInvoiceRepository;

        public TenantInvoiceAppService(
            ISubscriptionPaymentRepository subscriptionPaymentRepository,
            IInvoiceNumberGenerator invoiceNumberGenerator,
            EditionManager editionManager,
            IRepository<TenantInvoice> appInvoiceRepository)
        {
            _subscriptionPaymentRepository = subscriptionPaymentRepository;
            _invoiceNumberGenerator = invoiceNumberGenerator;
            _editionManager = editionManager;
            _appInvoiceRepository = appInvoiceRepository;
        }

        public async Task<TenantInvoiceDto> GetInvoiceInfo(EntityDto<long> input)
        {
            var payment = await _subscriptionPaymentRepository.GetAsync(input.Id);

            if (string.IsNullOrEmpty(payment.InvoiceNo))
            {
                throw new Exception("There is no invoice for this payment !");
            }

            if (payment.TenantId != AbpSession.GetTenantId())
            {
                throw new UserFriendlyException(L("ThisInvoiceIsNotYours"));
            }

            var appInvoice = await _appInvoiceRepository.FirstOrDefaultAsync(b => b.InvoiceNo == payment.InvoiceNo);
            if (appInvoice == null)
            {
                throw new UserFriendlyException();
            }

            var edition = await _editionManager.FindByIdAsync(payment.EditionId);
            var hostAddress = await SettingManager.GetSettingValueAsync(AppSettings.HostManagement.BillingAddress);

            return new TenantInvoiceDto
            {
                InvoiceNo = payment.InvoiceNo,
                InvoiceDate = appInvoice.InvoiceDate,
                Amount = payment.Amount,
                EditionDisplayName = edition.DisplayName,

                HostAddress = hostAddress.Replace("\r\n", "|").Split('|').ToList(),
                HostLegalName = await SettingManager.GetSettingValueAsync(AppSettings.HostManagement.BillingLegalName),

                TenantAddress = appInvoice.TenantAddress.Replace("\r\n", "|").Split('|').ToList(),
                TenantLegalName = appInvoice.TenantLegalName,
                TenantTaxNo = appInvoice.TenantTaxNo
            };
        }

        [UnitOfWork(IsolationLevel.ReadUncommitted)]
        public async Task CreateInvoice(TenantCreateInvoiceDto input)
        {
            var payment = await _subscriptionPaymentRepository.GetAsync(input.SubscriptionPaymentId);
            if (!string.IsNullOrEmpty(payment.InvoiceNo))
            {
                throw new Exception("Invoice is already generated for this payment.");
            }

            var invoiceNo = await _invoiceNumberGenerator.GetNewInvoiceNumber();

            var tenantLegalName = await SettingManager.GetSettingValueAsync(AppSettings.TenantManagement.BillingLegalName);
            var tenantAddress = await SettingManager.GetSettingValueAsync(AppSettings.TenantManagement.BillingAddress);
            var tenantTaxNo = await SettingManager.GetSettingValueAsync(AppSettings.TenantManagement.BillingTaxVatNo);

            if (string.IsNullOrEmpty(tenantLegalName) || string.IsNullOrEmpty(tenantAddress) || string.IsNullOrEmpty(tenantTaxNo))
            {
                throw new UserFriendlyException(L("InvoiceInfoIsMissingOrNotCompleted"));
            }

            await _appInvoiceRepository.InsertAsync(new TenantInvoice
            {
                InvoiceNo = invoiceNo,
                InvoiceDate = Clock.Now,
                TenantLegalName = tenantLegalName,
                TenantAddress = tenantAddress,
                TenantTaxNo = tenantTaxNo
            });

            payment.InvoiceNo = invoiceNo;
        }
    }
}