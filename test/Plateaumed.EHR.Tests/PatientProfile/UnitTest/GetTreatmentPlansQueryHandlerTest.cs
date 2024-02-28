using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.PatientProfile.Query;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Vaccines;
using Shouldly;
using Xunit;
namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category","Unit")]
    public class GetTreatmentPlansQueryHandlerTest
    {
        private readonly IRepository<Diagnosis,long> _diagnosisRepositoryMock
            = Substitute.For<IRepository<Diagnosis, long>>();
        private readonly IRepository<AllInputs.Medication,long> _medicationRepositoryMock
            = Substitute.For<IRepository<AllInputs.Medication, long>>();
        private readonly IRepository<Vaccination, long> _vaccinationRepositoryMock
            = Substitute.For<IRepository<Vaccination, long>>();
        private readonly IRepository<VaccineSchedule,long> _vaccineScheduleRepositoryMock
            = Substitute.For<IRepository<VaccineSchedule, long>>();
        private readonly IRepository<Vaccine,long> _vaccineRepositoryMock
            = Substitute.For<IRepository<Vaccine, long>>();
        private readonly IRepository<Procedure,long> _procedureRepositoryMock
            = Substitute.For<IRepository<Procedure, long>>();
        private readonly IRepository<AllInputs.PlanItems,long> _planItemsRepositoryMock
            = Substitute.For<IRepository<AllInputs.PlanItems, long>>();
        private readonly GetTreatmentPlansQueryHandler _getTreatmentPlansQueryHandler;
        public GetTreatmentPlansQueryHandlerTest()
        {
            _getTreatmentPlansQueryHandler = new GetTreatmentPlansQueryHandler(
                _diagnosisRepositoryMock,
                _medicationRepositoryMock,
                _vaccinationRepositoryMock,
                _vaccineScheduleRepositoryMock,
                _vaccineRepositoryMock,_procedureRepositoryMock,_planItemsRepositoryMock);
        }
        [Fact]
        public async Task GetTreatmentPlansQueryHandlerTest_Should_Return_TreatmentPlans_Successfully()
        {
            //Arrange
            var request = new GetTreatmentPlansRequest
            {
                PatientId = 1
            };
            _diagnosisRepositoryMock.GetAll().Returns(GetDiagnosis().BuildMock());
            _medicationRepositoryMock.GetAll().Returns(GetMedication().BuildMock());
            _vaccinationRepositoryMock.GetAll().Returns(GetVaccination().BuildMock());
            _vaccineScheduleRepositoryMock.GetAll().Returns(GetVaccineSchedule().BuildMock());
            _vaccineRepositoryMock.GetAll().Returns(GetVaccine().BuildMock());
            _procedureRepositoryMock.GetAll().Returns(GetProcedure().BuildMock());
            _planItemsRepositoryMock.GetAll().Returns(GetPlanItems().BuildMock());
            //Act
            var result = await _getTreatmentPlansQueryHandler.Handle(request);
            //Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
            result[0].TreatmentOtherPlanItems.Count.ShouldBe(2);
            result[0].TreatmentProcedures.Count.ShouldBe(2);
            result[0].TreatmentVaccination.ShouldNotBeNull();
            result[0].TreatmentMedication.ShouldNotBeNull();
            result[0].Diagnosis.ShouldBe("Diagnosis 2");
            result[0].DiagnosisDate.Date.ShouldBe(DateTime.UtcNow.Date);
            result[0].TreatmentMedication.Count.ShouldBe(2);
            result[0].TreatmentMedication[1].DoseAdministered.ShouldBe("3 / 9");
            result[0].TreatmentMedication[1].Details[0].DoseAdministered.ShouldBe("Dose 3 of 9");
            result[1].Diagnosis.ShouldBe("Diagnosis 1");
            result[1].DiagnosisDate.Date.ShouldBe(DateTime.UtcNow.AddDays(-2).Date);
            result[1].TreatmentOtherPlanItems.Count.ShouldBe(2);
            result[1].TreatmentProcedures.Count.ShouldBe(2);
            result[1].TreatmentVaccination.ShouldNotBeNull();
            result[1].TreatmentMedication.ShouldNotBeNull();


        }
        private IQueryable<AllInputs.PlanItems> GetPlanItems()
        {
            return new List<AllInputs.PlanItems>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    CreationTime = DateTime.UtcNow,
                    TenantId = 1,
                    Description = "Description 1",
                },
                new()
                {
                    Id = 2,
                    PatientId = 1,
                    CreationTime = DateTime.UtcNow,
                    TenantId = 1,
                    Description = "Description 2",
                },
                new()
                {
                    Id = 3,
                    PatientId = 1,
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    TenantId = 1,
                    Description = "Description 3",
                },
                new()
                {
                    Id = 4,
                    PatientId = 1,
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    TenantId = 1,
                    Description = "Description 4",
                }
            }.AsQueryable();
        }
        private IQueryable<Procedure> GetProcedure()
        {
            return new List<Procedure>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    CreationTime = DateTime.UtcNow,
                    TenantId = 1,
                    ProcedureType = ProcedureType.RequestProcedure,
                    SelectedProcedures = "SelectedProcedures 1",
                    SnowmedId = 1,
                    Note = "Note 1"
                },
                new()
                {
                    Id = 2,
                    PatientId = 1,
                    CreationTime = DateTime.UtcNow,
                    TenantId = 1,
                    ProcedureType = ProcedureType.RecordProcedure,
                    SelectedProcedures = "SelectedProcedures 2",
                    SnowmedId = 2,
                    Note = "Note 2"
                },
                new()
                {
                    Id = 3,
                    PatientId = 1,
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    TenantId = 1,
                    ProcedureType = ProcedureType.RequestProcedure,
                    SelectedProcedures = "SelectedProcedures 3",
                    SnowmedId = 3,
                    Note = "Note 3"
                },
                new()
                {
                    Id = 4,
                    PatientId = 1,
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    TenantId = 1,
                    ProcedureType = ProcedureType.RecordProcedure,
                    SelectedProcedures = "SelectedProcedures 4",
                    SnowmedId = 4,
                    Note = "Note 4"
                }
            }.AsQueryable();
        }
        private IQueryable<VaccineSchedule> GetVaccineSchedule()
        {
            return new List<VaccineSchedule>()
            {
                new()
                {
                    Id = 1,
                    VaccineId = 1,
                    CreationTime = DateTime.UtcNow,
                    Doses = 1,
                    Dosage = "Dosage 1",
                    RouteOfAdministration = "RouteOfAdministration 1",
                    Notes = "Notes 1",
                },
                new()
                {
                    Id = 2,
                    VaccineId = 2,
                    CreationTime = DateTime.UtcNow,
                    Doses = 2,
                    Dosage = "Dosage 2",
                    RouteOfAdministration = "RouteOfAdministration 2",
                    Notes = "Notes 2",
                },
                new()
                {
                    Id = 3,
                    VaccineId = 3,
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    Doses = 3,
                    Dosage = "Dosage 3",
                    RouteOfAdministration = "RouteOfAdministration 3",
                    Notes = "Notes 3",
                },
                new()
                {
                    Id = 4,
                    VaccineId = 4,
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    Doses = 4,
                    Dosage = "Dosage 4",
                    RouteOfAdministration = "RouteOfAdministration 4",
                    Notes = "Notes 4",
                }
            }.AsQueryable();
        }
        private IQueryable<Vaccination> GetVaccination()
        {
            return new List<Vaccination>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    VaccineScheduleId = 1,
                    CreationTime = DateTime.UtcNow,
                    TenantId = 1,
                    DateAdministered = DateTime.UtcNow,
                    VaccineId = 1,
                    Note = "Note 1",
                    VaccineBrand = "VaccineBrand 1",
                    DueDate = DateTime.UtcNow,
                    HasComplication = true,
                    VaccineBatchNo = "VaccineBatchNo 1",
                    IsAdministered = true
                },
                new()
                {
                    Id = 2,
                    PatientId = 1,
                    VaccineScheduleId = 2,
                    CreationTime = DateTime.UtcNow,
                    TenantId = 1,
                },
                new()
                {
                    Id = 3,
                    PatientId = 1,
                    VaccineScheduleId = 1,
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    TenantId = 1,
                },
                new()
                {
                    Id = 4,
                    PatientId = 1,
                    VaccineScheduleId = 2,
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    TenantId = 1,
                }
            }.AsQueryable();
        }
        private IQueryable<Vaccine> GetVaccine()
        {
            return new List<Vaccine>
            {
                new()
                {
                    Id = 1,
                    CreationTime = DateTime.UtcNow,
                    FullName = "Vaccine 1",
                    Name = "Vaccine 1",
                },
                new()
                {
                    Id = 2,
                    CreationTime = DateTime.UtcNow,
                    FullName = "Vaccine 2",
                    Name = "Vaccine 2",
                },
                new()
                {
                    Id = 3,
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    FullName = "Vaccine 3",
                    Name = "Vaccine 3",
                },
                new()
                {
                    Id = 4,
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    FullName = "Vaccine 4",
                    Name = "Vaccine 4",
                },

            }.AsQueryable();
        }
        private IQueryable<AllInputs.Medication> GetMedication()
        {
            return new List<AllInputs.Medication>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    ProductName = "Medication 1",
                    CreationTime = DateTime.UtcNow,
                    DoseUnit = "3 DoseUnit 1",
                    Frequency = "Frequency 1",
                    Duration = "Duration 1",
                    Direction = "Direction 1",
                    TenantId = 1,
                    MedicationAdministrationActivities = new List<MedicationAdministrationActivity>()
                },
                new()
                {
                    Id = 2,
                    PatientId = 1,
                    ProductName = "Medication 1",
                    CreationTime = DateTime.UtcNow,
                    DoseUnit = "6 DoseUnit 2",
                    Frequency = "Frequency 2",
                    Duration = "Duration 2",
                    Direction = "Direction 2",
                    TenantId = 1,
                    MedicationAdministrationActivities = new List<MedicationAdministrationActivity>()
                    {
                        new()
                        {
                            Id = 1,
                            MedicationId = 1,
                            DoseUnit = "DoseUnit 1",
                            DoseValue = 3,
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    PatientId = 1,
                    ProductName = "Medication 3",
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    DoseUnit = "DoseUnit 3",
                    Frequency = "Frequency 3",
                    Duration = "Duration 3",
                    Direction = "Direction 3",
                    TenantId = 1,
                    MedicationAdministrationActivities = new List<MedicationAdministrationActivity>()
                },
                new()
                {
                    Id = 4,
                    PatientId = 1,
                    ProductName = "Medication 4",
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    DoseUnit = "DoseUnit 4",
                    Frequency = "Frequency 4",
                    Duration = "Duration 4",
                    Direction = "Direction 4",
                    TenantId = 1,
                    MedicationAdministrationActivities = new List<MedicationAdministrationActivity>()
                }

            }.AsQueryable();
        }
        private IQueryable<Diagnosis> GetDiagnosis()
        {
            return new List<Diagnosis>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    Description = "Diagnosis 1",
                    CreationTime = DateTime.UtcNow.AddDays(-2),
                    Notes = "Diagnosis 1 Notes",
                    Status = 1,
                    Sctid = 1,
                    TenantId = 1,
                },
                new()
                {
                    Id = 2,
                    PatientId = 1,
                    Description = "Diagnosis 2",
                    CreationTime = DateTime.UtcNow,
                    Notes = "Diagnosis 2 Notes",
                    Status = 1,
                    Sctid = 2,
                    TenantId = 1,
                }
            }.AsQueryable();
        }
    }
}
