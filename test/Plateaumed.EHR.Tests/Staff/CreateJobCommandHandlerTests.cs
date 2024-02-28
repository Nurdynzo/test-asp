using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Bogus;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Staff.Handlers;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Staff
{
    [Trait("Category", "Unit")]
    public class CreateJobCommandHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenUserDoesNotExist_ShouldThrow()
        {
            // Arrange
            const int userId = 4;
            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetAll().Returns(MockUserQuery());

            var handler = CreateHandler(userRepository);
            // Act
            var createJobRequest = CreateJobRequest(userId);
            var exception = await Assert.ThrowsAnyAsync<UserFriendlyException>(() => handler.Handle(createJobRequest));
            // Assert
            exception.Message.ShouldBe("User not found");
        }

        [Fact]
        public async Task Handle_GivenUserIsNotStaffMember_ShouldThrow()
        {
            // Arrange
            const int userId = 4;

            var userRepository = Substitute.For<IUserRepository>();
            var user = TestUserBuilder.Create(1).WithId(userId).Build();
            userRepository.GetAll().Returns(MockUserQuery(user));


            var handler = CreateHandler(userRepository);
            // Act
            var createJobRequest = CreateJobRequest(userId);
            var exception = await Assert.ThrowsAnyAsync<UserFriendlyException>(() => handler.Handle(createJobRequest));
            // Assert
            exception.Message.ShouldBe("User is not a staff member and cannot be assigned jobs");
        }
        
        private CreateJobCommandHandler CreateHandler(IUserRepository userRepository)
        {
            return new CreateJobCommandHandler(userRepository, _objectMapper, Substitute.For<IUserManager>(),
                Substitute.For<IRepository<Ward, long>>(), Substitute.For<ISetStaffRolesCommandHandler>());
        }

        private static CreateOrEditJobRequest CreateJobRequest(int userId)
        {
            var faker = new Faker<JobDto>();
            faker.RuleFor(j => j.TeamRole, f => f.Name.JobTitle());
            faker.RuleFor(j => j.JobTitleId, f => f.IndexFaker);
            faker.RuleFor(j => j.JobLevelId, f => f.IndexFaker);
            faker.RuleFor(j => j.FacilityId, f => f.IndexFaker);
            faker.RuleFor(j => j.DepartmentId, f => f.IndexFaker);
            faker.RuleFor(j => j.UnitId, f => f.IndexFaker);
            faker.RuleFor(j => j.ServiceCentres, f => f.Make(2, f.PickRandom<ServiceCentreType>));
            return new CreateOrEditJobRequest
            {
                UserId = userId,
                Job = faker.Generate()
            };
        }

        private static IQueryable<User> MockUserQuery(params User[] users)
        {
            return users.AsQueryable().BuildMock();
        }
    }
}
