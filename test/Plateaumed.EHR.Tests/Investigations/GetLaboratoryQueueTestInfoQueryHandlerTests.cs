using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class GetLaboratoryQueueTestInfoQueryHandlerTests
    {
        private readonly IRepository<InvestigationRequest, long> _investigationRequests = Substitute.For<IRepository<InvestigationRequest, long>>();
        private readonly IRepository<Patient, long> _patient = Substitute.For<IRepository<Patient, long>>();
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMapping = Substitute.For<IRepository<PatientCodeMapping, long>>();
        private readonly IRepository<User, long> _user = Substitute.For<IRepository<User, long>>();

        [Fact]
        public async Task HandleGivenValidDetailsShouldReturnInvestigationTestInfo()
        {
            var request = new ViewTestInfoRequestCommand
            {
                PatientId = 1,
                InvestigationRequestId = 1
            };

            _investigationRequests.GetAll().Returns(GetInvestigationRequests().BuildMock());
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientCodeMapping.GetAll().Returns(GetPatientCodeMappings().BuildMock());
            _user.GetAll().Returns(GetUsers().BuildMock());

            var handler = new GetLaboratoryQueueTestInfoQueryHandler(_patient, _investigationRequests, _patientCodeMapping, _user);

            var result = await handler.Handle(request);
            result.ShouldNotBeNull();
            result.PatientCode.ShouldNotBeNull();
            result.PatientFirstName.ShouldNotBeNull();
            result.PatientLastName.ShouldNotBeNull();
        }

        [Fact]
        public async Task HandleGivenInvalidValidPatientIdShouldThrow()
        {
            var request = new ViewTestInfoRequestCommand
            {
                PatientId = 33,
                InvestigationRequestId = 1
            };

            _investigationRequests.GetAll().Returns(GetInvestigationRequests().BuildMock());
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientCodeMapping.GetAll().Returns(GetPatientCodeMappings().BuildMock());
            _user.GetAll().Returns(GetUsers().BuildMock());

            var handler = new GetLaboratoryQueueTestInfoQueryHandler(_patient, _investigationRequests, _patientCodeMapping, _user);

            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(request));

            exception.Message.ShouldBe("Patient not found");
        }

        [Fact]
        public async Task HandleGivenInvalidValidInvestigationRequestIdShouldThrow()
        {
            var request = new ViewTestInfoRequestCommand
            {
                PatientId = 1,
                InvestigationRequestId = 33
            };

            _investigationRequests.GetAll().Returns(GetInvestigationRequests().BuildMock());
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientCodeMapping.GetAll().Returns(GetPatientCodeMappings().BuildMock());
            _user.GetAll().Returns(GetUsers().BuildMock());

            var handler = new GetLaboratoryQueueTestInfoQueryHandler(_patient, _investigationRequests, _patientCodeMapping, _user);

            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(request));

            exception.Message.ShouldBe("Investigation Request not found");
        }

        private static IQueryable<InvestigationRequest> GetInvestigationRequests()
            => new List<InvestigationRequest>
            {
                new()
                {
                    Id = 1,
                    InvestigationId = 1,
                    PatientId = 1,
                    Investigation = new Investigation
                    {
                        Id = 1,
                        Name = "Electrolytes, Urea & Creatinine",
                        Type = "Chemistry",
                    },
                    DiagnosisId = 1,
                    Diagnosis = GetDiagnosis(),
                    CreatorUserId = 1
                },
                new()
                {
                    Id = 2,
                    InvestigationId = 2,
                    PatientId = 1,
                    Investigation = new Investigation
                    {
                        Id = 2,
                        Name = "Urinalysis",
                        Type = "Chemistry"
                    },
                    DiagnosisId = 1,
                    Diagnosis = GetDiagnosis(),
                    CreatorUserId = 1
                }
            }.AsQueryable();

        private static Diagnosis GetDiagnosis()
            => new()
            {
                Id = 1,
                PatientId = 1,
                Description = "Just testing",
                Notes = "Just like i did before"
            };

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

        private static IQueryable<User> GetUsers()
            => new List<User>
            {
                new()
                {
                    Id = 1,
                    Surname = "Test",
                    Name = "Tested",
                    TenantId = 1
                }
            }.AsQueryable();
    }
}

