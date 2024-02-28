using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Patients.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Staff;
namespace Plateaumed.EHR.Patients.Query
{
    public class GetViewAllPatientInClinicQueryHandler : IGetViewAllPatientInClinicQueryHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitRepository;
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<PatientAppointment,long> _patientAppointmentRepository;
        private readonly IRepository<StaffMember,long> _staffMemberRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<PatientEncounter,long> _patientEncounterRepository;
        public GetViewAllPatientInClinicQueryHandler(
            IRepository<Patient, long> patientRepository,
            IRepository<OrganizationUnitExtended, long> organizationUnitRepository,
            IRepository<Invoice, long> invoiceRepository,
            IRepository<PatientAppointment, long> patientAppointmentRepository,
            IRepository<StaffMember, long> staffMemberRepository,
            IRepository<User, long> userRepository,
            IRepository<PatientEncounter, long> patientEncounterRepository)
        {
            _patientRepository = patientRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _invoiceRepository = invoiceRepository;
            _patientAppointmentRepository = patientAppointmentRepository;
            _staffMemberRepository = staffMemberRepository;
            _userRepository = userRepository;
            _patientEncounterRepository = patientEncounterRepository;
        }

        public async Task<List<AllPatientInClinicResponse>> Handle(AllPatientInClinicRequest request, long facilityId)
        {
            string searchText = !string.IsNullOrEmpty(request.SearchText) ? request.SearchText.ToLower() : string.Empty;
            var today = DateTime.Now.Date;
            var query = from p in _patientRepository.GetAll()
                        join pa in _patientAppointmentRepository.GetAll() on p.Id equals pa.PatientId
                        join i in _invoiceRepository.GetAll() on pa.PatientId equals i.PatientId
                        join sm in _staffMemberRepository.GetAll() on pa.AttendingPhysicianId equals sm.Id
                        join u in _userRepository.GetAll() on sm.UserId equals u.Id
                        join o in _organizationUnitRepository.GetAll() on pa.AttendingClinicId equals o.Id
                        join pe in _patientEncounterRepository.GetAll() on pa.Id equals (pe != null ? pe.AppointmentId : 0) into peJoin
                        from pe in peJoin.DefaultIfEmpty()
                        where o.FacilityId == facilityId && pa.StartTime.Date == today
                        select new
                        {
                            pa.StartTime,
                            pa.CreationTime,
                            pa.Status,
                            AppointmentId=pa.Id,
                            i.PaymentStatus,
                            u.Name,
                            u.Title,
                            u.Surname,
                            u.MiddleName,
                            p.FirstName,
                            p.PictureUrl,
                            PatientId =p.Id,
                            p.LastName,
                            EncounterId=pe != null ? pe.Id:0,
                            o.DisplayName
                        };
            query= request switch
            {
                { SearchText: { Length: > 0 } }
                    => query.Where(x => x.FirstName.ToLower().Contains(searchText)
                                        || x.PaymentStatus.ToString().ToLower().Equals(searchText)
                                        || x.Status.ToString().ToLower().Equals(searchText)
                                        || x.Name.ToLower().Contains(searchText)),
                { SortFilter: ViewAllPatientInClinicSortFilter.Patient } => query.OrderBy(x => x.FirstName),
                { SortFilter: ViewAllPatientInClinicSortFilter.AppointmentStatus } => query.OrderBy(x => x.Status.ToString()),
                { SortFilter: ViewAllPatientInClinicSortFilter.PaymentStatus } => query.OrderBy(x => x.PaymentStatus.ToString()),
                _ => query.OrderByDescending(x => x.CreationTime),
            };
            return await query.Select(x =>
                new AllPatientInClinicResponse
                {
                    PictureUrl = x.PictureUrl,
                    PatientId = x.PatientId,
                    FullName = $"{x.FirstName} {x.LastName}",
                    AppointmentStatus = x.Status,
                    PaymentStatus = x.PaymentStatus,
                    Clinic = x.DisplayName,
                    AssignedDoctor = (x.Title.HasValue ? x.Title + " " : string.Empty) + x.Name + " " + (!string.IsNullOrEmpty(x.MiddleName) ? x.MiddleName.Substring(0, 1) + ". " : string.Empty) + x.Surname,
                    AppointmentId = x.AppointmentId,
                    EncounterId = x.EncounterId
                }).ToListAsync();
        }


    }
}
