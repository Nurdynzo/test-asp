using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Organizations.Abstractions;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Symptom.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Query.BaseQueryHelper;

public class DoctorReviewAndSaveBaseQuery : IDoctorReviewAndSaveBaseQuery
{
    private readonly IGetAllStaffMembersQueryHandler _allStaffQueryHandler;
    private readonly IRepository<StaffEncounter, long> _staffEncounterRepository;
    private readonly IRepository<PatientEncounter, long> _patientEncounterRepository;
    private readonly IGetStaffMemberWithUnitAndLevelQueryHandler _staffQueryHandler;
    private readonly InputNotes.Abstractions.IBaseQuery _InputNoteBaseQuery;
    private readonly IGetPatientPastMedicalHistoryQueryHandler _pastMedicationCondition;
    private readonly IGetOrganizationUnitsQueryHandler _organizationUnitClinicQueryHandler;

    public DoctorReviewAndSaveBaseQuery(IGetAllStaffMembersQueryHandler allStaffQueryHandler,
        IRepository<StaffEncounter, long> staffEncounterRepository,
        IGetStaffMemberWithUnitAndLevelQueryHandler staffQueryHandler,
        InputNotes.Abstractions.IBaseQuery InputNoteBaseQuery,
        IGetPatientPastMedicalHistoryQueryHandler pastMedicationCondition,
        IGetOrganizationUnitsQueryHandler organizationUnitClinicQueryHandler,
        IRepository<PatientEncounter, long> patientEncounterRepository)
    {
        _allStaffQueryHandler = allStaffQueryHandler;
        _staffQueryHandler = staffQueryHandler;
        _InputNoteBaseQuery = InputNoteBaseQuery;
        _pastMedicationCondition = pastMedicationCondition;
        _organizationUnitClinicQueryHandler = organizationUnitClinicQueryHandler;
        _staffEncounterRepository = staffEncounterRepository;
        _patientEncounterRepository = patientEncounterRepository;
    }

    public async Task<List<GetStaffMembersResponse>> GetDoctorsByUnit(long unitId, long jobTitleId)
    {
        var request = new GetAllStaffMembersRequest()
        {
            JobTitleIdFilter = jobTitleId
        };
        var staffMembers = await _allStaffQueryHandler.Handle(request);

        var list = staffMembers.Items
                .WhereIf(
                    unitId > 0,
                    u => u.UnitId == unitId
                ).ToList();

        return list;
    }
    public async Task<GetStaffMemberResponse> GetStaffByUserId(long staffUserId)
    {
        var ids = new EntityDto<long>();
        ids.Id = staffUserId;
        var staffMembers = await _staffQueryHandler.Handle(ids);

        return staffMembers;
    }

    public async Task<string> GetNoteTitle(long patientId, long encounterId, int tenantId)
    {
        var allnotes = await _InputNoteBaseQuery.GetPatientInputNotesByEncounter(patientId, encounterId, tenantId).ToListAsync();
        var title = allnotes.Count == 0 ? "Patient seen" :
                    allnotes.Any(a=>a.Description.Contains("Patient not on bed")) ? "Patient not on bed"
                        : "Patient seen";

        return title;
    }
    public async Task<string> GenerateNoteIntroduction(GetPatientDetailsOutDto patient,List<PatientSymptomSummaryForReturnDto> symptoms, long? unitId, long facilityId, int tenantId, long userId)
    {
        var patientId = patient.Id;
        int years = new DateTime(DateTime.Now.Subtract(patient.DateOfBirth).Ticks).Year - 1;
        var gender = patient.Gender == GenderType.Male && years < 19 ? "boy" :
                     patient.Gender == GenderType.Male && years >= 19 ? "man" :
                     patient.Gender == GenderType.Female && years < 19 ? "girl" :
                     patient.Gender == GenderType.Female && years >= 19 ? "woman" : "person";

        var medicalHistory = await _pastMedicationCondition.Handle(patientId);
        var passMedicalHistory = string.Empty;
        if(medicalHistory != null)
        {
            var pastMedicalCondition = medicalHistory.PastMedicalConditions.Select(s => s.ChronicCondition).ToList();
            passMedicalHistory = pastMedicalCondition.Count > 0 ? $" with a history of {String.Join(",", pastMedicalCondition)}" : string.Empty;
        }

        var orgUnit = await _organizationUnitClinicQueryHandler.Handle(new GetOrganizationUnitsInput()
        {
            UnitId = unitId,
            FacilityId = facilityId,
            IncludeClinics = true
        }, tenantId) ?? throw new UserFriendlyException("No organization unit or clinic found.");
        var unit = unitId != null ? orgUnit.Items.FirstOrDefault() : new OrganizationUnitDto();

        var summary = symptoms == null ? null : symptoms.Where(x => x.CreatorUserId == userId &&
                                                                    x.SymptomEntryType == Symptom.SymptomEntryType.Suggestion)
            .Select(s => s.Description).ToList();

        var suggestionSummary = string.Empty;
        if (summary != null)
            suggestionSummary = summary.Count > 0 ? $" on account of {String.Join(",", summary)}" : string.Empty;

        return string.IsNullOrEmpty(suggestionSummary) ? string.Empty : 
                $"A {years} year old {gender},{passMedicalHistory} who presented at {unit?.DisplayName}{suggestionSummary}";
    }

    public async Task<StaffEncounter> GetStaffEncounter(long encounterId, int tenantId)
    {
        var staffEncounter = await _staffEncounterRepository
            .GetAll()
            .Include(s => s.Staff)
            .ThenInclude(s => s.UserFk)
            .Include(s => s.Encounter)
            .ThenInclude(s => s.Patient)
            .Include(s => s.Encounter)
            .ThenInclude(s => s.InvestigationResults)
            .Include(s => s.Encounter)
        .ThenInclude(s => s.Unit)
            .FirstOrDefaultAsync(encounter => encounter.EncounterId == encounterId && encounter.TenantId == tenantId) ??
            throw new UserFriendlyException("No staff encounter found.");

        return staffEncounter;
    }

    public async Task<PatientEncounter> GetPatientEncounter(long encounterId, int tenantId)
    {
        var patientEncounter = await _patientEncounterRepository
                .GetAll()
                .Include(s => s.Patient)
                .Include(s => s.Unit)
                .Include(s => s.Facility)
                .Include(s => s.InvestigationResults)
                .ThenInclude(s => s.Investigation)
                .Include(s => s.NursingCareSummaries)
                .Include(s => s.Admission)
                .ThenInclude(s => s.AttendingPhysician)
                .Include(s => s.Ward)
                .Include(s => s.WardBed)
                .ThenInclude(s => s.BedType)
                .FirstOrDefaultAsync(encounter => encounter.Id == encounterId && encounter.TenantId == tenantId) ??
                throw new UserFriendlyException("No patient encounter found.");

        return patientEncounter;
    }
}
