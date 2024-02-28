using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Staff
{
    [Trait("Category", "Integration")]
    public class JobAppServiceTests : AppTestBase
    {
        [Fact]
        public async Task Create_GivenValidRequest_ShouldCreateJob()
        {
            // Arrange
            await LoginAsCustomTenantWithPermissions(AppPermissions.Pages_StaffMembers_Create, AppPermissions.Pages_StaffMembers);
            var tenantId = AbpSession.TenantId.Value;
            var (user, staffMember, facility, jobLevel, unit, dept, ward1, ward2) = UsingDbContext(context =>
            {
                TestRoleBuilder.Create(tenantId).WithName(StaticRoleNames.JobRoles.Nurse).Save(context);
                var group = TestFacilityGroupBuilder.Create(tenantId).Save(context);
                var facility = TestFacilityBuilder.Create(tenantId, group.Id).Save(context);
                var jobTitle = TestJobTitleBuilder.Create(tenantId).Save(context);
                var jobLevel = TestJobLevelBuilder.Create(tenantId).WithJobTitle(jobTitle).Save(context);
                var unit = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility.Id, "Unit").Save(context);
                var dept = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility.Id, "Dept").WithChildren(unit)
                    .Save(context);
                var user = TestUserBuilder.Create(tenantId).Save(context);
                var staffMember = TestStaffMemberBuilder.Create().WithUser(user).Save(context);
                var ward1 = TestWardBuilder.Create(tenantId, facility.Id).Save(context);
                var ward2 = TestWardBuilder.Create(tenantId, facility.Id).Save(context);
                return (user, staffMember, facility, jobLevel, unit, dept, ward1, ward2);
            });

            var request = new CreateOrEditJobRequest
            {
                UserId = user.Id,
                Job = new JobDto
                {
                    IsPrimary = true,
                    TeamRole = StaticRoleNames.JobRoles.Nurse,
                    FacilityId = facility.Id,
                    DepartmentId = dept.Id,
                    UnitId = unit.Id,
                    JobLevelId = jobLevel.Id,
                    JobTitleId = jobLevel.JobTitleId,
                    ServiceCentres = new List<ServiceCentreType>
                        { ServiceCentreType.InPatient, ServiceCentreType.OutPatient },
                    Wards = new List<long> { ward1.Id, ward2.Id }
                }
            };
            var appService = Resolve<IJobAppService>();
            
            // Act
            await appService.Create(request);
            var job = UsingDbContext(context => context.Jobs.Include(j =>j.TeamRole)
                .Include(j => j.JobLevel).Include(j => j.WardsJobs)
                .Include(j => j.JobServiceCentres)
                .First(j => j.StaffMemberId == staffMember.Id));
            
            // Assert
            job.IsPrimary.ShouldBe(request.Job.IsPrimary);
            job.TeamRole.Name.ShouldBe(request.Job.TeamRole);
            job.FacilityId.ShouldBe(request.Job.FacilityId);
            job.DepartmentId.ShouldBe(request.Job.DepartmentId);
            job.JobLevelId.ShouldBe(request.Job.JobLevelId);
            job.JobLevel.JobTitleId.ShouldBe(request.Job.JobTitleId.Value);
            job.UnitId.ShouldBe(request.Job.UnitId);
            job.WardsJobs.Count.ShouldBe(request.Job.Wards.Count);
            job.JobServiceCentres.Count.ShouldBe(request.Job.ServiceCentres.Count);
        }

        [Fact]
        public async Task Update_GivenValidRequest_ShouldCreateJob()
        {
            // Arrange
            await LoginAsCustomTenantWithPermissions(AppPermissions.Pages_StaffMembers_Create, AppPermissions.Pages_StaffMembers);
            var tenantId = AbpSession.TenantId.Value;
            var (user, staffMember, facility, jobLevel, unit, dept, ward1, ward2, job, admin, doctor) = UsingDbContext(context =>
            {
                var nurse = TestRoleBuilder.Create(tenantId).WithName(StaticRoleNames.JobRoles.Nurse).Save(context);
                var doctor = TestRoleBuilder.Create(tenantId).WithName(StaticRoleNames.JobRoles.Doctor).Save(context);
                var admin = TestRoleBuilder.Create(tenantId).WithName(StaticRoleNames.Tenants.Admin).Save(context);
                var group = TestFacilityGroupBuilder.Create(tenantId).Save(context);
                var facility = TestFacilityBuilder.Create(tenantId, group.Id).Save(context);
                var jobTitle = TestJobTitleBuilder.Create(tenantId).Save(context);
                var jobLevel = TestJobLevelBuilder.Create(tenantId).WithJobTitle(jobTitle).Save(context);
                var unit = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility.Id, "Unit").Save(context);
                var dept = TestOrganizationUnitExtendedBuilder.Create(tenantId, facility.Id, "Dept")
                    .WithChildren(unit).Save(context);
                var user = TestUserBuilder.Create(tenantId).Save(context);
                var staffMember = TestStaffMemberBuilder.Create().WithUser(user).WithAdminRole(admin).Save(context);
                var job = TestJobBuilder.Create(tenantId).WithStaffMember(staffMember)
                    .WithServiceCentre(ServiceCentreType.AccidentAndEmergency).WithTeamRole(nurse).Save(context);
                var ward1 = TestWardBuilder.Create(tenantId, facility.Id).Save(context);
                var ward2 = TestWardBuilder.Create(tenantId, facility.Id).Save(context);
                return (user, staffMember, facility, jobLevel, unit, dept, ward1, ward2, job, admin, doctor);
            });

            var request = new CreateOrEditJobRequest
            {
                UserId = user.Id,
                Job = new JobDto
                {
                    Id = job.Id,
                    IsPrimary = true,
                    TeamRole = StaticRoleNames.JobRoles.Doctor,
                    FacilityId = facility.Id,
                    DepartmentId = dept.Id,
                    UnitId = unit.Id,
                    JobLevelId = jobLevel.Id,
                    JobTitleId = jobLevel.JobTitleId,
                    ServiceCentres = new List<ServiceCentreType>
                        { ServiceCentreType.InPatient, ServiceCentreType.OutPatient },
                    Wards = new List<long> { ward1.Id, ward2.Id }
                }
            };
            var appService = Resolve<IJobAppService>();

            // Act
            await appService.Update(request);
            var savedJob = UsingDbContext(context => context.Jobs.Include(j => j.TeamRole)
                .Include(j => j.JobLevel).Include(j => j.WardsJobs)
                .Include(j => j.JobServiceCentres)
                .First(j => j.StaffMemberId == staffMember.Id));

            var savedUser = UsingDbContext(context => context.Users.Include(u => u.Roles)
                           .First(u => u.Id == request.UserId));

            // Assert
            savedJob.IsPrimary.ShouldBe(request.Job.IsPrimary);
            savedJob.TeamRole.Name.ShouldBe(request.Job.TeamRole);
            savedJob.FacilityId.ShouldBe(request.Job.FacilityId);
            savedJob.DepartmentId.ShouldBe(request.Job.DepartmentId);
            savedJob.JobLevelId.ShouldBe(request.Job.JobLevelId);
            savedJob.JobLevel.JobTitleId.ShouldBe(request.Job.JobTitleId.Value);
            savedJob.UnitId.ShouldBe(request.Job.UnitId);
            savedJob.WardsJobs.Count.ShouldBe(request.Job.Wards.Count);

            var serviceCentres = savedJob.JobServiceCentres.Where(d => !d.IsDeleted).ToList();
            serviceCentres[0].ServiceCentre.ShouldBe(ServiceCentreType.InPatient);
            serviceCentres[1].ServiceCentre.ShouldBe(ServiceCentreType.OutPatient);

            savedUser.Roles.Count.ShouldBe(2);
            savedUser.Roles.ShouldContain(r => r.RoleId == admin.Id);
            savedUser.Roles.ShouldContain(r => r.RoleId == doctor.Id);
        }
    }
}
