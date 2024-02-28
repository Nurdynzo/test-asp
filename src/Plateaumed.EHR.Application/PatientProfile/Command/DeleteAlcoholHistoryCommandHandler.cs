using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeleteAlcoholHistoryCommandHandler : IDeleteAlcoholHistoryCommandHandler
    {
        private readonly IRepository<AlcoholHistory, long> _alcoholHistory;

        public DeleteAlcoholHistoryCommandHandler(IRepository<AlcoholHistory, long> alcoholHistory)
        {
            _alcoholHistory = alcoholHistory;
        }


        public async Task Handle(long id)
        {
            var history = await _alcoholHistory.GetAll().SingleOrDefaultAsync(x => x.Id == id)
                ?? throw new UserFriendlyException("History does not exit");
            await _alcoholHistory.DeleteAsync(history);
        }
    }
}
