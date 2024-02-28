using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class RecordInvestigationCommandHandlerTests: AppTestBase
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        private readonly IAbpSession _abpSession;

        private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();

        public RecordInvestigationCommandHandlerTests() => _abpSession = Resolve<IAbpSession>();        

        [Fact]
        public async Task Handle_GivenValidValues_ShouldCreateInvestigation()
        {
            LoginAsDefaultTenantAdmin();
            // Arrange
            var command = new RecordInvestigationRequest
            {
                PatientId = 1,
                InvestigationId = 1,
                InvestigationRequestId = 1,
                EncounterId = 5,
                Name = "Investigation",
                Reference = "Reference",
                SampleCollectionDate = new DateOnly(2021, 1, 1),
                ResultDate = new DateOnly(2021, 1, 1),
                SampleTime = new TimeOnly(1, 1, 1),
                ResultTime = new TimeOnly(1, 1, 1),
                Specimen = "Blood",
                Conclusion = "Septic",
                SpecificOrganism = "HIV",
                View = null,
                Notes = "Looks nasty",
                ReviewerId = 1,
                InvestigationComponentResults = new List<InvestigationComponentResultDto>
                {
                    new()
                    {
                        Category = "Category",
                        Name = "Name",
                        Result = "Result",
                        NumericResult = 1,
                        Reference = "Reference",
                        RangeMin = 1,
                        RangeMax = 1,
                        Unit = "Unit"
                    }
                }
            };

            InvestigationResult savedResult = null;
            var repository = Substitute.For<IRepository<InvestigationResult, long>>();
            repository.InsertAsync(Arg.Do<InvestigationResult>(result => savedResult = result)).ReturnsForAnyArgs(new InvestigationResult { Id = 1 });

            var handler = CreateHandler(repository);
            // Act
            await handler.Handle(command, 1);
            await _unitOfWork.Received(1).Current.SaveChangesAsync();
          
            // Assert
            Assert.NotNull(savedResult);
            Assert.Equal(command.PatientId, savedResult.PatientId);
            Assert.Equal(command.InvestigationId, savedResult.InvestigationId);
            Assert.Equal(command.EncounterId, savedResult.EncounterId);
            Assert.Equal(command.Name, savedResult.Name);
            Assert.Equal(command.Reference, savedResult.Reference);
            Assert.Equal(command.SampleCollectionDate, savedResult.SampleCollectionDate);
            Assert.Equal(command.ResultDate, savedResult.ResultDate);
            Assert.Equal(command.SampleTime, savedResult.SampleTime);
            Assert.Equal(command.ResultTime, savedResult.ResultTime);
            Assert.Equal(command.Specimen, savedResult.Specimen);
            Assert.Equal(command.Conclusion, savedResult.Conclusion);
            Assert.Equal(command.SpecificOrganism, savedResult.SpecificOrganism);
            Assert.Equal(command.View, savedResult.View);
            Assert.Equal(command.Notes, savedResult.Notes);
            Assert.Single(savedResult.InvestigationComponentResults);
            Assert.Equal(command.InvestigationComponentResults[0].Category,
                               savedResult.InvestigationComponentResults[0].Category);
            Assert.Equal(command.InvestigationComponentResults[0].Name,
                               savedResult.InvestigationComponentResults[0].Name);
            Assert.Equal(command.InvestigationComponentResults[0].Result,
                               savedResult.InvestigationComponentResults[0].Result);
            Assert.Equal(command.InvestigationComponentResults[0].NumericResult,
                               savedResult.InvestigationComponentResults[0].NumericResult);
            Assert.Equal(command.InvestigationComponentResults[0].Reference,
                               savedResult.InvestigationComponentResults[0].Reference);
            Assert.Equal(command.InvestigationComponentResults[0].RangeMin,
                               savedResult.InvestigationComponentResults[0].RangeMin);
            Assert.Equal(command.InvestigationComponentResults[0].RangeMax,
                               savedResult.InvestigationComponentResults[0].RangeMax);
            Assert.Equal(command.InvestigationComponentResults[0].Unit,
                               savedResult.InvestigationComponentResults[0].Unit);
        }

        [Fact]
        public async Task Handle_GivenValidValues_ShouldCheckInvestigationExists()
        {
            // Arrange
            var command = new RecordInvestigationRequest
            {
                PatientId = 1,
                InvestigationId = 1,
                EncounterId = 5,
                InvestigationRequestId = 1
            };

            var encounterManager = Substitute.For<IEncounterManager>();
            InvestigationResult savedResult = null;
            var repository = Substitute.For<IRepository<InvestigationResult, long>>();

            repository.InsertAsync(Arg.Do<InvestigationResult>(result => savedResult = result)).ReturnsForAnyArgs(new InvestigationResult { Id = 1 });

            var handler = CreateHandler(repository, encounterManager);
            // Act
            await handler.Handle(command, 1);
            // Assert
            await encounterManager.Received(1).CheckEncounterExists(command.EncounterId);
        }

        private RecordInvestigationCommandHandler CreateHandler(IRepository<InvestigationResult, long> repository,
            IEncounterManager encounterManager = null)
        {
            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            patientRepository.GetAsync(Arg.Any<long>()).Returns(new Patient());

            var investigation = Substitute.For<IRepository<Investigation, long>>();
            investigation.GetAsync(Arg.Any<long>()).Returns(new Investigation
            {
                Id = 1,
                Name = "Electrolyte",
                Type = "Electrophysiology"
            });

            var investigationRequestRepository = Substitute.For<IRepository<InvestigationRequest, long>>();
            investigationRequestRepository.GetAsync(Arg.Any<long>()).Returns(new InvestigationRequest
            {
                Id = 1,
                InvestigationId = 1,
                InvestigationStatus = InvestigationStatus.ReportReady,
                PatientId = 1,
                Investigation = new Investigation
                {
                    Id = 1,
                    Name = "Electrolyte",
                    Type = "Electrophysiology"
                }
            });

            var reviewer = Substitute.For<IRepository<InvestigationResultReviewer, long>>();
            reviewer.GetAsync(Arg.Any<long>()).Returns(new InvestigationResultReviewer
            {
                InvestigationResultId = 1,
                ApproverId = 1,
                TenantId = 1,
                FacilityId = 1
            });

            var staffMember = Substitute.For<IRepository<StaffMember, long>>();
            staffMember.GetAsync(Arg.Any<long>()).Returns(new StaffMember
            {
                Id = 1,
                UserId = 1
            });

            var investigationComponent = Substitute.For<IRepository<InvestigationComponentResult, long>>();          

            var handler = new RecordInvestigationCommandHandler(repository, _objectMapper,
                investigationRequestRepository, patientRepository, investigation, encounterManager ?? Substitute.For<IEncounterManager>(),
                reviewer, investigationComponent, _abpSession, _unitOfWork, staffMember);
            return handler;
        }
    }
}