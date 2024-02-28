using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeleteCigaretteAndTobaccoHistory : IDeleteCigaretteAndTobaccoHistory
    {
        private readonly IRepository<CigeretteAndTobaccoHistory, long> _cigaretteAndTobaccoHistory;

        public DeleteCigaretteAndTobaccoHistory(IRepository<CigeretteAndTobaccoHistory, long> cigaretteAndTobaccoHistory)
        {
            _cigaretteAndTobaccoHistory = cigaretteAndTobaccoHistory;
        }

        public async Task Handle(long id)
        {
            var history = await _cigaretteAndTobaccoHistory.GetAll().FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new UserFriendlyException("History does not exit");
            await _cigaretteAndTobaccoHistory.DeleteAsync(history);
        }
    }
}
