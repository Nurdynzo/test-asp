using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Timing;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Editions;
using Plateaumed.EHR.MultiTenancy;
using Plateaumed.EHR.MultiTenancy.Dto;
using Plateaumed.EHR.MultiTenancy.Payments;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.MultiTenancy
{
    // ReSharper disable once InconsistentNaming
    [Trait("Category", "Integration")]
    public class TenantRegistrationAppService_Tests : AppTestBase
    {
        private readonly ITenantRegistrationAppService _tenantRegistrationAppService;
        
        public TenantRegistrationAppService_Tests()
        {
            _tenantRegistrationAppService = Resolve<ITenantRegistrationAppService>();
        }

        [MultiTenantFact(Skip = "TODO: Fix this test.")]
        public async Task SubscriptionEndDateUtc_ShouldBe_Null_After_Free_Registration()
        {
            //Arrange
            var edition = new SubscribableEdition
            {
                DisplayName = "Free Edition"
            };

            await UsingDbContextAsync(async context =>
            {
                context.SubscribableEditions.Add(edition);
                await context.SaveChangesAsync();
            });

            //Act
            var registerResult = await _tenantRegistrationAppService.RegisterTenant(new RegisterTenantInput
            {
                EditionId = edition.Id,
                AdminEmailAddress = "admin@volosoft.com",
                AdminPassword = "123qwe",
                Name = "Volosoft",
                SubscriptionStartType = SubscriptionStartType.Free,
                TenancyName = "Volosoft"
            });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var tenant = await context.Tenants.FirstOrDefaultAsync(t => t.Id == registerResult.TenantId);
                tenant.ShouldNotBe(null);
                tenant.SubscriptionEndDateUtc.HasValue.ShouldBe(false);
            });
        }

        [MultiTenantFact]
        public async Task Cannot_Register_To_Free_Edition_As_Trial()
        {
            //Arrange
            var edition = new SubscribableEdition
            {
                DisplayName = "Free Edition"
            };

            await UsingDbContextAsync(async context =>
            {
                context.SubscribableEditions.Add(edition);
                await context.SaveChangesAsync();
            });

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _tenantRegistrationAppService.RegisterTenant(new RegisterTenantInput
            {
                EditionId = edition.Id,
                AdminEmailAddress = "admin@volosoft.com",
                AdminPassword = "123qwe",
                Name = "Volosoft",
                SubscriptionStartType = SubscriptionStartType.Trial,
                TenancyName = "Volosoft"
            }));

            exception.Message.ShouldBe("Trial is not available for this edition !");
        }

        [MultiTenantFact(Skip = "Todo: Fix this test.")]
        public async Task Should_Subscribe_To_Edition_As_Trial_If_Trial_Is_Available()
        {
            //Arrange
            var utcNow = Clock.Now.ToUniversalTime();
            var trialDayCount = 10;
            var edition = new SubscribableEdition
            {
                DisplayName = "Standard Edition",
                TrialDayCount = trialDayCount,
                MonthlyPrice = 9,
                AnnualPrice = 99
            };

            await UsingDbContextAsync(async context =>
            {
                context.SubscribableEditions.Add(edition);
                await context.SaveChangesAsync();
            });

            var result = await _tenantRegistrationAppService.RegisterTenant(new RegisterTenantInput
            {
                EditionId = edition.Id,
                AdminEmailAddress = "admin@volosoft.com",
                AdminPassword = "123qwe",
                Name = "Volosoft",
                SubscriptionStartType = SubscriptionStartType.Trial,
                TenancyName = "Volosoft"
            });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var tenant = await context.Tenants.FirstOrDefaultAsync(t => t.Id == result.TenantId);
                tenant.ShouldNotBe(null);
                tenant.SubscriptionEndDateUtc.ShouldNotBe(null);
                tenant.SubscriptionEndDateUtc?.Date.ShouldBe(utcNow.Date.AddDays(trialDayCount));
            });
        }
        
        [MultiTenantFact]
        public async Task GetTenantOnboardingProgress_ShouldReturnCurrentProgress()
        {
            //Arrange
            var (tenant, user) = UsingDbContext(context =>
            {
                var tenant = TestTenantBuilder.Create().Save(context);
                var user = TestUserBuilder.Create(tenant.Id).Save(context);
                return (tenant, user);
            });
            LoginAsTenant(tenant.TenancyName, user.UserName);
            AddOnboardingStatus(tenant, TenantOnboardingStatus.Clinics);

            //Act
            var result = await _tenantRegistrationAppService.GetTenantOnboardingProgress();

            //Assert
            result.OnboardingProgress.Count.ShouldBe(1);
            result.OnboardingProgress[0].TenantOnboardingStatus.ShouldBe(TenantOnboardingStatus.Clinics);
            result.OnboardingProgress[0].Completed.ShouldBeTrue();
        }

        [MultiTenantFact]
        public async Task UpdateTenantOnboardingProgress_GivenValidTenant_ShouldUpdateProgress()
        {
            //Arrange
            var (tenant, user) = UsingDbContext(context =>
            {
                var tenant = TestTenantBuilder.Create().WithOnboardingStatus((TenantOnboardingStatus.Clinics, true)).Save(context);
                var user = TestUserBuilder.Create(tenant.Id).Save(context);
                return (tenant, user);
            });
            LoginAsTenant(tenant.TenancyName, user.UserName);
            AddOnboardingStatus(tenant, TenantOnboardingStatus.Clinics);

            var input = new UpdateTenantOnboardingProgressInput
            {
                OnboardingProgress = new List<TenantOnboardingProgressDto>
                {
                    new() { TenantOnboardingStatus = TenantOnboardingStatus.Departments, Completed = true }
                }
            };

            //Act
            await _tenantRegistrationAppService.UpdateTenantOnboardingProgress(input);
            var savedTenant = UsingDbContext(context => context.Tenants.Include(t => t.OnboardingProgress).FirstOrDefault(t => t.Id == tenant.Id));

            //Assert
            savedTenant.OnboardingProgress.Count.ShouldBe(2);
            savedTenant.OnboardingProgress.First().TenantOnboardingStatus.ShouldBe(TenantOnboardingStatus.Clinics);
            savedTenant.OnboardingProgress.First().Completed.ShouldBe(true);
            savedTenant.OnboardingProgress.Last().TenantOnboardingStatus.ShouldBe(TenantOnboardingStatus.Departments);
            savedTenant.OnboardingProgress.Last().Completed.ShouldBe(true);
        }

        private void AddOnboardingStatus(Tenant tenant, TenantOnboardingStatus tenantOnboardingStatus)
        {
            UsingDbContext(context =>
            {
                var t = context.Tenants.Include(t => t.OnboardingProgress).First(t => t.Id == tenant.Id);
                t.OnboardingProgress.Add(new TenantOnboardingProgress
                    { TenantOnboardingStatus = tenantOnboardingStatus, Completed = true });
                context.SaveChanges();
            });
        }
    }
}
