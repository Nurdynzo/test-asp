using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdatePatientFamilyHistoryCommandHandler : IUpdatePatientFamilyHistoryCommandHandler
    {
        private readonly IRepository<PatientFamilyHistory, long> _familyHistoryRepository;

        public UpdatePatientFamilyHistoryCommandHandler(IRepository<PatientFamilyHistory, long> familyHistoryRepository)
        {
            _familyHistoryRepository = familyHistoryRepository;
        }

        public async Task Handle(PatientFamilyHistoryDto request)
        {
            var result = await _familyHistoryRepository.GetAll().SingleOrDefaultAsync(x => x.PatientId == request.PatientId)
                ?? throw new UserFriendlyException("No family history for selected patient");
            result.TotalNumberOfFemaleSiblings = request.TotalNumberOfFemaleSiblings;
            result.TotalNumberOfMaleChildren = request.TotalNumberOfMaleChildren;
            result.TotalNumberOfSiblings = request.TotalNumberOfSiblings;
            result.TotalNumberOfChildren = request.TotalNumberOfChildren;
            result.TotalNumberOfFemaleChildren = request.TotalNumberOfFemaleChildren;
            result.TotalNumberOfMaleChildren = result.TotalNumberOfMaleChildren;

            await _familyHistoryRepository.UpdateAsync(result).ConfigureAwait(false);
        }
    }
}
