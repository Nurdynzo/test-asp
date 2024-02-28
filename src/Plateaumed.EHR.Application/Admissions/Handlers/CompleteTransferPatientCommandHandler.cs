using System.Threading.Tasks;
using Plateaumed.EHR.Encounters;

namespace Plateaumed.EHR.Admissions.Handlers
{
    public class CompleteTransferPatientCommandHandler : ICompleteTransferPatientCommandHandler
    {
        private readonly IEncounterManager _encounterManager;

        public CompleteTransferPatientCommandHandler(IEncounterManager encounterManager)
        {
            _encounterManager = encounterManager;
        }

        public async Task Handle(long encounterId)
        {
            await _encounterManager.CompleteTransferPatient(encounterId);
        }
    }
}
