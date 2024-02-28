using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class RecordInvestigationSampleCommandHandlerTests: AppTestBase
    {
        private readonly IRepository<InvestigationResult, long> _investigationResult = Substitute.For<IRepository<InvestigationResult, long>>();
        private readonly IRepository<InvestigationRequest, long> _investigationRequests = Substitute.For<IRepository<InvestigationRequest, long>>();
        private readonly IRepository<Patient, long> _patient = Substitute.For<IRepository<Patient, long>>();
        private readonly IRepository<Investigation, long> _investigation = Substitute.For<IRepository<Investigation, long>>();
        private readonly IRepository<PatientEncounter, long> _patientEncounter = Substitute.For<IRepository<PatientEncounter, long>>();
        private readonly IAbpSession _abpSession;

        public RecordInvestigationSampleCommandHandlerTests() => _abpSession = Resolve<IAbpSession>();

        [Fact]
        public async Task HandleGivenInvalidPatientIdShouldThrow()
        {   
            var request = new RecordInvestigationSampleRequest
            {
                EncounterId = 1,
                InvestigationId = 1,
                InvestigationRequestId = 1,
                NameOfInvestigation = "Ask me out",
                PatientId = 0,
                SampleCollectionDate = DateOnly.FromDateTime(DateTime.Now.Date),
                SampleCollectionTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            LoginAsDefaultTenantAdmin();

            _investigationResult.GetAsync(Arg.Any<long>()).Returns(new InvestigationResult());
            _investigation.GetAll().Returns(GetInvestigations().BuildMock());
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientEncounter.GetAll().Returns(GetPatientEncounters().BuildMock());
            _investigationRequests.GetAll().Returns(GetInvestigationRequests().BuildMock());

            var handler = new RecordInvestigationSampleCommandHandler(_investigationResult, _investigation, _patient, _patientEncounter, _investigationRequests, _abpSession);
            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(request));
            exception.Message.ShouldBe("Patient not found");
        }

        [Fact]
        public async Task HandleGivenInvalidInvestigationRequestIdShouldThrow()
        {
            var request = new RecordInvestigationSampleRequest
            {
                EncounterId = 1,
                InvestigationId = 1,
                InvestigationRequestId = 0,
                NameOfInvestigation = "Ask me out",
                PatientId = 1,
                SampleCollectionDate = DateOnly.FromDateTime(DateTime.Now.Date),
                SampleCollectionTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            LoginAsDefaultTenantAdmin();

            _investigationResult.GetAsync(Arg.Any<long>()).Returns(new InvestigationResult());
            _investigation.GetAll().Returns(GetInvestigations().BuildMock());
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientEncounter.GetAll().Returns(GetPatientEncounters().BuildMock());
            _investigationRequests.GetAll().Returns(GetInvestigationRequests().BuildMock());

            var handler = new RecordInvestigationSampleCommandHandler(_investigationResult, _investigation, _patient, _patientEncounter, _investigationRequests, _abpSession);
            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(request));
            exception.Message.ShouldBe("Investigation request not found");
        }

        [Fact]
        public async Task HandleGivenInvalidInvestigationIdShouldThrow()
        {
            var request = new RecordInvestigationSampleRequest
            {
                EncounterId = 1,
                InvestigationId = 0,
                InvestigationRequestId = 1,
                NameOfInvestigation = "Ask me out",
                PatientId = 1,
                SampleCollectionDate = DateOnly.FromDateTime(DateTime.Now.Date),
                SampleCollectionTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            LoginAsDefaultTenantAdmin();

            _investigationResult.GetAsync(Arg.Any<long>()).Returns(new InvestigationResult());
            _investigation.GetAll().Returns(GetInvestigations().BuildMock());
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientEncounter.GetAll().Returns(GetPatientEncounters().BuildMock());
            _investigationRequests.GetAll().Returns(GetInvestigationRequests().BuildMock());

            var handler = new RecordInvestigationSampleCommandHandler(_investigationResult, _investigation, _patient, _patientEncounter, _investigationRequests, _abpSession);
            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(request));
            exception.Message.ShouldBe("Investigation not found");
        }

        [Fact]
        public async Task HandleGivenInvalidPatientEncounterIdShouldThrow()
        {
            var request = new RecordInvestigationSampleRequest
            {
                EncounterId = 0,
                InvestigationId = 1,
                InvestigationRequestId = 1,
                NameOfInvestigation = "Ask me out",
                PatientId = 1,
                SampleCollectionDate = DateOnly.FromDateTime(DateTime.Now.Date),
                SampleCollectionTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            LoginAsDefaultTenantAdmin();

            _investigationResult.GetAsync(Arg.Any<long>()).Returns(new InvestigationResult());
            _investigation.GetAll().Returns(GetInvestigations().BuildMock());
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientEncounter.GetAll().Returns(GetPatientEncounters().BuildMock());
            _investigationRequests.GetAll().Returns(GetInvestigationRequests().BuildMock());

            var handler = new RecordInvestigationSampleCommandHandler(_investigationResult, _investigation, _patient, _patientEncounter, _investigationRequests, _abpSession);
            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(request));
            exception.Message.ShouldBe("Encounter not found");
        }

        [Fact]
        public async Task HandleGivenInvalidTenantIdShouldThrow()
        {
            var request = new RecordInvestigationSampleRequest
            {
                EncounterId = 1,
                InvestigationId = 1,
                InvestigationRequestId = 1,
                NameOfInvestigation = "Ask me out",
                PatientId = 1,
                SampleCollectionDate = DateOnly.FromDateTime(DateTime.Now.Date),
                SampleCollectionTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            LoginAsHostAdmin();
            _investigationResult.GetAsync(Arg.Any<long>()).Returns(new InvestigationResult());
            _investigation.GetAll().Returns(GetInvestigations().BuildMock());
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientEncounter.GetAll().Returns(GetPatientEncounters().BuildMock());
            _investigationRequests.GetAll().Returns(GetInvestigationRequests().BuildMock());

            var handler = new RecordInvestigationSampleCommandHandler(_investigationResult, _investigation, _patient, _patientEncounter, _investigationRequests, _abpSession);
            var exception = await Assert.ThrowsAsync<Abp.UI.UserFriendlyException>(() => handler.Handle(request));
            exception.Message.ShouldBe("User Tenant not found");
        }

        [Fact]
        public async Task HandleGivenValidRequestShouldSave()
        {
            var request = new RecordInvestigationSampleRequest
            {
                EncounterId = 1,
                InvestigationId = 1,
                InvestigationRequestId = 1,
                NameOfInvestigation = "Ask me out",
                PatientId = 1,
                SampleCollectionDate = DateOnly.FromDateTime(DateTime.Now.Date),
                SampleCollectionTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            LoginAsDefaultTenantAdmin();

            _investigationResult.GetAsync(Arg.Any<long>()).Returns(new InvestigationResult());
            _investigation.GetAll().Returns(GetInvestigations().BuildMock());
            _patient.GetAll().Returns(GetPatients().BuildMock());
            _patientEncounter.GetAll().Returns(GetPatientEncounters().BuildMock());
            _investigationRequests.GetAll().Returns(GetInvestigationRequests().BuildMock());

            var unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();

            InvestigationResult savedResult = null;
            var repository = Substitute.For<IRepository<InvestigationResult, long>>();
            await repository.InsertAsync(Arg.Do<InvestigationResult>(result => savedResult = result));
            var handler = new RecordInvestigationSampleCommandHandler(repository, _investigation, _patient, _patientEncounter, _investigationRequests, _abpSession);
            
            await handler.Handle(request);
            savedResult.ShouldNotBeNull();
            savedResult.PatientId.ShouldBe(request.PatientId);
            savedResult.InvestigationId.ShouldBe(request.InvestigationId);
            savedResult.EncounterId.ShouldBe(request.EncounterId);
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
                    CreatorUserId = 1
                }
                }.AsQueryable();
      
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
                },
                new()
                {
                    Id = 3,
                    FirstName = "Test3",
                    LastName = "Test_3",
                    MiddleName = ""
                },
            }.AsQueryable();

        private static IQueryable<Investigation> GetInvestigations() =>
            new List<Investigation>
            {
                new()
                {
                    Id = 1,
                    Name = "Electrolytes, Urea & Creatinine",
                    Type = "Chemistry",
                },
                new()
                {
                    Id = 2,
                    Name = "Urinalysis",
                    Type = "Chemistry"
                }
            }.AsQueryable();

        private static IQueryable<PatientEncounter> GetPatientEncounters() =>
            new List<PatientEncounter>
            {
                new()
                {
                    Id = 1,
                    PatientId = 1
                },
                new()
                {
                    Id= 2,
                    PatientId = 2
                }
            }.AsQueryable();
    }
}

