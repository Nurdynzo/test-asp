using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.PriceSettings.Dto;
using System.Linq;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.Snowstorm.Abstractions;
using Plateaumed.EHR.Snowstorm.Dtos;
using Plateaumed.EHR.Symptom;
namespace Plateaumed.EHR.PriceSettings.Query
{
    public class GetUnifyPriceItemSearchQueryHandler : IGetUnifyPriceItemSearchQueryHandler
    {
        private readonly IRepository<Ward,long> _wardRepository;
        private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitRepository;
        private readonly ISnowstormBaseQuery _snowstormBaseQuery;
        private readonly IRepository<SnowmedSuggestion, long> _snomedSuggestionRepository;
        private const string SemanticTag = "procedure", SemanticTag2 = "regime/therapy";

        public GetUnifyPriceItemSearchQueryHandler(IRepository<Ward, long> wardRepository,
            IRepository<OrganizationUnitExtended, long> organizationUnitRepository,
            ISnowstormBaseQuery snowstormBaseQuery,
            IRepository<SnowmedSuggestion, long> snomedSuggestionRepository)
        {
            _wardRepository = wardRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _snowstormBaseQuery = snowstormBaseQuery;
            _snomedSuggestionRepository = snomedSuggestionRepository;
        }

        public async Task<List<PriceItemsSearchResponse>> Handle(PriceItemsSearchRequest request)
        {
            if (request.SearchTerm?.Length <= 3)
            {
                throw new UserFriendlyException("Search term must be more than 3 characters");
            }
            var searchText = string.IsNullOrEmpty(request.SearchTerm) ? "" : request.SearchTerm.ToLower();
            var query = request switch {
                { PricingCategory: PricingCategory.Consultation } =>
                    await SearchForClinicAsync(searchText, request.FacilityId).ConfigureAwait(false),
                { PricingCategory: PricingCategory.Procedure }
                    => await SearchProcedureFromSnomed(searchText).ConfigureAwait(false),
                { PricingCategory: PricingCategory.WardAdmission}
                    => await SearchForWardAsync(searchText, request.FacilityId).ConfigureAwait(false),
                {PricingCategory:PricingCategory.Others}
                    => await SearchForOtherPlansAsync(searchText).ConfigureAwait(false),
                _ => new List<PriceItemsSearchResponse>()
            };
            return query;
        }
        private  Task<List<PriceItemsSearchResponse>> SearchForOtherPlansAsync(string searchText)
        {
            var query =  _snomedSuggestionRepository.GetAll()
                .Where(x =>x.Type == AllInputType.PlanItems && x.Name.ToLower().Contains(searchText))
                .Select(x => new PriceItemsSearchResponse { Name = x.Name, ItemId = x.SnowmedId })
                .ToListAsync();
            return query;
        }

        private Task<List<PriceItemsSearchResponse>> SearchForWardAsync(string searchText, long facilityId)
        {
            return _wardRepository.GetAll()
                .Where(x => x.FacilityId == facilityId
                            && x.Name.ToLower().Contains(searchText))
                .Select(x => new PriceItemsSearchResponse {Name = x.Name, ItemId = x.Id.ToString()})
                .ToListAsync();
        }
        private  Task<List<PriceItemsSearchResponse>> SearchForClinicAsync(string searchText,long facilityId)
        {
            return  _organizationUnitRepository.GetAll()
                .Where(x=>x.Type == OrganizationUnitType.Clinic
                          && x.FacilityId == facilityId
                          && x.DisplayName.ToLower().Contains(searchText))
                .Select(x =>
                    new PriceItemsSearchResponse { Name = x.DisplayName, ItemId = x.ShortName })
                .ToListAsync();
        }
        private async Task<List<PriceItemsSearchResponse>> SearchProcedureFromSnomed(string searchText)
        {
            var query = await _snomedSuggestionRepository.GetAll()
                .Where(x => x.Type == AllInputType.Procedure && x.Name.ToLower().Contains(searchText))
                .Select(x => new PriceItemsSearchResponse { Name = x.Name, ItemId = x.SnowmedId })
                .ToListAsync().ConfigureAwait(false);
            if (query.Count == 0)
            {
                var (response, status) = await _snowstormBaseQuery.GetTermBySemanticTags(new SnowstormRequestDto
                {
                    Term = searchText,
                    SemanticTag = SemanticTag,
                    SemanticTag2 = SemanticTag2
                }).ConfigureAwait(false);
                if (status)
                {
                    return response?.Items.Select(x => new PriceItemsSearchResponse
                    {
                        Name = x.Term,
                        ItemId = x.Concept?.Id
                    }).ToList();
                }
            }
            return query;
        }

    }
}
