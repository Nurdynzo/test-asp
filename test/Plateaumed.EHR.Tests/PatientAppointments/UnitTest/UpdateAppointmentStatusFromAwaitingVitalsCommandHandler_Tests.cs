using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Command;
using Plateaumed.EHR.Staff;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientAppointments.UnitTest
{
    public class UpdateAppointmentStatusFromAwaitingVitalsCommandHandler_Tests
    {

        [Fact]
        public async Task Handle_GivenValidEncounterId_ShouldUpdateAppointmentStatus()
        {
            // Arrange
            var encounterId = 1;

            var patientEncounters = GetPatientEncounters().ToList();

            var patientEncounterRepository = Substitute.For<IRepository<PatientEncounter, long>>();
            var roleRepository = Substitute.For<IRepository<Role>>();
            var userRoleRepository = Substitute.For<IRepository<UserRole, long>>();
            var patientAppointmentRepository = Substitute.For<IRepository<PatientAppointment, long>>();
            var staffMemberRepository = Substitute.For<IRepository<StaffMember, long>>();
            var unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();
            var abpSession = Substitute.For<IAbpSession>();
            
           patientEncounterRepository.GetAll().Returns(patientEncounters.AsQueryable().BuildMock());

            roleRepository.GetAll().Returns(GetRoles().BuildMock());

            userRoleRepository.GetAll().Returns(GetUserRole().BuildMock());

            staffMemberRepository.GetAll().Returns(GetStaffMember().BuildMock());

            var handler = new UpdateAppointmentStatusFromAwaitingVitalsCommandHandler(
                patientEncounterRepository
                , roleRepository, userRoleRepository,
                patientAppointmentRepository, staffMemberRepository,
                unitOfWorkManager, abpSession);

            // Act
            var result = await handler.Handle(encounterId);

            // Assert
            result.ShouldBe("Patient appointment has been updated.");

            Assert.True(patientEncounters.Any(encounter => encounter.Appointment.Status == AppointmentStatusType.Awaiting_Doctor));
        }


        [Fact]
        public async Task Handle_ThrowsException_WhenAppointmentStatusIsNotAwaitingVitals()
        {
            // Arrange
            var encounterId = 1;

            var patientEncounters = GetPatientEncounters().ToList();
            var appointment = patientEncounters[0].Appointment;
            appointment.Status = AppointmentStatusType.Awaiting_Clinician; 

            var patientEncounterRepository = Substitute.For<IRepository<PatientEncounter, long>>();
            var roleRepository = Substitute.For<IRepository<Role>>();
            var userRoleRepository = Substitute.For<IRepository<UserRole, long>>();
            var patientAppointmentRepository = Substitute.For<IRepository<PatientAppointment, long>>();
            var staffMemberRepository = Substitute.For<IRepository<StaffMember, long>>();
            var unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();
            var abpSession = Substitute.For<IAbpSession>();

            patientEncounterRepository.GetAll().Returns(patientEncounters.AsQueryable().BuildMock());
            roleRepository.GetAll().Returns(GetRoles().BuildMock());
            userRoleRepository.GetAll().Returns(GetUserRole().BuildMock());
            staffMemberRepository.GetAll().Returns(GetStaffMember().BuildMock());

            var handler = new UpdateAppointmentStatusFromAwaitingVitalsCommandHandler(
                patientEncounterRepository,
                roleRepository,
                userRoleRepository,
                patientAppointmentRepository,
                staffMemberRepository,
                unitOfWorkManager,
                abpSession);

            // Act
             var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(encounterId));
            // Assert
            exception.Message.ShouldBe("Appointment status must be awaiting vitals");
        }


        [Fact]
        public async Task Handle_ThrowsException_WhenLoggedInUserIsNotNurse()
        {
            // Arrange
            var encounterId = 1;

            var patientEncounters = GetPatientEncounters().ToList();
            var staffMembers = GetStaffMember().ToList();

            var patientEncounterRepository = Substitute.For<IRepository<PatientEncounter, long>>();
            var roleRepository = Substitute.For<IRepository<Role>>();
            var userRoleRepository = Substitute.For<IRepository<UserRole, long>>();
            var patientAppointmentRepository = Substitute.For<IRepository<PatientAppointment, long>>();
            var staffMemberRepository = Substitute.For<IRepository<StaffMember, long>>();
            var unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();
            var abpSession = Substitute.For<IAbpSession>();

            patientEncounterRepository.GetAll().Returns(patientEncounters.AsQueryable().BuildMock());
            roleRepository.GetAll().Returns(GetRoles().BuildMock());
            userRoleRepository.GetAll().Returns(GetUserRole().BuildMock());
            staffMemberRepository.GetAll().Returns(staffMembers.AsQueryable().BuildMock());

            staffMembers[0].AdminRole.Name = StaticRoleNames.JobRoles.LaboratoryTechnician;

            var handler = new UpdateAppointmentStatusFromAwaitingVitalsCommandHandler(
                patientEncounterRepository,
                roleRepository,
                userRoleRepository,
                patientAppointmentRepository,
                staffMemberRepository,
                unitOfWorkManager,
                abpSession);

            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(encounterId));
            // Assert
            exception.Message.ShouldBe("Logged in user must be a nurse");
        }

        private IQueryable<PatientEncounter> GetPatientEncounters()
        {
            return new List<PatientEncounter>()
            {
                new PatientEncounter
                {
                     Id = 1,
                     Appointment = new PatientAppointment
                     {
                          Id = 1,
                           Status = AppointmentStatusType.Awaiting_Vitals,
                           AttendingPhysicianId = 1
                     }
                }
            }.AsQueryable();
            
        }

        private IQueryable<Role> GetRoles()
        {
            return new List<Role>()
            {
                new Role
                {
                     Id = 1,
                     Name = StaticRoleNames.JobRoles.Doctor
                },
                new Role
                {
                     Id = 2,
                     Name = StaticRoleNames.JobRoles.Nurse
                }
            }.AsQueryable();
        }


        private IQueryable<UserRole> GetUserRole()
        {
            return new List<UserRole>()
            {
                new UserRole
                {
                     Id = 1,
                     UserId = 1,
                      RoleId = 1,
                },
                new UserRole
                {
                     Id = 2,
                     UserId = 2,
                     RoleId = 2,
                }
            }.AsQueryable();
        }

        private IQueryable<StaffMember> GetStaffMember()
        {
            return new List<StaffMember>()
            {
                new StaffMember
                {
                     UserId = 1,
                     AdminRole = new Role
                     {
                          Id=1,
                          Name = StaticRoleNames.JobRoles.Nurse
                     }
                     
                },
                new StaffMember
                {
                     UserId = 1,
                     AdminRole = new Role
                     {
                          Id=1,
                          Name = StaticRoleNames.JobRoles.Nurse
                     }
                }
            }.AsQueryable();
        }
    }
}
