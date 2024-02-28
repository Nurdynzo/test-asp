using System.Collections.Generic;
using Abp.Domain.Repositories;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Organizations;

namespace Plateaumed.EHR.PatientAppointments.Query.BaseQueryHelper
{
    public class AppointmentsBaseQuery : IBaseQuery
    {
        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMappingRepository;
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitRepository;
        private readonly IRepository<StaffMember, long> _staffMemberRepository;
        private readonly IRepository<PatientReferralDocument, long> _patientReferralDocumentRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<PatientScanDocument,long> _patientScanDocumentRepository;
        private readonly IRepository<Role> _roleRepository;

        /// <summary>
        /// Constructor for GetAppointmentForTodayQueryHandler
        /// </summary>
        /// <param name="patientAppointmentRepository"></param>
        /// <param name="patientCodeMappingRepository"></param>
        /// <param name="patientRepository"></param>
        /// <param name="organizationUnitRepository"></param>
        /// <param name="staffMemberRepository"></param>
        /// <param name="patientReferralDocumentRepository"></param>
        /// <param name="userRepository"></param>
        /// <param name="patientScanDocumentRepository"></param>
        /// <param name="roleRepository"></param>
        public AppointmentsBaseQuery(
            IRepository<PatientAppointment, long> patientAppointmentRepository,
            IRepository<PatientCodeMapping, long> patientCodeMappingRepository,
            IRepository<Patient, long> patientRepository,
            IRepository<OrganizationUnitExtended, long> organizationUnitRepository,
            IRepository<StaffMember, long> staffMemberRepository,
            IRepository<PatientReferralDocument, long> patientReferralDocumentRepository,
            IRepository<User, long> userRepository,
            IRepository<PatientScanDocument, long> patientScanDocumentRepository,
            IRepository<Role> roleRepository)
        {
            _patientAppointmentRepository = patientAppointmentRepository;
            _patientCodeMappingRepository = patientCodeMappingRepository;
            _patientRepository = patientRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _staffMemberRepository = staffMemberRepository;
            _patientReferralDocumentRepository = patientReferralDocumentRepository;
            _userRepository = userRepository;
            _patientScanDocumentRepository = patientScanDocumentRepository;
            _roleRepository = roleRepository;
        }

        /// <inheritdoc />
        public IQueryable<AppointmentBaseQuery> GetAppointmentsBaseQuery()
        {
            var query = from a in _patientAppointmentRepository.GetAll().Include(a => a.AttendingClinicFk)
                        join p in _patientRepository.GetAll() on a.PatientId equals p.Id
                        join m in _patientCodeMappingRepository.GetAll() on p.Id equals m.PatientId
                        join aou in _organizationUnitRepository.GetAll() on a.AttendingClinicId equals aou.Id into attendingGroup
                        from attending in attendingGroup.DefaultIfEmpty()
                        join aou2 in _organizationUnitRepository.GetAll() on a.ReferringClinicId equals aou2.Id into referringGroup
                        from referring in referringGroup.DefaultIfEmpty()
                        join sm in _staffMemberRepository.GetAll() on a.AttendingPhysicianId equals sm.Id into staffGroup
                        from staff in staffGroup.DefaultIfEmpty()
                        join u in _userRepository.GetAll().Include(x=>x.Roles) on staff.UserId equals u.Id into userGroup
                        from u in userGroup.DefaultIfEmpty()
                        join prd in _patientReferralDocumentRepository.GetAll() on a.PatientReferralDocumentId equals prd.Id into
                            prdGroup
                        from prd in prdGroup.DefaultIfEmpty()
                        
                        join s in _patientScanDocumentRepository.GetAll() on m.PatientCode equals s.PatientCode into
                            sGroup
                        from s in sGroup.DefaultIfEmpty()
                    
                        select new AppointmentBaseQuery
                        {
                            Appointment = a,
                            Patient = p,
                            PatientCodeMapping = m,
                            AttendingClinic = attending,
                            ReferringClinic = referring,
                            AttendingPhysician = staff,
                            ReferralDocument = prd,
                            StaffUser = u,
                            PatientScanDocument = s,
                            Roles = u.Roles != null ? _roleRepository
                                .GetAll()
                                .Where(x => u.Roles
                                    .Select(y=>y.RoleId)
                                    .Contains(x.Id))
                                .Select(n=>n.DisplayName) : new List<string>()
                        };

            return query;
        }
    }
}
