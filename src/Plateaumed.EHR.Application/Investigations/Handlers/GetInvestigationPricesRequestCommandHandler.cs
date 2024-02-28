using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Invoices.Dtos;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class GetInvestigationPricesRequestCommandHandler : IGetInvestigationPricesRequestCommandHandler
    {
        private readonly IRepository<InvestigationPricing, long> _investigationPricing;
        public GetInvestigationPricesRequestCommandHandler(IRepository<InvestigationPricing, long> investigationPricing)
        {
            _investigationPricing = investigationPricing;
        }
       
        public async Task<GetInvestigationPricessResponse> GetInvestigationPrice(GetInvestigationPricesRequest command)
        {
            ValidateInputs(command);
            return await GetPrices(command);
        }

        private async Task<GetInvestigationPricessResponse> GetPrices(GetInvestigationPricesRequest command)
        {
            var response = new Dictionary<long, MoneyDto>();

            foreach (var item in command.InvestigationIds)
            {
                var price = await GetPrice(item);
                if (price is not null)
                {
                    if (!response.ContainsKey(item))
                        response.Add(item, price);
                }
            }
            return new GetInvestigationPricessResponse { InvestigationsAndPrices = response };
        }

        private static void ValidateInputs(GetInvestigationPricesRequest request)
        {
            if(request.InvestigationIds.Count == 0)
                throw new UserFriendlyException("Investigations not found");
        }

        private async Task<MoneyDto> GetPrice(long id)
        {
            var request = await _investigationPricing
                            .GetAll()
                            .Where(x => x.InvestigationId == id)
                            .FirstOrDefaultAsync();

            if (request is not null)
                return new MoneyDto
                {
                    Amount = request.Amount.Amount,
                    Currency = request.Amount.Currency
                };

            return null;
        }
	}
}