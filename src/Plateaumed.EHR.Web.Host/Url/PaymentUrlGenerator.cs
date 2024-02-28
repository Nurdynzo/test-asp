﻿using Abp.Dependency;
using Plateaumed.EHR.MultiTenancy.Payments;
using Plateaumed.EHR.Url;

namespace Plateaumed.EHR.Web.Url
{
    public class PaymentUrlGenerator : IPaymentUrlGenerator, ITransientDependency
    {
        private readonly IWebUrlService _webUrlService;

        public PaymentUrlGenerator(IWebUrlService webUrlService)
        {
            _webUrlService = webUrlService;
        }

        public string CreatePaymentRequestUrl(SubscriptionPayment subscriptionPayment)
        {
            var webSiteRootAddress = _webUrlService.GetSiteRootAddress();

            return webSiteRootAddress +
                   "account/" +
                   subscriptionPayment.Gateway.ToString().ToLowerInvariant() + "-purchase" +
                   "?tenantId=" + subscriptionPayment.TenantId +
                   "&paymentId=" + subscriptionPayment.Id +
                   "&redirectUrl=account%2Fregister-tenant-result";
        }
    }
}