using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeleteDrugHistoryCommandHandler : IDeleteDrugHistoryCommandHandler
    {
        private readonly IRepository<DrugHistory, long> _drugHistoryRepository;

        public DeleteDrugHistoryCommandHandler(IRepository<DrugHistory, long> drugHistoryRepository)
        {
            _drugHistoryRepository = drugHistoryRepository;
        }

        public async Task Handle(long id)
        {
            var history = await _drugHistoryRepository.GetAll().SingleOrDefaultAsync(h => h.Id == id)
                ?? throw new UserFriendlyException("History not found");
            await _drugHistoryRepository.DeleteAsync(history).ConfigureAwait(false);
        }
    }
}
