using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateFamilyMemberDetailsCommandHandler : IUpdateFamilyMemberDetailsCommandHandler
    {
        private readonly IRepository<PatientFamilyMembers, long> _familyMemberRepository;

        public UpdateFamilyMemberDetailsCommandHandler(IRepository<PatientFamilyMembers, long> familyMemberRepository)
        {
            _familyMemberRepository = familyMemberRepository;
        }

        public async Task Handle(UpdateFamilyMembersDto request)
        {
            var familyMemberForUpdate = await _familyMemberRepository.GetAll()
                .SingleOrDefaultAsync(x => x.Id == request.Id) ?? throw new UserFriendlyException("Family member does not exist");

            familyMemberForUpdate.IsAlive = request.IsAlive;
            familyMemberForUpdate.AgeAtDeath = request.AgeAtDeath;
            familyMemberForUpdate.CausesOfDeath =request.CausesOfDeath;
            familyMemberForUpdate.AgeAtDiagnosis = request.AgeAtDiagnosis;
            familyMemberForUpdate.Relationship = request.Relationship;

            await _familyMemberRepository.UpdateAsync(familyMemberForUpdate).ConfigureAwait(false);
        }
    }
}
