using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdatePastMedicalConditionCommandHandler : IUpdatePastMedicalConditionCommandHandler
    {
        private readonly IRepository<PatientPastMedicalCondition, long> _pastMedicalConditionRepository;
        private readonly IRepository<PatientPastMedicalConditionMedication, long> _pastMedicalConditionMedicationRepository;
        private readonly IObjectMapper _mapper;
        public UpdatePastMedicalConditionCommandHandler(IRepository<PatientPastMedicalCondition, long> pastMedicalConditionRepository,
            IRepository<PatientPastMedicalConditionMedication, long> pastMedicalConditionMedicationRepository,
            IObjectMapper mapper)
        {
            _pastMedicalConditionRepository = pastMedicalConditionRepository;
            _pastMedicalConditionMedicationRepository = pastMedicalConditionMedicationRepository;
            _mapper = mapper;
        }

        public async Task Handle(PatientPastMedicalConditionCommandRequest request)
        {
            var history = await _pastMedicalConditionRepository.GetAll()
                .Include(x => x.Medications)
                .SingleOrDefaultAsync(x => x.Id == request.Id.GetValueOrDefault())
                .ConfigureAwait(false)
                ?? throw new UserFriendlyException("History not found");

            var newMedications = (from med in request.Medications
                                 where med.Id is null or <= 0
                                 select _mapper.Map<PatientPastMedicalConditionMedication>(med)).ToList();

            if(newMedications.Count > 0)
            {
                foreach (var medication in newMedications)
                {
                    if(medication != null)
                    {
                        medication.PatientPastMedicalConditionId = request.Id.GetValueOrDefault();
                        await _pastMedicalConditionMedicationRepository.InsertAsync(medication).ConfigureAwait(false);
                    }
                }
            }

            history.SnomedId = request.SnomedId;
            history.DiagnosisPeriod = request.DiagnosisPeriod;
            history.PeriodUnit = request.PeriodUnit;
            history.ChronicCondition = request.ChronicCondition;
            history.IsOnMedication = request.IsOnMedication;
            history.Notes = request.Notes;
            history.NumberOfPreviousInfarctions = request.NumberOfPreviousInfarctions;
            history.IsHistoryOfAngina = request.IsHistoryOfAngina;
            history.IsPreviousHistoryOfAngina = request.IsPreviousHistoryOfAngina;
            history.IsPreviousOfAngiogram = request.IsPreviousOfAngiogram;
            history.IsPreviousOfStenting = request.IsPreviousOfStenting;
            history.IsPreviousOfMultipleInfarction = request.IsPreviousOfMultipleInfarction;
            history.IsStillIll = request.IsStillIll;
            history.IsPrimaryTemplate = request.IsPrimaryTemplate;
            
            foreach(var item in request.Medications)
            {
                var medication = await _pastMedicalConditionMedicationRepository.GetAll()
                    .SingleOrDefaultAsync(x => x.Id == item.Id.GetValueOrDefault());
                if(medication is not null)
                {
                    medication.MedicationType = item.MedicationType;
                    medication.MedicationDose = item.MedicationDose;
                    medication.PrescriptionFrequency = item.PrescriptionFrequency;
                    medication.FrequencyUnit = item.FrequencyUnit; 
                    medication.IsCompliantWithMedication = item.IsCompliantWithMedication;
                    medication.MedicationUsageFrequency = item.MedicationUsageFrequency;
                    medication.MedicationUsageFrequencyUnit = item.MedicationUsageFrequencyUnit;

                    await _pastMedicalConditionMedicationRepository.UpdateAsync(medication).ConfigureAwait(false);
                }
            }
            await _pastMedicalConditionRepository.UpdateAsync(history).ConfigureAwait(false);
        }
    }
}
