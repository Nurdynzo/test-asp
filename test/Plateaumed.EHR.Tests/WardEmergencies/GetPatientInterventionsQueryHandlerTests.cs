using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.WardEmergencies;
using Plateaumed.EHR.WardEmergencies.Dto;
using Plateaumed.EHR.WardEmergencies.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.WardEmergencies
{
    [Trait("Category", "Unit")]
    public class GetPatientInterventionsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenInterventionsWithIds_ShouldReturnInterventions()
        {
            // Arrange
            var repository = Substitute.For<IRepository<PatientIntervention, long>>();
            var userRepo = Substitute.For<IRepository<User, long>>();
            repository.GetAll().Returns(GetInterventions());
            var handler = new GetPatientInterventionsQueryHandler(repository, userRepo);
            // Act
            var interventions = await handler.Handle(new GetPatientInterventionsRequest
            {
                PatientId = 6
            });
            // Assert
            interventions.Count.ShouldBe(1);
            interventions[0].Event.ShouldBe("Event");
            interventions[0].Interventions.Count.ShouldBe(2);
            interventions[0].Interventions[0].ShouldBe("Intervention1");
            interventions[0].Interventions[1].ShouldBe("Intervention2");
        }

        [Fact]
        public async Task Handle_GivenInterventionsWithFreeText_ShouldReturnInterventions()
        {
            // Arrange
            var repository = Substitute.For<IRepository<PatientIntervention, long>>();
            var userRepo = Substitute.For<IRepository<User, long>>();
            repository.GetAll().Returns(GetInterventions());
            var handler = new GetPatientInterventionsQueryHandler(repository, userRepo);
            // Act
            var interventions = await handler.Handle(new GetPatientInterventionsRequest
            {
                PatientId = 7
            });
            // Assert
            interventions.Count.ShouldBe(1);
            interventions[0].Event.ShouldBe("EventText");
            interventions[0].Interventions.Count.ShouldBe(2);
            interventions[0].Interventions[0].ShouldBe("Intervention3");
            interventions[0].Interventions[1].ShouldBe("Intervention4");
        }

        private static IQueryable<PatientIntervention> GetInterventions()
        {
            return new List<PatientIntervention>
            {
                new()
                {
                    PatientId = 6,
                    Event = new WardEmergency {Event = "Event"},
                    InterventionEvents = new List<InterventionEvent>
                    {
                        new()
                        {
                            NursingIntervention = new NursingIntervention {Name = "Intervention1"}
                        },
                        new()
                        {
                            NursingIntervention = new NursingIntervention {Name = "Intervention2"}
                        },
                    }
                },
                new()
                {
                    PatientId = 7,
                    EventText = "EventText",
                    InterventionEvents = new List<InterventionEvent>
                    {
                        new()
                        {
                            InterventionText = "Intervention3"
                        },
                        new()
                        {
                            InterventionText = "Intervention4"
                        },
                    }
                },
            }.AsQueryable().BuildMock();
        }
    }
}
