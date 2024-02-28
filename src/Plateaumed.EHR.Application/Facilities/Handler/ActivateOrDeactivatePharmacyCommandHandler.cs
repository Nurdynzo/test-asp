using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class ActivateOrDeactivatePharmacyCommandHandler : IActivateOrDeactivatePharmacyCommandHandler
    {
        private readonly IRepository<Facility, long> _facilityRepository;

        public ActivateOrDeactivatePharmacyCommandHandler(IRepository<Facility, long> facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        public async Task Handle(ActivateOrDeactivatePharmacyRequest request)
        {
            var facility = await _facilityRepository.FirstOrDefaultAsync((long)request.Id);

            if (facility == null)
            {
                throw new UserFriendlyException("Facility cannot be found");
            }

            facility.HasPharmacy = request.HasPharmacy;
            await _facilityRepository.UpdateAsync(facility);
        }
    }
}
