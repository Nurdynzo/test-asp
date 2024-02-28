using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Handlers;
using Xunit;
namespace Plateaumed.EHR.Tests.Medications
{
    public class MarkMedicationForDiscontinueCommandHandlerTest
    {
        private readonly IRepository<AllInputs.Medication,long> _medicationRepository
            = Substitute.For<IRepository<AllInputs.Medication, long>>();
        private readonly IAbpSession _abpSession = Substitute.For<IAbpSession>();
        private readonly IMarkMedicationForDiscontinueCommandHandler _markMedicationForDiscontinueCommandHandler;
        public MarkMedicationForDiscontinueCommandHandlerTest()
        {
            _markMedicationForDiscontinueCommandHandler = new MarkMedicationForDiscontinueCommandHandler(_medicationRepository,_abpSession);
        }
        [Fact]
        public async Task Handle_ShouldUpdateIsDiscontinueToTrue()
        {
            //arrange
            var medicationId = new List<long> { 1, 2, 3, 4 };
            _medicationRepository.GetAll().Returns(GetMedications().AsQueryable().BuildMock());
            _abpSession.UserId.Returns(1);
            //act
            await _markMedicationForDiscontinueCommandHandler.Handle(medicationId);
            //assert
            await _medicationRepository.Received(2).UpdateAsync(Arg.Is<AllInputs.Medication>(m => m.IsDiscontinued));
        }
        private List<AllInputs.Medication> GetMedications()
        {
            return new List<AllInputs.Medication>()
            {
                new()
                {
                    Id = 1,
                    IsDiscontinued = false
                },
                new()
                {
                    Id = 2,
                    IsDiscontinued = false
                },
                new()
                {
                    Id = 3,
                    IsDiscontinued = true
                },
                new()
                {
                    Id = 4,
                    IsDiscontinued = true
                }
            };
        }
    }
}
