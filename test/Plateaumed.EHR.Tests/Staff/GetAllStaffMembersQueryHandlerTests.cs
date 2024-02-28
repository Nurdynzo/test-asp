using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Staff.Handlers;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Staff
{
    [Trait("Category", "Unit")]
    public class GetAllStaffMembersQueryHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenJobTitleFilterAndAllFacilities_ShouldReturnFilteredStaffMembers()
        {
            // Arrange
            var testData = CreateTestData().BuildMock();
            var userRepository = Substitute.For<IRepository<User, long>>();
            userRepository.GetAll().Returns(testData);
            
            var request = new GetAllStaffMembersRequest
            {
                JobTitleIdFilter = 1,
                FacilityIdFilter = null
            };
            var handler = CreateHandler(userRepository);
            // Act
            var result = await handler.Handle(request);
            // Assert
            result.Items.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_GivenFilter_ShouldFilterStaffMembersByName()
        {
            // Arrange
            var testData = CreateTestData().BuildMock();
            var userRepository = Substitute.For<IRepository<User, long>>();
            userRepository.GetAll().Returns(testData);

            var request = new GetAllStaffMembersRequest
            {
                Filter = "admin"
            };
            var handler = CreateHandler(userRepository);
            // Act
            var result = await handler.Handle(request);
            // Assert
            result.Items.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Handle_FacilityFilter_ShouldFilterStaffMembersByFacility()
        {
            // Arrange
            var testData = CreateTestData().BuildMock();
            var userRepository = Substitute.For<IRepository<User, long>>();
            userRepository.GetAll().Returns(testData);

            var request = new GetAllStaffMembersRequest
            {
                FacilityIdFilter = 1
            };
            var handler = CreateHandler(userRepository);
            // Act
            var result = await handler.Handle(request);
            // Assert
            result.Items.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_RoleFilter_ShouldFilterStaffMembersByRoleId()
        {
            // Arrange
            var testData = CreateTestData().BuildMock();
            var userRepository = Substitute.For<IRepository<User, long>>();
            userRepository.GetAll().Returns(testData);

            var request = new GetAllStaffMembersRequest
            {
                Role = 1
            };
            var handler = CreateHandler(userRepository);
            // Act
            var result = await handler.Handle(request);
            // Assert
            result.Items.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_GivenFilters_ShouldMapFields()
        {
            // Arrange
            var testData = CreateTestData().BuildMock();
            var expected = testData.First(u => u.Name == "Admin");
            var userRepository = Substitute.For<IRepository<User, long>>();
            userRepository.GetAll().Returns(testData);

            var request = new GetAllStaffMembersRequest
            {
                JobTitleIdFilter = 1,
                FacilityIdFilter = 1
            };
            var handler = CreateHandler(userRepository);
            // Act
            var result = await handler.Handle(request);
            // Assert
            result.Items.Count.ShouldBe(1);
            var item = result.Items.First();
            item.Id.ShouldBe(expected.Id);
            item.StaffCode.ShouldBe(expected.StaffMemberFk.StaffCode);
            item.ContractEndDate.ShouldBe(expected.StaffMemberFk.ContractEndDate);
            item.ContractStartDate.ShouldBe(expected.StaffMemberFk.ContractStartDate);
            item.JobTitle.ShouldBe("JT1");
            item.JobLevel.ShouldBe("JL1");
            item.Name.ShouldBe(expected.Name);
            item.Surname.ShouldBe(expected.Surname);
            item.Department.ShouldBe("Dept1");
            item.Unit.ShouldBe("Unit1");
            item.Title.ShouldBe(expected.Title);
            item.EmailAddress.ShouldBe(expected.EmailAddress);
            item.PhoneNumber.ShouldBe(expected.PhoneNumber);
        }

        private static IQueryable<User> CreateTestData()
        {
            const int tenantId = 69;

            var facilityGroup = TestFacilityGroupBuilder.Create(tenantId).Build();
            var facility1 = TestFacilityBuilder.Create(tenantId, facilityGroup.Id).WithId(1).Build();
            var facility2 = TestFacilityBuilder.Create(tenantId, facilityGroup.Id).WithId(2).Build();

            var jobTitle1 = TestJobTitleBuilder.Create(tenantId).WithId(1).WithNames("JT1", "jt1")
                .WithFacilityId(facility1.Id).Build();
            var jobLevel1 = TestJobLevelBuilder.Create(tenantId).WithNames("JL1", "jl1")
                .WithJobTitle(jobTitle1).Build();
            var jobTitle2 = TestJobTitleBuilder.Create(tenantId).WithId(2).WithNames("JT2", "jt2")
                .WithFacilityId(facility1.Id).Build();
            var jobLevel2 = TestJobLevelBuilder.Create(tenantId).WithNames("JL2", "jl2")
                .WithJobTitle(jobTitle2).Build();

            var unit = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility1.Id, "Unit1").Build();
            var dept = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility1.Id, "Dept1").WithChildren(unit).Build();

            var job1 = TestJobBuilder.Create(tenantId).WithJobLevel(jobLevel1).AsPrimary().WithDepartment(dept).WithUnit(unit).Build();
            var job2 = TestJobBuilder.Create(tenantId).WithJobLevel(jobLevel2).AsPrimary().Build();
            var job3 = TestJobBuilder.Create(tenantId).WithJobLevel(jobLevel2).AsPrimary().Build();
            var job4 = TestJobBuilder.Create(tenantId).WithJobLevel(jobLevel1).AsPrimary().Build();
            var job5 = TestJobBuilder.Create(tenantId).WithJobLevel(jobLevel1).AsPrimary().Build();

            var staffMember1 = TestStaffMemberBuilder.Create().WithJob(job1).WithFacility(facility1).Build();
            var staffMember2 = TestStaffMemberBuilder.Create().WithJob(job2).WithFacility(facility1).Build();
            var staffMember3 = TestStaffMemberBuilder.Create().WithJob(job3).WithFacility(facility1).Build();
            var staffMember4 = TestStaffMemberBuilder.Create().WithJob(job4).WithFacility(facility2).Build();
            var staffMember5 = TestStaffMemberBuilder.Create().WithJob(job5).WithFacility(facility2).Build();

            var user1 = TestUserBuilder.Create(tenantId).WithNames("Admin", "User").WithStaffMember(staffMember1).WithRoleId(1).Build();
            var user2 = TestUserBuilder.Create(tenantId).WithNames("Support", "User").WithStaffMember(staffMember2).WithRoleId(2).Build();
            var user3 = TestUserBuilder.Create(tenantId).WithNames("Doctor", "User").WithStaffMember(staffMember3).WithRoleId(1).Build();
            var user4 = TestUserBuilder.Create(tenantId).WithNames("Nurse", "User").WithStaffMember(staffMember4).WithRoleId(2).Build();
            var user5 = TestUserBuilder.Create(tenantId).WithNames("Reception", "User").WithStaffMember(staffMember5).WithRoleId(1).Build();

            return new List<User> { user1, user2, user3, user4, user5 }.AsQueryable();
        }

        private GetAllStaffMembersQueryHandler CreateHandler(IRepository<User, long> userRepository)
        {
            return new GetAllStaffMembersQueryHandler(_objectMapper, userRepository,
                Substitute.For<IRepository<OrganizationUnitExtended, long>>(), 
                Substitute.For<IRepository<Role>>(), 
                Substitute.For<IRepository<UserRole, long>>());
        }
    }
}