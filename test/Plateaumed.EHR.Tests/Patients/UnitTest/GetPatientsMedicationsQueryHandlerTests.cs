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
    public class GetPatientsMedicationsQueryHandlerTests
	{
        private readonly IRepository<Patient, long> _patient = Substitute.For<IRepository<Patient, long>>();
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMapping = Substitute.For<IRepository<PatientCodeMapping, long>>();
        private readonly IRepository<AllInputs.Medication, long> _medications = Substitute.For<IRepository<AllInputs.Medication, long>>();

        [Fact]
        public async Task HandleGivenValidInputsShouldReturnPatientAndMedications()
        {  
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientCodeMapping.GetAll().Returns(GetPatientCodeMappings().BuildMock());
            _medications.GetAll().Returns(GetMedications().BuildMock());

            var handler = new GetPatientsMedicationsQueryHandler(_patient, _patientCodeMapping, _medications);

            var result = await handler.Handle(1,1);
            result.ShouldNotBeNull();
            result.PatientCode.ShouldNotBeNull();
            result.FirstName.ShouldNotBeNull();
            result.LastName.ShouldNotBeNull();
            result.PatientMedications.Count.ShouldBe(3);
        }

        [Fact]
        public async Task HandleGivenInvalidPatientIdShouldThrow()
        {
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientCodeMapping.GetAll().Returns(GetPatientCodeMappings().BuildMock());
            _medications.GetAll().Returns(GetMedications().BuildMock());

            var handler = new GetPatientsMedicationsQueryHandler(_patient, _patientCodeMapping, _medications);

            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(0,1));

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
                    PatientCodeMappings = new List<PatientCodeMapping>
                    {
                        new PatientCodeMapping
                        {
                            Id = 1,
                            PatientCode = $"1",
                            PatientId = 1,
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    FirstName = "Test2",
                    LastName = "Test_2",
                    MiddleName = "",
                    PatientCodeMappings = new List<PatientCodeMapping>
                    {
                        new PatientCodeMapping
                        {
                            Id=2,
                            PatientCode = $"12",
                            PatientId = 2,
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    FirstName = "Test3",
                    LastName = "Test_3",
                    MiddleName = "",
                    PatientCodeMappings = new List<PatientCodeMapping>
                    {
                        new PatientCodeMapping
                        {
                            Id = 3,
                            PatientCode = $"123",
                            PatientId = 3,
                        }
                    }
                },
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
                    FacilityId =1,
                    PatientId =2,
                },
                new()
                {
                    Id=3,
                    PatientCode = "3",
                    FacilityId =1,
                    PatientId =3,
                }
            }.AsQueryable();

        private static IQueryable<AllInputs.Medication> GetMedications()
            => new List<AllInputs.Medication>
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    ProductName = "Paracetamol"
                },
                new()
                {
                    Id = 2,
                    PatientId = 1,
                    ProductName = "Chloroquine"
                },
                new()
                {
                    Id = 3,
                    PatientId = 1,
                    ProductName = "Ibumol"
                }
            }.AsQueryable();
    }
}

