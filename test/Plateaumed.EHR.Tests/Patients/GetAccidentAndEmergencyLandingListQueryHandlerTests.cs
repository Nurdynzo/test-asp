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
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Patients.Query;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.VitalSigns;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Patients
{
    [Trait("Category", "Unit")]
    public class GetAccidentAndEmergencyLandingListQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenNoPatients_ShouldReturnEmptyList()
        {
            // Arrange
            var request = new GetAccidentAndEmergencyLandingListRequest();

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

            var handler = new GetAccidentAndEmergencyLandingListQueryHandler(repository, dateTimeProvider, vitals, diagnosis, users, admission);
            // Act
            var result = await handler.Handle(request);

            // Assert
            result.Items.ShouldBeEmpty();
        }

        [Fact]
        public async Task Handle_ShouldReturnAEPatientsInProgress()
        {
            // Arrange
            var request = new GetAccidentAndEmergencyLandingListRequest();

            var repository = Substitute.For<IRepository<PatientEncounter, long>>();
            var patientEncounters =new List<PatientEncounter>
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
                    ServiceCentre = ServiceCentreType.AccidentAndEmergency,
                    LastModifierUserId= 1,
                    WardBedId = 1,
                    WardBed = new EHR.Facilities.WardBed
                    {
                        Id = 1,
                        BedNumber = "21",
                        WardId = 1
                    }
                },
                new()
                {
                    Id = 2,
                    PatientId = 2,
                    Patient = new Patient
                    {
                        Id = 2,
                        FirstName = "Test2",
                        LastName = "Patient2"
                    },
                    Status = EncounterStatusType.Deceased,
                    ServiceCentre = ServiceCentreType.AccidentAndEmergency,
                    LastModifierUserId= 1,
                    WardBedId = 1,
                    WardBed = new EHR.Facilities.WardBed
                    {
                        Id = 2,
                        BedNumber = "22",
                        WardId = 1
                    }
                },
                new()
                {
                    Id = 3,
                    PatientId = 3,
                    Patient = new Patient
                    {
                        Id = 3,
                        FirstName = "Test3",
                        LastName = "Patient3"
                    },
                    Status = EncounterStatusType.InProgress,
                    ServiceCentre = ServiceCentreType.InPatient,
                    LastModifierUserId= 1,
                    WardBedId = 1,
                    WardBed = new EHR.Facilities.WardBed
                    {
                        Id = 3,
                        BedNumber = "23",
                        WardId = 1
                    }
                }
            };
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

            var handler = new GetAccidentAndEmergencyLandingListQueryHandler(repository, dateTimeProvider, vitals, diagnosis, users, admission);
            // Act
            var result = await handler.Handle(request);

            // Assert
            result.Items.Count.ShouldBe(1);
            result.Items.First().PatientId.ShouldBe(1);
        }

        [Theory]
        [InlineData("2021-01-02T00:01:00", "2021-01-01T11:59:00", 0)]
        [InlineData("2021-01-03T00:01:00", "2021-01-01T11:59:00", 0)]
        [InlineData("2021-01-01T11:59:00", "2021-01-01T00:00:01", 1)]
        public async Task Handle_PatientWithTransferredSince10am_ShouldReturnPatient(string now, string transferredOut, int returned)
        {
            // Arrange
            var request = new GetAccidentAndEmergencyLandingListRequest();

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
                    ServiceCentre = ServiceCentreType.AccidentAndEmergency,
                    LastModifierUserId= 1,
                    WardBedId = 1,
                    WardBed = new EHR.Facilities.WardBed
                    {
                        Id = 1,
                        BedNumber = "22",
                        WardId = 1
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

            var handler = new GetAccidentAndEmergencyLandingListQueryHandler(repository, dateTimeProvider, vitals, diagnosis, users, admission);
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
    }
}
