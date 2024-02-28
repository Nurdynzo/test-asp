using Abp.Domain.Repositories;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Facilities.Handler;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Abp.ObjectMapping;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using MockQueryable.NSubstitute;
using Abp.Runtime.Session;
using Plateaumed.EHR.MultiTenancy;

namespace Plateaumed.EHR.Tests.Facilities
{
    public  class GetUserFacilityQueryHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenValidEmailAddress_ShouldReturnUserFacilityViewDto()
        {
            // Arrange
            var emailAddress = "test@example.com";

            var user = new User
            {
                EmailAddress = emailAddress,
                IsEmailConfirmed = true
            };
            var tenant = new Tenant("Admin", "Admin", TenantType.Individual);

            var userRepository = Substitute.For<IRepository<User, long>>();
            userRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<User, bool>>>())
                .Returns(Task.FromResult(user));

            var facilityRepository = Substitute.For<IRepository<Facility, long>>();
            facilityRepository.GetAll().Returns(CreateFacilityTestData());

            var jobTitleRepository = Substitute.For<IRepository<JobTitle, long>>();
            jobTitleRepository.GetAll().Returns(CreateJobTitleTestData());

            var tenantRepository = Substitute.For<IRepository<Tenant>>();
            var abpSession = Substitute.For<IAbpSession>();
            tenantRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Tenant, bool>>>())
                .Returns(Task.FromResult(tenant));
            abpSession.TenantId.Returns(tenant.Id);

            var handler = new GetUserFacilityQueryHandler(
                facilityRepository,
                jobTitleRepository,
                userRepository,
                _objectMapper,
                tenantRepository,
                abpSession
            );

            // Act
            var result = await handler.Handle(emailAddress);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<GetUserFacilityViewDto>>();
            

            var userFacilityViewDto = result.First();
            userFacilityViewDto.UserFacility.ShouldNotBeNull();
            userFacilityViewDto.UserFacilityJobTitleDto.ShouldNotBeNull();

            Assert.True(userFacilityViewDto.UserFacility.IsEmailConfirmed);
            Assert.True(userFacilityViewDto.UserFacility.IsActive);
            Assert.Equal("Admin", userFacilityViewDto.UserFacility.TenancyName);
            Assert.Equal("Job Title 1", userFacilityViewDto.UserFacilityJobTitleDto[0].Name);

        }


        [Fact]
        public async Task Handle_GivenInvalidEmailAddress_ShouldThrowUserFriendlyException()
        {
            // Arrange
            var emailAddress = "nonexistent@example.com";

            var userRepository = Substitute.For<IRepository<User, long>>();
            userRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<User, bool>>>())
                .Returns(Task.FromResult<User>(null));

            var facilityRepository = Substitute.For<IRepository<Facility, long>>();
            var jobTitleRepository = Substitute.For<IRepository<JobTitle, long>>();
            var tenantRepository = Substitute.For<IRepository<Tenant>>();
            var abpSession = Substitute.For<IAbpSession>();

            var handler = new GetUserFacilityQueryHandler(facilityRepository, jobTitleRepository, userRepository, _objectMapper, tenantRepository,
                abpSession);

            // Act & Assert
            await Assert.ThrowsAsync<UserFriendlyException>(async () => await handler.Handle(emailAddress));
        }


        public static IQueryable<Facility> CreateFacilityTestData()
        {
            var facilities = new List<Facility>
        {
            new Facility
            {
                Id = 1,
                Name = "Blessing Hospital Ikeja",
                EmailAddress = "test@example.com",
                IsActive = true,
                LogoId = Guid.NewGuid(),
            }
        };

            return facilities.AsQueryable().BuildMock();
        }

        public static IQueryable<JobTitle> CreateJobTitleTestData()
        {
            var jobTitles = new List<JobTitle>
        {
            new JobTitle
            {
                FacilityId = 1,
                Name = "Job Title 1"
            }
        };

            return jobTitles.AsQueryable().BuildMock();
        }

    }

}
