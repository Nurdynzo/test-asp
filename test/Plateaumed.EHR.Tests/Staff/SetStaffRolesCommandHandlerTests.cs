using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Staff.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Staff
{
    [Trait("Category", "Unit")]
    public class SetStaffRolesCommandHandlerTests
    {
        private readonly IRepository<StaffMember, long> _repository;
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly IUnitOfWorkManager _unitOfWork;


        public SetStaffRolesCommandHandlerTests()
        {
            _repository = Substitute.For<IRepository<StaffMember, long>>();
            _userManager = Substitute.For<IUserManager>();
            _roleManager = Substitute.For<IRoleManager>();
            _unitOfWork = Substitute.For<IUnitOfWorkManager>();

        }

        [Fact]
        public async Task SetTeamRole_GivenNullJob_ShouldThrowException()
        {
            // Arrange
            var handler = CreateHandler();
            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.SetTeamRole(null, "HOD"));
            // Assert
            exception.Message.ShouldBe("Job cannot be null");
        }

        [Fact]
        public async Task SetTeamRole_GivenNullRole_ShouldSetToNull()
        {
            // Arrange
            const string teamRole = "Nope";
            var job = new Job();

            _roleManager.FindByNameAsync(teamRole).Returns(Task.FromResult((Role)null));
            var handler = CreateHandler();
            // Act
            await handler.SetTeamRole(job, teamRole);
            // Assert
            job.TeamRole.ShouldBeNull();
        }

        [Fact]
        public async Task SetTeamRole_GivenRole_ShouldSetToJob()
        {
            // Arrange
            const string teamRole = "Admin";
            var job = new Job();

            var role = new Role { Name = "Admin" };
            _roleManager.FindByNameAsync(teamRole).Returns(Task.FromResult(role));
            var handler = CreateHandler();
            // Act
            await handler.SetTeamRole(job, teamRole);
            // Assert
            job.TeamRole.ShouldBe(role);
        }

        [Fact]
        public async Task SetAdminRole_GivenNullStaffMember_ShouldThrowException()
        {
            // Arrange
            var handler = CreateHandler();
            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.SetAdminRole(null, "Admin"));
            // Assert
            exception.Message.ShouldBe("Staff member cannot be null");
        }

        [Fact]
        public async Task SetAdminRole_GivenNullRole_ShouldSetToNull()
        {
            // Arrange
            const string adminRole = "Nope";
            var staffMember = new StaffMember();

            _roleManager.FindByNameAsync(adminRole).Returns(Task.FromResult((Role)null));
            var handler = CreateHandler();
            // Act
            await handler.SetAdminRole(staffMember, adminRole);
            // Assert
            staffMember.AdminRole.ShouldBeNull();
        }

        [Fact]
        public async Task SetAdminRole_GivenRole_ShouldSetToStaffMember()
        {
            // Arrange
            const string adminRole = "Admin";
            var staffMember = new StaffMember();

            var role = new Role { Name = "Admin" };
            _roleManager.FindByNameAsync(adminRole).Returns(Task.FromResult(role));
            var handler = CreateHandler();
            // Act
            await handler.SetAdminRole(staffMember, adminRole);
            // Assert
            staffMember.AdminRole.ShouldBe(role);
        }

        [Fact]
        public async Task Handle_GivenStaffMember_ShouldSetJobRolesFromJobTitles()
        {
            // Arrange
            var job = new Job
            {
                JobLevel = new JobLevel { JobTitleFk = new JobTitle { Name = "Doctor" } }
            };
            var staffMember = new StaffMember
            {
                Id = 1,
                UserFk = new User { Roles = new List<UserRole>() },
                Jobs = new List<Job> { job }
            };
            var staffMembers = new List<StaffMember>
                { staffMember }.AsQueryable().BuildMock();

            _repository.GetAll().Returns(staffMembers);

            var role = new Role { Name = "Doctor" };
            _roleManager.FindByNameAsync("Doctor").Returns(Task.FromResult(role));

            var handler = CreateHandler();
            // Act
            await handler.Handle(staffMember.Id);
            // Assert
            job.JobRole.ShouldBe(role);
        }

        [Fact]
        public async Task Handle_GivenStaffMember_ShouldSetRolesFromTeamAdminAndJobRoles()
        {
            // Arrange
            var job = new Job
            {
                TeamRole = new Role { Name = "CMD" },
                JobLevel = new JobLevel { JobTitleFk = new JobTitle { Name = "Doctor" } }
            };
            var user = new User { Roles = new List<UserRole>() };
            var staffMember = new StaffMember
            {
                Id = 1,
                AdminRole = new Role { Name = "Admin" },
                UserFk = user,
                Jobs = new List<Job> { job }
            };
            var staffMembers = new List<StaffMember>
                { staffMember }.AsQueryable().BuildMock();

            _repository.GetAll().Returns(staffMembers);

            var role = new Role { Name = "Doctor" };
            _roleManager.FindByNameAsync("Doctor").Returns(Task.FromResult(role));
            User savedUser = null;
            string[] savedRoles = null;
            await _userManager.SetRolesAsync(Arg.Do<User>(x => savedUser = x), Arg.Do<string[]>(y => savedRoles = y));
            
            var handler = CreateHandler();
            // Act
            await handler.Handle(staffMember.Id);
            // Assert
            savedUser.ShouldBe(user);
            savedRoles.ShouldBeEquivalentTo(new[]{ "CMD", "Doctor", "Admin" });
        }


        private SetStaffRolesCommandHandler CreateHandler()
        {
            return new SetStaffRolesCommandHandler(_repository, _roleManager, _userManager, _unitOfWork);
        }
    }
}
