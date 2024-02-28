using System;
using System.Collections.Concurrent;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using Abp.Dependency;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.MultiTenancy;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using Plateaumed.EHR.Configuration;
using Plateaumed.EHR.Url;

namespace Plateaumed.EHR.Net.Emailing
{
    public class EmailTemplateProvider : IEmailTemplateProvider, ISingletonDependency
    {
        private static readonly string _emailTemplateDefaultURI = "Plateaumed.EHR.Net.Emailing.EmailTemplates.default.html";
        private static readonly string _placeHolderThisYear = "{THIS_YEAR}";
        private static readonly string _placeHolderHostLogoURL = "{HOST_LOGO_URL}";
        private static readonly string _placeHolderHostBannerURL = "{HOST_BANNER_URL}";
        private static readonly string _placeHolderHostName = "{HOST_NAME}";
        private static readonly string _placeHolderHostAddress = "{HOST_ADDRESS}";
        private static readonly string _placeHolderHostTechnicalSupportEmail = "{HOST_TECHNICAL_SUPPORT_EMAIL}";
        private static readonly string _placeHolderHostURLHttps = "{HOST_URL_HTTPS}";
        private static readonly string _placeHolderHostWebsite = "{HOST_WEBSITE}";

        private readonly IWebUrlService _webUrlService;
        private readonly ITenantCache _tenantCache;
        private readonly ConcurrentDictionary<string, string> _emailTemplate;
        readonly IConfigurationRoot _appConfiguration;

        public EmailTemplateProvider(IWebUrlService webUrlService, ITenantCache tenantCache, IAppConfigurationAccessor appConfigurationAccessor)
        {
            _webUrlService = webUrlService;
            _tenantCache = tenantCache;
            _emailTemplate = new ConcurrentDictionary<string, string>();
            _appConfiguration = appConfigurationAccessor.Configuration;
        }

        public string GetDefaultTemplate(int? tenantId)
        {
            var tenancyKey = tenantId.HasValue ? tenantId.Value.ToString() : "host";

            return _emailTemplate.GetOrAdd(tenancyKey, key =>
            {
                using var stream = typeof(EmailTemplateProvider).GetAssembly().GetManifestResourceStream(_emailTemplateDefaultURI);
                var bytes = stream.GetAllBytes();
                var template = Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);

                template = template.Replace(_placeHolderThisYear, DateTime.Now.Year.ToString());
                template = template.Replace(_placeHolderHostLogoURL, GetHostResourceUrl("app-email-logo"));
                template = template.Replace(_placeHolderHostBannerURL, GetHostResourceUrl("app-email-banner"));
                template = template.Replace(_placeHolderHostName, _appConfiguration.GetValue<string>("HostInformation:Name"));
                template = template.Replace(_placeHolderHostAddress, _appConfiguration.GetValue<string>("HostInformation:Location"));
                template = template.Replace(_placeHolderHostTechnicalSupportEmail, _appConfiguration.GetValue<string>("HostInformation:ContactSalesEmail"));
                template = template.Replace(_placeHolderHostURLHttps, "https://" + _appConfiguration.GetValue<string>("HostInformation:Website"));
                template = template.Replace(_placeHolderHostWebsite, _appConfiguration.GetValue<string>("HostInformation:Website"));

                return template;
            });
        }

        private string GetHostResourceUrl(string filename, string extension = "png")
        {
            return _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + "Common/Images/" + filename + '.' + extension;
        }

        // TODO: Reuse after tenant logo implementation
        private string GetTenantLogoUrl(int? tenantId, string extension = "png")
        {
            if (!tenantId.HasValue)
            {
                return _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + "Common/Images/app-email-logo." + extension;
            }

            var tenant = _tenantCache.Get(tenantId.Value);
            return _webUrlService.GetServerRootAddress(tenant.TenancyName).EnsureEndsWith('/') + "TenantCustomization/GetTenantLogo/light/" + tenantId.Value + "/logo" + "." + extension;
        }
    }
}