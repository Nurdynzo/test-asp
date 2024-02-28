using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Localization;
using Abp.Organizations;
using Abp.UI;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Organizations
{
    // ReSharper disable once InconsistentNaming
    public class OrganizationUnitAppService_Tests : AppTestBase
    {
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        private readonly ILocalizationManager _localizationManager;

        public OrganizationUnitAppService_Tests()
        {
            _organizationUnitAppService = Resolve<IOrganizationUnitAppService>();
            _localizationManager = Resolve<ILocalizationManager>();
        }

        [Fact]
        public async Task Test_GetOrganizationUnitUsers()
        {
            //Arrange
            var ou1 = GetOrganizationUnitByName("OU1");

            //Act
            await _organizationUnitAppService.GetOrganizationUnitUsers(
                new GetOrganizationUnitUsersInput
                {
                    Id = ou1.Id
                });
        }

        [Fact(Skip = "TODO: To be fixed")]
        public async Task Test_MoveOrganizationUnit()
        {
            //Arrange
            var ou11 = GetOrganizationUnitByName("OU11");
            var ou12 = GetOrganizationUnitByName("OU12");

            //Act
            var output = await _organizationUnitAppService.MoveOrganizationUnit(new MoveOrganizationUnitInput
            {
                Id = ou11.Id,
                NewParentId = ou12.Id
            });

            //Assert
            output.ParentId.ShouldBe(ou12.Id);
            output.Code.ShouldBe(OrganizationUnit.CreateCode(1, 2, 1));
        }

        [Fact]
        public async Task Test_DeleteOrganizationUnit()
        {
            //Arrange
            var ou11 = GetOrganizationUnitByName("OU11");

            UsingDbContext(context =>
            {
                context.Users
                    .FirstOrDefault(u => u.Id == AbpSession.UserId.Value && u.TenantId == AbpSession.TenantId.Value)
                    .ShouldNotBeNull();
            });

            //Act
            await _organizationUnitAppService.DeleteOrganizationUnit(new EntityDto<long>(ou11.Id));

            //Assert
            GetOrganizationUnitById(ou11.Id).IsDeleted.ShouldBeTrue();
        }

        [Fact]
        public async Task Test_AddUsersToOrganizationUnit()
        {
            //Arrange
            var ou12 = GetOrganizationUnitByName("OU12");
            var admin = await GetUserByUserNameAsync(AbpUserBase.AdminUserName);

            //Act
            await _organizationUnitAppService.AddUsersToOrganizationUnit(
                new UsersToOrganizationUnitInput
                {
                    UserIds = new[] { admin.Id },
                    OrganizationUnitId = ou12.Id
                });

            //Assert

            //check from database
            UsingDbContext(context =>
                context.UserOrganizationUnits.FirstOrDefault(uou =>
                    uou.OrganizationUnitId == ou12.Id && uou.UserId == admin.Id)).ShouldNotBeNull();

            //Check also from app service
            var output =
                await _organizationUnitAppService.GetOrganizationUnitUsers(new GetOrganizationUnitUsersInput
                    { Id = ou12.Id });
            output.Items.FirstOrDefault(u => u.Id == admin.Id).ShouldNotBeNull();
        }

        [Fact]
        public async Task Test_RemoveFromOrganizationUnit()
        {
            //Arrange
            var ou12 = GetOrganizationUnitByName("OU12");
            var admin = await GetUserByUserNameAsync(AbpUserBase.AdminUserName);

            UsingDbContext(context =>
                context.UserOrganizationUnits.Add(new UserOrganizationUnit(AbpSession.TenantId, admin.Id, ou12.Id)));

            //Act
            await _organizationUnitAppService.RemoveUserFromOrganizationUnit(
                new UserToOrganizationUnitInput
                {
                    UserId = admin.Id,
                    OrganizationUnitId = ou12.Id
                });

            //check from database
            UsingDbContext(context =>
            {
                var userOrganizationUnit = context
                    .UserOrganizationUnits
                    .FirstOrDefault(uou => uou.OrganizationUnitId == ou12.Id && uou.UserId == admin.Id);

                userOrganizationUnit.ShouldNotBe(null);
                userOrganizationUnit.IsDeleted.ShouldBeTrue();
            });
        }

        [Fact]
        public void Test_GetOrganizationUnits_Given_FacilityHasNoUnits_Should_ReturnEmpty()
        {
            // Arrange
            Facility facility2 = null;
            UsingDbContext(context =>
            {
                var group = TestFacilityGroupBuilder.Create(1).Save(context);
                var facility1 = TestFacilityBuilder.Create(1, group.Id).Save(context);
                facility2 = TestFacilityBuilder.Create(1, group.Id).Save(context);

                var ou11 = TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "OU11")
                    .WithCode(1, 1).Save(context);
                var ou12 = TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "OU12")
                    .WithCode(1, 2).Save(context);
                TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "OU1")
                    .WithCode(1).WithChildren(ou11, ou12).Save(context);
            });

            //Act
            var output =
                _organizationUnitAppService.GetOrganizationUnits(new GetOrganizationUnitsInput { FacilityId = facility2.Id });

            //Assert
            output.Result.Items.Count.ShouldBe(0);
        }

        [Fact]
        public void Test_GetOrganizationUnits_Given_IncludeClinicsFalse_Should_ExcludeClinics()
        {
            // Arrange
            var facility1 = CreateFacility();
            UsingDbContext(context =>
            {
                var clinic = TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "Clinic")
                    .WithCode(1, 1, 1).WithClinicType().Save(context);
                var orgUnit = TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "Org Unit")
                    .WithCode(1, 1).WithChildren(clinic).Save(context);
                TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "OU1")
                    .WithCode(1).WithChildren(orgUnit).Save(context);
            });

            //Act
            var output =
                _organizationUnitAppService.GetOrganizationUnits(new GetOrganizationUnitsInput { FacilityId = facility1.Id, IncludeClinics = false});

            //Assert
            output.Result.Items.Count.ShouldBe(2);
        }
        
        [Fact]
        public void Test_GetOrganizationUnits_Given_IncludeClinicsNull_Should_DefaultToExcludeClinics()
        {
            // Arrange
            var facility1 = CreateFacility();
            UsingDbContext(context =>
            {
                var clinic = TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "Clinic")
                    .WithCode(1, 1, 1).WithClinicType().Save(context);
                var orgUnit = TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "Org Unit")
                    .WithCode(1, 1).WithChildren(clinic).Save(context);
                TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "OU1")
                    .WithCode(1).WithChildren(orgUnit).Save(context);
            });

            //Act
            var output =
                _organizationUnitAppService.GetOrganizationUnits(new GetOrganizationUnitsInput { FacilityId = facility1.Id});

            //Assert
            output.Result.Items.Count.ShouldBe(2);
        }

        [Fact]
        public void Test_GetOrganizationUnits_Given_IncludeClinicsTrue_Should_ReturnReturnClinics()
        {
            // Arrange
            var facility1 = CreateFacility();
            UsingDbContext(context =>
            {
                var clinic = TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "Clinic")
                    .WithCode(1, 1, 1).WithClinicType().Save(context);
                var orgUnit = TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "Org Unit")
                    .WithCode(1, 1).WithChildren(clinic).Save(context);
                TestOrganizationUnitExtendedBuilder.Create(1, facility1.Id, "OU1")
                    .WithCode(1).WithChildren(orgUnit).Save(context);
            });

            //Act
            var output =
                _organizationUnitAppService.GetOrganizationUnits(new GetOrganizationUnitsInput { FacilityId = facility1.Id, IncludeClinics = true });

            //Assert
            output.Result.Items.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Test_CreateOrganizationUnit_Given_ValidInput_Should_Save()
        {
            //Arrange
            Facility facility = null;
            OrganizationUnitExtended orgUnit = null;
            UsingDbContext(context =>
            {
                var group = TestFacilityGroupBuilder.Create(1).Save(context);
                facility = TestFacilityBuilder.Create(1, group.Id).Save(context);
                orgUnit = TestOrganizationUnitExtendedBuilder.Create(1, facility.Id, "OU")
                    .WithCode(1).Save(context);
            });

            //Act
            var output = await _organizationUnitAppService.CreateOrganizationUnit(new CreateOrganizationUnitInput
            {
                ParentId = orgUnit.Id,
                DisplayName = "TestOU",
                ShortName = "TestShortName",
                FacilityId = facility.Id,
                IsActive = false,
                Type = OrganizationUnitType.Clinic.ToString()
                
            });

            //Assert
            UsingDbContext(context =>
            {
                var organizationUnit = context.OrganizationUnitExtended.FirstOrDefault(ou => ou.Id == output.Id);
                organizationUnit.ShouldNotBeNull();
                organizationUnit.DisplayName.ShouldBe("TestOU");
                organizationUnit.ShortName.ShouldBe("TestShortName");
                organizationUnit.FacilityId.ShouldBe(facility.Id);
                organizationUnit.IsActive.ShouldBe(false);
                organizationUnit.Type.ShouldBe(OrganizationUnitType.Clinic);
                organizationUnit.ParentId.ShouldBe(orgUnit.Id);
            }); 
        }

        [Fact]
        public async Task Test_UpdateOrganizationUnit_Given_ValidInput_Should_Save()
        {
            //Arrange
            Facility facility = null;
            OrganizationUnitExtended orgUnit = null;
            UsingDbContext(context =>
            {
                var group = TestFacilityGroupBuilder.Create(1).Save(context);
                facility = TestFacilityBuilder.Create(1, group.Id).Save(context);
                orgUnit = TestOrganizationUnitExtendedBuilder.Create(1, facility.Id, "OU")
                    .WithShortName("OUShort").WithActive(false).WithDepartmentType().WithCode(1).Save(context);
            });

            //Act
            await _organizationUnitAppService.UpdateOrganizationUnit(new UpdateOrganizationUnitInput
            {
                Id = orgUnit.Id,
                DisplayName = "NewDisplay",
                ShortName = "NewShortName",
                IsActive = true,
            });

            //Assert
            UsingDbContext(context =>
            {
                var organizationUnit = context.OrganizationUnitExtended.FirstOrDefault(ou => ou.Id == orgUnit.Id);
                organizationUnit.ShouldNotBeNull();
                organizationUnit.DisplayName.ShouldBe("NewDisplay");
                organizationUnit.ShortName.ShouldBe("NewShortName");
                organizationUnit.FacilityId.ShouldBe(facility.Id);
                organizationUnit.IsActive.ShouldBe(true);
            });
        }

        [Fact]
        public async Task Test_UpdateOrganizationUnit_Given_OrgUnitDoesNotExist_Should_Throw()
        {
            //Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                await _organizationUnitAppService.UpdateOrganizationUnit(new UpdateOrganizationUnitInput
                {
                    Id = 6969,
                    DisplayName = "NewDisplay",
                    ShortName = "NewShortName",
                });
            });

            //Assert
            exception.Message.ShouldBe(GetLocalizedString("OrganizationUnitNotFoundForThisUser"));
        }

        private Facility CreateFacility()
        {
            return UsingDbContext(context =>
            {
                var group = TestFacilityGroupBuilder.Create(DefaultTenantId).Save(context);
                return TestFacilityBuilder.Create(DefaultTenantId, group.Id).Save(context);
            });
        }

        private string GetLocalizedString(string message)
        {
            return _localizationManager.GetString(EHRConsts.LocalizationSourceName, message);
        }

        private OrganizationUnit GetOrganizationUnitByName(string displayName)
        {
            var organizationUnit = UsingDbContext(context =>
                context.OrganizationUnits.FirstOrDefault(ou => ou.DisplayName == displayName));
            organizationUnit.ShouldNotBeNull();
            return organizationUnit;
        }

        private OrganizationUnit GetOrganizationUnitById(long id)
        {
            var organizationUnit =
                UsingDbContext(context => context.OrganizationUnits.FirstOrDefault(ou => ou.Id == id));
            organizationUnit.ShouldNotBeNull();
            return organizationUnit;
        }
    }
}