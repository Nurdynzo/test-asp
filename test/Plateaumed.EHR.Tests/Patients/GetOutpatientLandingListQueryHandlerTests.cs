using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations;
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
    public class GetOutpatientLandingListQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenAttendingPhysician_ShouldReturnsAppointmentsAwaitingDoctorAssignedToUser()
        {
            // Arrange
            var input = new GetAllForLookupTableInput
            {
                OutPatientListingType = OutPatientListingType.AttendingPhysician
            };

            var handler = CreateHandler(1);
            // Act
            var pagedResultDto = await handler.Handle(input);
            // Assert
            pagedResultDto.Items.Count.ShouldBe(1);
            pagedResultDto.Items[0].Id.ShouldBe(1);
        }

        [Fact]
        public async Task Handle_GivenAttendingClinicAndStatusNull_ShouldReturnAppointmentsAwaitingDoctorOrVitals()
        {
            // Arrange
            var input = new GetAllForLookupTableInput
            {
                OutPatientListingType = OutPatientListingType.AttendingClinic
            };

            var handler = CreateHandler(1);
            // Act
            var pagedResultDto = await handler.Handle(input);
            // Assert
            pagedResultDto.Items.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_GivenAttendingClinicAndStatusAwaitingDoctor_ShouldReturnAppointmentsAwaitingDoctor()
        {
            // Arrange
            var input = new GetAllForLookupTableInput
            {
                OutPatientListingType = OutPatientListingType.AttendingClinic,
                Status = AppointmentStatusType.Awaiting_Doctor
                
            };

            var handler = CreateHandler(1);
            // Act
            var pagedResultDto = await handler.Handle(input);
            // Assert
            pagedResultDto.Items.Count.ShouldBe(2);
        }

        private GetOutpatientLandingListQueryHandler CreateHandler(int userId)
        {
            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            patientRepository.GetAll().Returns(GetPatients());
            var patientAppointmentRepository = Substitute.For<IRepository<PatientAppointment, long>>();
            patientAppointmentRepository.GetAll().Returns(GetPatientAppointments());
            var patientEncounterRepository = Substitute.For<IRepository<PatientEncounter, long>>();
            patientEncounterRepository.GetAll().Returns(GetPatientEncounters());
            var patientCodeMappingRepository = Substitute.For<IRepository<PatientCodeMapping, long>>();
            patientCodeMappingRepository.GetAll().Returns(GetPatientCodeMappings());
            var staffMemberRepository = Substitute.For<IRepository<StaffMember, long>>();
            staffMemberRepository.GetAll().Returns(GetStaffMembers());
            var userOrganizationUnitRepository = Substitute.For<IRepository<UserOrganizationUnit, long>>();
            userOrganizationUnitRepository.GetAll().Returns(GetUserOrgUnits());
            var organizationUnitExtendedRepository = Substitute.For<IRepository<OrganizationUnitExtended, long>>();
            organizationUnitExtendedRepository.GetAll().Returns(GetOrgUnitExtended());
            var abpSession = Substitute.For<IAbpSession>();
            abpSession.UserId.Returns(userId);
            var getCurrentUserFacilityIdQueryHandler = Substitute.For<IGetCurrentUserFacilityIdQueryHandler>();
            var diagnosis = Substitute.For<IRepository<Diagnosis, long>>();
            diagnosis.GetAll().Returns(GetDiagnosis());
            var vitals = Substitute.For<IRepository<PatientVital, long>>();
            vitals.GetAll().Returns(GetVitals());
            var invoice = Substitute.For<IRepository<Invoice, long>>();
            invoice.GetAll().Returns(GetInvoice());

            return new GetOutpatientLandingListQueryHandler(
                patientRepository,
                patientAppointmentRepository,
                patientEncounterRepository,
                patientCodeMappingRepository,
                staffMemberRepository,
                userOrganizationUnitRepository,
                organizationUnitExtendedRepository,
                abpSession,
                getCurrentUserFacilityIdQueryHandler,
                vitals,
                diagnosis,
                invoice
            );
        }

        private IQueryable<PatientAppointment> GetPatientAppointments()
        {
            return new List<PatientAppointment>
            {
                new PatientAppointment
                {
                    Id = 1,
                    PatientId = 1,
                    AttendingClinicId = 1,
                    AttendingPhysicianId = 1,
                    Status = AppointmentStatusType.Awaiting_Doctor,
                    StartTime = DateTime.Now
                },
                new PatientAppointment
                {
                    Id = 2,
                    PatientId = 2,
                    AttendingClinicId = 1,
                    AttendingPhysicianId = 2,
                    Status = AppointmentStatusType.Awaiting_Doctor,
                    StartTime = DateTime.Now
                },
                new PatientAppointment
                {
                    Id = 3,
                    PatientId = 3,
                    AttendingClinicId = 1,
                    AttendingPhysicianId = 1,
                    Status = AppointmentStatusType.Awaiting_Vitals,
                    StartTime = DateTime.Now
                },
            }.AsQueryable().BuildMock();
        }
        
        private IQueryable<StaffMember> GetStaffMembers()
        {
            return new List<StaffMember>
            {
                new StaffMember
                {
                    Id = 1,
                    UserId = 1,
                    UserFk = new User
                    {
                        Id = 1,
                        Name = "test",
                        Surname = "test",
                    }
                },
                new StaffMember
                {
                    Id = 2,
                    UserId = 2,
                    UserFk = new User
                    {
                        Id = 2,
                        Name = "test",
                        Surname = "test",
                    }
                },
            }.AsQueryable().BuildMock();
        }

        private IQueryable<PatientEncounter> GetPatientEncounters()
        {
            return new List<PatientEncounter>
            {
                new PatientEncounter
                {
                    Id = 1,
                    PatientId = 1,
                    AppointmentId = 1,
                    Status = EncounterStatusType.InProgress,
                    TimeIn = DateTime.Now
                },
                new PatientEncounter
                {
                    Id = 2,
                    PatientId = 2,
                    AppointmentId = 2,
                    Status = EncounterStatusType.InProgress,
                    TimeIn = DateTime.Now
                },
                new PatientEncounter
                {
                    Id = 3,
                    PatientId = 3,
                    AppointmentId = 3,
                    Status = EncounterStatusType.InProgress,
                    TimeIn = DateTime.Now
                },
            }.AsQueryable().BuildMock();
        }

        private static IQueryable<Patient> GetPatients()
        {
            return new List<Patient>
            {
                new Patient
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                },
                new Patient
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                },
                new Patient
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Smith",
                },
            }.AsQueryable().BuildMock();
        }

        private IQueryable<PatientCodeMapping> GetPatientCodeMappings()
        {
            return new List<PatientCodeMapping>
            {
                new PatientCodeMapping
                {
                    PatientId = 1,
                    PatientCode = "test"
                },
                new PatientCodeMapping
                {
                    PatientId = 2,
                    PatientCode = "test"
                },
                new PatientCodeMapping
                {
                    PatientId = 3,
                    PatientCode = "test"
                },
            }.AsQueryable().BuildMock();
        }

        private static IQueryable<OrganizationUnitExtended> GetOrgUnitExtended()
        {
            return new List<OrganizationUnitExtended>
            {
                new OrganizationUnitExtended
                {
                    Id = 1,
                }
            }.AsQueryable().BuildMock();
        }

        private static IQueryable<UserOrganizationUnit> GetUserOrgUnits()
        {
            return new List<UserOrganizationUnit>
            {
                new UserOrganizationUnit
                {
                    Id = 1,
                    OrganizationUnitId = 1,
                    UserId = 1
                },
                new UserOrganizationUnit
                {
                    Id = 2,
                    OrganizationUnitId = 2,
                    UserId = 2
                },
            }.AsQueryable().BuildMock();
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

        private static IQueryable<Invoice> GetInvoice() =>
            new List<Invoice>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    PaymentStatus = PaymentStatus.Paid,
                    FacilityId = 1
                }                
            }.AsQueryable();
    }
}