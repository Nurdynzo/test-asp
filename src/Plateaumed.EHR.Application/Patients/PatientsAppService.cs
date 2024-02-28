using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Patients.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Abp.Authorization.Users;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients.Query;

namespace Plateaumed.EHR.Patients
{
    /// <summary>
    /// PatientAppService is used to perform CRUD operations on Patient entities
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Patients)]
    public class PatientsAppService : EHRAppServiceBase, IPatientsAppService
    {
        private readonly ICreatePatientCommandHandler _createPatientCommandHandler;
        private readonly IGetOutpatientLandingListQueryHandler _getOutpatientLandingList;
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<PatientOccupation, long> _patientOccupationRepository;
        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
        private readonly IRepository<StaffMember, long> _staffMemberRepository;
        private readonly IRepository<SerialCode, long> _serialCodeRepository;
        private readonly IRepository<Facility, long> _facilityRepository;
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMappingRepository;
        private readonly IGetPatientDetailedHistoryQueryHandler _getPatientDetailedHistoryQueryHandler;
        private readonly IGetWardRoundAndClinicsNotesHandler _getWardRoundAndClinicsNotesHandler;
        private readonly IRepository<PatientRelation, long> _patientRelationRepository;
        private readonly IRepository<PatientEncounter, long> _patientEncounterRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IGetInpatientLandingListQueryHandler _getInpatientLandingList;
        private readonly IGetAccidentAndEmergencyLandingListQueryHandler _getAccidentAndEmergencyLandingList;
        private readonly IGetViewAllPatientInClinicQueryHandler _getViewAllPatientInClinicQueryHandler;
        private readonly IGetPatientsMedicationsQueryHandler _getPatientsMedicationsQueryHandler;
        private readonly IGetPatientStabilityStatusQueryHandler _getPatientStabilityStatusHandler;
        private readonly ICreatePatientStabilityStatusCommandHandler _createPatientStabilityStatusHandler;
        private readonly IRepository<PatientReferralDocument, long> _patientReferralDocumentsRepository;
        private readonly IGetPatientDetailsQueryHandler _getPatientDetails;
        private readonly IUpdateAppointmentStatusFromAwaitingVitalsCommandHandler _updateAppointmentStatusFromAwaiting;

        /// <summary>
        /// PatientsAppService constructor
        /// </summary>
        /// <param name="createPatientCommandHandler"></param>
        /// <param name="getOutpatientLandingList"></param>
        /// <param name="getViewAllPatientInClinicQueryHandler"></param>
        /// <param name="patientRepository"></param>
        /// <param name="patientRelationRepository"></param>
        /// <param name="patientOccupationRepository"></param>
        /// <param name="patientAppointmentRepository"></param>
        /// <param name="serialCodeRepository"></param>
        /// <param name="facilityRepository"></param>
        /// <param name="patientCodeMappingRepository"></param>
        /// <param name="staffMemberRepository"></param>
        /// <param name="getPatientDetailedHistoryQueryHandler"></param>
        /// <param name="getWardRoundAndClinicsNotesHandler"></param>
        /// <param name="patientEncounterRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="getInpatientLandingList"></param>
        /// <param name="getPatientsMedicationsQueryHandler"></param>
        /// <param name="getAccidentAndEmergencyLandingList"></param>
        /// <param name="createPatientStabilityStatusHandler"></param>
        /// <param name="getPatientStabilityStatusHandler"></param>
        /// <param name="patientReferralDocumentsRepository"></param>
        /// <param name="getPatientDetails"></param>
        /// <param name="updateAppointmentStatusFromAwaiting"></param>

        public PatientsAppService(
            ICreatePatientCommandHandler createPatientCommandHandler,
            IGetOutpatientLandingListQueryHandler getOutpatientLandingList,
            IGetViewAllPatientInClinicQueryHandler getViewAllPatientInClinicQueryHandler,
            IRepository<Patient, long> patientRepository,
            IRepository<PatientRelation, long> patientRelationRepository,
            IRepository<PatientOccupation, long> patientOccupationRepository,
            IRepository<PatientAppointment, long> patientAppointmentRepository,
            IRepository<SerialCode, long> serialCodeRepository,
            IRepository<Facility, long> facilityRepository,
            IRepository<PatientCodeMapping, long> patientCodeMappingRepository,
            IRepository<StaffMember, long> staffMemberRepository,
            IGetPatientDetailedHistoryQueryHandler getPatientDetailedHistoryQueryHandler,
            IGetWardRoundAndClinicsNotesHandler getWardRoundAndClinicsNotesHandler, 
            IRepository<PatientEncounter, long> patientEncounterRepository,
            IRepository<Role> roleRepository, IRepository<UserRole, long> userRoleRepository,
            IGetInpatientLandingListQueryHandler getInpatientLandingList,
            IGetPatientsMedicationsQueryHandler getPatientsMedicationsQueryHandler,
            IGetAccidentAndEmergencyLandingListQueryHandler getAccidentAndEmergencyLandingList,
            IGetPatientStabilityStatusQueryHandler getPatientStabilityStatusHandler,
            ICreatePatientStabilityStatusCommandHandler createPatientStabilityStatusHandler,
            IRepository<PatientReferralDocument, long> patientReferralDocumentsRepository,
            IGetPatientDetailsQueryHandler getPatientDetails,
            IUpdateAppointmentStatusFromAwaitingVitalsCommandHandler updateAppointmentStatusFromAwaiting)
        {
            _createPatientCommandHandler = createPatientCommandHandler;
            _getOutpatientLandingList = getOutpatientLandingList;
            _patientRepository = patientRepository;
            _patientRelationRepository = patientRelationRepository;
            _patientOccupationRepository = patientOccupationRepository;
            _patientAppointmentRepository = patientAppointmentRepository;
            _serialCodeRepository = serialCodeRepository;
            _facilityRepository = facilityRepository;
            _patientCodeMappingRepository = patientCodeMappingRepository;
            _staffMemberRepository = staffMemberRepository;
            _getPatientDetailedHistoryQueryHandler = getPatientDetailedHistoryQueryHandler;
            _getWardRoundAndClinicsNotesHandler = getWardRoundAndClinicsNotesHandler;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _getInpatientLandingList = getInpatientLandingList;
            _getAccidentAndEmergencyLandingList = getAccidentAndEmergencyLandingList;
            _getViewAllPatientInClinicQueryHandler = getViewAllPatientInClinicQueryHandler;
            _patientEncounterRepository = patientEncounterRepository;
            _getPatientsMedicationsQueryHandler = getPatientsMedicationsQueryHandler;
            _getPatientStabilityStatusHandler = getPatientStabilityStatusHandler;
            _createPatientStabilityStatusHandler = createPatientStabilityStatusHandler;
            _patientReferralDocumentsRepository = patientReferralDocumentsRepository;
            _getPatientDetails = getPatientDetails;
            _updateAppointmentStatusFromAwaiting = updateAppointmentStatusFromAwaiting;
        }

        /// <summary>
        /// Get all or filtered patients with pagination
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetPatientForViewDto>> GetAll(GetAllPatientsInput input)
        {
            var filteredPatients = _patientRepository
                .GetAll()
                .Include(x => x.PatientCodeMappings)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e =>
                        false
                        || e.PatientCodeMappings.Any(x => x.PatientCode.Contains(input.Filter) && x.PatientId == e.Id)
                        || e.Ethnicity.Contains(input.Filter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.PatientCodeFilter),
                    e => e.PatientCodeMappings.Any(x => x.PatientCode.Contains(input.PatientCodeFilter) && x.PatientId == e.Id)
                );

            var pagedAndFilteredPatients = filteredPatients
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var patients =
                from o in pagedAndFilteredPatients
                select new
                {
                    o.PatientCodeMappings.FirstOrDefault().PatientCode,
                    o.Ethnicity,
                    o.Religion,
                    o.MaritalStatus,
                    o.BloodGroup,
                    o.BloodGenotype,
                    Id = o.Id,
                };

            var totalCount = await filteredPatients.CountAsync();

            var dbList = await patients.ToListAsync();
            var results = new List<GetPatientForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPatientForViewDto()
                {
                    Patient = new PatientDto
                    {
                        PatientCode = o.PatientCode,
                        Ethnicity = o.Ethnicity,
                        Religion = o.Religion,
                        MaritalStatus = o.MaritalStatus,
                        BloodGroup = o.BloodGroup,
                        BloodGenotype = o.BloodGenotype,
                        Id = o.Id,
                    },
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPatientForViewDto>(totalCount, results);
        }

        /// <summary>
        /// Get patient for edit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Patients_Edit)]
        public async Task<CreateOrEditPatientDto> GetPatientForEdit(EntityDto<long> input)
        {
            var output = await (from p in _patientRepository.GetAll()
                                join m in _patientCodeMappingRepository.GetAll() on p.Id equals m.PatientId
                                join pr in _patientReferralDocumentsRepository.GetAll() on p.Id equals pr.PatientId
                                into referralDocuments
                                from rd in referralDocuments.DefaultIfEmpty()
                                select new CreateOrEditPatientDto
                                {
                                    Id = p.Id,
                                    PatientCode = m.PatientCode,
                                    Ethnicity = p.Ethnicity,
                                    Religion = p.Religion,
                                    MaritalStatus = p.MaritalStatus,
                                    BloodGroup = p.BloodGroup,
                                    BloodGenotype = p.BloodGenotype,
                                    DateOfBirth = p.DateOfBirth,
                                    PhoneNumber = p.PhoneNumber,
                                    EmailAddress = p.EmailAddress,
                                    GenderType = p.GenderType,
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    MiddleName = p.MiddleName,
                                    Address = p.Address,
                                    Title = p.Title,
                                    CountryId = p.CountryId,
                                    StateOfOriginId = p.StateOfOriginId,
                                    DistrictId = p.DistrictId,
                                    IdentificationCode = p.IdentificationCode,
                                    IdentificationType = p.IdentificationType,
                                    NuclearFamilySize = p.NuclearFamilySize,
                                    NumberOfChildren = p.NumberOfChildren,
                                    NumberOfSiblings = p.NumberOfSiblings,
                                    NumberOfSpouses = p.NumberOfSpouses,
                                    NoOfFemaleChildren = p.NoOfFemaleChildren,
                                    NoOfMaleChildren = p.NoOfMaleChildren,
                                    NoOfFemaleSiblings = p.NoOfFemaleSiblings,
                                    NoOfMaleSiblings = p.NoOfMaleSiblings,
                                    PositionInFamily = p.PositionInFamily,
                                    IsNewToHospital = p.IsNewToHospital,
                                    ProfilePictureId = p.ProfilePictureId,
                                    ProfilePictureUrl = p.PictureUrl,
                                    ReferralDocument = rd != null ? rd.ReferralDocument : null,
                                    PatientOccupations = _patientOccupationRepository.GetAll()
                                    .Where(o => o.PatientId == p.Id)
                                    .Select(x => ObjectMapper.Map<PatientOccupationDto>(x))
                                    .ToList(),
                                    Relations = _patientRelationRepository.GetAll()
                                        .Where(r => r.PatientId == p.Id)
                                        .Include(d => d.Diagnoses)
                                        .Select(x => ObjectMapper.Map<CreateOrEditPatientRelationDto>(x)).ToList(),
                                }).FirstOrDefaultAsync(x => x.Id == input.Id);


            return output;
        }
       

        /// <summary>
        /// Check patient exist by PhoneNumber and GenderType
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<CheckPatientExistOutput>> CheckPatientExist(CheckPatientExistInput input)
        {
            var patients = await _patientRepository
                .GetAll()
                .Include(x => x.PatientCodeMappings)
                .Where(p => p.GenderType == input.GenderType && p.PhoneNumber == input.PhoneNumber)
                .Select(p => new CheckPatientExistOutput
                {
                    Id = p.Id,
                    PatientCode = p.PatientCodeMappings.FirstOrDefault(z => z.PatientId == p.Id).PatientCode,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    EmailAddress = p.EmailAddress,
                    DateOfBirth = p.DateOfBirth,
                    PhoneNumber = p.PhoneNumber,
                }).ToListAsync();

            return patients;
        }

        /// <summary>
        /// <![CDATA[Create or edit patient. If Id is null or <= 0 then create else edit]]>
        /// </summary>
        /// <param name="input"></param>
        public async Task<CreateOrEditPatientDto> CreateOrEdit(CreateOrEditPatientDto input)
        {
            if (input.Id is null or <= 0)
            {
                return await Create(input);
            }
            return await Update(input);

        }

        /// <summary>
        /// Create new patient if the code does not exist
        /// </summary>
        /// <param name="input"></param>
        /// <returns>{CreateOrEditPatientDto}</returns>
        /// <exception cref="UserFriendlyException"></exception>
        [AbpAuthorize(AppPermissions.Pages_Patients_Create)]
        protected virtual async Task<CreateOrEditPatientDto> Create(CreateOrEditPatientDto input)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _createPatientCommandHandler.Handle(input, facilityId);
        }
        
        /// <summary>
        /// Update patient if id is not null and patient code is not modified
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [AbpAuthorize(AppPermissions.Pages_Patients_Edit)]
        protected virtual async Task<CreateOrEditPatientDto> Update(CreateOrEditPatientDto input)
        {
            var patient = await _patientRepository.FirstOrDefaultAsync(input.Id.GetValueOrDefault());

            ObjectMapper.Map(input, patient);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map(patient, input);
        }

        [AbpAuthorize(AppPermissions.Pages_Patients_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _patientRepository.DeleteAsync(input.Id);
        }
        
        [AbpAuthorize(AppPermissions.Pages_Patients)]
        public async Task<PagedResultDto<PatientPatientOccupationLookupTableDto>> GetAllPatientOccupationForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _patientOccupationRepository
                .GetAll().Include(o=> o.Occupation)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Occupation.Name != null && e.Occupation.Name.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var patientOccupationList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<PatientPatientOccupationLookupTableDto>();
            foreach (var patientOccupation in patientOccupationList)
            {
                lookupTableDtoList.Add(
                    new PatientPatientOccupationLookupTableDto
                    {
                        Id = patientOccupation.Id,
                        DisplayName = patientOccupation.Occupation.Name?.ToString()
                    }
                );
            }

            return new PagedResultDto<PatientPatientOccupationLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetPatientLandingListOuptDto>> GetOutPatientLandingList(
            GetAllForLookupTableInput input) =>
            await _getOutpatientLandingList.Handle(input);

        public async Task<PagedResultDto<GetInpatientLandingListResponse>> GetInpatientLandingList(
            GetInpatientLandingListRequest request) =>
            await _getInpatientLandingList.Handle(request); 
        
        public async Task<PagedResultDto<GetAccidentAndEmergencyLandingListResponse>> GetAccidentAndEmergencyLandingList(
            GetAccidentAndEmergencyLandingListRequest request) =>
            await _getAccidentAndEmergencyLandingList.Handle(request);

        public async Task<string> UpdateAppointmentStatusFromAwaitingVitals(long encounterId)
        {
            return await _updateAppointmentStatusFromAwaiting.Handle(encounterId);
        }
        
        public async Task<GetPatientDetailsOutDto> GetPatientDetails(GetPatientDetailsInput input)
        {
            return await _getPatientDetails.Handle(input);
        }
        
        /// <inheritdoc />
        [AbpAuthorize(AppPermissions.Pages_Patients_Create)]
        public async Task<string> GetNewPatientCode()
        {
            var user = await GetCurrentUserAsync();

            var facilityId = GetCurrentUserFacilityId();
            if (facilityId <= 0)
            {
                throw new UserFriendlyException("The current user is not assigned to any default facility");
            }


            var facilityTemplate = await _facilityRepository.Query(async x => await
                x.Include(p => p.PatientCodeTemplate)
                    .FirstOrDefaultAsync(f => f.Id == facilityId));

            var lastPatientCode = await _serialCodeRepository.FirstOrDefaultAsync(x => x.TenantId == user.TenantId && x.FacilityId == facilityId);
            if (facilityTemplate?.PatientCodeTemplate == null)
            {
                throw new UserFriendlyException("Facility or Facility template code is  not setup");
            }
            if (lastPatientCode != null)
            {
                lastPatientCode.LastGeneratedNo += 1;

            }
            else
            {
                lastPatientCode = new SerialCode
                {
                    TenantId = user.TenantId.GetValueOrDefault(),
                    LastGeneratedNo = 1,
                    FacilityId = facilityId

                };
            }
            // check if the last generated code is not already used
            var lastGeneratedPatientCode = GetPatientCode(facilityTemplate, new SerialCode
            {
                LastGeneratedNo = lastPatientCode.LastGeneratedNo - 1,
                FacilityId = facilityId,
                TenantId = user.TenantId.GetValueOrDefault()
            });
            if (lastPatientCode.LastGeneratedNo > 1 &&
                !await _patientCodeMappingRepository.GetAll()
                    .AnyAsync(x => x.PatientCode.Equals(lastGeneratedPatientCode) && x.FacilityId == facilityId))
            {
                lastPatientCode.LastGeneratedNo -= 1;
                return lastGeneratedPatientCode;
            }

            var newPatientCode = GetPatientCode(facilityTemplate, lastPatientCode);
            await _serialCodeRepository.InsertOrUpdateAsync(lastPatientCode);
            await CurrentUnitOfWork.SaveChangesAsync();
            return newPatientCode;

        }
      
        /// <inheritdoc />
        [HttpGet]
        public async Task<List<SearchPatientOutput>> SearchPatient(string searchText)
        {
            var facilityId = GetCurrentUserFacilityId();
            var searchTerms = searchText.ToLower();
            var query = (from p in _patientRepository.GetAll()
                         join pc in _patientCodeMappingRepository.GetAll() on p.Id equals pc.PatientId
                         // this search can still be extended to include other necessary entities
                         select new { p, pc }
                ).Where(x => x.pc.FacilityId == facilityId)
                .WhereIf(!string.IsNullOrWhiteSpace(searchTerms), p =>
                    p.pc.PatientCode.ToLower().Contains(searchTerms) ||
                    p.p.FirstName.ToLower().Contains(searchTerms) ||
                    p.p.LastName.ToLower().Contains(searchTerms) ||
                    p.p.PhoneNumber.ToLower().Contains(searchTerms) ||
                    p.p.EmailAddress.ToLower().Contains(searchTerms) ||
                    p.p.Ethnicity.ToLower().Contains(searchTerms) ||
                    p.p.Address.ToLower().Contains(searchTerms) ||
                    p.p.MiddleName.ToLower().Contains(searchTerms)

                ).Select(x => new SearchPatientOutput
                {
                    DateOfBirth = x.p.DateOfBirth.ToString(CultureInfo.InvariantCulture),
                    EmailAddress = x.p.EmailAddress,
                    PhoneNumber = x.p.PhoneNumber,
                    GenderType = x.p.GenderType,
                    PatientCode = x.pc.PatientCode,
                    Fullname = x.p.FirstName + " " + x.p.LastName,
                    Id = x.p.Id,
                    Uuid = x.p.UuId,
                    PictureUrl = x.p.PictureUrl

                }).Take(20);
            return await query.ToListAsync();
        }

        /// <summary>
        /// Get Patient Family History fpr a patient Overview
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<PatientDetailsQueryResponse> GetPatientHistory(long patientId)
        {
            var facilityId =GetCurrentUserFacilityId();
            return await _getPatientDetailedHistoryQueryHandler.Handle(patientId, facilityId);
        }

        /// <summary>
        /// Get Patient Ward Round and Clinic Notes
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetPatientWardRoundAndClinicNotesResponse>> GetPatientWardRoundAndClinicNotes(GetPatientWardRoundAndClinicNotesQueryRequest request)
            => await _getWardRoundAndClinicsNotesHandler.Handle(request);
     

        public  List<string> GetIntensityUnits()
            => Enum.GetValues<Intensity>()
                .AsEnumerable()
                .Select(x => x.ToString())
                .ToList();

        public async Task<List<AllPatientInClinicResponse>> GetAllPatientInClinicForToday(AllPatientInClinicRequest request)
            => await _getViewAllPatientInClinicQueryHandler.Handle(request, GetCurrentUserFacilityId());

        public async Task<GetPatientsMedicationsResponse> GetPatientMedications(long patientId)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getPatientsMedicationsQueryHandler.Handle(patientId, facilityId);
        }

        public async Task CreatePatientStabilityStatus(PatientStabilityRequestDto request)
            => await _createPatientStabilityStatusHandler.Handle(request);

        public async Task<List<PatientStabilityResponseDto>> GetPatientStabilityStatus(long patientId, long encounterId)
            => await _getPatientStabilityStatusHandler.Handle(patientId, encounterId);

        public List<string> GetStabilityStatus()
            => Enum.GetValues<PatientStabilityStatus>()
            .AsEnumerable()
            .Select(x => x.ToString())
            .ToList();

        #region Private Methods

        /// <summary>
        /// The generate patient code based on facility setup
        /// </summary>
        /// <param name="facility"></param>
        /// <param name="lastPatientCode"></param>
        /// <returns></returns>
        private static string GetPatientCode(Facility facility, SerialCode lastPatientCode)
        {
            var formatLength = facility.PatientCodeTemplate.Length;
            var formatString = lastPatientCode.LastGeneratedNo.ToString();
            if (formatString.Length < formatLength)
            {
                formatString = formatString.PadLeft(formatLength, '0');
            }
            string patientCode = "";
            switch (facility.PatientCodeTemplate)
            {
                case { Prefix: var prefix, Suffix: var suffix } when !string.IsNullOrWhiteSpace(prefix) && !string.IsNullOrWhiteSpace(suffix):
                    patientCode = $"{prefix}-{formatString}-{suffix}";
                    break;

                case { Prefix: var prefix } when !string.IsNullOrWhiteSpace(prefix):
                    patientCode = $"{prefix}-{formatString}";
                    break;

                case { Suffix: var suffix } when !string.IsNullOrWhiteSpace(suffix):
                    patientCode = $"{formatString}-{suffix}";
                    break;

                default:
                    patientCode = formatString;
                    break;
            }
            return patientCode;
        }

        #endregion

    }
}
