using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientProfile.Command;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class UpdatePastMedicalHistoryCommandHandlerTest
    {
        private readonly IRepository<PatientPastMedicalCondition, long> _pastMedicalConditionRepository
          = Substitute.For<IRepository<PatientPastMedicalCondition, long>>();
        private readonly IRepository<PatientPastMedicalConditionMedication, long> _patientPastMedicalConditionMedicationRepository
            = Substitute.For<IRepository<PatientPastMedicalConditionMedication, long>>();
        private readonly IObjectMapper _mapper = Substitute.For<IObjectMapper>();

        [Fact]
        public async Task UpdatePastMedicalHistoryShouldUpdateCorrectly()
        {
            //Arrange
            _pastMedicalConditionRepository.GetAll().Returns(GetPatientPastMedicalConditions().BuildMock());
            _patientPastMedicalConditionMedicationRepository.GetAll().Returns(GetPastMedicalConditionMedications().BuildMock());
            var edit = new PatientPastMedicalConditionCommandRequest
            {
                Id = 1,
                SnomedId = 1234,
                DiagnosisPeriod = 3,
                PeriodUnit = Misc.UnitOfTime.Day,
                Control = ConditionControl.WellControlled,
                IsOnMedication = true,
                Notes = "Test Note 2",
                NumberOfPreviousInfarctions = 2,
                IsHistoryOfAngina = true,
                IsPreviousHistoryOfAngina = false,
                IsPreviousOfAngiogram = false,
                IsPreviousOfStenting = false,
                IsPreviousOfMultipleInfarction = true,
                IsStillIll = true,
                PatientId = 1,
                IsPrimaryTemplate = true,
                ChronicCondition = "Headache",
                Medications = new List<PatientPastMedicalConditionMedicationRequest>()
            };
            //Act
            var handler = new UpdatePastMedicalConditionCommandHandler(_pastMedicalConditionRepository, 
                _patientPastMedicalConditionMedicationRepository, _mapper);
            await handler.Handle(edit);
            var result = _pastMedicalConditionRepository.GetAll().SingleOrDefault(x => x.Id == 1);
            var medication = _patientPastMedicalConditionMedicationRepository.GetAll().Where(x => x.PatientPastMedicalConditionId == edit.Id).ToList();
            //Assert
            result.ChronicCondition.ShouldBe("Headache");
            result.Notes.ShouldBe("Test Note 2");
            medication.Count.ShouldBe(1);
        }


        private static IQueryable<PatientPastMedicalCondition> GetPatientPastMedicalConditions()
        {
            return new List<PatientPastMedicalCondition>()
            {
                new()
                {
                    Id = 1,
                    TenantId = 1,
                    SnomedId = 1234,
                    DiagnosisPeriod = 3,
                    PeriodUnit = Misc.UnitOfTime.Day,
                    Control = ConditionControl.WellControlled,
                    IsOnMedication = true,
                    Notes = "Test Note",
                    NumberOfPreviousInfarctions = 2,
                    IsHistoryOfAngina = true,
                    IsPreviousHistoryOfAngina = false,
                    IsPreviousOfAngiogram = false,
                    IsPreviousOfStenting = false,
                    IsPreviousOfMultipleInfarction = true,
                    IsStillIll = true,
                    PatientId = 1,
                    IsPrimaryTemplate = true,
                    ChronicCondition = "Migrane"
                }
            }.AsQueryable();
        }


        private static IQueryable<PatientPastMedicalConditionMedication> GetPastMedicalConditionMedications()
        {
            return new List<PatientPastMedicalConditionMedication>()
            {
                new()
                {
                    TenantId = 1,
                    MedicationType = "Tablet",
                    MedicationDose = "2",
                    PrescriptionFrequency = 3,
                    FrequencyUnit = "4",
                    IsCompliantWithMedication = true,
                    MedicationUsageFrequency = 3,
                    MedicationUsageFrequencyUnit = "2",
                    PatientPastMedicalConditionId = 1
                }
            }.AsQueryable();
        }
    }
}
