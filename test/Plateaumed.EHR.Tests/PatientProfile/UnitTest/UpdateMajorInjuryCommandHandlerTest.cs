using Abp.Domain.Repositories;
using Plateaumed.EHR.PatientProfile.Command;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Plateaumed.EHR.Patients;
using NSubstitute;
using MockQueryable.NSubstitute;
using Shouldly;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class UpdateMajorInjuryCommandHandlerTest
    {
        private readonly IRepository<PatientMajorInjury, long> _patientMajorInjuryRepository = Substitute.For<IRepository<PatientMajorInjury, long>>();
        [Fact]
        public async Task UpdateMajorInjury_Should_Update_Successfully()
        {
            //Arrange
            var edit = new CreatePatientMajorInjuryRequest
            {
                Id = 1,
                Diagnosis = "edit diagnosis",
                PeriodOfInjury = 1,
                PeriodOfInjuryUnit = Misc.UnitOfTime.Day,
                IsOngoing = true,
                Notes = "Test notes",
                IsComplicationPresent = true
            };
            _patientMajorInjuryRepository.GetAll().Returns(GetSurgicalHistories().BuildMock());
            //Act
            var handler = new UpdateMajorInjuryCommandHandler(_patientMajorInjuryRepository);
            await handler.Handle(edit);
            var editedItem = _patientMajorInjuryRepository.GetAll().SingleOrDefault(x => x.Id == 1);
            //Assert
            editedItem.Diagnosis.ShouldBe("edit diagnosis");
        }

        private static IQueryable<PatientMajorInjury> GetSurgicalHistories()
        {
            return new List<PatientMajorInjury>
            {
                new()
                {
                    PatientId = 1,
                    Diagnosis = "another diagnosis",
                    PeriodOfInjury = 1,
                    PeriodOfInjuryUnit = Misc.UnitOfTime.Day,
                    IsOngoing = true,
                    Notes = "Test notes",
                    IsComplicationPresent = true,
                    Id = 1
                },
                new()
                {
                    PatientId = 2,
                    Diagnosis = "another diagnosis 2",
                    PeriodOfInjury = 1,
                    PeriodOfInjuryUnit = Misc.UnitOfTime.Day,
                    IsOngoing = true,
                    Notes = "Test notes",
                    IsComplicationPresent = true,
                    Id = 2
                }
            }.AsQueryable();
        }
    }
}
