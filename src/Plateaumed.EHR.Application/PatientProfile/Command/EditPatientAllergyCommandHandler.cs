using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Command
{
    public class EditPatientAllergyCommandHandler : IEditPatientAllergyCommandHandler
    {
        private readonly IRepository<PatientAllergy,long> _allergyRepository;
        public EditPatientAllergyCommandHandler(IRepository<PatientAllergy, long> allergyRepository)
        {
            _allergyRepository = allergyRepository;
        }
        public async Task Handle(EditPatientAllergyCommandRequest request)
        {
            var allergy = await _allergyRepository.FirstOrDefaultAsync(x => x.Id == request.Id)
                          ?? throw new UserFriendlyException("Allergy not found");
            allergy.AllergyType = request.AllergyType;
            allergy.AllergySnomedId = request.AllergySnomedId;
            allergy.Reaction = request.Reaction;
            allergy.ReactionSnomedId = request.ReactionSnomedId;
            allergy.Notes = request.Notes;
            allergy.Severity = request.Severity;
            allergy.Interval = request.Interval;
             await _allergyRepository.UpdateAsync(allergy).ConfigureAwait(false);
        }
    }
}
