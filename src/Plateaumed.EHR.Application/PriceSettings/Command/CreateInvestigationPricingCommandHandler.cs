using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Investigations.Command
{
    public class CreateInvestigationPricingCommandHandler : ICreateInvestigationPricingCommandHandler
    {
        private readonly IRepository<InvestigationPricing, long> _pricing;
        private readonly IAbpSession _abpSession;
        private readonly IObjectMapper _mapper;

        public CreateInvestigationPricingCommandHandler(IRepository<InvestigationPricing,
            long> pricing, IAbpSession abpSession, IObjectMapper mapper)
        {  
            _pricing = pricing;
            _abpSession = abpSession;
            _mapper = mapper;
        }

        public async Task Handle(List<CreateInvestigationPricingDto> request)
        {
            var tenantId = _abpSession.TenantId ?? throw new UserFriendlyException("Tenant not found");

            var investigationPricingItem = _mapper.Map<List<InvestigationPricing>>(request);

            investigationPricingItem.ForEach(x =>
            {
                x.TenantId = tenantId;
                ValidateRequest(x);
            });
            await _pricing.InsertRangeAsync(investigationPricingItem);
        }

        private static void ValidateRequest(InvestigationPricing request)
        {
            if (request.InvestigationId <= 0) throw new UserFriendlyException("Investigation not found");
        }
    }
}

