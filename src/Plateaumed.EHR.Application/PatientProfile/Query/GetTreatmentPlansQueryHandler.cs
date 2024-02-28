using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Vaccines;
namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetTreatmentPlansQueryHandler : IGetTreatmentPlansQueryHandler
    {
        private readonly IRepository<Diagnosis,long> _diagnosisRepository;
        private readonly IRepository<AllInputs.Medication,long> _medicationRepository;
        private readonly IRepository<Vaccination, long> _vaccinationRepository;
        private readonly IRepository<VaccineSchedule,long> _vaccineScheduleRepository;
        private readonly IRepository<Vaccine,long> _vaccineRepository;
        private readonly IRepository<Procedure,long> _procedureRepository;
        private readonly IRepository<AllInputs.PlanItems,long> _planItemsRepository;
        public GetTreatmentPlansQueryHandler(
            IRepository<Diagnosis, long> diagnosisRepository,
            IRepository<AllInputs.Medication, long> medicationRepository,
            IRepository<Vaccination, long> vaccinationRepository,
            IRepository<VaccineSchedule, long> vaccineScheduleRepository,
            IRepository<Vaccine, long> vaccineRepository,
            IRepository<Procedure, long> procedureRepository,
            IRepository<AllInputs.PlanItems, long> planItemsRepository)
        {
            _diagnosisRepository = diagnosisRepository;
            _medicationRepository = medicationRepository;
            _vaccinationRepository = vaccinationRepository;
            _vaccineScheduleRepository = vaccineScheduleRepository;
            _vaccineRepository = vaccineRepository;
            _procedureRepository = procedureRepository;
            _planItemsRepository = planItemsRepository;
        }
        public async Task<List<GetTreatmentPlansQueryResponse>> Handle(GetTreatmentPlansRequest request)
        {
            var query = from d in _diagnosisRepository.GetAll()
                        where d.PatientId == request.PatientId
                        group d by d.CreationTime.Date into g

                        orderby g.Key descending
                        select new GetTreatmentPlansQueryResponse()
                        {
                            DiagnosisDate = g.Key,
                            Diagnosis = g.FirstOrDefault().Description,
                            TreatmentMedication = (from m in _medicationRepository.GetAll().Include(x=>x.MedicationAdministrationActivities)
                                                   where m.PatientId == request.PatientId && m.CreationTime.Date == g.Key

                                                   select new TreatmentMedication
                                                   {
                                                       Medication = m.ProductName,
                                                       DosageAndUnit = m.DoseUnit,
                                                       Frequency = m.Frequency,
                                                       Duration = m.Duration,
                                                       Date = m.CreationTime,
                                                       Details = m.MedicationAdministrationActivities.Select(x => new TreatmentMedicationDetails()
                                                       {
                                                           Date = x.CreationTime,
                                                           DosageAndUnit = x.DoseUnit,
                                                           Notes = x.Note,
                                                           Status = x.IsAvailable ? "Administered" : "Not Available",
                                                           DoseValue = x.DoseValue ?? 0
                                                       }).ToList()

                                                   }).ToList(),
                            TreatmentVaccination = (from v in _vaccinationRepository.GetAll()
                                                    join s in _vaccineScheduleRepository.GetAll() on v.VaccineScheduleId equals s.Id
                                                    join va in _vaccineRepository.GetAll() on s.VaccineId equals va.Id into vg
                                                    from va in vg.DefaultIfEmpty()
                                                    where v.PatientId == request.PatientId && v.CreationTime.Date == g.Key
                                                    group v by v.CreationTime.Date into vg
                                                    select new TreatmentVaccination()
                                                    {
                                                        Vaccination = vg.FirstOrDefault().VaccineBrand,
                                                        Date = vg.Key,
                                                        DosesAdministered = "",// to be determined,
                                                        DateAdministered = vg.FirstOrDefault().DateAdministered,
                                                        NextDueDate = vg.FirstOrDefault().DueDate,
                                                        Details = vg.Select(x => new TreatmentVaccinationDetails()
                                                        {
                                                            Doses = 0,// to be determined,
                                                            Brand = x.VaccineBrand,
                                                            BatchNo = x.VaccineBatchNo,
                                                            Notes = x.Note,
                                                            HasComplication = x.HasComplication,
                                                            NextDueDate = x.DueDate,
                                                            DateAdministered = x.DateAdministered
                                                        }).ToList()
                                                    }).FirstOrDefault(),
                            TreatmentProcedures = (from p in _procedureRepository.GetAll()
                                                   where p.PatientId == request.PatientId && p.CreationTime.Date == g.Key
                                                   select new TreatmentProcedure()
                                                   {
                                                       Name = p.SelectedProcedures,
                                                       DateTime = p.CreationTime,
                                                       Note = p.Note
                                                   }).ToList(),
                            TreatmentOtherPlanItems = (from pi in _planItemsRepository.GetAll()
                                                      where pi.PatientId == request.PatientId && pi.CreationTime.Date == g.Key
                                                      select new TreatmentOtherPlanItems()
                                                      {
                                                          Description = pi.Description,
                                                          DateTime = pi.CreationTime
                                                      }).ToList(),

                        };
            query = request switch
            {
                { Filter: TreatmentPlanTimeFilter.Today } => query.Where(x => x.DiagnosisDate.Date == DateTime.UtcNow.Date),
                { Filter: TreatmentPlanTimeFilter.ThisWeek } => query.Where(x => x.DiagnosisDate.Date >= DateTime.UtcNow.Date.AddDays(-7)),
                { Filter: TreatmentPlanTimeFilter.ThisMonth } => query.Where(x => x.DiagnosisDate.Date >= DateTime.UtcNow.Date.AddDays(-30)),
                { Filter: TreatmentPlanTimeFilter.ThisYear } => query.Where(x => x.DiagnosisDate.Date >= DateTime.UtcNow.Date.AddDays(-365)),
                _ => query
            };
            var result = await query.ToListAsync();
            var calculatedResult = result.Select(x => new GetTreatmentPlansQueryResponse
            {
                DiagnosisDate = x.DiagnosisDate,
                Diagnosis = x.Diagnosis,
                TreatmentMedication = CalculateDoseUsage(x.TreatmentMedication),
                TreatmentVaccination = x.TreatmentVaccination,
                TreatmentProcedures = x.TreatmentProcedures,
                TreatmentOtherPlanItems = x.TreatmentOtherPlanItems

            }).ToList();
            return calculatedResult;


        }
        private List<TreatmentMedication> CalculateDoseUsage(List<TreatmentMedication> treatmentMedications)
        {
            var totalDose = treatmentMedications.GroupBy(x=>x.Medication)
                .Select(x=>
                    x.Sum(y=> int.TryParse(y.DosageAndUnit.Length>0?y.DosageAndUnit.Trim().Split(" ")[0]:y.DosageAndUnit,out var dose)?dose:0))
                .Sum();
            return treatmentMedications.Select(x => new TreatmentMedication
            {
                Medication = x.Medication,
                DosageAndUnit = x.DosageAndUnit,
                Frequency = x.Frequency,
                Duration = x.Duration,
                Date = x.Date,
                DoseAdministered = $"{x.Details.Sum(y=>y.DoseValue)} / {totalDose}",
                Details = x.Details.Select(y => new TreatmentMedicationDetails
                {
                    Date = y.Date,
                    DosageAndUnit = y.DosageAndUnit,
                    Notes = y.Notes,
                    Status = y.Status,
                    DoseAdministered = $"Dose {y.DoseValue} of {totalDose}"
                }).ToList()
            }).ToList();
        }


    }
}
