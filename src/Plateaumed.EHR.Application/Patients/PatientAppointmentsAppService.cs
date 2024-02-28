using Plateaumed.EHR.Staff;
using Abp.Organizations;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Patients.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.PatientAppointments.Dtos;

namespace Plateaumed.EHR.Patients
{
    [AbpAuthorize(AppPermissions.Pages_PatientAppointments)]
    public class PatientAppointmentsAppService : EHRAppServiceBase, IPatientAppointmentsAppService
    {
        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
        private readonly IRepository<Patient, long> _lookup_patientRepository;
        private readonly IRepository<PatientReferralDocument, long> _lookup_patientReferralDocumentRepository;
        private readonly IRepository<StaffMember, long> _lookup_staffMemberRepository;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;
        private readonly IGetAppointmentByPatientIdQueryHandler _getAppointmentByPatientIdQueryHandler;
        private readonly IRepository<PatientEncounter, long> _encounterRepository;

        public PatientAppointmentsAppService(IRepository<PatientAppointment, long> patientAppointmentRepository, IRepository<Patient, long> lookupPatientRepository, IRepository<PatientReferralDocument, long> lookupPatientReferralDocumentRepository, IRepository<StaffMember, long> lookupStaffMemberRepository, IRepository<OrganizationUnit, long> lookupOrganizationUnitRepository, IGetAppointmentByPatientIdQueryHandler getAppointmentByPatientIdQueryHandler, IRepository<PatientEncounter, long> encounterRepository)
        {
            _patientAppointmentRepository = patientAppointmentRepository;
            _lookup_patientRepository = lookupPatientRepository;
            _lookup_patientReferralDocumentRepository = lookupPatientReferralDocumentRepository;
            _lookup_staffMemberRepository = lookupStaffMemberRepository;
            _lookup_organizationUnitRepository = lookupOrganizationUnitRepository;
            _getAppointmentByPatientIdQueryHandler = getAppointmentByPatientIdQueryHandler;
            _encounterRepository = encounterRepository;
        }

        public async Task<PagedResultDto<GetPatientAppointmentForViewDto>> GetAll(GetAllPatientAppointmentsInput input)
        {
            var statusFilter = input.StatusFilter.HasValue
                        ? (AppointmentStatusType)input.StatusFilter
                        : default;
            var typeFilter = input.TypeFilter.HasValue
                ? (AppointmentType)input.TypeFilter
                : default;

            var filteredPatientAppointments = _patientAppointmentRepository.GetAll()
                        .Include(e => e.PatientFk)
                        .ThenInclude(x => x.PatientCodeMappings)
                        .Include(e => e.PatientReferralDocumentFk)
                        .Include(e => e.AttendingPhysicianFk)
                        .Include(e => e.ReferringClinicFk)
                        .Include(e => e.AttendingClinicFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter) || e.Notes.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(input.MinDurationFilter != null, e => e.Duration >= input.MinDurationFilter)
                        .WhereIf(input.MaxDurationFilter != null, e => e.Duration <= input.MaxDurationFilter)
                        .WhereIf(input.MinStartTimeFilter != null, e => e.StartTime >= input.MinStartTimeFilter)
                        .WhereIf(input.MaxStartTimeFilter != null, e => e.StartTime <= input.MaxStartTimeFilter)
                        .WhereIf(input.IsRepeatFilter.HasValue && input.IsRepeatFilter > -1, e => (input.IsRepeatFilter == 1 && e.IsRepeat) || (input.IsRepeatFilter == 0 && !e.IsRepeat))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NotesFilter), e => e.Notes.Contains(input.NotesFilter))
                        .WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(input.TypeFilter.HasValue && input.TypeFilter > -1, e => e.Type == typeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PatientPatientCodeFilter),
                            e => e.PatientFk != null && e.PatientFk.PatientCodeMappings != null
                                                     && e.PatientFk.PatientCodeMappings.Any(x => x.PatientCode.Equals(input.PatientPatientCodeFilter)))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PatientReferralReferringHospitalFilter), e => e.PatientReferralDocumentFk != null && e.PatientReferralDocumentFk.ReferringHealthCareProvider == input.PatientReferralReferringHospitalFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.StaffMemberStaffCodeFilter), e => e.AttendingPhysicianFk != null && e.AttendingPhysicianFk.StaffCode == input.StaffMemberStaffCodeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.ReferringClinicFk != null && e.ReferringClinicFk.DisplayName == input.OrganizationUnitDisplayNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayName2Filter), e => e.AttendingClinicFk != null && e.AttendingClinicFk.DisplayName == input.OrganizationUnitDisplayName2Filter);

            var pagedAndFilteredPatientAppointments = filteredPatientAppointments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var patientAppointments = from o in pagedAndFilteredPatientAppointments
                                      join o1 in _lookup_patientRepository.GetAll() on o.PatientId equals o1.Id into j1
                                      from s1 in j1.DefaultIfEmpty()

                                      join o2 in _lookup_patientReferralDocumentRepository.GetAll() on o.PatientReferralDocumentId equals o2.Id into j2
                                      from s2 in j2.DefaultIfEmpty()

                                      join o3 in _lookup_staffMemberRepository.GetAll() on o.AttendingPhysicianId equals o3.Id into j3
                                      from s3 in j3.DefaultIfEmpty()

                                      join o4 in _lookup_organizationUnitRepository.GetAll() on o.ReferringClinicId equals o4.Id into j4
                                      from s4 in j4.DefaultIfEmpty()

                                      join o5 in _lookup_organizationUnitRepository.GetAll() on o.AttendingClinicId equals o5.Id into j5
                                      from s5 in j5.DefaultIfEmpty()

                                      select new
                                      {

                                          o.Title,
                                          o.Duration,
                                          o.StartTime,
                                          o.IsRepeat,
                                          o.Notes,
                                          o.RepeatType,
                                          o.Status,
                                          o.Type,
                                          Id = o.Id,
                                          PatientPatientCode = s1 == null || s1.PatientCodeMappings == null || s1.PatientCodeMappings.Count == 0 ? "" :
                                              s1.PatientCodeMappings.FirstOrDefault().PatientCode,
                                          PatientReferralReferringHospital = s2 == null || s2.ReferringHealthCareProvider == null ? "" : s2.ReferringHealthCareProvider.ToString(),
                                          StaffMemberStaffCode = s3 == null || s3.StaffCode == null ? "" : s3.StaffCode.ToString(),
                                          OrganizationUnitDisplayName = s4 == null || s4.DisplayName == null ? "" : s4.DisplayName.ToString(),
                                          OrganizationUnitDisplayName2 = s5 == null || s5.DisplayName == null ? "" : s5.DisplayName.ToString()
                                      };

            var totalCount = await filteredPatientAppointments.CountAsync();

            var dbList = await patientAppointments.ToListAsync();
            var results = new List<GetPatientAppointmentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPatientAppointmentForViewDto()
                {
                    PatientAppointment = new PatientAppointmentDto
                    {

                        Title = o.Title,
                        Duration = o.Duration,
                        StartTime = o.StartTime,
                        IsRepeat = o.IsRepeat,
                        Notes = o.Notes,
                        RepeatType = o.RepeatType,
                        Status = o.Status,
                        Type = o.Type,
                        Id = o.Id,
                    },
                    PatientPatientCode = o.PatientPatientCode,
                    PatientReferralReferringHospital = o.PatientReferralReferringHospital,
                    StaffMemberStaffCode = o.StaffMemberStaffCode,
                    OrganizationUnitDisplayName = o.OrganizationUnitDisplayName,
                    OrganizationUnitDisplayName2 = o.OrganizationUnitDisplayName2
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPatientAppointmentForViewDto>(
                totalCount,
                results
            );

        }

        
        [AbpAuthorize(AppPermissions.Pages_PatientAppointments_Edit)]
        public async Task<GetPatientAppointmentForEditOutput> GetPatientAppointmentForEdit(EntityDto<long> input)
        {
            var patientAppointment = await _patientAppointmentRepository.GetAll()
                .Include(x => x.PatientFk)
                .ThenInclude(x => x.PatientCodeMappings)
                .Include(x => x.ReferringClinicFk)
                .Include(x => x.AttendingPhysicianFk)
                .Select(x => new GetPatientAppointmentForEditOutput
                {
                    PatientAppointment = new CreateOrEditPatientAppointmentDto
                    {
                        Duration = x.Duration,
                        StartTime = x.StartTime,
                        IsRepeat = x.IsRepeat,
                        Notes = x.Notes,
                        RepeatType = x.RepeatType,
                        Status = x.Status,
                        Type = x.Type,
                        Id = x.Id,
                        PatientId = x.PatientId,
                        PatientReferralId = x.PatientReferralDocumentId,
                        AttendingPhysicianId = x.AttendingPhysicianId,
                        ReferringClinicId = x.ReferringClinicId,
                        AttendingClinicId = x.AttendingClinicId,
                        TransferredClinic = x.TransferredClinic,
                        Title = x.Title
                    },
                    PatientPatientCode = x.PatientFk.PatientCodeMappings.FirstOrDefault().PatientCode,
                    PatientReferralReferringHospital = x.ReferringClinicFk.DisplayName,
                    StaffMemberStaffCode = x.AttendingPhysicianFk.StaffCode,
                    OrganizationUnitDisplayName = x.ReferringClinicFk.DisplayName,

                })
                .FirstOrDefaultAsync(x => x.PatientAppointment.Id == input.Id);

            return patientAppointment;
        }

        public Task<GetPatientConsultingRooms> GetConsultingRooms()
        {
            return Task.FromResult(new GetPatientConsultingRooms(new List<string>()
            {
                "Room 1",
                "Room 2",
                "Room 4",
                "Room 5"
            }));
        }
        
        [AbpAuthorize(AppPermissions.Pages_PatientAppointments)]
        public async Task<PagedResultDto<PatientAppointmentPatientLookupTableDto>> GetAllPatientForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_patientRepository.GetAll()
                .Include(x => x.PatientCodeMappings).WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.PatientCodeMappings.FirstOrDefault().PatientCode != null
                       && e.PatientCodeMappings.Any(x => x.PatientCode.Equals(input.Filter))
               );

            var totalCount = await query.CountAsync();

            var patientList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<PatientAppointmentPatientLookupTableDto>();
            foreach (var patient in patientList)
            {
                lookupTableDtoList.Add(new PatientAppointmentPatientLookupTableDto
                {
                    Id = patient.Id,
                    DisplayName = patient.PatientCodeMappings.FirstOrDefault()?.PatientCode
                });
            }

            return new PagedResultDto<PatientAppointmentPatientLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_PatientAppointments)]
        public async Task<PagedResultDto<PatientAppointmentPatientReferralLookupTableDto>> GetAllPatientReferralForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_patientReferralDocumentRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.ReferringHealthCareProvider != null && e.ReferringHealthCareProvider.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var patientReferralList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<PatientAppointmentPatientReferralLookupTableDto>();
            foreach (var patientReferral in patientReferralList)
            {
                lookupTableDtoList.Add(new PatientAppointmentPatientReferralLookupTableDto
                {
                    Id = patientReferral.Id,
                    DisplayName = patientReferral.ReferringHealthCareProvider?.ToString()
                });
            }

            return new PagedResultDto<PatientAppointmentPatientReferralLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_PatientAppointments)]
        public async Task<PagedResultDto<PatientAppointmentStaffMemberLookupTableDto>> GetAllStaffMemberForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_staffMemberRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.StaffCode != null && e.StaffCode.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var staffMemberList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<PatientAppointmentStaffMemberLookupTableDto>();
            foreach (var staffMember in staffMemberList)
            {
                lookupTableDtoList.Add(new PatientAppointmentStaffMemberLookupTableDto
                {
                    Id = staffMember.Id,
                    DisplayName = staffMember.StaffCode?.ToString()
                });
            }

            return new PagedResultDto<PatientAppointmentStaffMemberLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_PatientAppointments)]
        public async Task<PagedResultDto<PatientAppointmentOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_organizationUnitRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.DisplayName != null && e.DisplayName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var organizationUnitList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<PatientAppointmentOrganizationUnitLookupTableDto>();
            foreach (var organizationUnit in organizationUnitList)
            {
                lookupTableDtoList.Add(new PatientAppointmentOrganizationUnitLookupTableDto
                {
                    Id = organizationUnit.Id,
                    DisplayName = organizationUnit.DisplayName?.ToString()
                });
            }

            return new PagedResultDto<PatientAppointmentOrganizationUnitLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_PatientAppointments)]
        public async Task UpdateAppointmentStatus(EditAppointmentStatusDto input)
        {
            var tenandId = AbpSession.TenantId;
            var patientAppointment = await _patientAppointmentRepository.FirstOrDefaultAsync(x => x.TenantId == tenandId & x.Id == input.Id);
            ObjectMapper.Map(input, patientAppointment);
        }

        /// <summary>
        /// An endpoint to get all appointments by patientId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AppointmentListResponse>> GetAppointmentsByPatientId(GetAppointmentsByPatientIdRequest request)
        {
            return await _getAppointmentByPatientIdQueryHandler.Handle(request);
        }


        [AbpAuthorize(AppPermissions.Pages_PatientAppointments)]
        public async Task<string> ReassignePatientAppointment(ReassignPatientAppointmentDto input)
        {
            var encounter = await _encounterRepository.GetAll()
                .Include(x => x.Appointment)
                .SingleOrDefaultAsync(v => v.Id == input.EncounterId);

            var patientAppointment = encounter.Appointment;
            if (patientAppointment == null)
                throw new UserFriendlyException("Patient appointment not found.");

            var staffMember = await _lookup_staffMemberRepository.GetAll().SingleOrDefaultAsync(v => v.Id == (long)input.NewAttendingPhysicianId);
            if (staffMember == null)
                throw new UserFriendlyException("Staff member not found.");

            // update appointment data
            patientAppointment.AttendingPhysicianId = input.NewAttendingPhysicianId;

            await _patientAppointmentRepository.UpdateAsync(patientAppointment);
            await CurrentUnitOfWork.SaveChangesAsync();

            return "Patient appointment has been reassigned.";
        }
    }
}