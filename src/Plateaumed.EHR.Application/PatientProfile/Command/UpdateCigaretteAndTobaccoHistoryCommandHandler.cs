using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateCigaretteAndTobaccoHistoryCommandHandler : IUpdateCigaretteAndTobaccoHistoryCommandHandler
    {
        private readonly IRepository<CigeretteAndTobaccoHistory, long> _cigaretteAndTobaccoHistoryrepository;

        public UpdateCigaretteAndTobaccoHistoryCommandHandler(IRepository<CigeretteAndTobaccoHistory, long> cigaretteAndTobaccoHistoryrepository)
        {
            _cigaretteAndTobaccoHistoryrepository = cigaretteAndTobaccoHistoryrepository;
        }

        public async Task Handle(CreateCigaretteHistoryRequestDto request)
        {
            var existingRecord = await _cigaretteAndTobaccoHistoryrepository.GetAll()
                .SingleOrDefaultAsync(x => x.Id == request.Id) ?? throw new UserFriendlyException("History not found");

            existingRecord.PatientDoesNotConsumeTobacco = request.PatientDoesNotConsumeTobacco;
            existingRecord.FormOfTobacco = request.FormOfTobacco ?? existingRecord.FormOfTobacco;
            existingRecord.Route = request.Route ?? existingRecord.Route;
            existingRecord.NumberOfDaysPerWeek = request.NumberOfDaysPerWeek;
            existingRecord.NumberOfPacksOrUnitsPerDay = request.NumberOfPacksOrUnitsPerDay;
            existingRecord.StillTakesSubstance = request.StillTakesSubstance;
            existingRecord.Note = request.Note ?? existingRecord.Note;
            existingRecord.BeginningFrequency = request.BeginningFrequency;
            existingRecord.BeginningInterval = request.BeginningInterval;
            existingRecord.EndFrequency = request.EndFrequency;
            existingRecord.EndInterval = request.EndInterval;

            await _cigaretteAndTobaccoHistoryrepository.UpdateAsync(existingRecord);
        }
    }
}

