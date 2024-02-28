using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeletePatientFamilyMemberCommandHandler : IDeletePatientFamilyMemberCommandHandler
    {
        private readonly IRepository<PatientFamilyMembers, long> _familyMemberRepository;

        public DeletePatientFamilyMemberCommandHandler(IRepository<PatientFamilyMembers, long> familyMemberRepository)
        {
            _familyMemberRepository = familyMemberRepository;
        }

        public async Task Handle(long id)
        {
            var familyMember = await _familyMemberRepository.GetAll().SingleOrDefaultAsync(x => x.Id == id)
                ?? throw new UserFriendlyException("Family member does not exist");
            await _familyMemberRepository.DeleteAsync(familyMember).ConfigureAwait(false);
        }
    }
}
