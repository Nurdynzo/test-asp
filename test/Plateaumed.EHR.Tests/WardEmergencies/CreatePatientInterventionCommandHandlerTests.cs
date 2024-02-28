using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.WardEmergencies;
using Plateaumed.EHR.WardEmergencies.Dto;
using Plateaumed.EHR.WardEmergencies.Handlers;
using Xunit;

namespace Plateaumed.EHR.Tests.WardEmergencies
{
    [Trait("Category", "Unit")]
    public class CreatePatientInterventionCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidInterventionIds_ShouldCreatePatientIntervention()
        {
            // Arrange
            var request = new CreatePatientInterventionRequest
            {
                PatientId = 1,
                EncounterId = 2,
                InterventionIds = new List<long> { 3, 4 },
                EventId = 5,
            };

            var interventionRepository = Substitute.For<IRepository<PatientIntervention, long>>();
            var nursingInterventionRepository = Substitute.For<IRepository<NursingIntervention, long>>();
            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            var encounterRepository = Substitute.For<IRepository<PatientEncounter, long>>();
            var wardEmergencyRepository = Substitute.For<IRepository<WardEmergency, long>>();

            wardEmergencyRepository.GetAsync(request.EventId.Value).Returns(new WardEmergency { Id = 5 });
            patientRepository.GetAsync(request.PatientId).Returns(new Patient { Id = 1 });
            encounterRepository.GetAsync(request.EncounterId).Returns(new PatientEncounter { Id = 2 });

            var nursingInterventions = new List<NursingIntervention> { new() { Id = 3 }, new() { Id = 4 } }
                .AsQueryable().BuildMock();

            nursingInterventionRepository.GetAll().Returns(nursingInterventions);

            PatientIntervention intervention = null;
            await interventionRepository.InsertAsync(Arg.Do<PatientIntervention>(x => intervention = x));

            var handler = new CreatePatientInterventionCommandHandler(interventionRepository,
                nursingInterventionRepository, patientRepository, encounterRepository, wardEmergencyRepository,
                Substitute.For<IUnitOfWorkManager>());

            // Act
            await handler.Handle(request);
            // Assert
            Assert.NotNull(intervention);
            Assert.Equal(request.PatientId, intervention.PatientId);
            Assert.Equal(request.EncounterId, intervention.EncounterId);
            Assert.Equal(request.EventId, intervention.EventId);
            Assert.Equal(2, intervention.InterventionEvents.Count);
            Assert.Equal(3, intervention.InterventionEvents[0].NursingInterventionId);
            Assert.Equal(4, intervention.InterventionEvents[1].NursingInterventionId);
        }

        [Fact]
        public async Task Handle_GivenFreeText_ShouldCreatePatientIntervention()
        {
            // Arrange
            var request = new CreatePatientInterventionRequest
            {
                PatientId = 1,
                EncounterId = 2,
                EventText = "Test Event",
                InterventionTexts = new List<string> { "Test Intervention 1", "Test Intervention 2" },
            };

            var interventionRepository = Substitute.For<IRepository<PatientIntervention, long>>();
            var nursingInterventionRepository = Substitute.For<IRepository<NursingIntervention, long>>();
            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            var encounterRepository = Substitute.For<IRepository<PatientEncounter, long>>();
            var wardEmergencyRepository = Substitute.For<IRepository<WardEmergency, long>>();

            patientRepository.GetAsync(request.PatientId).Returns(new Patient { Id = 1 });
            encounterRepository.GetAsync(request.EncounterId).Returns(new PatientEncounter { Id = 2 });
            nursingInterventionRepository.GetAll().Returns(new List<NursingIntervention>().AsQueryable().BuildMock());

            PatientIntervention intervention = null;
            await interventionRepository.InsertAsync(Arg.Do<PatientIntervention>(x => intervention = x));

            var handler = new CreatePatientInterventionCommandHandler(interventionRepository,
                nursingInterventionRepository, patientRepository, encounterRepository, wardEmergencyRepository,
                Substitute.For<IUnitOfWorkManager>());

            // Act
            await handler.Handle(request);
            // Assert
            Assert.NotNull(intervention);
            Assert.Equal(request.PatientId, intervention.PatientId);
            Assert.Equal(request.EncounterId, intervention.EncounterId);
            Assert.Equal(request.EventText, intervention.EventText);
            Assert.Equal(2, intervention.InterventionEvents.Count);
            Assert.Equal(request.InterventionTexts[0], intervention.InterventionEvents[0].InterventionText);
            Assert.Equal(request.InterventionTexts[1], intervention.InterventionEvents[1].InterventionText);
        }
    }
}