using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateReviewDetailsHistoryStateCommandHandler : IUpdateReviewDetailsHistoryStateCommandHandler
    {
        private readonly IRepository<ReviewDetailsHistoryState, long> _reviewDetailsHistoryStateRepository;

        public UpdateReviewDetailsHistoryStateCommandHandler(
            IRepository<ReviewDetailsHistoryState, long> reviewDetailsHistoryStateRepository)
        {
            _reviewDetailsHistoryStateRepository = reviewDetailsHistoryStateRepository;
        }

        public async Task Handle(ReviewDetailsHistoryStateDto request, string updatedBy)
        {
            var states = await _reviewDetailsHistoryStateRepository.GetAll().SingleOrDefaultAsync(x => x.PatientId == request.PatientId) ?? throw new UserFriendlyException("Patient not found");
            states.PatientDoesNotTakeAlcohol = request.PatientDoesNotTakeAlcohol;
            states.PatientDoesNotSmoke = request.PatientDoesNotSmoke;
            states.NoFamilyHistory = request.NoFamilyHistory;
            states.NoPhysicalExerciseHistory = request.NoPhysicalExerciseHistory;
            states.NoBloodTransfusionHistory = request.NoSurgicalHistory;
            states.NoChronicIllness = request.NoChronicIllness;
            states.NoMajorInjuries = request.NoMajorInjuries;
            states.NoTravelHistory = request.NoTravelHistory;
            states.NoSurgicalHistory = request.NoSurgicalHistory;
            states.NoVaccinationHistory = request.NoVaccinationHistory;
            states.NoUseOfContraceptives = request.NoUseOfContraceptives;
            states.NoGynaecologicalIllness = request.NoGynaecologicalIllness;
            states.NoGynaecologicalSurgery = request.NoGynaecologicalSurgery;
            states.NoHistoryOfCervicalScreening = request.NoHistoryOfCervicalScreening;
            states.NeverBeenPregnant = request.NeverBeenPregnant;
            states.NoDeliveryDetails = request.NoDeliveryDetails;
            states.NoUseOfRecreationalDrugs = request.NoUseOfRecreationalDrugs;
            states.NotCurrentlyOnMedication = request.NotCurrentlyOnMedication;
            states.NoAllergies = request.NoAllergies;
            states.NoImplant = request.NoImplant;
            states.LastEditorName = updatedBy;
            await _reviewDetailsHistoryStateRepository.UpdateAsync(states).ConfigureAwait(false);
            
        }
    }
}
