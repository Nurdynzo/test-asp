using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Procedures.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Procedures
{
    [Trait("Category", "Unit")]
    public class GetPatientInterventionsQueryHandlerTests
	{
        private readonly IRepository<Patient, long> _patient = Substitute.For<IRepository<Patient, long>>();
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMapping = Substitute.For<IRepository<PatientCodeMapping, long>>();
        private readonly IRepository<Procedure, long> _procedure = Substitute.For<IRepository<Procedure, long>>();
        private readonly IAbpSession _abpSession = Substitute.For<IAbpSession>();
        private readonly IRepository<User, long> _userRepository = Substitute.For<IRepository<User, long>>();

        [Fact]
        public async Task HandleGivenValidInputsShouldReturnPatientAndInterventions()
        {
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientCodeMapping.GetAll().Returns(GetPatientCodeMappings().BuildMock());
            _procedure.GetAll().Returns(GetProcedure().BuildMock());
            _abpSession.TenantId.Returns(1);

            var handler = new GetPatientInterventionsQueryHandler(_patient, _patientCodeMapping, _procedure, _abpSession, _userRepository);

            var result = await handler.Handle(1, 1);
            result.ShouldNotBeNull();
            result.PatientCode.ShouldNotBeNull();
            result.FirstName.ShouldNotBeNull();
            result.LastName.ShouldNotBeNull();
            result.Interventions.Count.ShouldBe(1);
        }

        [Fact]
        public async Task HandleGivenInvalidPatientIdShouldThrow()
        {
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientCodeMapping.GetAll().Returns(GetPatientCodeMappings().BuildMock());
            _procedure.GetAll().Returns(GetProcedure().BuildMock());
            _abpSession.UserId.Returns(1);

            var handler = new GetPatientInterventionsQueryHandler(_patient, _patientCodeMapping, _procedure, _abpSession, _userRepository);

            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(0, 1));

            exception.Message.ShouldBe("Patient not found");
        }

        private static IQueryable<Patient> GetPatients()
            => new List<Patient>
            {
                new()
                {
                    Id = 1,
                    FirstName = "Test1",
                    LastName = "Test_1",
                    MiddleName = "",
                    PatientCodeMappings = GetPatientCodeMappings().ToList()
                }
            }.AsQueryable();

        private static IQueryable<PatientCodeMapping> GetPatientCodeMappings()
            => new List<PatientCodeMapping>
            {
                new()
                {
                    Id = 1,
                    PatientCode = "1",
                    FacilityId =1,
                    PatientId =1,
                },
                new()
                {
                    Id=2,
                    PatientCode = "2",
                    FacilityId =2,
                    PatientId =1,
                }
            }.AsQueryable();

        private static IQueryable<Procedure> GetProcedure()
            => new List<Procedure>
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    SelectedProcedures = "[{\"SnowmedId\":20127008,\"ProcedureName\":\"Application of dressing, fixed\"}]",
                    TenantId = 1
                }
            }.AsQueryable();
    }
}
