using Abp.Dependency;
using Abp.Extensions;
using Abp.MultiTenancy;
using Plateaumed.EHR.Url;

namespace Plateaumed.EHR.Web.Url
{
    public abstract class AppUrlServiceBase : IAppUrlService, ITransientDependency
    {
        public abstract string EmailActivationRoute { get; }

        public abstract string PasswordResetRoute { get; }

        protected readonly IWebUrlService WebUrlService;
        protected readonly ITenantCache TenantCache;

        protected AppUrlServiceBase(IWebUrlService webUrlService, ITenantCache tenantCache)
        {
            WebUrlService = webUrlService;
            TenantCache = tenantCache;
        }

        public string CreateEmailActivationUrlFormat(int? tenantId)
        {
            return CreateEmailActivationUrlFormat(GetTenancyName(tenantId));
        }

        public string CreatePasswordResetUrlFormat(int? tenantId)
        {
            return CreatePasswordResetUrlFormat(GetTenancyName(tenantId));
        }

        public string CreateSharedRegistrationUrlFormat(string tenancyName)
        {
            return WebUrlService.GetSiteRootAddress(tenancyName).EnsureEndsWith('/') + "account/share-access";
        }

        public string CreateEmailActivationUrlFormat(string tenancyName)
        {
            var activationLink = WebUrlService.GetSiteRootAddress(tenancyName).EnsureEndsWith('/') +
                                 EmailActivationRoute + "?userId={userId}&confirmationCode={confirmationCode}";

            if (tenancyName != null)
            {
                activationLink = activationLink + "&tenantId={tenantId}";
            }

            return activationLink;
        }

        public string CreatePasswordResetUrlFormat(string tenancyName)
        {
            var resetLink = WebUrlService.GetSiteRootAddress(tenancyName).EnsureEndsWith('/') + PasswordResetRoute +
                            $"?userId={{userId}}&resetCode={{resetCode}}&expireDate={{expireDate}}";

            if (tenancyName != null)
            {
                resetLink += "&tenantId={tenantId}";
            }

            return resetLink;
        }

        public string CreateFacilityURL(string tenancyName)
        {
            var freeTrialLink = WebUrlService.GetSiteRootAddress(tenancyName).EnsureEndsWith('/');

            return freeTrialLink;
        }

        private string GetTenancyName(int? tenantId)
        {
            return tenantId.HasValue ? TenantCache.Get(tenantId.Value).TenancyName : null;
        }
    }
}