using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Facilities.Dtos;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class ActivateOrDeactivateWardCommandHandler : IActivateOrDeactivateWardCommandHandler
    {
        private readonly IRepository<Ward, long> _wardRepository;

        public ActivateOrDeactivateWardCommandHandler(
            IRepository<Ward, long> wardRepository)
        {
            _wardRepository = wardRepository;
        }

        public virtual async Task Handle(ActivateOrDeactivateWardRequest input)
        {
            var ward = await _wardRepository.FirstOrDefaultAsync((long)input.Id);
            if (ward == null)
            {
                throw new UserFriendlyException("Ward cannot be found");
            }
            ward.IsActive = input.IsActive;

            await _wardRepository.UpdateAsync(ward);
        }
    }
}
