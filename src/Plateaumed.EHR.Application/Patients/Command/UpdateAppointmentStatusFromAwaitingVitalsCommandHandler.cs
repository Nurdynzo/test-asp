using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Patients.Command
{
    public class UpdateAppointmentStatusFromAwaitingVitalsCommandHandler : IUpdateAppointmentStatusFromAwaitingVitalsCommandHandler
    {
        private readonly IRepository<PatientEncounter, long> _patientEncounterRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
        private readonly IRepository<StaffMember, long> _staffMemberRepository;
        private readonly IUnitOfWorkManager _unitOfWork;
        private readonly IAbpSession _abpSession;

        public UpdateAppointmentStatusFromAwaitingVitalsCommandHandler(
            IRepository<PatientEncounter, long> patientEncounterRepository,
            IRepository<Role> roleRepository, IRepository<UserRole, long> userRoleRepository,
            IRepository<PatientAppointment, long> patientAppointmentRepository,
            IRepository<StaffMember, long> staffMemberRepository,
            IUnitOfWorkManager unitOfWork,
            IAbpSession abpSession)
        {
            _patientEncounterRepository = patientEncounterRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _patientAppointmentRepository = patientAppointmentRepository;
            _staffMemberRepository = staffMemberRepository;
            _unitOfWork = unitOfWork;
            _abpSession = abpSession;
        }
        public async Task<string> Handle(long encounterId)
        {

            // validate that the appointment exist
            var encounter = await _patientEncounterRepository.GetAll()
                .Include(x => x.Appointment)
                .SingleOrDefaultAsync(v => v.Id == encounterId);
            var appointment = encounter.Appointment;
            if (appointment == null)
                throw new UserFriendlyException("Appointment does not exist.");

            if (appointment.Status != AppointmentStatusType.Awaiting_Vitals) 
                throw new UserFriendlyException("Appointment status must be awaiting vitals");

                // get any doctor base on the role
                var role = await _roleRepository.GetAll().FirstOrDefaultAsync(v => v.Name == StaticRoleNames.JobRoles.Doctor &&
                                                                               v.TenantId == _abpSession.TenantId);
            if (role == null)
                throw new UserFriendlyException($"No role with name {StaticRoleNames.JobRoles.Doctor} exist.");


            var userWithDoctorRole = await _userRoleRepository.GetAll().FirstOrDefaultAsync(v => v.RoleId == role.Id);
            if (userWithDoctorRole == null)
                throw new UserFriendlyException("No user with doctor role has been setup.");

            var nurse = await _staffMemberRepository.GetAll().FirstOrDefaultAsync(v => v.UserId == userWithDoctorRole.UserId);

            if (nurse.AdminRole.Name != StaticRoleNames.JobRoles.Nurse)
                throw new UserFriendlyException("Logged in user must be a nurse");

            var doctor = await _staffMemberRepository.GetAll().FirstOrDefaultAsync(v => v.UserId == userWithDoctorRole.UserId);
            if (doctor == null)
                throw new UserFriendlyException("No doctor account available to assign the appointment to.");

            appointment.AttendingPhysicianId = (long)doctor.Id;
            appointment.Status = AppointmentStatusType.Awaiting_Doctor;

            // update the appointment
            await _patientAppointmentRepository.UpdateAsync(appointment);
            await _unitOfWork.Current.SaveChangesAsync();

            return "Patient appointment has been updated.";
        }
    }
}
