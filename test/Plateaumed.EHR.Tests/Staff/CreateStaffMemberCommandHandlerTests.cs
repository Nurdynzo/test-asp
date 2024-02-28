using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Bogus;
using Microsoft.AspNetCore.Identity;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Authorization.Users.Dto;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Staff.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Staff
{
    [Trait("Category", "Unit")]
    public class CreateStaffMemberCommandHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldSave()
        {
            // Arrange
            var request = CreateRequest();

            const int tenantId = 5;
            var abpSession = Substitute.For<IAbpSession>();
            abpSession.TenantId.Returns(tenantId);

            User savedUser = null;
            var userManager = Substitute.For<IUserManager>();
            await userManager.CreateAsync(Arg.Do<User>(user => savedUser = user));

            var user = new User();
            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetAll().Returns(new List<User> { user }.AsQueryable().BuildMock());

            var handler = CreateHandler(userManager, abpSession, userRepository: userRepository);
            // Act
            await handler.Handle(request, CheckErrors());
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
            savedUser.ShouldChangePasswordOnNextLogin.ShouldBe(true);
            savedUser.StaffMemberFk.ShouldNotBeNull();
            savedUser.StaffMemberFk.StaffCode.ShouldBe(request.StaffCode);
            savedUser.StaffMemberFk.ContractStartDate.ShouldBe(request.ContractStartDate);
            savedUser.StaffMemberFk.ContractEndDate.ShouldBe(request.ContractEndDate);
            savedUser.StaffMemberFk.Jobs.ShouldNotBeNull();
            savedUser.StaffMemberFk.Jobs.Count.ShouldBe(1);
            var job = savedUser.StaffMemberFk.Jobs.First();
            job.JobLevelId.ShouldBe(request.Job.JobLevelId);
            job.FacilityId.ShouldBe(request.Job.FacilityId);
            job.IsPrimary.ShouldBe(true);
            job.TenantId.ShouldBe(tenantId);
        }

        [Fact]
        public async Task Handle_ShouldHashGeneratePasswordAndHash()
        {
            // Arrange
            var request = CreateRequest();

            User savedUser = null;
            const string password = "Password123";
            var userManager = Substitute.For<IUserManager>();
            await userManager.CreateAsync(Arg.Do<User>(user => savedUser = user));
            userManager.CreateRandomPassword().Returns(Task.FromResult(password));

            var passwordHasher = Substitute.For<IPasswordHasher<User>>();
            const string expectedHash = "#Hashed#";
            passwordHasher.HashPassword(Arg.Any<User>(), password).Returns(expectedHash);

            var user = new User();
            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetAll().Returns(new List<User> { user }.AsQueryable().BuildMock());

            var handler = CreateHandler(userManager, passwordHasher: passwordHasher, userRepository: userRepository);
            // Act
            await handler.Handle(request, CheckErrors());
            // Assert
            savedUser.Password.ShouldBe(expectedHash);
        }

        [Fact]
        public async Task Handle_GivenWards_ShouldSaveJobWards()
        {
            // Arrange
            var request = CreateRequest();
            request.Job.Wards = new List<long> { 1, 2 };

            User savedUser = null;
            var userManager = Substitute.For<IUserManager>();
            await userManager.CreateAsync(Arg.Do<User>(user => savedUser = user));

            var wardRepository = Substitute.For<IRepository<Ward, long>>();
            var ward1 = new Ward();
            var ward2 = new Ward();
            wardRepository.GetAsync(1).Returns(Task.FromResult(ward1));
            wardRepository.GetAsync(2).Returns(Task.FromResult(ward2));

            var user = new User();
            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetAll().Returns(new List<User> { user }.AsQueryable().BuildMock());

            var handler = CreateHandler(userManager, wardRepository: wardRepository, userRepository: userRepository);
            // Act
            await handler.Handle(request, CheckErrors());
            // Assert
            var job = savedUser.StaffMemberFk.Jobs.First();
            job.WardsJobs.Count.ShouldBe(2);
            job.WardsJobs.First().Ward.ShouldBe(ward1);
            job.WardsJobs.First().Job.ShouldBe(job);
            job.WardsJobs.Last().Ward.ShouldBe(ward2);
            job.WardsJobs.Last().Job.ShouldBe(job);
        }

        [Fact]
        public async Task Handle_GivenServiceCentres_ShouldSaveCentres()
        {
            // Arrange
            var request = CreateRequest();
            request.Job.ServiceCentres = new List<ServiceCentreType> { ServiceCentreType.InPatient, ServiceCentreType.OutPatient };

            User savedUser = null;
            var userManager = Substitute.For<IUserManager>();
            await userManager.CreateAsync(Arg.Do<User>(user => savedUser = user));
            var userRepository = Substitute.For<IUserRepository>();

            var user = new User();
            userRepository.GetAll().Returns(new List<User> { user }.AsQueryable().BuildMock());
            var handler = CreateHandler(userManager, userRepository: userRepository);

            // Act
            await handler.Handle(request, CheckErrors());
            // Assert
            var job = savedUser.StaffMemberFk.Jobs.First();
            job.JobServiceCentres.Count.ShouldBe(2);
            job.JobServiceCentres.First().ServiceCentre.ShouldBe(ServiceCentreType.InPatient);
            job.JobServiceCentres.First().Job.ShouldBe(job);
            job.JobServiceCentres.Last().ServiceCentre.ShouldBe(ServiceCentreType.OutPatient);
            job.JobServiceCentres.Last().Job.ShouldBe(job);
        }

        [Fact]
        public async Task Handle_GivenNonExistentWard_ShouldThrowException()
        {
            // Arrange
            var request = CreateRequest();
            request.Job.Wards = new List<long> { 1, 2 };

            var wardRepository = Substitute.For<IRepository<Ward, long>>();
            wardRepository.GetAsync(1).Returns(Task.FromResult(new Ward()));
            wardRepository.GetAsync(2).Returns(Task.FromResult<Ward>(null));

            var user = new User();
            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetAll().Returns(new List<User> { user }.AsQueryable().BuildMock());

            var handler = CreateHandler(Substitute.For<IUserManager>(), wardRepository: wardRepository, userRepository: userRepository);
            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request, CheckErrors()));
            // Assert
            exception.Message.ShouldBe("Ward not found");
        }

        [Fact]
        public async Task Handle_GivenRoles_ShouldAddToUser()
        {
            // Arrange
            var request = CreateRequest();
            request.Job.TeamRole = "CMD";
            request.AdminRole = "Admin";

            User savedUser = null;
            var userManager = Substitute.For<IUserManager>();
            await userManager.CreateAsync(Arg.Do<User>(user => savedUser = user));

            var user = new User();
            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetAll().Returns(new List<User> { user }.AsQueryable().BuildMock());

            var setStaffRoles = Substitute.For<ISetStaffRolesCommandHandler>();
            var handler = CreateHandler(userManager, userRepository: userRepository, setStaffRoles: setStaffRoles);
            // Act
            await handler.Handle(request, CheckErrors());
            // Assert
            await setStaffRoles.Received().Handle(Arg.Any<long>());
        }

        [Fact]
        public async Task Handle_DuplicateStaffCodeForAFacility_ShouldThrowException()
        {
            // Arrange
            var request = CreateRequest();
            request.StaffCode = "123";
            request.Job.FacilityId = 2;
            request.ContractStartDate = DateTime.Now;
            request.ContractEndDate = DateTime.Now.AddYears(2);

            var userManager = Substitute.For<IUserManager>();

            var user = new User 
            { 
                StaffMemberFk = new StaffMember()
                {
                    StaffCode = "123",
                    Jobs = new Collection<Job> { new Job() { FacilityId = 2} }
                }
            };
            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetAll().Returns(new List<User> { user }.AsQueryable().BuildMock());

            var handler = CreateHandler(userManager, userRepository: userRepository);

            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request, CheckErrors()));

            // Assert
            exception.Message.ShouldBe("Staff code exists already within the tenant");
        }

        [Fact]
        public async Task Handle_ContractStartDateIsLaterThanContractEndDate_ShouldThrowException()
        {
            // Arrange
            var request = CreateRequest();
            request.StaffCode = "123";
            request.Job.FacilityId = 2;
            request.ContractStartDate = DateTime.Now.AddYears(10);
            request.ContractEndDate = DateTime.Now.AddYears(5);
            request.User.DateOfBirth = DateTime.Now;

            var userManager = Substitute.For<IUserManager>();

            var handler = CreateHandler(userManager);

            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request, CheckErrors()));

            // Assert
            exception.Message.ShouldBe("Contract end date must be later than contract start date");
        }

        [Fact]
        public async Task Handle_ContractStartDateIsEarlierThanDateOfBirth_ShouldThrowException()
        {
            // Arrange
            var request = CreateRequest();
            request.StaffCode = "123";
            request.Job.FacilityId = 2;
            request.ContractStartDate = DateTime.Now;
            request.ContractEndDate = DateTime.Now.AddYears(10);
            request.User.DateOfBirth = DateTime.Now.AddYears(5);

            var userManager = Substitute.For<IUserManager>();

            var handler = CreateHandler(userManager);

            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request, CheckErrors()));

            // Assert
            exception.Message.ShouldBe("Date of birth must be earlier than contract start date");
        }

        private CreateStaffMemberCommandHandler CreateHandler(IUserManager userManager,
            IAbpSession abpSession = null, IPasswordHasher<User> passwordHasher = null, 
            IRepository<Ward, long> wardRepository = null, IRepository<User, long> userRepository = null,
            ISetStaffRolesCommandHandler setStaffRoles = null)
        {
            if (abpSession == null)
            {
                abpSession = Substitute.For<IAbpSession>();
                abpSession.TenantId.Returns(1);
            }

            passwordHasher ??= Substitute.For<IPasswordHasher<User>>();
            wardRepository ??= Substitute.For<IRepository<Ward, long>>();
            userRepository ??= Substitute.For<IRepository<User, long>>();
            setStaffRoles ??= Substitute.For<ISetStaffRolesCommandHandler>();
            return new CreateStaffMemberCommandHandler(userManager, _objectMapper, abpSession, passwordHasher,
                wardRepository, Substitute.For<IUserPolicy>(), userRepository, setStaffRoles);
        }

        private static CreateOrEditStaffMemberRequest CreateRequest()
        {
            var userFaker = new Faker<UserEditDto>();
            userFaker.RuleFor(x => x.Title, f => f.PickRandom<TitleType>());
            userFaker.RuleFor(x => x.Name, f => f.Name.FirstName());
            userFaker.RuleFor(x => x.Surname, f => f.Name.LastName());
            userFaker.RuleFor(x => x.MiddleName, f => f.Name.FirstName());
            userFaker.RuleFor(x => x.UserName, f => f.Internet.UserName());
            userFaker.RuleFor(x => x.Password, f => f.Internet.Password());
            userFaker.RuleFor(x => x.Gender, f => f.PickRandom<GenderType>());
            userFaker.RuleFor(x => x.DateOfBirth, f => f.Date.Past(20, DateTime.Now.AddYears(-20)));
            userFaker.RuleFor(x => x.IdentificationCode, f => f.Random.AlphaNumeric(10));
            userFaker.RuleFor(x => x.IdentificationType, f => f.PickRandom<IdentificationType>());
            userFaker.RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber());
            userFaker.RuleFor(x => x.EmailAddress, f => f.Internet.Email());
            userFaker.RuleFor(x => x.AltEmailAddress, f => f.Internet.Email());
            var user = userFaker.Generate();

            var faker = new Faker<CreateOrEditStaffMemberRequest>();

            faker.RuleFor(x => x.StaffCode, f => f.Random.AlphaNumeric(10));
            faker.RuleFor(x => x.ContractStartDate, f => f.Date.Past(2));
            faker.RuleFor(x => x.ContractEndDate, f => f.Date.Future());

            var updateStaffMemberRequest = faker.Generate();
            updateStaffMemberRequest.User = user;
            updateStaffMemberRequest.Job = new JobDto();
            return updateStaffMemberRequest;
        }

        private Action<IdentityResult> CheckErrors()
        {
            return _ => {};
        }
    }
}