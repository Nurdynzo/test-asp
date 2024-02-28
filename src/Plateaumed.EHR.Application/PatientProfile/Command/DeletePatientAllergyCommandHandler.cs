using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.PatientProfile.Abstraction;
namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeletePatientAllergyCommandHandler : IDeletePatientAllergyCommandHandler
    {
        private readonly IRepository<PatientAllergy,long> _allergyRepository;
        public DeletePatientAllergyCommandHandler(IRepository<PatientAllergy, long> allergyRepository)
        {
            _allergyRepository = allergyRepository;
        }
        public async Task Handle(long id)
        {
            var allergy = await _allergyRepository.FirstOrDefaultAsync(x => x.Id == id)
                          ?? throw new UserFriendlyException("Allergy not found");
            await _allergyRepository.DeleteAsync(allergy).ConfigureAwait(false);
        }

    }
}
