using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.DateUtils;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Patients.Query;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.VitalSigns;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Patients
{
    [Trait("Category", "Unit")]
    public class GetInpatientLandingListQueryHandlerTests
    {
        [Fact]
        public async Task Handle_NoPatients_ShouldReturnEmptyList()
        {
            // Arrange
            var request = new GetInpatientLandingListRequest();

            var repository = Substitute.For<IRepository<PatientEncounter, long>>();
            repository.GetAll().Returns(Array.Empty<PatientEncounter>().AsQueryable().BuildMock());

            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.Now.Returns(DateTime.Now);

            var vitals = Substitute.For<IRepository<PatientVital, long>>();
            vitals.GetAll().Returns(GetVitals().BuildMock());

            var diagnosis = Substitute.For<IRepository<Diagnosis, long>>();
            diagnosis.GetAll().Returns(GetDiagnosis().BuildMock());

            var users = Substitute.For<IRepository<User, long>>();
            users.GetAll().Returns(GetUsers().BuildMock());

            var admission = Substitute.For<IRepository<Admission, long>>();
            admission.GetAll().Returns(GetAdmission().BuildMock());
            var medications = Substitute.For<IRepository<AllInputs.Medication, long>>();
            medications.GetAll().Returns(GetMedications().BuildMock());

            var procedures = Substitute.For<IRepository<Procedure, long>>();
            procedures.GetAll().Returns(GetProcedures().BuildMock());

            var stability = Substitute.For<IRepository<PatientStability, long>>();
            stability.GetAll().Returns(GetPatientStability().BuildMock());

            var invoices = Substitute.For<IRepository<Invoice, long>>();
            invoices.GetAll().Returns(GetInvoices().BuildMock());

            var handler = new GetInpatientLandingListQueryHandler(repository, dateTimeProvider, vitals, diagnosis, users, admission, medications, procedures, stability, invoices);
            // Act
            var result = await handler.Handle(request);

            // Assert
            result.Items.ShouldBeEmpty();
        }

        [Fact]
        public async Task Handle_PatientWithInProgressEncounterInWard_ShouldReturnPatient()
        {
            // Arrange
            var request = new GetInpatientLandingListRequest
            {
                WardId = 1
            };

            var patientEncounters = new List<PatientEncounter>
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    Patient = new Patient
                    {
                        Id = 1,
                        FirstName = "Test",
                        LastName = "Patient"
                    },
                    Status = EncounterStatusType.InProgress,
                    ServiceCentre = ServiceCentreType.InPatient,
                    WardId = request.WardId,
                    LastModifierUserId= 1,
                    WardBedId = 1,
                    WardBed = new WardBed
                    {
                        Id = 1,
                        BedNumber = "22",
                        WardId = request.WardId
                    }
                },
                new()
                {
                    Id = 2,
                    PatientId = 2,
                    Patient = new Patient
                    {
                        Id = 2,
                        FirstName = "Test",
                        LastName = "Patient"
                    },
                    Status = EncounterStatusType.InProgress,
                    ServiceCentre = ServiceCentreType.InPatient,
                    WardId = 2,
                    LastModifierUserId = 1,
                    WardBedId = 2,
                    WardBed = new WardBed
                    {
                        Id = 2,
                        BedNumber = "23",
                        WardId = request.WardId
                    }
                }
            };

            var repository = Substitute.For<IRepository<PatientEncounter, long>>();
            repository.GetAll().Returns(patientEncounters.AsQueryable().BuildMock());

            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.Now.Returns(DateTime.Now);

            var vitals = Substitute.For<IRepository<PatientVital, long>>();
            vitals.GetAll().Returns(GetVitals().BuildMock());

            var diagnosis = Substitute.For<IRepository<Diagnosis, long>>();
            diagnosis.GetAll().Returns(GetDiagnosis().BuildMock());

            var users = Substitute.For<IRepository<User, long>>();
            users.GetAll().Returns(GetUsers().BuildMock());

            var admission = Substitute.For<IRepository<Admission, long>>();
            admission.GetAll().Returns(GetAdmission().BuildMock());
            var medications = Substitute.For<IRepository<AllInputs.Medication, long>>();
            medications.GetAll().Returns(GetMedications().BuildMock());

            var procedures = Substitute.For<IRepository<Procedure, long>>();
            procedures.GetAll().Returns(GetProcedures().BuildMock());

            var stability = Substitute.For<IRepository<PatientStability, long>>();
            stability.GetAll().Returns(GetPatientStability().BuildMock());

            var invoices = Substitute.For<IRepository<Invoice, long>>();
            invoices.GetAll().Returns(GetInvoices().BuildMock());

            var handler = new GetInpatientLandingListQueryHandler(repository, dateTimeProvider, vitals, diagnosis, users, admission, medications, procedures, stability, invoices);
            // Act
            var result = await handler.Handle(request);
            // Assert
            result.TotalCount.ShouldBe(1);
            result.Items[0].PatientId.ShouldBe(1);
            result.Items[0].FullName.ShouldBe("Test Patient");
        }

        [Theory]
        [InlineData("2021-01-03T10:01:00", "2021-01-02T23:00:00", 0)]
        [InlineData("2021-01-02T10:01:00", "2021-01-01T09:00:00", 0)]
        [InlineData("2021-01-02T10:01:00", "2021-01-01T11:00:00", 0)]
        [InlineData("2021-01-01T21:01:00", "2021-01-01T11:00:00", 1)]
        [InlineData("2021-01-01T09:59:00", "2021-01-01T10:01:00", 1)]
        public async Task Handle_PatientWithTransferredSince10am_ShouldReturnPatient(string now, string transferredOut, int returned)
        {
            // Arrange
            var request = new GetInpatientLandingListRequest
            {
                WardId = 1
            };

            var patientEncounters = new List<PatientEncounter>
            {
                new()
                {
                    PatientId = 1,
                    Patient = new Patient
                    {
                        Id = 1,
                        FirstName = "Test",
                        LastName = "Patient"
                    },
                    TimeOut = DateTime.Parse(transferredOut),
                    Status = EncounterStatusType.Transferred,
                    ServiceCentre = ServiceCentreType.InPatient,
                    WardId = request.WardId,
                    LastModifierUserId = 1,
                    WardBedId = 1,
                    WardBed = new WardBed
                    {
                        Id = 1,
                        BedNumber = "22",
                        WardId = request.WardId
                    }
                }
            };

            var repository = Substitute.For<IRepository<PatientEncounter, long>>();
            repository.GetAll().Returns(patientEncounters.AsQueryable().BuildMock());

            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            var currentDate = DateTime.Parse(now);
            dateTimeProvider.Now.Returns(currentDate);

            var vitals = Substitute.For<IRepository<PatientVital, long>>();
            vitals.GetAll().Returns(GetVitals().BuildMock());

            var diagnosis = Substitute.For<IRepository<Diagnosis, long>>();
            diagnosis.GetAll().Returns(GetDiagnosis().BuildMock());

            var users = Substitute.For<IRepository<User, long>>();
            users.GetAll().Returns(GetUsers().BuildMock());

            var admission = Substitute.For<IRepository<Admission, long>>();
            admission.GetAll().Returns(GetAdmission().BuildMock());

            var medications = Substitute.For<IRepository<AllInputs.Medication, long>>();
            medications.GetAll().Returns(GetMedications().BuildMock());

            var procedures = Substitute.For<IRepository<Procedure, long>>();
            procedures.GetAll().Returns(GetProcedures().BuildMock());

            var stability = Substitute.For<IRepository<PatientStability, long>>();
            stability.GetAll().Returns(GetPatientStability().BuildMock());

            var invoices = Substitute.For<IRepository<Invoice, long>>();
            invoices.GetAll().Returns(GetInvoices().BuildMock());

            var handler = new GetInpatientLandingListQueryHandler(repository, dateTimeProvider, vitals, diagnosis, users, admission, medications, procedures, stability, invoices);
            // Act
            var result = await handler.Handle(request);

            // Assert
            result.Items.Count.ShouldBe(returned);
        }

        private static IQueryable<PatientVital> GetVitals() =>
            new List<PatientVital>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    MeasurementRange = new MeasurementRange
                    {
                        Id = 1,
                        Lower = 1,
                        Unit = "CPM",
                        Upper = 2,

                    },
                    VitalSign = new VitalSign
                    {
                        Id = 1,
                        Sign = "Heart Beat"
                    }
                }
            }.AsQueryable();

        private static IQueryable<Diagnosis> GetDiagnosis() =>
            new List<Diagnosis>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    Description = "testing 101"
                },
                new()
                {
                    Id = 2,
                    PatientId = 1,
                    Description = "Testing 102"
                }
            }.AsQueryable();

        private static IQueryable<User> GetUsers() =>
           new List<User>()
           {
                new() { Id = 1, Surname = "Tester", Name ="Tester" },
           }.AsQueryable();

        private static IQueryable<Admission> GetAdmission() =>
            new List<Admission>()
            {
                new() {
                    Id = 1,
                    PatientId = 1,
                    AttendingPhysicianId = 1,
                    AttendingPhysician = new StaffMember
                    {
                        Id = 1,
                        UserId = 1,
                        UserFk = new User
                        {
                            Surname = "Prince",
                            Name = "Chuk",
                            Title = TitleType.Dr,

                        },
                        Jobs = new List<Job>()
                        {
                            new Job
                            {
                                Unit = new EHR.Organizations.OrganizationUnitExtended
                                {
                                    DisplayName = "Test Unit"
                                }
                            }
                        }
                    }
                }
            }.AsQueryable();

        private static IQueryable<AllInputs.Medication> GetMedications() =>
            new List<AllInputs.Medication>()
            {
                new ()
                {
                    Id = 1, PatientId = 1,
                    Direction = "Swallow",
                    DoseUnit = "1",
                    Frequency = "3x Daily",
                    ProductName = "Paracetamol"
                }
            }.AsQueryable();

        private static IQueryable<Procedure> GetProcedures() =>
            new List<Procedure>()
            {
                new()
                {
                    Id = 1,
                    SelectedProcedures = "",
                    IsDeleted = false,
                    ProcedureStatus = ProcedureStatus.Ongoing
                }
            }.AsQueryable();

        private static IQueryable<PatientStability> GetPatientStability() =>
            new List<PatientStability>()
            {
               new ()
               {
                   Id = 1,
                   CreationTime = DateTime.Now,
                   PatientId = 1,
                   Status= PatientStabilityStatus.CriticallyIll
               }
            }.AsQueryable();

        private static IQueryable<Invoice> GetInvoices() =>
            new List<Invoice>()
            {
                new ()
                {
                    Id = 1,
                    PatientId = 1,
                    AmountPaid = new ValueObjects.Money{ Amount = 10, Currency = "NGN"},
                    OutstandingAmount = new ValueObjects.Money{ Amount = 1, Currency = "NGN"},
                    PatientEncounterId = 1
                }
            }.AsQueryable();

    }
}