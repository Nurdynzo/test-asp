using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Handlers;
using Xunit;
namespace Plateaumed.EHR.Tests.Medications
{
    public class MarkMedicationForAdministerCommandHandlerTest
    {
        private readonly IRepository<AllInputs.Medication,long> _medicationRepository
            = Substitute.For<IRepository<AllInputs.Medication, long>>();
        private readonly IMarkMedicationForAdministerCommandHandler _markMedicationForAdministerCommandHandler;
        public MarkMedicationForAdministerCommandHandlerTest()
        {
            _markMedicationForAdministerCommandHandler = new MarkMedicationForAdministerCommandHandler(_medicationRepository);
        }
        [Fact]
        public async Task Handle_ShouldUpdateIsAdministerToTrue()
        {
            //arrange
            var medicationId = new List<long> { 1, 2, 3,4 };
            _medicationRepository.GetAll().Returns(GetMedications().AsQueryable().BuildMock());
            //act
            await _markMedicationForAdministerCommandHandler.Handle(medicationId);
            //assert
            await _medicationRepository.Received(3).UpdateAsync(Arg.Is<AllInputs.Medication>(m => m.IsAdministered));
        }
        private List<AllInputs.Medication> GetMedications()
        {
            return new List<AllInputs.Medication>()
            {
                new()
                {
                    Id = 1,
                    IsAdministered = false
                },
                new()
                {
                    Id = 2,
                    IsAdministered = false
                },
                new()
                {
                    Id = 3,
                    IsAdministered = false
                },
                new()
                {
                    Id = 4,
                    IsAdministered = true
                }
            };
        }
    }
}
