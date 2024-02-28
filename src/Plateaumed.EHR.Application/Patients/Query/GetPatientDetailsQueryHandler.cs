using System;
using System.Linq;
using Plateaumed.EHR.Patients.Dtos;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.Patients.Query
{
    public class GetPatientDetailsQueryHandler : IGetPatientDetailsQueryHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<PatientEncounter, long> _patientEncounterRepository;
        private readonly IRepository<StaffEncounter, long> _staffEncounterRepository;
        private readonly IGetCurrentUserFacilityIdQueryHandler _getCurrentUserFacility;
        private readonly IRepository<SpecializedProcedure, long> _specializedProcedureRepository;

        public GetPatientDetailsQueryHandler(
            IRepository<Patient, long> patientRepository,
            IRepository<PatientEncounter, long> patientEncounterRepository,
            IRepository<StaffEncounter, long> staffEncounterRepository, 
            IGetCurrentUserFacilityIdQueryHandler getCurrentUserFacility,
            IRepository<SpecializedProcedure, long> specializedProcedureRepository)
        {
            _patientRepository = patientRepository;
            _patientEncounterRepository = patientEncounterRepository;
            _staffEncounterRepository = staffEncounterRepository;
            _getCurrentUserFacility = getCurrentUserFacility;
            _specializedProcedureRepository = specializedProcedureRepository;
        }

        public async Task<GetPatientDetailsOutDto> Handle(GetPatientDetailsInput input)
        {
            var facilityId = await _getCurrentUserFacility.Handle();

            var output = await _patientRepository.GetAll()
                .Include(p => p.PatientCodeMappings)
                .Include(p => p.UserFk)
                .Include(x => x.PatientEncounters)
                .ThenInclude(x => x.Admission)
                .Include(p => p.PatientEncounters)
                .ThenInclude(x => x.StaffEncounters)
                .ThenInclude(x => x.Staff.Jobs)
                .ThenInclude(x => x.JobRole)
                .Where(p => p.Id == input.PatientId)
                .Select(p => new GetPatientDetailsOutDto
                {
                    Id = p.Id,
                    PatientCode = p.PatientCodeMappings
                        .Where(x => !facilityId.HasValue || x.FacilityId == facilityId)
                        .Select(x => x.PatientCode)
                        .FirstOrDefault(),
                    BloodGenotype = p.BloodGenotype,
                    BloodGroup = p.BloodGroup,
                    Gender = p.GenderType,
                    FullName = p.FirstName + " " + p.LastName,
                    DateOfBirth = p.DateOfBirth,
                    PictureUrl = p.PictureUrl,
                    EncounterStatus = p.PatientEncounters.OrderByDescending(x => x.CreationTime)
                        .Where(x => !input.EncounterId.HasValue || x.Id == input.EncounterId)
                        .Select(x => x.Status).FirstOrDefault()
                }).FirstOrDefaultAsync();

            var (doctorName, doctorDate) =
                LastSeenBy(input.PatientId, input.EncounterId, StaticRoleNames.JobRoles.Doctor) ??
                new Tuple<string, DateTime?>(null, null);
            var (nurseName, nurseDate) =
                LastSeenBy(input.PatientId, input.EncounterId, StaticRoleNames.JobRoles.Nurse) ??
                new Tuple<string, DateTime?>(null, null);

            var dateAdmitted = GetAdmissionDate(input);

            output.LastSeenByDoctor = doctorDate;
            output.LastSeenByDoctorName = doctorName;
            output.LastSeenByNurse = nurseDate;
            output.LastSeenByNurseName = nurseName;
            output.DateAdmitted = dateAdmitted;
            output.LengthOfStayDays = dateAdmitted.HasValue ? DateTime.Now.Subtract(dateAdmitted.Value).Days : 0;
            output.DaysPostOp = await GetDaysPostOp(input);

            return output;
        }

        private async Task<int?> GetDaysPostOp(GetPatientDetailsInput input)
        {
            var procedureDate = await _specializedProcedureRepository.GetAll()
                .Include(x => x.Procedure)
                .Where(x => x.Procedure.PatientId == input.PatientId)
                .Where(x => x.Procedure.ProcedureType == ProcedureType.RecordProcedure)
                .Where(x => x.Procedure.ProcedureStatus == ProcedureStatus.Done)
                .OrderByDescending(x => x.ProposedDate)
                .Select(x => x.ProposedDate)
                .FirstOrDefaultAsync();
            return procedureDate != null ? DateTime.Now.Subtract(procedureDate.Value).Days : null;
        }

        private DateTime? GetAdmissionDate(GetPatientDetailsInput input)
        {
            return _patientEncounterRepository.GetAll()
                .Include(x => x.Admission)
                .Where(x => x.PatientId == input.PatientId)
                .OrderByDescending(x => x.CreationTime)
                .Select(x => x.Admission != null ? x.Admission.CreationTime : (DateTime?)null)
                .FirstOrDefault();
        }

        private Tuple<string, DateTime?> LastSeenBy(long patientId, long? encounterId, string role)
        {
            return _staffEncounterRepository.GetAll()
                .Include(x => x.Encounter)
                .Include(x => x.Staff.UserFk)
                .Include(x => x.Staff.Jobs)
                .ThenInclude(x => x.JobRole)
                .Where(e => e.Encounter.PatientId == patientId)
                .WhereIf(encounterId.HasValue, e => e.EncounterId == encounterId)
                .Where(e => e.Staff.Jobs.Any(x => x.IsPrimary && x.JobRole != null && x.JobRole.Name == role))
                .OrderByDescending(e => e.CreationTime)
                .Select(x => new Tuple<string, DateTime?>(x.Staff.UserFk.FullName, x.CreationTime))
                .FirstOrDefault();
        }
    }
}
