using System;
using System.Threading.Tasks;
using Plateaumed.EHR.Procedures;
using System.Linq;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures.Dtos;
using Shouldly;
using Xunit;
namespace Plateaumed.EHR.Tests.Procedures.Integration
{
    [Trait("Category", "Integration")]
    public class ProcedureAppServiceTest : AppTestBase
    {
        private readonly IProcedureAppService _procedureAppService;
        public ProcedureAppServiceTest()
        {
            _procedureAppService = Resolve<IProcedureAppService>();
        }
        [MultiTenantFact]
        public async Task DeleteProcedure_Should_Delete_Procedure_With_Matching_Id()
        {
            //Arrange
            LoginAsDefaultTenantAdmin();

            var createdProcedure = UsingDbContext(ctx =>
            {
                var patient = GetPatient();
                ctx.Patients.Add(patient);
                ctx.SaveChanges();
                var procedure = CreateProcedureRequest(patient.Id);
                ctx.Procedures.Add(procedure);
                ctx.SaveChanges();
                return procedure;
            });
            //act
            await _procedureAppService.DeleteProcedure(createdProcedure.Id);
            //assert
            var deletedProcedure = UsingDbContext(ctx => 
                ctx.Procedures.FirstOrDefault(x => x.Id == createdProcedure.Id));
            deletedProcedure.ShouldNotBeNull();
            deletedProcedure.IsDeleted.ShouldBeTrue();
        }
        private static Patient GetPatient()
        {
            var patient = new Patient
            {
                FirstName = "John",
                LastName = "Joe",
                PhoneNumber = "08060421709",
                EmailAddress = "test@example.com",
                DateOfBirth = DateTime.Now,
                GenderType = GenderType.Male,
                Title = TitleType.Mr
            };
            return patient;
        }
        [MultiTenantFact]
        public async Task CreateSpecializedProcedureNurseDetail_Should_Create_Specialized_Procedure_Nurse_Detail()
        {
            //Arrange
            LoginAsDefaultTenantAdmin();
            var createdProcedure = UsingDbContext(ctx =>
            {
                var patient = GetPatient();
                ctx.Patients.Add(patient);
                ctx.SaveChanges();
                var procedure = CreateProcedureRequest(patient.Id);
                ctx.Procedures.Add(procedure);
                ctx.SaveChanges();
                return procedure;
            });
            var request = new CreateSpecializedProcedureNurseDetailCommand
            {
                ProcedureId = createdProcedure.Id,
                TimePatientReceived = TimeOnly.FromDateTime(DateTime.Now),
                CirculatingStaffMemberId = AbpSession.UserId.GetValueOrDefault(),
                ScrubStaffMemberId = AbpSession.UserId.GetValueOrDefault()
            };
            //act
            await _procedureAppService.CreateSpecializedProcedureNurseDetail(request);
            //assert
            var createdNurseDetail = UsingDbContext(ctx =>
                ctx.SpecializedProcedureNurseDetails.FirstOrDefault(x => x.ProcedureId == createdProcedure.Id));
            createdNurseDetail.ShouldNotBeNull();
            createdNurseDetail.ProcedureId.ShouldBe(createdProcedure.Id);
            createdNurseDetail.CirculatingStaffMemberId.ShouldBe(AbpSession.UserId.Value);
            createdNurseDetail.ScrubStaffMemberId.ShouldBe(AbpSession.UserId.Value);
        }
        private static Procedure CreateProcedureRequest(long patientId)
        {

            return new Procedure
            {
                PatientId = patientId,
                ProcedureType = ProcedureType.RequestProcedure,
                Note = "Test",
                SnowmedId = 1,
                ProcedureEntryType = ProcedureEntryType.Intraop,
                TenantId = 1
            };
        }
    }
}
