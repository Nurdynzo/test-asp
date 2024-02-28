using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp;
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Editions;
using Plateaumed.EHR.MultiTenancy.Demo;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Runtime.Security;
using Microsoft.AspNetCore.Identity;
using Plateaumed.EHR.Notifications;
using System;
using System.Diagnostics;
using Abp.BackgroundJobs;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.MultiTenancy.Payments;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Storage;
using Microsoft.EntityFrameworkCore;
using Abp.Threading;
using Abp.Authorization.Users;
using Plateaumed.EHR.BedTypes;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.MultiTenancy
{
    /// <summary>
    /// Tenant manager.
    /// </summary>
    public class TenantManager : AbpTenantManager<Tenant, User>, ITenantManager
    {
        public IAbpSession AbpSession { get; set; }

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IUserEmailer _userEmailer;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<SubscribableEdition> _subscribableEditionRepository;
        private readonly IRepository<FacilityGroup, long> _facilityGroupRepository;
        protected readonly IRepository<TenantDocument, long> _tenantDocumentRepository;
        protected readonly IBackgroundJobManager _backgroundJobManager;
        protected readonly ITempFileCacheManager _tempFileCacheManager;
        protected readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<BedType, long> _bedTypeRepository;
        private IRepository<JobTitle, long> _jobTitleRepository;

        public TenantManager(
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            EditionManager editionManager,
            IUnitOfWorkManager unitOfWorkManager,
            RoleManager roleManager,
            IUserEmailer userEmailer,
            UserManager userManager,
            INotificationSubscriptionManager notificationSubscriptionManager,
            IAppNotifier appNotifier,
            IAbpZeroFeatureValueStore featureValueStore,
            IAbpZeroDbMigrator abpZeroDbMigrator,
            IPasswordHasher<User> passwordHasher,
            IRepository<SubscribableEdition> subscribableEditionRepository,
            IRepository<FacilityGroup, long> facilityGroupRepository,
            IRepository<TenantDocument, long> tenantDocumentRepository,
            IRepository<BedType, long> bedTypeRepository,
            IBackgroundJobManager backgroundJobManager,
            ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager,
            IRepository<JobTitle, long> jobTitleRepository,
            IRepository<JobLevel, long> jobLevelRepository) : base(
            tenantRepository,
            tenantFeatureRepository,
            editionManager,
            featureValueStore
        )
        {
            AbpSession = NullAbpSession.Instance;

            _unitOfWorkManager = unitOfWorkManager;
            _roleManager = roleManager;
            _userEmailer = userEmailer;
            _userManager = userManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _appNotifier = appNotifier;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _passwordHasher = passwordHasher;
            _subscribableEditionRepository = subscribableEditionRepository;
            _facilityGroupRepository = facilityGroupRepository;
            _tenantDocumentRepository = tenantDocumentRepository;
            _bedTypeRepository = bedTypeRepository;
            _backgroundJobManager = backgroundJobManager;
            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _jobTitleRepository = jobTitleRepository;
        }

        public async Task<int> CreateWithAdminUserAsync(
            string tenancyName,
            string name,
            string adminPassword,
            string adminEmailAddress,
            string connectionString,
            bool isActive,
            int? editionId,
            int? countryId,
            bool shouldChangePasswordOnNextLogin,
            bool sendActivationEmail,
            DateTime? subscriptionEndDate,
            bool isInTrialPeriod,
            string emailActivationLink,
            string facilityGroupName,
            TenantCategoryType category = TenantCategoryType.Private,
            TenantType type = TenantType.Business,
            bool hasSignedAgreement = false,
            string adminName = null,
            string adminSurname = null,
            string individualSpecialization = null,
            string individualGraduatingSchool = null,
            string individualGraduatingYear = null,
            string individualDocumentToken = null,
            TenantDocumentType? individualDocumentType = null
        )
        {
            int newTenantId;
            long newAdminId;

            await CheckEditionAsync(editionId, isInTrialPeriod);

            if (isInTrialPeriod && !subscriptionEndDate.HasValue)
            {
                throw new UserFriendlyException(LocalizationManager.GetString(
                    EHRConsts.LocalizationSourceName, "TrialWithoutEndDateErrorMessage"));
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                //Create tenant
                var tenant = new Tenant(tenancyName, name, type)
                {
                    Category = category,
                    Type = type,
                    IsActive = isActive,
                    CountryId = countryId,
                    EditionId = editionId,
                    SubscriptionEndDateUtc = subscriptionEndDate?.ToUniversalTime(),
                    IsInTrialPeriod = isInTrialPeriod,
                    ConnectionString = connectionString.IsNullOrWhiteSpace()
                        ? null
                        : SimpleStringCipher.Instance.Encrypt(connectionString)
                };

                if (type == TenantType.Individual)
                {
                    tenant.IndividualGraduatingSchool = individualGraduatingSchool;
                    tenant.IndividualGraduatingYear = individualGraduatingYear;
                    tenant.IndividualSpecialization = individualSpecialization;
                }

                await CreateAsync(tenant);
                await _unitOfWorkManager.Current.SaveChangesAsync(); //To get new tenant's id.

                //Create Individual Tenant Document
                if (tenant.Type == TenantType.Individual)
                {
                    if (individualDocumentType.HasValue && !string.IsNullOrWhiteSpace(individualDocumentToken))
                    {
                        await CreateTenantDocumentAsync(tenant.Id, (TenantDocumentType)individualDocumentType, individualDocumentToken);
                    }
                    else
                    {
                        throw new UserFriendlyException("There is no supporting document for this individual registration");
                    }
                }

                //Create tenant database
                _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

                //We are working entities of new tenant, so changing tenant filter
                using (_unitOfWorkManager.Current.SetTenantId(tenant.Id))
                {
                    //Create Tenant's Facility Group
                    var facilityGroup = new FacilityGroup()
                    {
                        Name = string.IsNullOrEmpty(facilityGroupName) ? FacilityGroupConsts.DefaultName : facilityGroupName
                    };

                    await _facilityGroupRepository.InsertAsync(facilityGroup);
                    await _unitOfWorkManager.Current.SaveChangesAsync();

                    CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get static role ids

                    //grant all permissions to admin role
                    var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                    await _roleManager.GrantAllPermissionsAsync(adminRole);

                    //User role should be default
                    var userRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.User);
                    userRole.IsDefault = true;
                    CheckErrors(await _roleManager.UpdateAsync(userRole));

                    //Create admin user for the tenant
                    var adminUser = User.CreateTenantAdminUser(tenant.Id, AbpUserBase.AdminUserName, adminEmailAddress, adminName, adminSurname);
                    adminUser.ShouldChangePasswordOnNextLogin = shouldChangePasswordOnNextLogin;
                    adminUser.IsActive = true;

                    if (adminPassword.IsNullOrEmpty())
                    {
                        adminPassword = await _userManager.CreateRandomPassword();
                    }
                    else
                    {
                        await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
                        foreach (var validator in _userManager.PasswordValidators)
                        {
                            CheckErrors(await validator.ValidateAsync(_userManager, adminUser, adminPassword));
                        }
                    }

                    adminUser.Password = _passwordHasher.HashPassword(adminUser, adminPassword);

                    CheckErrors(await _userManager.CreateAsync(adminUser));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get admin user's id                    

                    //Assign admin user to admin role!
                    CheckErrors(await _userManager.AddToRoleAsync(adminUser, adminRole.Name));

                    //Notifications
                    await _appNotifier.WelcomeToTheApplicationAsync(adminUser);

                    //Send activation email
                    if (sendActivationEmail)
                    {
                        adminUser.SetNewEmailConfirmationCode();
                        await _userEmailer.SendEmailActivationLinkAsync(adminUser, emailActivationLink, adminPassword);
                    }

                    await _unitOfWorkManager.Current.SaveChangesAsync();

                    await _backgroundJobManager.EnqueueAsync<TenantDemoDataBuilderJob, int>(tenant.Id);

                    newTenantId = tenant.Id;
                    newAdminId = adminUser.Id;
                }

                await uow.CompleteAsync();
            }

            //Used a second UOW since UOW above sets some permissions and _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync needs these permissions to be saved.
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                {
                    await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(
                        new UserIdentifier(newTenantId, newAdminId));
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                    await uow.CompleteAsync();
                }
            }

            return newTenantId;
        }

        public override async Task<Tenant> GetByIdAsync(int id)
        {
            return await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                return await TenantRepository.GetAll()
                .Include(t => t.CountryFk)
                .Include(t => t.OnboardingProgress)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
            });
        }

        public override Tenant GetById(int id)
        {
            return AsyncHelper.RunSync(() => GetByIdAsync(id));
        }

        private async Task CreateTenantDocumentAsync(int id, TenantDocumentType individualDocumentType, string individualDocumentToken)
        {
            try
            {

                var fileToken = individualDocumentToken;

                if (fileToken.IsNullOrWhiteSpace())
                {
                    return;
                }

                var fileCache = _tempFileCacheManager.GetFileInfo(fileToken);

                if (fileCache == null)
                {
                    throw new UserFriendlyException(
                        "There is no such file with the token: " + fileToken
                    );
                }

                var storedFile = new BinaryObject(
                    AbpSession.TenantId,
                    fileCache.File,
                    fileCache.FileName
                );

                await _binaryObjectManager.SaveAsync(storedFile);

                var tenantDocument = new TenantDocument() { TenantId = storedFile.TenantId, Type = individualDocumentType, Document = storedFile.Id, FileName = storedFile.Description };

                await _tenantDocumentRepository.InsertAsync(tenantDocument);
            }
            catch (Exception)
            {
                throw new UserFriendlyException("Failed to save submitted registration document(s)");
            }
        }

        public async Task CheckEditionAsync(int? editionId, bool isInTrialPeriod)
        {
            await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                if (!editionId.HasValue || !isInTrialPeriod)
                {
                    return;
                }

                var edition = await _subscribableEditionRepository.GetAsync(editionId.Value);
                if (!edition.IsFree)
                {
                    return;
                }

                var error = LocalizationManager.GetSource(EHRConsts.LocalizationSourceName)
                    .GetString("FreeEditionsCannotHaveTrialVersions");
                throw new UserFriendlyException(error);
            });
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public decimal GetUpgradePrice(SubscribableEdition currentEdition, SubscribableEdition targetEdition,
            int totalRemainingHourCount, PaymentPeriodType paymentPeriodType)
        {
            int numberOfHoursPerDay = 24;

            var totalRemainingDayCount = totalRemainingHourCount / numberOfHoursPerDay;
            var unusedPeriodCount = totalRemainingDayCount / (int)paymentPeriodType;
            var unusedHoursCount = totalRemainingHourCount % ((int)paymentPeriodType * numberOfHoursPerDay);

            decimal currentEditionPriceForUnusedPeriod = 0;
            decimal targetEditionPriceForUnusedPeriod = 0;

            var currentEditionPrice = currentEdition.GetPaymentAmount(paymentPeriodType);
            var targetEditionPrice = targetEdition.GetPaymentAmount(paymentPeriodType);

            if (currentEditionPrice > 0)
            {
                currentEditionPriceForUnusedPeriod = currentEditionPrice * unusedPeriodCount;
                currentEditionPriceForUnusedPeriod += (currentEditionPrice / (int)paymentPeriodType) /
                    numberOfHoursPerDay * unusedHoursCount;
            }

            if (targetEditionPrice > 0)
            {
                targetEditionPriceForUnusedPeriod = targetEditionPrice * unusedPeriodCount;
                targetEditionPriceForUnusedPeriod += (targetEditionPrice / (int)paymentPeriodType) /
                    numberOfHoursPerDay * unusedHoursCount;
            }

            return targetEditionPriceForUnusedPeriod - currentEditionPriceForUnusedPeriod;
        }

        public async Task<Tenant> UpdateTenantAsync(
            int tenantId,
            bool isActive,
            bool? isInTrialPeriod,
            PaymentPeriodType? paymentPeriodType,
            int editionId,
            EditionPaymentType editionPaymentType)
        {
            var tenant = await FindByIdAsync(tenantId);

            tenant.IsActive = isActive;
            tenant.EditionId = editionId;

            if (isInTrialPeriod.HasValue)
            {
                tenant.IsInTrialPeriod = isInTrialPeriod.Value;
            }

            if (paymentPeriodType.HasValue)
            {
                tenant.UpdateSubscriptionDateForPayment(paymentPeriodType.Value, editionPaymentType);
            }

            return tenant;
        }

        public async Task<EndSubscriptionResult> EndSubscriptionAsync(Tenant tenant, SubscribableEdition edition,
            DateTime nowUtc)
        {
            if (tenant.EditionId == null || tenant.HasUnlimitedTimeSubscription())
            {
                throw new Exception(
                    $"Can not end tenant {tenant.TenancyName} subscription for {edition.DisplayName} tenant has unlimited time subscription!");
            }

            Debug.Assert(tenant.SubscriptionEndDateUtc != null, "tenant.SubscriptionEndDateUtc != null");

            var subscriptionEndDateUtc = tenant.SubscriptionEndDateUtc.Value;
            if (!tenant.IsInTrialPeriod)
            {
                subscriptionEndDateUtc =
                    tenant.SubscriptionEndDateUtc.Value.AddDays(edition.WaitingDayAfterExpire ?? 0);
            }

            if (subscriptionEndDateUtc >= nowUtc)
            {
                throw new Exception(
                    $"Can not end tenant {tenant.TenancyName} subscription for {edition.DisplayName} since subscription has not expired yet!");
            }

            if (!tenant.IsInTrialPeriod && edition.ExpiringEditionId.HasValue)
            {
                tenant.EditionId = edition.ExpiringEditionId.Value;
                tenant.SubscriptionEndDateUtc = null;

                await UpdateAsync(tenant);

                return EndSubscriptionResult.AssignedToAnotherEdition;
            }

            tenant.IsActive = false;
            tenant.IsInTrialPeriod = false;

            await UpdateAsync(tenant);

            return EndSubscriptionResult.TenantSetInActive;
        }

        public override Task UpdateAsync(Tenant tenant)
        {
            if (tenant.IsInTrialPeriod && !tenant.SubscriptionEndDateUtc.HasValue)
            {
                throw new UserFriendlyException(LocalizationManager.GetString(
                    EHRConsts.LocalizationSourceName, "TrialWithoutEndDateErrorMessage"));
            }

            return base.UpdateAsync(tenant);
        }
    }
}