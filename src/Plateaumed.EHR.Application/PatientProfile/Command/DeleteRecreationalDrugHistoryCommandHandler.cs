using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeleteRecreationalDrugHistoryCommandHandler : IDeleteRecreationalDrugHistoryCommandHandler
    {
        private readonly IRepository<RecreationalDrugHistory, long> _recreationalDrugHistoryRepository;

        public DeleteRecreationalDrugHistoryCommandHandler(IRepository<RecreationalDrugHistory, long> recreationalDrugHistoryRepository)
        {
            _recreationalDrugHistoryRepository = recreationalDrugHistoryRepository;
        }

        public async Task Handle(long id)
        {
            var history = await _recreationalDrugHistoryRepository.GetAll().SingleOrDefaultAsync(x => x.Id == id)
                ?? throw new UserFriendlyException("History not found");
            await _recreationalDrugHistoryRepository.DeleteAsync(history);
        }
    }
}
