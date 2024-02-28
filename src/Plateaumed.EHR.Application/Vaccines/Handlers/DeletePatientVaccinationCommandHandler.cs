using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Vaccines.Abstractions;
namespace Plateaumed.EHR.Vaccines.Handlers
{
    public class DeletePatientVaccinationCommandHandler : IDeletePatientVaccinationCommandHandler
    {
        private readonly IRepository<Vaccination, long> _vaccinationRepository;
        public DeletePatientVaccinationCommandHandler(IRepository<Vaccination, long> vaccinationRepository)
        {
            _vaccinationRepository = vaccinationRepository;
        }
        public async Task Handle(long id)
        {
            var vaccination = await _vaccinationRepository.GetAsync(id) ??
                              throw new UserFriendlyException("Vaccination not found");
            await _vaccinationRepository.DeleteAsync(vaccination);
        }
    }
}
