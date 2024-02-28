using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Query;

public class GetPatientPastMedicalHistoryQueryHandler : IGetPatientPastMedicalHistoryQueryHandler
{
    private readonly IRepository<PatientPastMedicalCondition, long> _patientPastMedicalConditionRepository;
    private readonly IRepository<User,long> _userRepository;

    public GetPatientPastMedicalHistoryQueryHandler(IRepository<PatientPastMedicalCondition, long> patientPastMedicalConditionRepository, IRepository<User, long> userRepository)
    {
        _patientPastMedicalConditionRepository = patientPastMedicalConditionRepository;
        _userRepository = userRepository;
    }

    public async Task<GetPatientPastMedicalConditionQueryResponse> Handle(long patientId)
    {
        var query = await (
                from m in _patientPastMedicalConditionRepository.GetAll().Include(x=>x.Medications)
                join u in _userRepository.GetAll() on m.CreatorUserId equals u.Id
                where m.PatientId == patientId
                orderby m.CreationTime descending
                select new GetPatientPastMedicalCondition
                {
                    Id = m.Id,
                    ChronicCondition = m.ChronicCondition,
                    SnomedId = m.SnomedId,
                    DiagnosisPeriod = $"{m.DiagnosisPeriod} {m.PeriodUnit} ago",
                    PeriodUnit = $"{m.PeriodUnit}",
                    Control = $"{m.Control}",
                    IsOnMedication = m.IsOnMedication,
                    Notes = m.Notes,
                    NumberOfPreviousInfarctions = m.NumberOfPreviousInfarctions,
                    IsHistoryOfAngina = m.IsHistoryOfAngina,
                    IsPreviousHistoryOfAngina = m.IsPreviousHistoryOfAngina,
                    IsPreviousOfAngiogram = m.IsPreviousOfAngiogram,
                    IsPreviousOfStenting = m.IsPreviousOfStenting,
                    IsPreviousOfMultipleInfarction = m.IsPreviousOfMultipleInfarction,
                    IsStillIll = m.IsStillIll,
                    PatientId = m.PatientId,
                    IsPrimaryTemplate = m.IsPrimaryTemplate,
                    LastEnteredByDate = m.CreationTime,
                    LastEnteredBy = $"{u.Title}. {u.Name}",
                    Medications = m.Medications.Select(x=> new PatientPastMedicalConditionMedicationResponse
                    {
                        MedicationType = x.MedicationType,
                        MedicationDose = x.MedicationDose,
                        PrescriptionFrequency = $"{x.PrescriptionFrequency}",
                        IsCompliantWithMedication = x.IsCompliantWithMedication,
                        MedicationUsageFrequency = $"{x.MedicationUsageFrequency}",
                        MedicationUsageFrequencyUnit = x.MedicationUsageFrequencyUnit,
                        FrequencyUnit = x.FrequencyUnit,
                        Id = x.Id,
                    }).ToList()

                }
            ).ToListAsync().ConfigureAwait(false);

        return new GetPatientPastMedicalConditionQueryResponse()
        {
            PastMedicalConditions = query,
            LastEnteredBy = query.FirstOrDefault()?.LastEnteredBy,
            LastEnteredByDate = query.FirstOrDefault()?.LastEnteredByDate
        };
    }
}
