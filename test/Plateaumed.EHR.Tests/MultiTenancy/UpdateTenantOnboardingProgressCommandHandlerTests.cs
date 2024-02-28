using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.MultiTenancy;
using Plateaumed.EHR.MultiTenancy.Dto;
using Plateaumed.EHR.MultiTenancy.Handlers;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.MultiTenancy
{
    [Trait("Category", "Unit")]
    public class UpdateTenantOnboardingProgressCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenMissingTenant_ShouldThrow()
        {
            //Arrange
            var request = new UpdateTenantOnboardingProgressInput
            {
                OnboardingProgress = new List<TenantOnboardingProgressDto>
                {
                    new() { TenantOnboardingStatus = TenantOnboardingStatus.Departments, Completed = true }
                }
            };

            var handler = CreateHandler(null, out _);
            //Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request));
            //Assert
            exception.Message.ShouldBe("Tenant not found");
        }

        [Fact]
        public async Task Handle_GivenOnboardingStatus_ShouldUpdate()
        {
            //Arrange
            var request = new UpdateTenantOnboardingProgressInput
            {
                OnboardingProgress = new List<TenantOnboardingProgressDto>
                {
                    new() { TenantOnboardingStatus = TenantOnboardingStatus.Departments, Completed = true }
                }
            };
            var tenant = TestTenantBuilder.Create()
                .WithOnboardingStatus((TenantOnboardingStatus.Clinics, true),
                    (TenantOnboardingStatus.Departments, false))
                .Build();

            Tenant savedTenant = null;
            var handler = CreateHandler(tenant, out var tenantManager);
            await tenantManager.UpdateAsync(Arg.Do<Tenant>(t => savedTenant = t));
            //Act
            await handler.Handle(request);
            //Assert
            savedTenant.OnboardingProgress.Count.ShouldBe(2);
            savedTenant.OnboardingProgress.First().TenantOnboardingStatus.ShouldBe(TenantOnboardingStatus.Clinics);
            savedTenant.OnboardingProgress.First().Completed.ShouldBe(true);
            savedTenant.OnboardingProgress.Last().TenantOnboardingStatus.ShouldBe(TenantOnboardingStatus.Departments);
            savedTenant.OnboardingProgress.Last().Completed.ShouldBe(true);
        }

        [Fact]
        public async Task Handle_GivenOnboardingStatusIsFinalize_ShouldCompleteOnboarding()
        {
            //Arrange
            var request = new UpdateTenantOnboardingProgressInput
            {
                OnboardingProgress = new List<TenantOnboardingProgressDto>
                {
                    new() { TenantOnboardingStatus = TenantOnboardingStatus.Finalize, Completed = true }
                }
            };
            var tenant = TestTenantBuilder.Create().WithIsOnboarded(false)
                .WithOnboardingStatus((TenantOnboardingStatus.JobTitlesAndLevels, true))
                .Build();

            Tenant savedTenant = null;
            var handler = CreateHandler(tenant, out var tenantManager);
            await tenantManager.UpdateAsync(Arg.Do<Tenant>(t => savedTenant = t));
            //Act
            await handler.Handle(request);
            //Assert
            savedTenant.OnboardingProgress.Count.ShouldBe(2);
            
            savedTenant.IsOnboarded.ShouldBe(true);
        }

        private static UpdateTenantOnboardingProgressCommandHandler CreateHandler(Tenant tenant,
            out ITenantManager tenantManager)
        {
            tenantManager = Substitute.For<ITenantManager>();
            var abpSession = Substitute.For<IAbpSession>();
            abpSession.TenantId.Returns(1);
            tenantManager.GetByIdAsync(1).Returns(tenant);
            return new UpdateTenantOnboardingProgressCommandHandler(tenantManager, abpSession);
        }
    }
}