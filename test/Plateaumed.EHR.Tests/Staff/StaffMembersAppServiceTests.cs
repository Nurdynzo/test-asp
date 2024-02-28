using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NPOI.SS.Formula.Functions;
using NSubstitute;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Authorization.Users.Dto;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Staff.Handlers;

namespace Plateaumed.EHR.Tests.Staff
{
    [Trait("Category", "Integration")]
    public class StaffMembersAppServiceTests : AppTestBase
    {
        private readonly IStaffMembersAppService _staffMembersAppService;

        public StaffMembersAppServiceTests()
        {
            _staffMembersAppService = Resolve<IStaffMembersAppService>();
        }

        [Fact]
        public async Task GetAll_GivenFilters_ShouldReturnFilteredStaffMembers()
        {
            // Arrange
            await LoginAsCustomTenantWithPermissions(AppPermissions.Pages_StaffMembers);
            var tenantId = AbpSession.TenantId.Value;


            var (facility2, user2) = UsingDbContext(context =>
            {
                var facilityGroup = TestFacilityGroupBuilder.Create(tenantId).Save(context);
                var facility1 = TestFacilityBuilder.Create(tenantId, facilityGroup.Id).Save(context);
                var facility2 = TestFacilityBuilder.Create(tenantId, facilityGroup.Id).Save(context);

                var user1 = TestUserBuilder.Create(tenantId).Save(context);
                var user2 = TestUserBuilder.Create(tenantId).Save(context);
                var user3 = TestUserBuilder.Create(tenantId).Save(context);

                TestStaffMemberBuilder.Create().WithUser(user1).WithFacility(facility1)
                    .Save(context);
                TestStaffMemberBuilder.Create().WithUser(user2).WithFacility(facility2)
                    .Save(context);
                TestStaffMemberBuilder.Create().WithUser(user3).WithFacility(facility1)
                    .Save(context);
                return (facility2, user2);

            });

            var request = new GetAllStaffMembersRequest
            {
                FacilityIdFilter = facility2.Id
            };

            // Act
            var result = await _staffMembersAppService.GetAll(request);
            // Assert
            result.TotalCount.ShouldBe(1);
            result.Items[0].Id.ShouldBe(user2.Id);
        }

        [Fact]
        public async Task GetAllGetAllStaffWithJobs_GivenFilters_ShouldReturnFilteredStaffMembersWithJobs()
        {
            // Arrange
            await LoginAsCustomTenantWithPermissions(AppPermissions.Pages_StaffMembers);
            var tenantId = AbpSession.TenantId.Value;


            var (facility2, user2) = UsingDbContext(context =>
            {
                var facilityGroup = TestFacilityGroupBuilder.Create(tenantId).Save(context);
                var facility1 = TestFacilityBuilder.Create(tenantId, facilityGroup.Id).Save(context);
                var facility2 = TestFacilityBuilder.Create(tenantId, facilityGroup.Id).Save(context);

                var user1 = TestUserBuilder.Create(tenantId).Save(context);
                var user2 = TestUserBuilder.Create(tenantId).Save(context);
                var user3 = TestUserBuilder.Create(tenantId).Save(context);

                TestStaffMemberBuilder.Create().WithUser(user1).WithFacility(facility1)
                    .Save(context);
                TestStaffMemberBuilder.Create().WithUser(user2).WithFacility(facility2)
                    .Save(context);
                TestStaffMemberBuilder.Create().WithUser(user3).WithFacility(facility1)
                    .Save(context);
                return (facility2, user2);

            });

            var request = new GetStaffMembersWithJobsRequest
            {
                FacilityIdFilter = facility2.Id
            };

            // Act
            var result = await _staffMembersAppService.GetAllStaffWithJobs(request);
            // Assert
            result.TotalCount.ShouldBe(1);
        }


        [Fact]
        public async Task Update_GivenExistingWards_ShouldUpdateWardsUpdate_GivenExistingWards_ShouldUpdateWards()
        {
            // Arrange
            await LoginAsCustomTenantWithPermissions(AppPermissions.Pages_StaffMembers, AppPermissions.Pages_StaffMembers_Edit);
            var tenantId = AbpSession.TenantId.Value;

            var (user, ward1, ward2, ward3) = UsingDbContext(context =>
            {
                var facilityGroup = TestFacilityGroupBuilder.Create(tenantId).Save(context);
                var facility = TestFacilityBuilder.Create(tenantId, facilityGroup.Id).Save(context);
                var unit = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility.Id, "Unit").Save(context);
                var dept = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility.Id, "Dept").WithChildren(unit).Save(context);
                var ward1 = TestWardBuilder.Create(tenantId, facility.Id).Save(context);
                var ward2 = TestWardBuilder.Create(tenantId, facility.Id).Save(context);
                var ward3 = TestWardBuilder.Create(tenantId, facility.Id).Save(context);

                var user = TestUserBuilder.Create(tenantId).Save(context);
                var staffMember = TestStaffMemberBuilder.Create().WithUser(user).Save(context);
                var job = TestJobBuilder.Create(tenantId).AsPrimary().WithStaffMember(staffMember).WithUnit(unit)
                    .WithDepartment(dept).WithWard(ward1).WithWard(ward2).Save(context);
                job.StaffMember = staffMember;
                return (user, ward1, ward2, ward3);
            });

            // Act
            var request = CreateRequest();
            request.User.Id = user.Id;
            request.Job.Wards = new List<long> { ward2.Id, ward3.Id };
            await _staffMembersAppService.CreateOrEdit(request);

            var job = UsingDbContext(context =>
            {
                return context.Users.Include(u => u.StaffMemberFk.Jobs).ThenInclude(j => j.WardsJobs)
                    .First(u => u.Id == user.Id).StaffMemberFk.Jobs.First();
            });

            // Assert
            var wardJobs = job.WardsJobs.Where(x => !x.IsDeleted).ToList();
            wardJobs.Count.ShouldBe(2);
            wardJobs.First().WardId.ShouldBe(ward2.Id);
            wardJobs.Last().WardId.ShouldBe(ward3.Id);
        }

        [Fact]
        public async Task Handle_ActivateOrDeactivateStaffMember_ShouldUpdate()
        {
            // Arrange
            var request = new ActivateOrDeactivateStaffMemberRequest
            {
                IsActive = false,
                Id = 1
            };

            var user = new User
            {
                Id = 1,
                IsActive = true
            };
            var staff = new StaffMember { Id = 1, UserFk = user, UserId = user.Id };

            var staffRepository = NSubstitute.Substitute.For<IRepository<StaffMember, long>>();
            staffRepository.GetAll().Returns(new List<StaffMember> { staff }.AsQueryable().BuildMock());

            var handler = CreateHandler(staffMemberRepository: staffRepository);

            // Act
            await handler.Handle(request);

            // Assert
            staff.UserFk.IsActive.ShouldBe(false);
        }


        [Fact]
        public async Task Create_GivenValidRequest_ShouldSave()
        {
            // Arrange
            await LoginAsCustomTenantWithPermissions(AppPermissions.Pages_StaffMembers, AppPermissions.Pages_StaffMembers_Create);
            var tenantId = AbpSession.TenantId.Value;

            var ward = UsingDbContext(context =>
            {
                var facilityGroup = TestFacilityGroupBuilder.Create(tenantId).Save(context);
                var facility = TestFacilityBuilder.Create(tenantId, facilityGroup.Id).Save(context);
                var unit = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility.Id, "Unit").Save(context);
                var dept = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility.Id, "Dept").WithChildren(unit).Save(context);
                var ward = TestWardBuilder.Create(tenantId, facility.Id).Save(context);
              
                return ward;
            });

            // Act
            var request = CreateRequest();
            request.Job.Wards = new List<long>{ward.Id};
            request.Job.ServiceCentres = new List<ServiceCentreType> { ServiceCentreType.InPatient };
            await _staffMembersAppService.CreateOrEdit(request);

            var savedUser = UsingDbContext(context =>
            {
                return context.Users.Include(u => u.Roles)
                    .Include(u => u.StaffMemberFk.Jobs)
                    .ThenInclude(j => j.WardsJobs)
                    .Include(u => u.StaffMemberFk.Jobs)
                    .ThenInclude(j => j.JobServiceCentres)
                    .First(u => u.StaffMemberFk.StaffCode == request.StaffCode);
            });

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
            var job = savedUser.StaffMemberFk.Jobs.First();
            job.JobLevelId.ShouldBe(request.Job.JobLevelId);
            job.FacilityId.ShouldBe(request.Job.FacilityId);
            job.IsPrimary.ShouldBe(true);
            job.TenantId.ShouldBe(tenantId);
        }

        [Fact]
        public async Task Create_GivenRoles_ShouldSave()
        {
            // Arrange
            await LoginAsCustomTenantWithPermissions(AppPermissions.Pages_StaffMembers, AppPermissions.Pages_StaffMembers_Create);
            var tenantId = AbpSession.TenantId.Value;

            UsingDbContext(context =>
            {
                var doctor = new Role(tenantId, StaticRoleNames.JobRoles.Doctor, StaticRoleNames.JobRoles.Doctor);
                doctor.SetNormalizedName();
                context.Roles.Add(doctor);
                var admin = new Role(tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin);
                admin.SetNormalizedName();
                context.Roles.Add(admin);
                context.SaveChangesAsync();

            });
            var request = CreateRequest();
            request.AdminRole = StaticRoleNames.Tenants.Admin;
            request.Job.TeamRole = StaticRoleNames.JobRoles.Doctor;

            // Act

            await _staffMembersAppService.CreateOrEdit(request);

            var savedUser = UsingDbContext(context =>
            {
                return context.Users.Include(u => u.Roles)
                    .Include(u => u.StaffMemberFk.AdminRole)
                    .Include(u => u.StaffMemberFk.Jobs)
                    .ThenInclude(j => j.TeamRole)
                    .First(u => u.StaffMemberFk.StaffCode == request.StaffCode);
            });

            // Assert
            savedUser.Roles.Count.ShouldBe(2);
            savedUser.StaffMemberFk.AdminRole.Name.ShouldBe(request.AdminRole);
            var job = savedUser.StaffMemberFk.Jobs.First();
            job.TeamRole.Name.ShouldBe(request.Job.TeamRole);
        }

        [Fact]
        public async Task Update_GivenExistingServiceCenters_ShouldUpdateServiceCentres()
        {
            // Arrange
            await LoginAsCustomTenantWithPermissions(AppPermissions.Pages_StaffMembers, AppPermissions.Pages_StaffMembers_Edit);
            var tenantId = AbpSession.TenantId.Value;

            var user = UsingDbContext(context =>
            {
                var facilityGroup = TestFacilityGroupBuilder.Create(tenantId).Save(context);
                var facility = TestFacilityBuilder.Create(tenantId, facilityGroup.Id).Save(context);
                var unit = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility.Id, "Unit").Save(context);
                var dept = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility.Id, "Dept").WithChildren(unit).Save(context);

                var user = TestUserBuilder.Create(tenantId).Save(context);
                var staffMember = TestStaffMemberBuilder.Create().WithUser(user).Save(context);
                var job = TestJobBuilder.Create(tenantId).AsPrimary().WithStaffMember(staffMember).WithServiceCentre(ServiceCentreType.InPatient)
                    .WithServiceCentre(ServiceCentreType.OutPatient).WithUnit(unit)
                    .WithDepartment(dept).Save(context);
                job.StaffMember = staffMember;
                return user;
            });

            // Act
            var request = CreateRequest();
            request.User.Id = user.Id;
            request.Job.ServiceCentres = new List<ServiceCentreType> { ServiceCentreType.InPatient, ServiceCentreType.AccidentAndEmergency};
            await _staffMembersAppService.CreateOrEdit(request);

            var job = UsingDbContext(context =>
            {
                return context.Users.Include(u => u.StaffMemberFk.Jobs).ThenInclude(j => j.JobServiceCentres)
                    .First(u => u.Id == user.Id).StaffMemberFk.Jobs.First();
            });

            // Assert
            var centres = job.JobServiceCentres.Where(x => !x.IsDeleted).ToList();
            centres.Count.ShouldBe(2);
            centres.First().ServiceCentre.ShouldBe(ServiceCentreType.InPatient);
            centres.Last().ServiceCentre.ShouldBe(ServiceCentreType.AccidentAndEmergency);
        }

        [Fact]
        public async Task Update_GivenChangedRoles_ShouldSaveChanges()
        {
            // Arrange
            await LoginAsCustomTenantWithPermissions(AppPermissions.Pages_StaffMembers, AppPermissions.Pages_StaffMembers_Edit);
            var tenantId = AbpSession.TenantId.Value;

            var user = UsingDbContext(context =>
            {
                var doctor = new Role(tenantId, StaticRoleNames.JobRoles.Doctor, StaticRoleNames.JobRoles.Doctor);
                doctor.SetNormalizedName();
                context.Roles.Add(doctor);
                var nurse = new Role(tenantId, StaticRoleNames.JobRoles.Nurse, StaticRoleNames.JobRoles.Nurse);
                nurse.SetNormalizedName();
                context.Roles.Add(nurse);
                var admin = new Role(tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin);
                admin.SetNormalizedName();
                context.Roles.Add(admin);
                var basicUser = new Role(tenantId, StaticRoleNames.Tenants.User, StaticRoleNames.Tenants.User);
                basicUser.SetNormalizedName();
                context.Roles.Add(basicUser);
                var labs = new Role(tenantId, StaticRoleNames.JobRoles.LaboratoryScientist, StaticRoleNames.JobRoles.LaboratoryScientist);
                basicUser.SetNormalizedName();
                context.Roles.Add(basicUser);

                var user = TestUserBuilder.Create(tenantId).WithRoleId(doctor.Id).WithRoleId(admin.Id).WithRoleId(labs.Id).Save(context);
                var staffMember = TestStaffMemberBuilder.Create().WithUser(user).WithAdminRole(admin).Save(context);
                TestJobBuilder.Create(tenantId).AsPrimary().WithStaffMember(staffMember).WithTeamRole(doctor).Save(context);
                TestJobBuilder.Create(tenantId).WithStaffMember(staffMember).WithTeamRole(labs).Save(context);
                return user;
            });

            // Act
            var request = CreateRequest();
            request.User.Id = user.Id;
            request.AdminRole = StaticRoleNames.Tenants.User;
            request.Job.TeamRole = StaticRoleNames.JobRoles.Nurse;

            await _staffMembersAppService.CreateOrEdit(request);

            var savedUser = UsingDbContext(context =>
            {
                return context.Users.Include(u => u.Roles)
                    .Include(u => u.StaffMemberFk.AdminRole)
                    .Include(u => u.StaffMemberFk.Jobs)
                    .ThenInclude(j => j.TeamRole)
                    .First(u => u.Id == user.Id);
            });

            // Assert
            savedUser.Roles.Count.ShouldBe(3);
            savedUser.StaffMemberFk.AdminRole.Name.ShouldBe(request.AdminRole);
            var job = savedUser.StaffMemberFk.Jobs.First();
            job.TeamRole.Name.ShouldBe(request.Job.TeamRole);
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
            updateStaffMemberRequest.Job = new JobDto { IsPrimary = true };
            return updateStaffMemberRequest;
        }


        private ActivateOrDeactivateStaffMemberHandler CreateHandler(IRepository<StaffMember, long> staffMemberRepository = null)
        {
            staffMemberRepository ??= NSubstitute.Substitute.For<IRepository<StaffMember, long>>();
            return new ActivateOrDeactivateStaffMemberHandler(staffMemberRepository);
        }
    }
}