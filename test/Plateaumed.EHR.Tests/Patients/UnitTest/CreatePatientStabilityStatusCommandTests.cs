using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Handlers;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Command;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Staff;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Patients.UnitTest
{
    [Trait("Category", "Unit")]
    public class CreatePatientStabilityStatusCommandTests : AppTestBase
    {
        private readonly IRepository<Patient, long> _patientRepository = Substitute.For<IRepository<Patient, long>>();      
        private readonly IRepository<PatientEncounter, long> _patientEncounter = Substitute.For<IRepository<PatientEncounter, long>>();
        private readonly IAbpSession _abpSession;

        public CreatePatientStabilityStatusCommandTests() => _abpSession = Resolve<IAbpSession>();

        [Fact]
        public async Task HandleGivenValidPatientIdShouldSave()
        {
            // Arrange
            var request = new PatientStabilityRequestDto
            {
                EncounterId = 1,
                PatientId = 1,
                Status= Misc.PatientStabilityStatus.CriticallyIll
            };
            LoginAsDefaultTenantAdmin();


            _patientRepository.GetAll().Returns(GetPatients().BuildMock());
            _patientEncounter.GetAll().Returns(GetPatientEncounter().BuildMock());

            PatientStability savedResult = null;
            var repository = Substitute.For<IRepository<PatientStability, long>>();
            await repository.InsertAsync(Arg.Do<PatientStability>(result => savedResult = result));
            var handler = new CreatePatientStabilityStatusCommandHandler(_patientRepository, _patientEncounter, repository, _abpSession);

            // Act
            await handler.Handle(request);

            //Assert
            savedResult.ShouldNotBeNull();
            savedResult.PatientId.ShouldBe(request.PatientId);
            savedResult.Status.ShouldBe(request.Status);
        }

        [Fact]
        public async Task HandleGivenInValidPatientIdShouldThrow()
        {
            // Arrange
            var request = new PatientStabilityRequestDto
            {
                EncounterId = 1,
                PatientId = 0,
                Status = Misc.PatientStabilityStatus.CriticallyIll
            };
            LoginAsDefaultTenantAdmin();

            _patientRepository.GetAll().Returns(GetPatients().BuildMock());
            _patientEncounter.GetAll().Returns(GetPatientEncounter().BuildMock());

            PatientStability savedResult = null;
            var repository = Substitute.For<IRepository<PatientStability, long>>();
            await repository.InsertAsync(Arg.Do<PatientStability>(result => savedResult = result));
            var handler = new CreatePatientStabilityStatusCommandHandler(_patientRepository, _patientEncounter, repository, _abpSession);         

            //Act
            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(request));
            //Assert
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
                    MiddleName = ""
                },
                new()
                {
                    Id = 2,
                    FirstName = "Test2",
                    LastName = "Test_2",
                    MiddleName = ""
                }
          }.AsQueryable();

        private static IQueryable<PatientEncounter> GetPatientEncounter()
            => new List<PatientEncounter>
               {
                   new()
                   {
                       Id = 1,
                       PatientId = 1
                   },
                   new()
                   {
                       Id = 1,
                       PatientId = 2
                   }
               }.AsQueryable();
        
    }
}