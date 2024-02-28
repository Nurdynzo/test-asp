using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.NurseCarePlans;
using Plateaumed.EHR.NurseCarePlans.Dto;
using Plateaumed.EHR.NurseCarePlans.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.NurseCarePlan
{
    [Trait("Category", "Unit")]
    public class GetNurseCareSummaryQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenNurseCareWithIdsAndTexts_ShouldReturnACombinationOfIdsAndTexts()
        {
            // Arrange
            var nursingSummaryRepository = Substitute.For<IRepository<NursingCareSummary, long>>();
            var nursingOutcomes = Substitute.For<IRepository<NursingOutcome, long>>();
            var nursingActivities = Substitute.For<IRepository<NursingActivity, long>>();
            var nursingInterventions = Substitute.For<IRepository<NursingCareIntervention, long>>();

            nursingSummaryRepository.GetAll().Returns(new List<NursingCareSummary>
            {
                new()
                {
                    Id = 1,
                    EncounterId = 1,
                    PatientId = 6,
                    NursingDiagnosis = new NursingDiagnosis
                    {
                        Name = "Diagnosis 1",
                        Id = 1
                    },
                    NursingDiagnosisId = 1,
                    NursingDiagnosisText = "Custom diagnosis 1",
                    NursingOutcomes = new List<PatientNursingOutcome> { new(){Id = 1, NursingOutcome = new NursingOutcome{Id = 1, Name = "outcome 1"}} },
                    NursingActivities = new List<PatientNursingActivity> { new(){Id = 1, NursingActivity = new NursingActivity{Id = 1,  Name = "activity 1"}} },
                    NursingCareInterventions = new List<PatientNursingCareIntervention> { new () {Id = 1, NursingCareIntervention = new NursingCareIntervention{Id = 1, Name = "intervention 1"}}},
                    NursingEvaluation = new NursingEvaluation
                    {
                        Id = 1,
                        Name = "Evaluation 1"
                    },
                    NursingEvaluationText = "Evaluation 2"
                }
            }.AsQueryable().BuildMock());
            
            var handler = new GetNurseCareSummaryQueryHandler(nursingSummaryRepository, nursingOutcomes,
                nursingActivities, nursingInterventions);

            // Act
            var summaries = await handler.Handle(new GetNurseCareRequest
            {
                PatientId = 6,
                EncounterId = 1
            });

            // Assert
            summaries.Count.ShouldBe(1);
            summaries[0].Diagnosis.ShouldBe("Diagnosis 1");
            summaries[0].Evaluation.ShouldBe("Evaluation 2");
            summaries[0].Activities[0].ShouldBe("activity 1");
            summaries[0].Interventions[0].ShouldBe("intervention 1");
            summaries[0].Outcomes[0].ShouldBe("outcome 1");
        }
    }
}