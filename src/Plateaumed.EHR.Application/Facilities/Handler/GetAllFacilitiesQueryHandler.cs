using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class GetAllFacilitiesQueryHandler : IGetAllFacilitiesQueryHandler
    {
        private readonly IRepository<Facility, long> _facilityRepository;
        private readonly IObjectMapper _objectMapper;

        public GetAllFacilitiesQueryHandler(
            IRepository<Facility, long> facilityRepository,
            IObjectMapper objectMapper
            )

        {
            _facilityRepository = facilityRepository;
            _objectMapper = objectMapper;
        }

        public async Task<List<FacilityDto>> Handle()
        {
            var filteredFacilities = _facilityRepository
                .GetAll()
                .IgnoreQueryFilters()
                .Include(f => f.GroupFk)
                .Include(f => f.TypeFk);

            var facilities = await filteredFacilities.ToListAsync();

            var facility = _objectMapper.Map<List<FacilityDto>>(facilities);
            return facility;
        }


    }
}