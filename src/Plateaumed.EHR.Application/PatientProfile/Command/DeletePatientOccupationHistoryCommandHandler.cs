using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeletePatientOccupationHistoryCommandHandler : IDeletePatientOccupationHistoryCommandHandler
    {
        private readonly IRepository<OccupationalHistory, long> _occupationHistory;

        public DeletePatientOccupationHistoryCommandHandler(IRepository<OccupationalHistory, long> occupationHistory)
        {
            _occupationHistory = occupationHistory;
        }

        public async Task Handle(long id)
        {
            var history = await _occupationHistory.GetAll().SingleOrDefaultAsync(x => x.Id == id)
                ?? throw new UserFriendlyException("History not found");
            await _occupationHistory.DeleteAsync(history).ConfigureAwait(false);
        }
    }
}
