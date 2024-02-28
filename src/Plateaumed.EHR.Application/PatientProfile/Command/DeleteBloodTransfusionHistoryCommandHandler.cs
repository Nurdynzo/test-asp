using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeleteBloodTransfusionHistoryCommandHandler : IDeleteBloodTransfusionHistoryCommandHandler
    {
        private readonly IRepository<BloodTransfusionHistory, long> _bloodTransfusionHistoryRepository;

        public DeleteBloodTransfusionHistoryCommandHandler(IRepository<BloodTransfusionHistory, long> bloodTransfusionHistoryRepository)
        {
            _bloodTransfusionHistoryRepository = bloodTransfusionHistoryRepository;
        }

        public async Task Handle(long historyId)
        {
            var history = await _bloodTransfusionHistoryRepository.GetAll().SingleOrDefaultAsync(x => x.Id == historyId)
                ?? throw new UserFriendlyException("History not found");
            await _bloodTransfusionHistoryRepository.DeleteAsync(history);
        }
    }
}
