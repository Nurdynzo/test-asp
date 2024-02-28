using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeletePatientImplantCommandHandler : IDeletePatientImplantCommandHandler
    {
        private readonly IRepository<PatientImplant, long> _patientImplantRepository;

        public DeletePatientImplantCommandHandler(IRepository<PatientImplant, long> patientImplantRepository)
        {
            _patientImplantRepository = patientImplantRepository;
        }

        public async Task Handle(long id)
        {
            var history = await _patientImplantRepository.GetAll().SingleOrDefaultAsync(x => x.Id == id)
                ?? throw new UserFriendlyException("History not found");
            await _patientImplantRepository.DeleteAsync(history).ConfigureAwait(false);
        }
    }
}
