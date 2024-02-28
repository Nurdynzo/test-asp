using System;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateDrugHistoryCommandHandler : IUpdateDrugHistoryCommandHandler
    {
        private readonly IRepository<DrugHistory, long> _drugHistoryRepository;

        public UpdateDrugHistoryCommandHandler(IRepository<DrugHistory, long> drugHistoryRepository)
        {
            _drugHistoryRepository = drugHistoryRepository;
        }

        public async Task Handle(CreateDrugHistoryRequestDto request)
        {
            var existingHistory = await _drugHistoryRepository.GetAll().SingleOrDefaultAsync(h => h.Id == request.Id)
                ?? throw new UserFriendlyException("History not found");

            existingHistory.PatientOnMedication = request.PatientOnMedication;
            existingHistory.MedicationName = request.MedicationName ?? existingHistory.MedicationName;
            existingHistory.Route = request.Route ?? existingHistory.Route;
            existingHistory.Dose = request.Dose;
            existingHistory.DoseUnit = request.DoseUnit ?? existingHistory.DoseUnit;
            existingHistory.PrescriptionFrequency = request.PrescriptionFrequency;
            existingHistory.PrescriptionInterval = request.PrescriptionInterval;
            existingHistory.CompliantWithMedication = request.CompliantWithMedication;
            existingHistory.UsageFrequency = request.UsageFrequency;
            existingHistory.UsageInterval = request.UsageInterval;
            existingHistory.IsMedicationStillBeingTaken = request.IsMedicationStillBeingTaken;
            existingHistory.WhenMedicationStopped = request.WhenMedicationStopped;
            existingHistory.StopInterval = request.StopInterval;
            existingHistory.Note = request.Note ?? existingHistory.Note;

            await _drugHistoryRepository.UpdateAsync(existingHistory).ConfigureAwait(false);

        }
    }
}
