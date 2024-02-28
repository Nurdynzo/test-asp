using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientProfile.Command;
using Plateaumed.EHR.PatientProfile.Dto;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class UpdateSurgicalHistoryHandlerTest
    {
        private readonly IRepository<SurgicalHistory, long> _patientSurgicalHistoryRepository = Substitute.For<IRepository<SurgicalHistory, long>>();
        [Fact]
        public async Task UpdateSurgicalHistoryHandler_Should_Update_Successfully()
        {
            //Arrange
            var edit = new CreateSurgicalHistoryRequestDto
            {
                PatientId = 1,
                Diagnosis = "another diagnosis",
                DiagnosisSnomedId = 1233,
                Procedure = "test procedure",
                ProcedureSnomedId = 3453,
                Interval = Misc.UnitOfTime.Month,
                PeriodSinceSurgery = 40,
                NoComplicationsPresent = true,
                Note = "test note"
            };
            _patientSurgicalHistoryRepository.GetAll().Returns(GetSurgicalHistories().BuildMock());
            //Act
            var handler = new UpdateSurgicalHistoryCommandHandler(_patientSurgicalHistoryRepository);
            await handler.Handle(1, edit);
            var editedItem = _patientSurgicalHistoryRepository.GetAll().SingleOrDefault(x => x.Id == 1);
            //Assert
            editedItem.Diagnosis.ShouldBe("another diagnosis");
        }

        private static IQueryable<SurgicalHistory> GetSurgicalHistories()
        {
            return new List<SurgicalHistory>
            {
                new()
                {
                    PatientId = 1,
                    Diagnosis = "test diagnosis",
                    DiagnosisSnomedId = 1233,
                    Procedure = "test procedure",
                    ProcedureSnomedId = 3453,
                    Interval = Misc.UnitOfTime.Month,
                    PeriodSinceSurgery = 40,
                    NoComplicationsPresent = true,
                    Note = "test note",
                    Id = 1
                },
                new()
                {
                    PatientId = 1,
                    Diagnosis = "test2 diagnosis",
                    DiagnosisSnomedId = 1233,
                    Procedure = "test2 procedure",
                    ProcedureSnomedId = 3453,
                    Interval = Misc.UnitOfTime.Month,
                    PeriodSinceSurgery = 40,
                    NoComplicationsPresent = true,
                    Note = "test note",
                    Id = 2
                }
            }.AsQueryable();
        }
    }
}
