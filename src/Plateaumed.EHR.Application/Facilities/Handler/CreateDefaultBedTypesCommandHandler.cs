using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.BedTypes;
using Plateaumed.EHR.Facilities.Abstractions;

namespace Plateaumed.EHR.Facilities.Handler
{
    /// <inheritdoc />
    public class CreateDefaultBedTypesCommandHandler : ICreateDefaultBedTypesCommandHandler
    {
        private readonly IRepository<BedType, long> _bedTypeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bedTypeRepository"></param>
        public CreateDefaultBedTypesCommandHandler(IRepository<BedType, long> bedTypeRepository)
        {
            _bedTypeRepository = bedTypeRepository;
        }

        /// <inheritdoc />
        public async Task Handle(int tenantId, long facilityId)
        {
            foreach (var bedType in StaticBedTypes.GetAll())
            {
                await _bedTypeRepository.InsertAsync(new BedType { TenantId = tenantId, Name = bedType, FacilityId = facilityId});
            }
        }
    }
}
