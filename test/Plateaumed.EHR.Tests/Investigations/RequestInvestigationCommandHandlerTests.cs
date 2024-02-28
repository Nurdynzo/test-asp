using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class RequestInvestigationCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenInvalidPatient_ShouldThrow()
        {
            // Arrange
            var request = new List<RequestInvestigationRequest>
            {
                new()
                {
                    PatientId = 5, 
                    InvestigationId = 6, 
                    EncounterId = 8, 
                    Urgent = true 
                }
            };
                

            var investigationRequestRepository = Substitute.For<IRepository<InvestigationRequest, long>>();
            
            var handler = CreateHandler(investigationRequestRepository, CreatePatientRepository(null));
            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request));
            // Assert
            exception.Message.ShouldBe("Patient not found");
        }

        [Fact]
        public async Task Handle_GivenInvalidInvestigation_ShouldThrow()
        {
            // Arrange
            var request = new List<RequestInvestigationRequest>
            {
                new()
                { 
                    PatientId = 5, 
                    InvestigationId = 6, 
                    EncounterId = 8, 
                    Urgent = true 
                }
            };
                

            var investigationRequestRepository = Substitute.For<IRepository<InvestigationRequest, long>>();

            var handler = CreateHandler(investigationRequestRepository,
                investigationRepository: CreateInvestigationRepository(null));
            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request));
            // Assert
            exception.Message.ShouldBe("Investigation not found");
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldSave()
        {
            // Arrange
            var request = new List<RequestInvestigationRequest>
            {
                new()
                { 
                    PatientId = 5, 
                    InvestigationId = 6, 
                    EncounterId = 8,  
                    Urgent = true, 
                    Notes = "Do some things" 
                }
            };
                

            var investigationRequestRepository = Substitute.For<IRepository<InvestigationRequest, long>>();
            InvestigationRequest savedRequest = null;

            await investigationRequestRepository.InsertAsync(Arg.Do<InvestigationRequest>(i => savedRequest = i));

            var unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();

            var handler = CreateHandler(investigationRequestRepository, unitOfWorkManager: unitOfWorkManager);
            // Act
            await handler.Handle(request);

            // Assert
            savedRequest.ShouldNotBeNull();
            savedRequest.PatientId.ShouldBe(request[0].PatientId);
            savedRequest.InvestigationId.ShouldBe(request[0].InvestigationId);
            savedRequest.PatientEncounterId.ShouldBe(request[0].EncounterId);
            savedRequest.Urgent.ShouldBe(request[0].Urgent);
            savedRequest.Notes.ShouldBe(request[0].Notes);
            savedRequest.InvestigationStatus.ShouldBe(InvestigationStatus.Requested);
            await unitOfWorkManager.Received(1).Current.SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_ShouldCheckEncounterExists()
        {
            // Arrange
            var request = new List<RequestInvestigationRequest>
            {
                new()
                {
                    PatientId = 5, 
                    InvestigationId = 6, 
                    EncounterId = 8, 
                    Urgent = true,
                    Notes = "Do some things"
                }
            };

            var investigationRequestRepository = Substitute.For<IRepository<InvestigationRequest, long>>();

            var encounterManager = Substitute.For<IEncounterManager>();

            var handler = CreateHandler(investigationRequestRepository, encounterManager: encounterManager);
            // Act
            await handler.Handle(request);

            // Assert
            await encounterManager.Received(1).CheckEncounterExists(request[0].EncounterId.Value);
        }

        private static RequestInvestigationCommandHandler CreateHandler(
            IRepository<InvestigationRequest, long> investigationRequestRepository,
            IRepository<Patient, long> patientRepository = null, 
            IRepository<Investigation, long> investigationRepository = null,
            IUnitOfWorkManager unitOfWorkManager = null,
            IEncounterManager encounterManager = null )
        {
            patientRepository ??= CreatePatientRepository(new Patient());
            investigationRepository ??= CreateInvestigationRepository(new Investigation());
            unitOfWorkManager ??= Substitute.For<IUnitOfWorkManager>();
            encounterManager ??= Substitute.For<IEncounterManager>();
            
            return new RequestInvestigationCommandHandler(investigationRequestRepository, patientRepository,
                investigationRepository, unitOfWorkManager, encounterManager);
        }

        private static IRepository<Investigation, long> CreateInvestigationRepository(Investigation investigation)
        {
            var investigationRepository = Substitute.For<IRepository<Investigation, long>>();
            investigationRepository.GetAsync(Arg.Any<long>()).Returns(investigation);
            return investigationRepository;
        }

        private static IRepository<Patient, long> CreatePatientRepository(Patient patient)
        {
            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            patientRepository.GetAsync(Arg.Any<long>()).Returns(patient);
            return patientRepository;
        }
    }
}
