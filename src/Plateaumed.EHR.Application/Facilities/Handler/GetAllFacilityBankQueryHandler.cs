using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class GetAllFacilityBankQueryHandler : IGetAllFacilityBankQueryHandler
    {
        private readonly IRepository<FacilityBank, long> _facilityBankRepository;
        private readonly IObjectMapper _objectMapper;

        public GetAllFacilityBankQueryHandler(
            IRepository<FacilityBank, long> facilityBankRepository,
            IObjectMapper objectMapper
            )
        {
            _facilityBankRepository = facilityBankRepository;
            _objectMapper = objectMapper;
        }

        public async Task<PagedResultDto<GetFacilityBankForViewDto>> Handle(GetAllFacilityBanksInput input)
        {
            var filterTerms = !string.IsNullOrWhiteSpace(input.Filter) ? input.Filter.ToLower().Split(" ") : null;

            var filteredFacilityBanks = _facilityBankRepository
               .GetAll()
               .Include(x => x.Facility)
               .WhereIf(filterTerms != null, f => filterTerms.All(name => f.Facility.Name.ToLower().Contains(name.ToLower())))
               .WhereIf(input.FacilityIdFilter.HasValue, f => f.FacilityId == input.FacilityIdFilter);

            var totalCount = await filteredFacilityBanks.CountAsync();

            var pagedAndFilteredFacilityBanks = filteredFacilityBanks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var results = await pagedAndFilteredFacilityBanks
                .Select(facilityBank => new GetFacilityBankForViewDto
                {
                    FacilityBankResponse = _objectMapper.Map<FacilityBankResponseDto>(facilityBank),
                    FacilityName = facilityBank.Facility.Name
                })
                .ToListAsync();

             return new PagedResultDto<GetFacilityBankForViewDto>(totalCount, results);
        }
    }
}