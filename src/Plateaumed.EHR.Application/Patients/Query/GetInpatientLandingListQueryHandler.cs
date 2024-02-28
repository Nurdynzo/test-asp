using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.DateUtils;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.Utility;
using Plateaumed.EHR.VitalSigns;

namespace Plateaumed.EHR.Patients.Query
{
    public class GetInpatientLandingListQueryHandler : IGetInpatientLandingListQueryHandler
    {
        private readonly IRepository<PatientEncounter, long> _repository;
        private readonly IDateTimeProvider _dateTime;
        private readonly IRepository<PatientVital, long> _patientVitals;
        private readonly IRepository<Diagnosis, long> _diagnosis;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Admission, long> _admission;
        private readonly IRepository<AllInputs.Medication, long> _medications;
        private readonly IRepository<Procedure, long> _procedure;
        private readonly IRepository<PatientStability, long> _patientStability;
        private readonly IRepository<Invoice, long> _invoices;

        public GetInpatientLandingListQueryHandler(IRepository<PatientEncounter, long> repository,
            IDateTimeProvider dateTime, IRepository<PatientVital, long> patientVitals,
            IRepository<Diagnosis, long> diagnosis,
            IRepository<User, long> userRepository,
            IRepository<Admission, long> admission,
            IRepository<AllInputs.Medication, long> medications,
            IRepository<Procedure, long> procedure,
            IRepository<PatientStability, long> patientStability,
            IRepository<Invoice, long> invoices)
        {
            _repository = repository;
            _dateTime = dateTime;
            _patientVitals = patientVitals;
            _diagnosis = diagnosis;
            _userRepository = userRepository;
            _admission = admission;
            _medications = medications;
            _procedure = procedure;
            _patientStability = patientStability;
            _invoices = invoices;
        }

        public async Task<PagedResultDto<GetInpatientLandingListResponse>> Handle(
            GetInpatientLandingListRequest request)
        {
            var query = _repository.GetAll()
                .Include(x => x.Patient)
                .Include(x=>x.WardBed)
                .Where(x => x.WardId == request.WardId)
                .Where(x => x.ServiceCentre == ServiceCentreType.InPatient)
                .Where(InProgressOrLeftWardSincePrevious10Am());           

            var q2 = from q in query                   
                     join pv in _patientVitals.GetAll().Include(x => x.MeasurementRange).Include(x => x.VitalSign) on q.PatientId equals pv.PatientId
                     into vitals
                     let id = q.LastModifierUserId ?? q.CreatorUserId.Value
                     select new
                     {
                         q.Id,
                         Encounter = q,
                         Diagnosis = (from d in _diagnosis.GetAll() where q.PatientId == d.PatientId orderby d.CreationTime descending select d).ToList(),
                         Vitals = vitals,
                         SeenBy = (from u in _userRepository.GetAll().AsNoTracking() where id == u.Id select u).FirstOrDefault(),
                         Physician = (from p in _admission.GetAll().Include(x=>x.AttendingPhysician).ThenInclude(x=>x.UserFk) where q.PatientId == p.PatientId select p).FirstOrDefault(),
                         Medications = (from m in _medications.GetAll().Where(x=>x.PatientId == q.PatientId).OrderByDescending(x=>x.CreationTime) select m).ToList(),
                         Interventions = (from i in _procedure.GetAll() where i.PatientId  == q.PatientId orderby i.CreationTime descending select i).ToList(),
                         StabilityStatus = (from s in _patientStability.GetAll() where s.PatientId == q.PatientId orderby s.CreationTime descending select s).FirstOrDefault(),
                         Invoice = (from i in _invoices.GetAll() where q.PatientId == i.PatientId && q.Id == i.PatientEncounterId orderby i.CreationTime descending select i).FirstOrDefault(),
                     };         

            var paged = q2.OrderBy(request.Sorting ?? "Id asc")
                        .PageBy(request)
                        .Select(x => new GetInpatientLandingListResponse
                        {
                            PatientId = x.Encounter.PatientId,
                            ImageUrl = x.Encounter.Patient.ProfilePictureId,
                            FullName = x.Encounter.Patient.FullName,
                            EncounterId = x.Encounter.Id,
                            Status = x.Encounter.Status,
                            BirthDate = x.Encounter.Patient.DateOfBirth,
                            Gender = x.Encounter.Patient.GenderType,
                            Diagnosis = x.Diagnosis.Count > 0 ? x.Diagnosis.Select(x => x.Description).Take(5).ToList() : new List<string>(),
                            BedNumber = x.Encounter.WardBed.BedNumber,
                            PatientVitals = x.Vitals.Select(x => new PatientVitalsDto
                            {
                                Measurement = x.MeasurementRange.Unit,
                                Reading = x.VitalReading.ToString(),
                                VitalSign = x.VitalSign.Sign
                            }).Take(5).ToList(),
                            LastSeenBy = x.SeenBy == null ? "" : x.SeenBy.FullName,
                            LastSeenAt = x.SeenBy == null ? null : (x.SeenBy.LastModificationTime ?? x.SeenBy.CreationTime),
                            AttendingPhysician = new AttendingPhysician
                            {
                                Name = x.Physician.AttendingPhysician.UserFk.DisplayName,
                                Unit = x.Physician.AttendingPhysician.Jobs
                                                        .Where(u => u.FacilityId == x.Encounter.FacilityId)
                                                        .FirstOrDefault().Unit.DisplayName
                            },
                            PatientMedications = x.Medications.Select(x => new MedicationsListDto
                            {
                                Direction = x.Direction,
                                DoseUnit = x.DoseUnit,
                                Duration = x.Duration,
                                Frequency = x.Frequency,
                                ProductName = x.ProductName
                            }).Take(2).ToList(),
                            Interventions = x.Interventions.Select( pr => new SelectedProceduresDto
                            {
                                ProcedureId = pr.Id,
                                SelectedProcedure = pr.SelectedProcedures == null ? new List<SelectedProcedureListDto>()
                                    : JsonConvert.DeserializeObject<List<SelectedProcedureListDto>>(pr.SelectedProcedures),
                                IsDeleted = pr.IsDeleted,                               
                                Status = pr.ProcedureStatus
                            }).Take(1).ToList(),
                            StabilityStatus = x.StabilityStatus == null ? "" : x.StabilityStatus.Status.ToString(),
                            PaidAmount = x.Invoice == null ? new MoneyDto() : x.Invoice.AmountPaid.ToMoneyDto(),
                            OutstandingAmount = x.Invoice == null ? new MoneyDto() : x.Invoice.OutstandingAmount.ToMoneyDto(),
                            TotalAmount = x.Invoice == null ? new MoneyDto() : x.Invoice.TotalAmount.ToMoneyDto()
                        });

            var responses = await paged.ToListAsync();
            var total = await query.CountAsync();

            return new PagedResultDto<GetInpatientLandingListResponse>(total, responses);
        }       

        private Expression<Func<PatientEncounter, bool>> InProgressOrLeftWardSincePrevious10Am() =>
            x => x.Status == EncounterStatusType.InProgress || (x.Status == EncounterStatusType.Transferred ||
                                                                x.Status == EncounterStatusType.Discharged ||
                                                                x.Status == EncounterStatusType.Deceased)
                && x.TimeOut.HasValue &&
                (x.TimeOut.Value.Date == _dateTime.Now.Date ||
                 x.TimeOut.Value > _dateTime.Now.Date.AddDays(-1) && _dateTime.Now.Hour < 10);
    }
}