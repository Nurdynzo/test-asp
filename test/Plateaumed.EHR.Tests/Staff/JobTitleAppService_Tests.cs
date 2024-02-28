using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Staff
{
    [Trait("Category", "Integration")]
    public class JobTitleAppService_Tests : AppTestBase
    {
        private readonly IJobTitlesAppService _jobTitlesAppService;

        public JobTitleAppService_Tests()
        {
            _jobTitlesAppService = Resolve<IJobTitlesAppService>();
        }

        [Fact]
        public async Task Create_GivenValidJobTitle_ShouldSave()
        {
            //Arrange
            LoginAsDefaultTenantAdmin();
            var facility = CreateFacility();

            //Act
            var input = new CreateOrEditJobTitleDto
            {
                Name = "Jerb",
                ShortName = "J",
                FacilityId = facility.Id,
                JobLevels = new List<CreateOrEditJobLevelDto>
                {
                    new()
                    {
                        Name = "Jerb Level",
                        Rank = 1,
                        ShortName = "JL",
                    }
                }
                
            };
            await _jobTitlesAppService.CreateOrEdit(input);

            var jobTitle =
                UsingDbContext(context => { return context.JobTitles.Include(j => j.JobLevels).FirstOrDefault(x => x.Name == input.Name); });

            //Assert
            jobTitle.Name.ShouldBe(input.Name);
            jobTitle.ShortName.ShouldBe(input.ShortName);
            jobTitle.TenantId.ShouldBe(AbpSession.TenantId);
            jobTitle.IsActive.ShouldBe(true);
            jobTitle.FacilityId.ShouldBe(facility.Id);
            jobTitle.JobLevels.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Update_GivenValidJobTitle_ShouldUpdate()
        {
            //Arrange
            LoginAsDefaultTenantAdmin();
            var tenantId = AbpSession.TenantId.Value;
            var savedTitle = UsingDbContext(context => TestJobTitleBuilder.Create(tenantId).Save(context));

            var input = new CreateOrEditJobTitleDto
            {
                Id = savedTitle.Id,
                Name = "upJob",
                ShortName = "UJ",
            };
            //Act
            await _jobTitlesAppService.CreateOrEdit(input);

            var jobTitle = UsingDbContext(context =>
            {
                return context.JobTitles.Include(j => j.JobLevels).Where(j => !j.IsDeleted)
                    .FirstOrDefault(x => x.Id == savedTitle.Id);
            });

            //Assert
            jobTitle.Name.ShouldBe(input.Name);
            jobTitle.ShortName.ShouldBe(input.ShortName);
            jobTitle.TenantId.ShouldBe(tenantId);
        }

        [Fact]
        public async Task GetAll_GivenIncludeLevels_ShouldReturnSavedJobTitlesAndLevels()
        {
            //Arrange
            await LoginAsCustomTenant();

            var tenantId = AbpSession.TenantId.Value;
            var facility = CreateFacility();
            UsingDbContext(context =>
            {
                TestJobTitleBuilder.Create(tenantId).WithFacilityId(facility.Id).Save(context);
                TestJobTitleBuilder.Create(tenantId).Save(context);
                context.SaveChanges();
            });

            var input = new GetAllJobTitlesInput
            {
                FacilityId = facility.Id,
            };
            //Act
            var output = await _jobTitlesAppService.GetAll(input);
            
            //Assert
            output.Items.Count.ShouldBe(1);
            output.Items[0].FacilityId.ShouldBe(facility.Id);
        }

        [Fact]
        public async Task GetAll_GivenFacilityId_ShouldReturnSavedJobTitlesForFacility()
        {
            //Arrange
            await LoginAsCustomTenant();

            var tenantId = AbpSession.TenantId.Value;
            UsingDbContext(context => 
            {
                var title1 = TestJobTitleBuilder.Create(tenantId).Save(context);
                TestJobLevelBuilder.Create(tenantId).WithJobTitle(title1).Save(context);
                var title2 = TestJobTitleBuilder.Create(tenantId).Save(context);
                TestJobLevelBuilder.Create(tenantId).WithJobTitle(title2).Save(context);
                context.SaveChanges();
            });

            var input = new GetAllJobTitlesInput
            {
                IncludeLevels = true
            };
            //Act
            var output = await _jobTitlesAppService.GetAll(input);

            //Assert
            output.Items.Count.ShouldBe(2);
            output.Items[0].JobLevels.Count.ShouldBe(1);
            output.Items[1].JobLevels.Count.ShouldBe(1);
        }

        [Fact]
        public async Task GetAll_GivenIncludeLevelsFalse_ShouldReturnSavedJobTitlesWithoutLevels()
        {
            //Arrange
            await LoginAsCustomTenant();
            var tenantId = AbpSession.TenantId.Value;
            UsingDbContext(context =>
            {
                var title1 = TestJobTitleBuilder.Create(tenantId).Save(context);
                TestJobLevelBuilder.Create(tenantId).WithJobTitle(title1).Save(context);
                var title2 = TestJobTitleBuilder.Create(tenantId).Save(context);
                TestJobLevelBuilder.Create(tenantId).WithJobTitle(title2).Save(context);
                context.SaveChanges();
            });

            var input = new GetAllJobTitlesInput
            {
                IncludeLevels = false
            };
            //Act
            var output = await _jobTitlesAppService.GetAll(input);
            
            //Assert
            output.Items.Count.ShouldBe(2);
            output.Items[0].JobLevels.Count.ShouldBe(0);
            output.Items[1].JobLevels.Count.ShouldBe(0);
        }

        private async Task LoginAsCustomTenant()
        {
            var (tenant, user) = UsingDbContext(context =>
            {
                var tenant = TestTenantBuilder.Create().Save(context);
                var user = TestUserBuilder.Create(tenant.Id).Save(context);
                return (tenant, user);
            });

            LoginAsTenant(tenant.TenancyName, user.UserName);
            await SetUserPermissions(user, AppPermissions.Pages_JobTitles);
        }

        private Facility CreateFacility()
        {
            return UsingDbContext(context =>
            {
                var group = TestFacilityGroupBuilder.Create(DefaultTenantId).Save(context);
                var facility = TestFacilityBuilder.Create(DefaultTenantId, group.Id).Save(context);

                context.SaveChanges();
                return facility;
            });
        }
    }
}