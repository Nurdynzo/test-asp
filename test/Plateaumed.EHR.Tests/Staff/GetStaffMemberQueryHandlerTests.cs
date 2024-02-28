using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.Staff.Handlers;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Staff
{
    public class GetStaffMemberQueryHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenUserDoesNotExist_ShouldThrow()
        {
            // Arrange
            var request = new EntityDto<long>(1);

            var userRepository = Substitute.For<IUserRepository>();
            var countryRepository = Substitute.For<IRepository<Country, int>>();

            var empty = MockUserQuery();
            userRepository.GetAll().Returns(empty);

            var handler = new GetStaffMemberQueryHandler(userRepository, _objectMapper, countryRepository);
            // Act

            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request));
            // Assert
            exception.Message.ShouldBe("User does not exist");
        }

        [Fact]
        public async Task Handle_GivenUserIsNotStaffMember_ShouldThrow()
        {
            // Arrange
            const int tenantId = 4;
            const int userId = 1;
            var request = new EntityDto<long>(userId);

            var userRepository = Substitute.For<IUserRepository>();
            var countryRepository = Substitute.For<IRepository<Country, int>>();

            var user = TestUserBuilder.Create(tenantId).WithId(userId).WithStaffMember(null).Build();
            var savedUsers = MockUserQuery(user);
            userRepository.GetAll().Returns(savedUsers);

            var handler = new GetStaffMemberQueryHandler(userRepository, _objectMapper, countryRepository);
            // Act

            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request));
            // Assert
            exception.Message.ShouldBe("User is not a staff member");
        }


        [Fact]
        public async Task Handle_GivenUser_ShouldMapFields()
        {
            // Arrange
            const int tenantId = 1;
            const long facilityId = 3;
            const long userId = 45;
            var request = new EntityDto<long>(userId);
            var userRepository = Substitute.For<IUserRepository>();
            var countryRepository = Substitute.For<IRepository<Country, int>>();

            var doctorRole = TestRoleBuilder.Create(tenantId).WithName(StaticRoleNames.JobRoles.Doctor).Build();
            var labRole = TestRoleBuilder.Create(tenantId).WithName(StaticRoleNames.JobRoles.LaboratoryScientist).Build();
            var adminRole = TestRoleBuilder.Create(tenantId).WithName(StaticRoleNames.Tenants.Admin).Build();

            var jobTitle = TestJobTitleBuilder.Create(tenantId).WithId(5).Build();
            var jobLevel = TestJobLevelBuilder.Create(tenantId).WithId(17).WithJobTitle(jobTitle).Build();

            var unit = TestOrganizationUnitExtendedBuilder.Create(tenantId, facilityId, "Unit").WithActive(true).Build();
            var dept = TestOrganizationUnitExtendedBuilder.Create(tenantId, facilityId, "Unit").WithActive(true).Build(); 

            var ward = TestWardBuilder.Create(tenantId, 2).Build();
            var primaryJob = TestJobBuilder.Create(tenantId).AsPrimary().WithDepartment(dept).WithWard(ward).WithTeamRole(doctorRole).WithUnit(unit)
                .WithServiceCentre(ServiceCentreType.InPatient).WithJobLevel(jobLevel).Build();
            var secondaryJob = TestJobBuilder.Create(tenantId).WithTeamRole(labRole).Build();
            var staffMember = TestStaffMemberBuilder.Create().WithJob(primaryJob, secondaryJob).WithAdminRole(adminRole).Build();
            var user = TestUserBuilder.Create(tenantId).WithId(userId).WithStaffMember(staffMember).Build();

            var savedUsers = MockUserQuery(user);
            userRepository.GetAll().Returns(savedUsers);

            var handler = new GetStaffMemberQueryHandler(userRepository, _objectMapper, countryRepository);
            // Act

            var response = await handler.Handle(request);
            // Assert
            response.User.Id.ShouldBe(user.Id);
            response.User.UserName.ShouldBe(user.UserName);
            response.User.EmailAddress.ShouldBe(user.EmailAddress);
            response.User.Name.ShouldBe(user.Name);
            response.User.Surname.ShouldBe(user.Surname);
            response.User.PhoneNumber.ShouldBe(user.PhoneNumber);
            response.User.IsActive.ShouldBe(user.IsActive);
            response.StaffCode.ShouldBe(staffMember.StaffCode);
            response.ContractStartDate.ShouldBe(staffMember.ContractStartDate);
            response.ContractEndDate.ShouldBe(staffMember.ContractEndDate);
            response.AdminRole.ShouldBe(staffMember.AdminRole.Name);
            response.Jobs.Count.ShouldBe(2);
            response.Jobs[0].IsPrimary.ShouldBe(primaryJob.IsPrimary);
            response.Jobs[0].JobLevelId.ShouldBe(primaryJob.JobLevel.Id);
            response.Jobs[0].TeamRole.ShouldBe(primaryJob.TeamRole.Name);
            response.Jobs[0].Unit.DisplayName.ShouldBe(primaryJob.Unit.DisplayName);
            response.Jobs[0].Department.ShortName.ShouldBe(primaryJob.Department.ShortName);
            response.Jobs[0].Wards.Count.ShouldBe(1);
            response.Jobs[0].Wards[0].ShouldBe(ward.Id);
            response.Jobs[1].IsPrimary.ShouldBe(secondaryJob.IsPrimary);
            response.Jobs[1].JobLevelId.ShouldBe(null);
            response.Jobs[1].TeamRole.ShouldBe(secondaryJob.TeamRole.Name);
            response.Jobs[1].Wards.Count.ShouldBe(0);
        }

        private static IQueryable<User> MockUserQuery(params User[] users)
        {
            return users.AsQueryable().BuildMock();
        }
    }
}
