using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.VitalSigns;

namespace Plateaumed.EHR.Patients.Query
{
    public class GetOutpatientLandingListQueryHandler : IGetOutpatientLandingListQueryHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
        private readonly IRepository<PatientEncounter, long> _patientEncounterRepository;
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMappingRepository;
        private readonly IRepository<StaffMember, long> _staffMemberRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitExtendedRepository;
        private readonly IAbpSession _abpSession;
        private readonly IGetCurrentUserFacilityIdQueryHandler _getCurrentUserFacility;
        private readonly IRepository<PatientVital, long> _patientVitals;
        private readonly IRepository<Diagnosis, long> _diagnosis;
        private readonly IRepository<Invoice, long> _invoices;

        public GetOutpatientLandingListQueryHandler(IRepository<Patient, long> patientRepository,
            IRepository<PatientAppointment, long> patientAppointmentRepository,
            IRepository<PatientEncounter, long> patientEncounterRepository,
            IRepository<PatientCodeMapping, long> patientCodeMappingRepository,
            IRepository<StaffMember, long> staffMemberRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IRepository<OrganizationUnitExtended, long> organizationUnitExtendedRepository,
            IAbpSession abpSession,
            IGetCurrentUserFacilityIdQueryHandler getCurrentUserFacility,
            IRepository<PatientVital, long> patientVitals,
            IRepository<Diagnosis, long> diagnosis,
            IRepository<Invoice, long> invoices)
        {
            _patientRepository = patientRepository;
            _patientAppointmentRepository = patientAppointmentRepository;
            _patientEncounterRepository = patientEncounterRepository;
            _patientCodeMappingRepository = patientCodeMappingRepository;
            _staffMemberRepository = staffMemberRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _organizationUnitExtendedRepository = organizationUnitExtendedRepository;
            _abpSession = abpSession;
            _getCurrentUserFacility = getCurrentUserFacility;
            _patientVitals = patientVitals;
            _diagnosis = diagnosis;
            _invoices = invoices;
        }

        public async Task<PagedResultDto<GetPatientLandingListOuptDto>> Handle(GetAllForLookupTableInput input)
        {
            var facilityId = await _getCurrentUserFacility.Handle();

            var today = DateTime.Now.Date;
            var filterTerms = !string.IsNullOrWhiteSpace(input.Filter) ? input.Filter.ToLower().Trim() : string.Empty;

            var query = from pa in _patientAppointmentRepository.GetAll().Include(u => u.AttendingClinicFk)
                        join pe in _patientEncounterRepository.GetAll() on pa.Id equals pe.AppointmentId
                        join p in _patientRepository.GetAll() on pa.PatientId equals p.Id                        
                        join pcm in _patientCodeMappingRepository.GetAll() on p.Id equals pcm.PatientId into pcmJoin
                        from pcm in pcmJoin.DefaultIfEmpty()
                        join ap in _staffMemberRepository.GetAll().Include(u => u.UserFk) on pa.AttendingPhysicianId equals
                            ap.Id into apJoin
                        from ap in apJoin.DefaultIfEmpty()
                        join uo in _userOrganizationUnitRepository.GetAll() on pa.AttendingClinicId equals uo.OrganizationUnitId
                            into uoJoin
                        from uo in uoJoin.DefaultIfEmpty()
                        join ou in _organizationUnitExtendedRepository.GetAll() on pa.AttendingClinicId equals ou.Id into
                            ouJoin
                        from ou in ouJoin.DefaultIfEmpty()
                        join pv in _patientVitals.GetAll().Include(x => x.MeasurementRange).Include(x => x.VitalSign) on pa.PatientId equals pv.PatientId
                            into vitals
                        select new
                        {
                            pa, pe, p, pcm, ap, uo, ou, vitals,
                            i = (from i in _invoices.GetAll() where pa.PatientId ==  i.PatientId select i).FirstOrDefault(),
                            Diagnosis = (from d in _diagnosis.GetAll() where pa.PatientId == d.PatientId select d).ToList()
                        };
        
            query = query.WhereIf(facilityId.HasValue, s => s.pe.FacilityId == facilityId);
            
            if (!string.IsNullOrWhiteSpace(filterTerms))
            {
                query = query.Where(s =>
                    s.pcm.PatientCode.ToLower().Equals(filterTerms) ||
                    s.p.EmailAddress.ToLower().Contains(filterTerms) ||
                    s.pa.Status.ToString().ToLower().Contains(filterTerms.Replace(" ", "_")) ||
                    s.p.FirstName.ToLower().Contains(filterTerms) ||
                    s.p.LastName.ToLower().Contains(filterTerms) ||
                    s.p.PhoneNumber.ToLower().Contains(filterTerms));
            }

            query = input.OutPatientListingType switch
            {
                OutPatientListingType.AttendingPhysician =>
                    query.Where(s => s.pa.StartTime.Date == today)
                        .Where(s => s.ap.UserId == _abpSession.UserId &&
                                    s.pa.Status == AppointmentStatusType.Awaiting_Doctor),

                OutPatientListingType.AttendingClinic when input.Status == null =>
                    query.Where(s => s.pa.StartTime.Date == today)
                        .Where(s => s.pa.Status == AppointmentStatusType.Awaiting_Vitals ||
                                    s.pa.Status == AppointmentStatusType.Awaiting_Doctor),

                OutPatientListingType.AttendingClinic when input.Status != null =>
                    query.Where(s => s.pa.StartTime.Date == today)
                        .Where(s => s.pa.Status == input.Status),

                _ => query.Take(10)
            };

            var results = await query.Select(x => new GetPatientLandingListOuptDto
            {
                Id = x.pa.Id,
                EncounterId = x.pe.Id,
                PatientCode = x.pcm.PatientCode,
                Name = x.p.FirstName + " " + x.p.LastName,
                StartTime = x.pa.StartTime,
                PatientId = x.p.Id,
                Status = x.pa.Status,
                Gender = x.p.GenderType,
                AppointmentType = x.pa.Type,
                Clinic = x.ou.DisplayName,
                DateOfBirth = x.p.DateOfBirth,
                AttendingPhysicianStaffCode = x.ap == null ? string.Empty : x.ap.StaffCode,
                AttendingPhysician = x.ap == null ? string.Empty : x.ap.UserFk.FullName,
                PictureUrl = x.p.PictureUrl,
                PatientVitals = x.vitals.Select(x => new PatientVitalsDto
                {
                    Measurement = x.MeasurementRange.Unit,
                    Reading = x.VitalReading.ToString(),
                    VitalSign = x.VitalSign.Sign
                }).ToList(),
                Diagnosis = x.Diagnosis.Count > 0 ? x.Diagnosis.Select(x => x.Description).ToList() : new List<string>(),
                PaymentStatus = x.i == null ? string.Empty : x.i.PaymentStatus.ToString()
            }).OrderBy(input.Sorting ?? "id asc")
                .PageBy(input)
                .ToListAsync();

            var totalCount = await query.CountAsync();

            return new PagedResultDto<GetPatientLandingListOuptDto>(totalCount, results);
        }
    }
}