using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.Vaccines;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeleteVaccinationHistoryCommandHandler : IDeleteVaccinationHistoryCommandHandler
    {
        private readonly IRepository<VaccinationHistory, long> _vaccinationHistoryRepository;

        public DeleteVaccinationHistoryCommandHandler(IRepository<VaccinationHistory, long> vaccinationHistoryRepository)
        {
            _vaccinationHistoryRepository = vaccinationHistoryRepository;
        }

        public async Task Handle(long id)
        {
            var history = await _vaccinationHistoryRepository.GetAll().SingleOrDefaultAsync(x => x.Id == id)
                ?? throw new UserFriendlyException("History not found");
            await _vaccinationHistoryRepository.DeleteAsync(history).ConfigureAwait(false);
        }
    }
}
