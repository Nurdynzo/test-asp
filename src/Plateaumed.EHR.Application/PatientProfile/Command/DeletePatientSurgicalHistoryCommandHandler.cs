using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeletePatientSurgicalHistoryCommandHandler : IDeletePatientSurgicalHistoryCommandHandler
    {
        private readonly IRepository<SurgicalHistory, long> _surgicalHistoryRepository;

        public DeletePatientSurgicalHistoryCommandHandler(IRepository<SurgicalHistory, long> surgicalHistoryRepository)
        {
            _surgicalHistoryRepository = surgicalHistoryRepository;
        }

        public async Task Handle(long surigcalHistoryId)
        {
            var history = await _surgicalHistoryRepository.GetAll().SingleOrDefaultAsync(x => x.Id == surigcalHistoryId)
                ?? throw new UserFriendlyException("History not found");
            await _surgicalHistoryRepository.DeleteAsync(history);
        }
    }
}
