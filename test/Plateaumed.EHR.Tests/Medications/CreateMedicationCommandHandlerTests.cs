using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using NSubstitute;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Medication.Dtos;
using Plateaumed.EHR.Medication.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Medications
{
    [Trait("Category", "Unit")]
    public class CreateMedicationCommandHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
        private readonly IAbpSession _abpSession = Substitute.For<IAbpSession>();
        
        [Fact]
        public async Task Handle_GivenMedications_ShouldCreateMedications()
        {
            // Arrange
            var request = new CreateMultipleMedicationsDto
            {
                EncounterId = 9,
                Prescriptions = new List<CreateMedicationDto>
                {
                    new CreateMedicationDto
                    {
                        PatientId = 1,
                        ProductId = 1,
                        ProductName = "test",
                        ProductSource = "test",
                        DoseUnit = "test",
                        Frequency = "test",
                        Duration = "test",
                        Direction = "test",
                        Note = "test"
                    },
                    new CreateMedicationDto
                    {
                        PatientId = 1,
                        ProductId = 1,
                        ProductName = "test",
                        ProductSource = "test",
                        DoseUnit = "test",
                        Frequency = "test",
                        Duration = "test",
                        Direction = "test",
                        Note = "test"
                    }
                }
            };

            var medicationRepository = Substitute.For<IRepository<AllInputs.Medication, long>>();
            var unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();
            var encounterManager = Substitute.For<IEncounterManager>();
            var abpSession = Substitute.For<IAbpSession>();
            abpSession.TenantId.Returns(1);

            List<AllInputs.Medication> medications = new();
            await medicationRepository.InsertAsync(Arg.Do<AllInputs.Medication>(medications.Add));

            var handler = new CreateMedicationCommandHandler(medicationRepository, unitOfWorkManager, _objectMapper,
                encounterManager, abpSession);
            // Act
            await handler.Handle(request);
            // Assert
            await encounterManager.Received(1).CheckEncounterExists(9);
            medications.Count.ShouldBe(2);
            medications[0].EncounterId.ShouldBe(9);
            medications[1].EncounterId.ShouldBe(9);
        }
    }
}
