using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;

namespace Plateaumed.EHR.PriceSettings.Query
{
    public class GetInvestigationPricesQueryHandler : IGetInvestigationPricesQueryHandler
	{
		private readonly IRepository<InvestigationPricing, long> _investigationPricingRepository;
		public GetInvestigationPricesQueryHandler(IRepository<InvestigationPricing, long> investigationPricingRepository)
		{
			_investigationPricingRepository = investigationPricingRepository;
		}

		public async Task<PagedResultDto<GetInvestigationPricingResponseDto>> Handle(GetInvestigationPricingRequestDto request)
		{
            var nameFilter = !string.IsNullOrEmpty(request.TestName) ? request.TestName.ToLower() : "";
			var testTypeFilter = !string.IsNullOrWhiteSpace(request.InvestigationType) ? request.InvestigationType.ToLower() : "";

			var query = new List<InvestigationPricing>();

			if(request.InvestigationPricingId > 0)
			{
				query = await _investigationPricingRepository
								.GetAll()
                                .Include(x => x.Investigation)
                                .Where(x => x.Id.Equals(request.InvestigationPricingId))
								.ToListAsync();
			}
			else if (!string.IsNullOrWhiteSpace(nameFilter))
			{
				query = await _investigationPricingRepository
							.GetAll()
                            .Include(x => x.Investigation)
                            .Where(x => x.Investigation.Name.ToLower().Contains(nameFilter))
							.ToListAsync();
			}
			else if(!string.IsNullOrWhiteSpace(testTypeFilter) && testTypeFilter.Contains(InvestigationTypes.Others))
			{
				query = await _investigationPricingRepository
							.GetAll()
                            .Include(x => x.Investigation)
                            .Where(x =>
							!x.Investigation.Type.ToLower().Contains(InvestigationTypes.Chemistry) &&
							!x.Investigation.Type.ToLower().Contains(InvestigationTypes.Haematology) &&
                            !x.Investigation.Type.ToLower().Contains(InvestigationTypes.Electrophysiology) &&
                            !x.Investigation.Type.ToLower().Contains(InvestigationTypes.Microbiology) &&
                            !x.Investigation.Type.ToLower().Contains(InvestigationTypes.RadiologyAndPulm) &&
                            !x.Investigation.Type.ToLower().Contains(InvestigationTypes.Serology))
                            .ToListAsync();
			}
			else if(!string.IsNullOrWhiteSpace(testTypeFilter))
			{
				query = await _investigationPricingRepository
					.GetAll()
                    .Include(x => x.Investigation)
                    .Where(x => x.Investigation.Type.ToLower().Contains(testTypeFilter))
					.ToListAsync();
            }
			else
			{
				query = await _investigationPricingRepository
					
					.GetAll()
					.Include(x=>x.Investigation)
					.ToListAsync();
			}

			var list = query.Select(x => new GetInvestigationPricingResponseDto
            {
                Id = x.Id,
				InvestigationId  = x.InvestigationId,
                Amount = new Invoices.Dtos.MoneyDto { Amount = x.Amount.Amount, Currency = x.Amount.Currency },
                NameOfInvestigation = x.Investigation.Name,
                TypeOfInvestigation = x.Investigation.Type,
                IsActive = x.IsActive,
                Notes = x.Notes,
                DateCreated = x.CreationTime
            }).ToList();


            SortInvestigationPricing(request.SortBy, list);

            var count = list.Count;
			var items = list.Skip(request.SkipCount)
							.Take(request.MaxResultCount)
							.ToList();

            return new PagedResultDto<GetInvestigationPricingResponseDto>(count, items);
        }

		private static void SortInvestigationPricing(string sortBy, List<GetInvestigationPricingResponseDto> list)
		{
            if (sortBy == Convert.ToString(InvestigationPricingSortFields.TestNameAsc))
                _ = list.OrderBy(x => x.NameOfInvestigation).ToList();

            if (sortBy == Convert.ToString(InvestigationPricingSortFields.TestNameDesc))
                _ = list.OrderByDescending(x => x.NameOfInvestigation).ToList();

            if (sortBy == Convert.ToString(InvestigationPricingSortFields.PriceHighest))
                 _ = list.OrderByDescending(x => x.Amount).ToList();

            if (sortBy == Convert.ToString(InvestigationPricingSortFields.PriceLowest))
                _ = list.OrderBy(x => x.Amount).ToList();

            if (sortBy == Convert.ToString(InvestigationPricingSortFields.DateMostRecent))
                _ = list.OrderBy(x => x.DateCreated).ToList();
        }
    }
}

