using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.NurseCarePlans;
using Plateaumed.EHR.NurseCarePlans.Dto;
using Plateaumed.EHR.NurseCarePlans.Handlers;
using Plateaumed.EHR.Patients;
using Xunit;

namespace Plateaumed.EHR.Tests.NurseCarePlan
{
    [Trait("Category", "Unit")]
    public class CreateNurseCareSummaryCommandHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenValidIds_ShouldCreateSummary()
        {
            // Arrange
            var request = new CreateNurseCarePlanRequest
            {
                PatientId = 1,
                EncounterId = 2,
                ActivitiesId = new List<long> { 3, 4 },
                ActivitiesText = new List<string> { "test 1" },
                OutcomesId = new List<long> { 3, 4 },
                OutcomesText = new List<string> { "test 2" },
                InterventionsText = new List<string> { "test 3" },
                InterventionsId = new List<long> { 3, 4 },
                EvaluationId = 5,
                DiagnosisId = 5,
                DiagnosisText = "test 4",
                EvaluationText = "test 5"
            };
            var activitiesRepository = Substitute.For<IRepository<NursingActivity, long>>();
            var interventionsRepository = Substitute.For<IRepository<NursingCareIntervention, long>>();
            var diagnosisRepository = Substitute.For<IRepository<NursingDiagnosis, long>>();
            var evaluationRepository = Substitute.For<IRepository<NursingEvaluation, long>>();
            var outcomesRepository = Substitute.For<IRepository<NursingOutcome, long>>();
            var summaryRepository = Substitute.For<IRepository<NursingCareSummary, long>>();
            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            var encounterRepository = Substitute.For<IRepository<PatientEncounter, long>>();
            var unitOfWorkRepository = Substitute.For<IUnitOfWorkManager>();

            patientRepository.GetAsync(request.PatientId).Returns(new Patient { Id = 1 });
            encounterRepository.GetAsync(request.EncounterId).Returns(new PatientEncounter { Id = 2 });
            evaluationRepository.GetAsync(request.EvaluationId.Value).Returns(new NursingEvaluation { Id = 5 });
            diagnosisRepository.GetAsync(request.DiagnosisId.Value).Returns(new NursingDiagnosis { Id = 5 });


            outcomesRepository.GetAll().Returns(
                new List<NursingOutcome> { new() { Id = 5 }, new() { Id = 4 } }
                    .AsQueryable().BuildMock());
            interventionsRepository.GetAll().Returns(
                new List<NursingCareIntervention> { new() { Id = 5 }, new() { Id = 4 } }
                    .AsQueryable().BuildMock());
            activitiesRepository.GetAll().Returns(new List<NursingActivity> { new() { Id = 5 }, new() { Id = 4 } }
                .AsQueryable().BuildMock());


            NursingCareSummary nursingCareSummary = null;
            await summaryRepository.InsertAsync(Arg.Do<NursingCareSummary>(x => nursingCareSummary = x));

            var handler = new CreateNurseCareSummaryCommandHandler(
                activitiesRepository, interventionsRepository, diagnosisRepository, evaluationRepository,
                outcomesRepository, summaryRepository, patientRepository, encounterRepository, unitOfWorkRepository,
                _objectMapper
            );

            // Act
            await handler.Handle(request);
            // Assert
            Assert.NotNull(nursingCareSummary);
            Assert.Equal(request.PatientId, nursingCareSummary.PatientId);
            Assert.Equal(request.EncounterId, nursingCareSummary.EncounterId);
            Assert.Equal(request.DiagnosisId, nursingCareSummary.NursingDiagnosisId);
            Assert.Equal(request.EvaluationId, nursingCareSummary.NursingEvaluationId);

            Assert.Equal(2, nursingCareSummary.NursingCareInterventions.Count);
            Assert.Equal(4, nursingCareSummary.NursingCareInterventions[0].NursingCareInterventionsId);

            Assert.Equal(2, nursingCareSummary.NursingActivities.Count);
            Assert.Equal(4, nursingCareSummary.NursingActivities[0].NursingActivitiesId);


            Assert.Equal(2, nursingCareSummary.NursingOutcomes.Count);
            Assert.Equal(4, nursingCareSummary.NursingOutcomes[0].NursingOutcomeId);
        }
    }
}