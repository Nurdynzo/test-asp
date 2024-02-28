using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Medication.Query;
using Shouldly;
using Xunit;
namespace Plateaumed.EHR.Tests.Medications
{
    [Trait("Category", "Unit")]
    public class GetViewMedicationQueryHandlerTest
    {
        private readonly IRepository<AllInputs.Medication,long> _medicationRepository
            = Substitute.For<IRepository<AllInputs.Medication, long>>();
        [Fact]
        public async Task Handle_GivenPatientId_ShouldReturnMedications()
        {
            // Arrange
            var patientId = 1;
            var medications = GetMedications();
            _medicationRepository.GetAll().Returns(medications.AsQueryable().BuildMock());
            var handler = new GetViewMedicationQueryHandler(_medicationRepository);
            // Act
            var result = await handler.Handle(patientId);
            // Assert
            result.Count.ShouldBe(2);
        }
        private static List<AllInputs.Medication> GetMedications()
        {

            var medications = new List<AllInputs.Medication>
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    ProductName = "test",
                    DoseUnit = "test",
                    Frequency = "test",
                    Duration = "test",
                    Direction = "test"
                },
                new()
                {
                    Id = 2,
                    PatientId = 1,
                    ProductName = "test",
                    DoseUnit = "test",
                    Frequency = "test",
                    Duration = "test",
                    Direction = "test"
                }
            };
            return medications;
        }
    }
}
