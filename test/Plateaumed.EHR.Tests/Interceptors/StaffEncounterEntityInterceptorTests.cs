using System;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.InputNotes;
using Plateaumed.EHR.InputNotes.Dtos;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Interceptors
{
    [Trait("Category", "Integration")]
    public class StaffEncounterEntityInterceptorTests: AppTestBase
    {
        [Fact]
        public async Task SaveChangesAsync_GivenChangesToAnEncounterEntity_ShouldCreateStaffEncounter()
        {
            // Arrange

            await LoginAsCustomTenantWithPermissions(AppPermissions.Pages_InputNotes_Create, AppPermissions.Pages_InputNotes);

            var tenantId = AbpSession.TenantId.Value;
            var userId = AbpSession.UserId;

            var (staffMember, patientEncounter) = UsingDbContext(context =>
            {
                var user = context.Users.Find(userId);
                
                var role = new Role(tenantId, "Doctor");
                var staffMember = TestStaffMemberBuilder.Create().WithUser(user).WithAdminRole(role).Save(context);
                var patient = context.Patients.Add(GetPatient()).Entity;
                context.SaveChanges();
                var encounter = context.PatientEncounters.Add(new PatientEncounter{TenantId = tenantId,  PatientId = patient.Id}).Entity;
                context.SaveChanges();
                return (staffMember, encounter);
            });

            var appService = Resolve<IInputNotesAppService>();
            // Act
            await appService.CreateInputNotes(new CreateInputNotesDto
            {
                EncounterId = patientEncounter.Id,
                PatientId = patientEncounter.PatientId,
                Description = "test"
            });

            var staffEncounters = UsingDbContext(context =>
                context.StaffEncounters.Where(x =>
                    x.EncounterId == patientEncounter.Id && x.StaffId == staffMember.Id).ToList());
            //Assert
            staffEncounters.Count.ShouldBe(1);
        }

        private static Patient GetPatient()
        {
            return new Patient
            {
                FirstName = "Test",
                LastName = "User",
                EmailAddress = "test@user.com",
                PhoneNumber = "1234567890",
                GenderType = GenderType.Female,
                DateOfBirth = DateTime.Now.AddYears(-40),
                UuId = Guid.NewGuid()
            };
        }
    }
}
