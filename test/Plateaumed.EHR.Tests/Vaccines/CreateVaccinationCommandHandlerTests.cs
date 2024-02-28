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
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Vaccines
{
    [Trait("Category", "Unit")]
    public class CreateVaccinationCommandHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenEncounterId_ShouldCheckAndSave()
        {
            // Arrange
            var request = new CreateMultipleVaccinationDto
            {
                EncounterId = 7, Vaccinations = new List<CreateVaccinationDto>
                {
                    new()
                    {
                        VaccineId = 1,
                        PatientId = 1,
                        Note = "test",
                    },
                    new()
                    {
                        VaccineId = 2,
                        PatientId = 2,
                        Note = "test2",
                    }
                }
            };

            var repository = Substitute.For<IRepository<Vaccination, long>>();

            var encounterManager = Substitute.For<IEncounterManager>();
            var handler = new CreateVaccinationCommandHandler(repository, Substitute.For<IUnitOfWorkManager>(), _objectMapper, encounterManager);
            // Act
            List<Vaccination> vaccination = new();
            await repository.InsertAsync(Arg.Do<Vaccination>(vaccination.Add));

            await handler.Handle(request);
            
            // Assert
            await encounterManager.Received().CheckEncounterExists(request.EncounterId);
            vaccination[0].EncounterId.ShouldBe(request.EncounterId);
            vaccination[0].PatientId.ShouldBe(request.Vaccinations[0].PatientId);
            vaccination[1].EncounterId.ShouldBe(request.EncounterId);
            vaccination[1].PatientId.ShouldBe(request.Vaccinations[1].PatientId);
        }
    }
}
