using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Symptom.Dtos;
using Plateaumed.EHR.Symptom.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Symptoms
{
    [Trait("Category", "Unit")]
    public class CreateSymptomCommandHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_ShouldCheckAndSaveEncounterId()
        {
            // Arrange
            var request = new CreateSymptomDto{EncounterId = 6, SymptomEntryType = "Suggestion"};

            var repository = Substitute.For<IRepository<AllInputs.Symptom, long>>();

            var encounterManager = Substitute.For<IEncounterManager>();
            var abpSession = Substitute.For<IAbpSession>();
            abpSession.TenantId.Returns(1);
            var handler = new CreateSymptomCommandHandler(repository, Substitute.For<IUnitOfWorkManager>(), _objectMapper, encounterManager, abpSession);

            // Act
            AllInputs.Symptom symptom = null;
            await repository.InsertAsync(Arg.Do<AllInputs.Symptom>(j => symptom = j));

            await handler.Handle(request);
            
            // Assert
            await encounterManager.Received().CheckEncounterExists(request.EncounterId);
            symptom.EncounterId.ShouldBe(request.EncounterId);
        }
    }
}
