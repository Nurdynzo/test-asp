using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.Vaccines;
using Plateaumed.EHR.Vaccines.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateVaccinationHistoryCommandHandler : IUpdateVaccinationHistoryCommandHandler
    {
        private readonly IRepository<VaccinationHistory, long> _vaccinationHistoryRepository;

        public UpdateVaccinationHistoryCommandHandler(IRepository<VaccinationHistory, long> vaccinationHistoryRepositroy)
        {
            _vaccinationHistoryRepository = vaccinationHistoryRepositroy;
        }

        public async Task Handle(CreateVaccinationHistoryDto request)
        {
            var history = await _vaccinationHistoryRepository.GetAll().SingleOrDefaultAsync(x => x.Id == request.Id)
                ?? throw new UserFriendlyException("History not found");
            history.Note = request.Note ?? history.Note;
            history.VaccineId = request.VaccineId;
            history.HasComplication = request.HasComplication;
            history.LastVaccineDuration = request.LastVaccineDuration;
            history.NumberOfDoses = request.NumberOfDoses;

            await _vaccinationHistoryRepository.UpdateAsync(history).ConfigureAwait(false);
        }
    }
}
