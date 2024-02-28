using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Symptom.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction;

public interface IDoctorReviewAndSaveBaseQuery : ITransientDependency
{
    Task<List<GetStaffMembersResponse>> GetDoctorsByUnit(long unitId, long jobTitleId);
    Task<GetStaffMemberResponse> GetStaffByUserId(long staffUserId);
    Task<string> GetNoteTitle(long patientId, long encounterId, int tenantId);
    Task<string> GenerateNoteIntroduction(GetPatientDetailsOutDto patient, List<PatientSymptomSummaryForReturnDto> symptoms, long? unitId, long facilityId, int tenantId, long userId);
    Task<StaffEncounter> GetStaffEncounter(long encounterId, int tenantId);
    Task<PatientEncounter> GetPatientEncounter(long encounterId, int tenantId);
}