using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Query;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Patients.UnitTest
{
    [Trait("Category", "Unit")]

    public class GetPatientStabilityStatusQueryHandlerTests
	{
        private readonly IRepository<Patient, long> _patientRepository = Substitute.For<IRepository<Patient, long>>();
        private readonly IRepository<PatientStability, long> _patientStability = Substitute.For<IRepository<PatientStability, long>>();
        private readonly IRepository<PatientEncounter, long> _patientEncounter = Substitute.For<IRepository<PatientEncounter, long>>();

        [Fact]
        public async Task HandleGivenValidInputShouldReturnStabilityStatus()
        {
            // Arrange
            _patientRepository.GetAll().Returns(GetPatients().BuildMock());
            _patientStability.GetAll().Returns(GetStabilityStatus().BuildMock());
            _patientEncounter.GetAll().Returns(GetPatientEncounter().BuildMock());
            
            var handler = new GetPatientStabilityStatusQueryHandler(_patientRepository, _patientStability, _patientEncounter);

            // Act
            var result = await handler.Handle(1,1);

            //Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);           
        }

        [Fact]
        public async Task HandleGivenInvalidPatientIdShouldThrow()
        {
            //Arrange
            _patientRepository.GetAll().Returns(GetPatients().BuildMock());
            _patientStability.GetAll().Returns(GetStabilityStatus().BuildMock());
            _patientEncounter.GetAll().Returns(GetPatientEncounter().BuildMock());

            var handler = new GetPatientStabilityStatusQueryHandler(_patientRepository, _patientStability, _patientEncounter);

            //Act
            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(0,0));
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
                { Id = 1, PatientId = 1 },
                new()
                { Id = 2, PatientId =1 }
           }.AsQueryable();

        private static IQueryable<PatientStability> GetStabilityStatus()
            => new List<PatientStability>
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    Status = Misc.PatientStabilityStatus.CriticallyIll,
                    EncounterId = 1,
                    CreationTime = DateTime.Now.AddHours(1),
                    CreatorUserId = 1
                },
                new()
                {
                    Id = 2,
                    PatientId = 1,
                    Status = Misc.PatientStabilityStatus.Stable,
                    EncounterId = 1,
                    CreationTime = DateTime.Now.AddHours(12),
                    CreatorUserId = 2
                }
            }.AsQueryable();            
    }
}

