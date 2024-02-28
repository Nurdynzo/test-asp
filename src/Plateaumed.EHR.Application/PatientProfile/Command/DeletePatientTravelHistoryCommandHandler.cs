using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.Patients;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeletePatientTravelHistoryCommandHandler : IDeletePatientTravelHistoryCommandHandler
    {
        private readonly IRepository<PatientTravelHistory, long> _travelHistoryRepository;

        public DeletePatientTravelHistoryCommandHandler(IRepository<PatientTravelHistory, long> travelHistoryRepository)
        {
            _travelHistoryRepository = travelHistoryRepository;
        }

        public async Task Handle(long id)
        {
            var travelHistory = await _travelHistoryRepository.GetAll().SingleOrDefaultAsync(h => h.Id == id)
                ?? throw new UserFriendlyException("Travel history not found");
            await _travelHistoryRepository.DeleteAsync(travelHistory).ConfigureAwait(false);
        }
    }
}
