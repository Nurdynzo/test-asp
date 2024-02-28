using Abp.Application.Features;
using Abp.Application.Services.Dto;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using Abp.Zero.Configuration;
using Plateaumed.EHR.Configuration;
using Plateaumed.EHR.Editions;
using Plateaumed.EHR.Editions.Dto;
using Plateaumed.EHR.Features;
using Plateaumed.EHR.MultiTenancy.Dto;
using Plateaumed.EHR.Notifications;
using Plateaumed.EHR.Security.Recaptcha;
using Plateaumed.EHR.Url;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.MultiTenancy.Payments;
using Abp.Auditing;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Plateaumed.EHR.Misc.Country;
using Abp.Domain.Repositories;
using Plateaumed.EHR.MultiTenancy.Abstractions;
using Plateaumed.EHR.MultiTenancy.Handlers;

namespace Plateaumed.EHR.MultiTenancy
{
    public class TenantRegistrationAppService : EHRAppServiceBase, ITenantRegistrationAppService
    {
        public IAppUrlService AppUrlService { get; set; }

        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IRecaptchaValidator _recaptchaValidator;
        private readonly EditionManager _editionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly ILocalizationContext _localizationContext;
        private readonly TenantManager _tenantManager;
        private readonly ISubscriptionPaymentRepository _subscriptionPaymentRepository;
        private readonly IClientInfoProvider _clientInfoProvider;
        private readonly IAppConfigurationAccessor _configurationAccessor;
        private readonly IRepository<Country> _countryRepository;
        private readonly IUpdateTenantOnboardingProgressCommandHandler _updateTenantOnboardingProgressCommandHandler;


        public TenantRegistrationAppService(
            IMultiTenancyConfig multiTenancyConfig,
            IRecaptchaValidator recaptchaValidator,
            EditionManager editionManager,
            IAppNotifier appNotifier,
            ILocalizationContext localizationContext,
            TenantManager tenantManager,
            ISubscriptionPaymentRepository subscriptionPaymentRepository,
            IClientInfoProvider clientInfoProvider,
            IAppConfigurationAccessor configurationAccessor,
            IRepository<Country> countryRepository,
            IUpdateTenantOnboardingProgressCommandHandler updateTenantOnboardingProgressCommandHandler)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _recaptchaValidator = recaptchaValidator;
            _editionManager = editionManager;
            _appNotifier = appNotifier;
            _localizationContext = localizationContext;
            _tenantManager = tenantManager;
            _subscriptionPaymentRepository = subscriptionPaymentRepository;
            _clientInfoProvider = clientInfoProvider;
            _configurationAccessor = configurationAccessor;
            _countryRepository = countryRepository;
            _updateTenantOnboardingProgressCommandHandler = updateTenantOnboardingProgressCommandHandler;

            AppUrlService = NullAppUrlService.Instance;
        }

        public async Task<SuggestTenantCountryByIpOutput> SuggestTenantCountryByIp()
        {
            var suggestion = new SuggestTenantCountryByIpOutput();
            var ipStackBaseUrl = _configurationAccessor.Configuration["IpStack:BaseUrl"];
            var ipStackApiAccesskey = _configurationAccessor.Configuration["IpStack:ApiAccessKey"];
            var requestUri = $"{ipStackBaseUrl}{_clientInfoProvider.ClientIpAddress}?access_key={ipStackApiAccesskey}";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    using (HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUri))
                    {
                        if (httpResponse != null && httpResponse.IsSuccessStatusCode)
                        {
                            var apiResponse = await httpResponse.Content.ReadAsStringAsync();
                            var jObject = JObject.Parse(apiResponse);
                            var countryCode = jObject.SelectToken("country_code").ToString();

                            if (string.IsNullOrWhiteSpace(countryCode)) return null;

                            var country = await _countryRepository.FirstOrDefaultAsync(c => c.Code == suggestion.Code);

                            if (country == null) return null;

                            suggestion.Id = country.Id;
                            suggestion.Name = country.Name;
                            suggestion.Code = country.Code;

                            return suggestion;
                        }
                    }
                }
                catch (HttpRequestException)
                {
                    return null;
                }
            }

            return suggestion;
        }

        public async Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input)
        {
            if (!input.SubscriptionStartType.HasValue)
            {
                input.SubscriptionStartType = SubscriptionStartType.Free;
            }

            if (input.EditionId.HasValue)
            {
                await CheckEditionSubscriptionAsync(input.EditionId.Value, input.SubscriptionStartType.Value);
            }

            using (CurrentUnitOfWork.SetTenantId(null))
            {
                CheckTenantRegistrationIsEnabled();

                if (UseCaptchaOnRegistration())
                {
                    await _recaptchaValidator.ValidateAsync(input.CaptchaResponse);
                }

                //Getting host-specific settings
                var isActive = await IsNewRegisteredTenantActiveByDefault(input.SubscriptionStartType.Value);
                var isEmailConfirmationRequired = await SettingManager.GetSettingValueForApplicationAsync<bool>(
                    AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin
                );

                DateTime? subscriptionEndDate = null;
                var isInTrialPeriod = false;

                if (input.EditionId.HasValue)
                {
                    isInTrialPeriod = input.SubscriptionStartType == SubscriptionStartType.Trial;

                    if (isInTrialPeriod)
                    {
                        var edition = (SubscribableEdition)await _editionManager.GetByIdAsync(input.EditionId.Value);
                        subscriptionEndDate = Clock.Now.AddDays(edition.TrialDayCount ?? 0);
                    }
                }

                var tenantId = await _tenantManager.CreateWithAdminUserAsync(
                    input.TenancyName,
                    input.Name,
                    input.AdminPassword,
                    input.AdminEmailAddress,
                    null,
                    isActive,
                    input.EditionId,
                    input.CountryId,
                    shouldChangePasswordOnNextLogin: false,
                    sendActivationEmail: true,
                    subscriptionEndDate,
                    isInTrialPeriod,
                    AppUrlService.CreateEmailActivationUrlFormat(input.TenancyName),
                    category: input.Category,
                    type: input.Type,
                    hasSignedAgreement: input.HasSignedAgreement,
                    adminName: input.AdminName,
                    adminSurname: input.AdminSurname,
                    individualSpecialization: input.IndividualSpecialization,
                    individualGraduatingSchool: input.IndividualGraduatingSchool,
                    individualGraduatingYear: input.IndividualGraduatingYear,
                    individualDocumentToken: input.IndividualDocumentToken,
                    individualDocumentType: input.IndividualDocumentType,
                    facilityGroupName: input.FacilityGroupName
                );

                var tenant = await TenantManager.GetByIdAsync(tenantId);
                await _appNotifier.NewTenantRegisteredAsync(tenant);

                return new RegisterTenantOutput
                {
                    TenantId = tenant.Id,
                    TenancyName = tenant.TenancyName,
                    Category = tenant.Category,
                    Type = tenant.Type,
                    Name = tenant.Name,
                    UserName = input.AdminEmailAddress,
                    EmailAddress = input.AdminEmailAddress,
                    IsEmailConfirmationRequired = isEmailConfirmationRequired,
                    IsTenantActive = tenant.IsActive,
                    HasSignedAgreement = tenant.HasSignedAgreement
                };
            }
        }

        private async Task<bool> IsNewRegisteredTenantActiveByDefault(SubscriptionStartType subscriptionStartType)
        {
            if (subscriptionStartType == SubscriptionStartType.Paid)
            {
                return false;
            }

            return await SettingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.TenantManagement
                .IsNewRegisteredTenantActiveByDefault);
        }

        private async Task CheckRegistrationWithoutEdition()
        {
            var editions = await _editionManager.GetAllAsync();
            if (editions.Any())
            {
                throw new Exception(
                    "Tenant registration is not allowed without edition because there are editions defined !");
            }
        }

        public async Task<GetTenantOnboardingProgressOutput> GetTenantOnboardingProgress()
        {
            var tenant = await GetCurrentTenantAsync();

            return tenant == null ? null : ObjectMapper.Map<GetTenantOnboardingProgressOutput>(tenant);
        }

        public async Task UpdateTenantOnboardingProgress(UpdateTenantOnboardingProgressInput input)
        {
            await _updateTenantOnboardingProgressCommandHandler.Handle(input);
        }

        public async Task<EditionsSelectOutput> GetEditionsForSelect()
        {
            var features = FeatureManager
                .GetAll()
                .Where(feature =>
                    (feature[FeatureMetadata.CustomFeatureKey] as FeatureMetadata)?.IsVisibleOnPricingTable ?? false);

            var flatFeatures = ObjectMapper
                .Map<List<FlatFeatureSelectDto>>(features)
                .OrderBy(f => f.DisplayName)
                .ToList();

            var editions = (await _editionManager.GetAllAsync())
                .Cast<SubscribableEdition>()
                .OrderBy(e => e.MonthlyPrice)
                .ToList();

            var featureDictionary = features.ToDictionary(feature => feature.Name, f => f);

            var editionWithFeatures = new List<EditionWithFeaturesDto>();
            foreach (var edition in editions)
            {
                editionWithFeatures.Add(await CreateEditionWithFeaturesDto(edition, featureDictionary));
            }

            if (AbpSession.UserId.HasValue)
            {
                var currentEditionId = (await _tenantManager.GetByIdAsync(AbpSession.GetTenantId()))
                    .EditionId;

                if (currentEditionId.HasValue)
                {
                    editionWithFeatures = editionWithFeatures.Where(e => e.Edition.Id != currentEditionId).ToList();

                    var currentEdition =
                        (SubscribableEdition)(await _editionManager.GetByIdAsync(currentEditionId.Value));
                    if (!currentEdition.IsFree)
                    {
                        var lastPayment = await _subscriptionPaymentRepository.GetLastCompletedPaymentOrDefaultAsync(
                            AbpSession.GetTenantId(),
                            null,
                            null);

                        if (lastPayment != null)
                        {
                            editionWithFeatures = editionWithFeatures
                                .Where(e =>
                                    e.Edition.GetPaymentAmount(lastPayment.PaymentPeriodType) >
                                    currentEdition.GetPaymentAmount(lastPayment.PaymentPeriodType)
                                )
                                .ToList();
                        }
                    }
                }
            }

            return new EditionsSelectOutput
            {
                AllFeatures = flatFeatures,
                EditionsWithFeatures = editionWithFeatures,
            };
        }

        public async Task<EditionSelectDto> GetEdition(int editionId)
        {
            var edition = await _editionManager.GetByIdAsync(editionId);
            var editionDto = ObjectMapper.Map<EditionSelectDto>(edition);

            return editionDto;
        }

        private async Task<EditionWithFeaturesDto> CreateEditionWithFeaturesDto(SubscribableEdition edition,
            Dictionary<string, Feature> featureDictionary)
        {
            return new EditionWithFeaturesDto
            {
                Edition = ObjectMapper.Map<EditionSelectDto>(edition),
                FeatureValues = (await _editionManager.GetFeatureValuesAsync(edition.Id))
                    .Where(featureValue => featureDictionary.ContainsKey(featureValue.Name))
                    .Select(fv => new NameValueDto(
                        fv.Name,
                        featureDictionary[fv.Name].GetValueText(fv.Value, _localizationContext))
                    )
                    .ToList()
            };
        }

        private void CheckTenantRegistrationIsEnabled()
        {
            if (!IsSelfRegistrationEnabled())
            {
                throw new UserFriendlyException(L("SelfTenantRegistrationIsDisabledMessage_Detail"));
            }

            if (!_multiTenancyConfig.IsEnabled)
            {
                throw new UserFriendlyException(L("MultiTenancyIsNotEnabled"));
            }
        }

        private bool IsSelfRegistrationEnabled()
        {
            return SettingManager.GetSettingValueForApplication<bool>(
                AppSettings.TenantManagement.AllowSelfRegistration);
        }

        private bool UseCaptchaOnRegistration()
        {
            return SettingManager.GetSettingValueForApplication<bool>(AppSettings.TenantManagement
                .UseCaptchaOnRegistration);
        }

        private async Task CheckEditionSubscriptionAsync(int editionId, SubscriptionStartType subscriptionStartType)
        {
            var edition = await _editionManager.GetByIdAsync(editionId) as SubscribableEdition;

            CheckSubscriptionStart(edition, subscriptionStartType);
        }

        private static void CheckSubscriptionStart(SubscribableEdition edition,
            SubscriptionStartType subscriptionStartType)
        {
            switch (subscriptionStartType)
            {
                case SubscriptionStartType.Free:
                    if (!edition.IsFree)
                    {
                        throw new Exception("This is not a free edition !");
                    }

                    break;
                case SubscriptionStartType.Trial:
                    if (!edition.HasTrial())
                    {
                        throw new Exception("Trial is not available for this edition !");
                    }

                    break;
                case SubscriptionStartType.Paid:
                    if (edition.IsFree)
                    {
                        throw new Exception("This is a free edition and cannot be subscribed as paid !");
                    }

                    break;
            }
        }
    }
}