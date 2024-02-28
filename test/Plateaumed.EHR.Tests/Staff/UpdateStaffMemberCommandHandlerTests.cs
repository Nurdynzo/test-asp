using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Bogus;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Authorization.Users.Dto;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Staff.Handlers;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Staff;

[Trait("Category", "Unit")]
public class UpdateStaffMemberCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

    [Fact]
    public async Task Handle_GivenNonExistentUser_ShouldThrow()
    {
        // Arrange
        var request = CreateRequest();
        var userRepository = Substitute.For<IUserRepository>();

        userRepository.GetAll().Returns(new List<User>().AsQueryable().BuildMock());

        var handler = CreateHandler(userRepository);
        // Act
        var exception = await Assert.ThrowsAnyAsync<UserFriendlyException>(() => handler.Handle(request));
        // Assert
        exception.Message.ShouldBe("User not found");
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldUpdateSavedUser()
    {
        // Arrange
        var request = CreateRequest();

        const int tenantId = 5;
        var abpSession = Substitute.For<IAbpSession>();
        abpSession.TenantId.Returns(tenantId);

        User savedUser = null;
        var userManager = Substitute.For<IUserManager>();
        await userManager.UpdateAsync(Arg.Do<User>(user => savedUser = user));

        var job = TestJobBuilder.Create(tenantId).AsPrimary().WithId(5).Build();
        var staffMember = TestStaffMemberBuilder.Create().WithJob(job).Build();

        var userRepository = Substitute.For<IUserRepository>();
        var user = TestUserBuilder.Create(tenantId).WithStaffMember(staffMember).Build();
        userRepository.GetAll().Returns(new List<User>{user}.AsQueryable().BuildMock());
        
        var handler = CreateHandler(userRepository, abpSession, userManager: userManager);
        // Act
        await handler.Handle(request);
        // Assert
        savedUser.ShouldNotBeNull();
        savedUser.TenantId.ShouldBe(tenantId);
        savedUser.Name.ShouldBe(request.User.Name);
        savedUser.Surname.ShouldBe(request.User.Surname);
        savedUser.MiddleName.ShouldBe(request.User.MiddleName);
        savedUser.Gender.ShouldBe(request.User.Gender);
        savedUser.DateOfBirth.ShouldBe(request.User.DateOfBirth);
        savedUser.IdentificationCode.ShouldBe(request.User.IdentificationCode);
        savedUser.IdentificationType.ShouldBe(request.User.IdentificationType);
        savedUser.PhoneNumber.ShouldBe(request.User.PhoneNumber);
        savedUser.EmailAddress.ShouldBe(request.User.EmailAddress);
        savedUser.AltEmailAddress.ShouldBe(request.User.AltEmailAddress);
        savedUser.StaffMemberFk.ShouldNotBeNull();
        savedUser.StaffMemberFk.StaffCode.ShouldBe(request.StaffCode);
        savedUser.StaffMemberFk.ContractStartDate.ShouldBe(request.ContractStartDate);
        savedUser.StaffMemberFk.ContractEndDate.ShouldBe(request.ContractEndDate);
        savedUser.StaffMemberFk.Jobs.ShouldNotBeNull();
        savedUser.StaffMemberFk.Jobs.Count.ShouldBe(1);
        var savedJob = savedUser.StaffMemberFk.Jobs.First();
        savedJob.Id.ShouldBe(job.Id);
        savedJob.JobLevelId.ShouldBe(request.Job.JobLevelId);
        savedJob.FacilityId.ShouldBe(request.Job.FacilityId);
        savedJob.IsPrimary.ShouldBe(true);
        savedJob.TenantId.ShouldBe(tenantId);
    }
    
    [Fact]
    public async Task Handle_GivenNonExistentWard_ShouldThrowException()
    {
        // Arrange
        const int tenantId = 5;
        var request = CreateRequest();

        request.Job.Wards = new List<long> { 1, 2 };

        var wardRepository = Substitute.For<IRepository<Ward, long>>();
        wardRepository.GetAsync(1).Returns(Task.FromResult(new Ward()));
        wardRepository.GetAsync(2).Returns(Task.FromResult<Ward>(null));

        var job = TestJobBuilder.Create(tenantId).AsPrimary().WithId(5).Build();
        var staffMember = TestStaffMemberBuilder.Create().WithJob(job).Build();

        var user = TestUserBuilder.Create(tenantId).WithStaffMember(staffMember).Build();

        var userRepository = Substitute.For<IUserRepository>();
        userRepository.GetAll().Returns(new List<User>{user}.AsQueryable().BuildMock());

        var handler = CreateHandler(userRepository, wardRepository: wardRepository);
        // Act
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request));
        // Assert
        exception.Message.ShouldBe("Ward not found");
    }
        
    private UpdateStaffMemberCommandHandler CreateHandler(IUserRepository userRepository,
        IAbpSession abpSession = null, IRepository<Ward, long> wardRepository = null, IUserManager userManager = null)
    {
        if (abpSession == null)
        {
            abpSession = Substitute.For<IAbpSession>();
            abpSession.TenantId.Returns(1);
        }

        wardRepository ??= Substitute.For<IRepository<Ward, long>>();
        userManager ??= Substitute.For<IUserManager>();

        return new UpdateStaffMemberCommandHandler(userRepository, _objectMapper, wardRepository,
            userManager, Substitute.For<ISetStaffRolesCommandHandler>());
    }

    private static CreateOrEditStaffMemberRequest CreateRequest()
    {
        var userFaker = new Faker<UserEditDto>();
        userFaker.RuleFor(x => x.Id, f => f.IndexFaker);
        userFaker.RuleFor(x => x.Title, f => f.PickRandom<TitleType>());
        userFaker.RuleFor(x => x.Name, f => f.Name.FirstName());
        userFaker.RuleFor(x => x.Surname, f => f.Name.LastName());
        userFaker.RuleFor(x => x.MiddleName, f => f.Name.FirstName());
        userFaker.RuleFor(x => x.UserName, f => f.Internet.UserName());
        userFaker.RuleFor(x => x.Password, f => f.Internet.Password());
        userFaker.RuleFor(x => x.Gender, f => f.PickRandom<GenderType>());
        userFaker.RuleFor(x => x.DateOfBirth, f => f.Date.Past());
        userFaker.RuleFor(x => x.IdentificationCode, f => f.Random.AlphaNumeric(10));
        userFaker.RuleFor(x => x.IdentificationType, f => f.PickRandom<IdentificationType>());
        userFaker.RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber());
        userFaker.RuleFor(x => x.EmailAddress, f => f.Internet.Email());
        userFaker.RuleFor(x => x.AltEmailAddress, f => f.Internet.Email());
        var user = userFaker.Generate();

        var faker = new Faker<CreateOrEditStaffMemberRequest>();

        faker.RuleFor(x => x.StaffCode, f => f.Random.AlphaNumeric(10));
        faker.RuleFor(x => x.ContractStartDate, f => f.Date.Past());
        faker.RuleFor(x => x.ContractEndDate, f => f.Date.Future());

        var updateStaffMemberRequest = faker.Generate();
        updateStaffMemberRequest.User = user;
        updateStaffMemberRequest.Job = new JobDto { IsPrimary = true };
        return updateStaffMemberRequest;
    }
}