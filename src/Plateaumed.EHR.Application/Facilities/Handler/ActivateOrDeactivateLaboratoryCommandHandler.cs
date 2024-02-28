using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class ActivateOrDeactivateLaboratoryCommandHandler : IActivateOrDeactivateLaboratoryCommandHandler
    {
        private readonly IRepository<Facility, long> _facilityRepository;

        public ActivateOrDeactivateLaboratoryCommandHandler(IRepository<Facility, long> facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        public async Task Handle(ActivateOrDeactivateLaboratoryRequest request)
        {
            var facility = await _facilityRepository.FirstOrDefaultAsync((long)request.Id);

            if (facility == null)
            {
                throw new UserFriendlyException("Facility cannot be found");
            }

            facility.HasLaboratory = request.HasLaboratory;
            await _facilityRepository.UpdateAsync(facility);
        }
    }
}