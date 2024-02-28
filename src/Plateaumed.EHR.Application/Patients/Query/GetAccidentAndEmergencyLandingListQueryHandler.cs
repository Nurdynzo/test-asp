using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Plateaumed.EHR.DateUtils;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Plateaumed.EHR.Misc;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.VitalSigns;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Authorization.Users;
using System.Collections.Generic;

namespace Plateaumed.EHR.Patients.Query
{
    public class GetAccidentAndEmergencyLandingListQueryHandler : IGetAccidentAndEmergencyLandingListQueryHandler
    {
        private readonly IRepository<PatientEncounter, long> _repository;
        private readonly IDateTimeProvider _dateTime;
        private readonly IRepository<PatientVital, long> _patientVitals;
        private readonly IRepository<Diagnosis, long> _diagnosis;
        private readonly IRepository<Admission, long> _admission;
        private readonly IRepository<User, long> _userRepository;

        public GetAccidentAndEmergencyLandingListQueryHandler(IRepository<PatientEncounter, long> repository,
            IDateTimeProvider dateTime,
            IRepository<PatientVital, long> patientVitals,
            IRepository<Diagnosis, long> diagnosis,
            IRepository<User, long> userRepository,
            IRepository<Admission, long> admission)
        {
            _repository = repository;
            _dateTime = dateTime;
            _patientVitals = patientVitals;
            _diagnosis = diagnosis;
            _userRepository = userRepository;
            _admission = admission;
        }

        public async Task<PagedResultDto<GetAccidentAndEmergencyLandingListResponse>> Handle(
            GetAccidentAndEmergencyLandingListRequest request)
        {
            var query = _repository.GetAll()
                .Where(x => x.ServiceCentre == ServiceCentreType.AccidentAndEmergency)
                .Where(InProgressOrLeftSinceMidnight())
                .Include(x => x.WardBed);

            var q2 = from q in query
                     join pv in _patientVitals.GetAll().Include(x => x.MeasurementRange).Include(x => x.VitalSign) on q.PatientId equals pv.PatientId
                     into vitals
                     let id = q.LastModifierUserId ?? q.CreatorUserId.Value
                     select new
                     {
                         q.TimeIn,
                         Encounter = q,
                         Vitals = vitals,
                         Diagnosis = (from d in _diagnosis.GetAll() where q.PatientId == d.PatientId select d).ToList(),
                         LastSeenBy = (from u in _userRepository.GetAll().AsNoTracking() where id == u.Id select u).FirstOrDefault(),
                         AttendingPhysician = (from p in _admission.GetAll().Include(x => x.AttendingPhysician)
                                                .ThenInclude(x => x.UserFk) where q.PatientId == p.PatientId select p)
                                                .FirstOrDefault()
                     };

            var paged = q2.OrderBy(request.Sorting ?? "TimeIn asc")
                .PageBy(request)
                .Select(x =>
                    new GetAccidentAndEmergencyLandingListResponse
                    {
                        PatientId = x.Encounter.PatientId,
                        ImageUrl = x.Encounter.Patient.ProfilePictureId,
                        FullName = x.Encounter.Patient.FullName,
                        EncounterId = x.Encounter.Id,
                        Status = x.Encounter.Status,
                        BirthDate = x.Encounter.Patient.DateOfBirth,
                        Gender = x.Encounter.Patient.GenderType,
                        Diagnosis = x.Diagnosis.Count > 0 ? x.Diagnosis.Select(x => x.Description).ToList() : new List<string>(),
                        BedNumber = x.Encounter.WardBed.BedNumber,
                        PatientVitals = x.Vitals.Select(x => new AandEPatientVitalsDto
                        {
                            Measurement = x.MeasurementRange.Unit,
                            Reading = x.VitalReading.ToString(),
                            VitalSign = x.VitalSign.Sign
                        }).ToList(),
                        LastSeenBy = x.LastSeenBy == null ? "" : x.LastSeenBy.FullName,
                        LastSeenAt = x.LastSeenBy == null ? null : (x.LastSeenBy.LastModificationTime ?? x.LastSeenBy.CreationTime),
                        AttendingPhysician = new AandEAttendingPhysician
                        {
                            Name = x.AttendingPhysician.AttendingPhysician.UserFk.DisplayName,
                            Unit = x.AttendingPhysician.AttendingPhysician.Jobs
                                                        .Where(u => u.FacilityId == x.Encounter.FacilityId)
                                                        .FirstOrDefault().Unit.DisplayName
                        }
                    });

            var responses = await paged.ToListAsync();
            var total = await query.CountAsync();

            return new PagedResultDto<GetAccidentAndEmergencyLandingListResponse>(total, responses);
        }

        private Expression<Func<PatientEncounter, bool>> InProgressOrLeftSinceMidnight()
        {
            return x => x.Status == EncounterStatusType.InProgress || (x.Status == EncounterStatusType.Transferred ||
                                                                       x.Status == EncounterStatusType.Discharged)
                && x.TimeOut.HasValue && x.TimeOut.Value > _dateTime.Now.Date;
        }
    }
}