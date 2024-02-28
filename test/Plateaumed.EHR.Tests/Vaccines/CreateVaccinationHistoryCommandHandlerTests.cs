using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Vaccines;
using Plateaumed.EHR.Vaccines.Dto;
using Plateaumed.EHR.Vaccines.Handlers;
using Xunit;

namespace Plateaumed.EHR.Tests.Vaccines
{
    [Trait("Category", "Unit")]
    public class CreateVaccinationHistoryCommandHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenEncounterId_ShouldSaveToAllHistories()
        {
            // Arrange
            var request = new CreateMultipleVaccinationHistoryDto
            {
                EncounterId = 6,
                VaccinationHistory = new List<CreateVaccinationHistoryDto>
                {
                    new()
                    {
                        PatientId = 2,
                        Note = "test1"
                    },
                    new()
                    {
                        PatientId = 2,
                        Note = "test2"
                    }
                }
            };

            var repository = Substitute.For<IRepository<VaccinationHistory, long>>();
            var unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();
            var encounterManager = Substitute.For<IEncounterManager>();

            var handler = new CreateVaccinationHistoryCommandHandler(repository, unitOfWorkManager, _objectMapper, encounterManager);
            // Act
            await handler.Handle(request);

            // Assert
            await encounterManager.Received(1).CheckEncounterExists(request.EncounterId);
            await repository.Received(2).InsertAsync(Arg.Is<VaccinationHistory>(x => x.EncounterId == request.EncounterId)); 
        }
    }
}
