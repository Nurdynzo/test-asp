using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.PriceSettings.Command
{
    public class UpdateInvestigationPricingCommandHandler : IUpdateInvestigationPricingCommandHandler
    {
        private readonly IRepository<InvestigationPricing, long> _investigationPricingRepository;
        private readonly IAbpSession _abpSession;
        private readonly IRepository<Investigation, long> _investigation;

        public UpdateInvestigationPricingCommandHandler(IRepository<InvestigationPricing, long> investigationPricingRepository,
            IAbpSession abpSession, IRepository<Investigation, long> investigation)
        {
            _investigationPricingRepository = investigationPricingRepository;
            _abpSession = abpSession;
            _investigation = investigation;
        }
           
        
        public async Task Handle(UpdateInvestigationPricingRequestCommand request)
        {
            _ = _abpSession.TenantId ?? throw new UserFriendlyException("Tenant not found");

            ValidateRequest(request);
            var item = await _investigationPricingRepository.GetAsync(request.InvestigationPricingId)
                ?? throw new UserFriendlyException($"Investigation Pricing Item with id {request.InvestigationPricingId} not found");
            _ = await _investigation.GetAsync(request.InvestigationId)
                ?? throw new UserFriendlyException($"Investigation item with Id {request.InvestigationId} not found");

            item.Amount = request.Amount.ToMoney();
            item.Notes = request.Notes;
            item.IsActive = request.IsActive;

            await _investigationPricingRepository.UpdateAsync(item);
        }

        private static void ValidateRequest(UpdateInvestigationPricingRequestCommand request)
        {
            if (request.InvestigationId <= 0)
                throw new UserFriendlyException("Investigation not found");

            if (request.InvestigationPricingId <= 0)
                throw new UserFriendlyException("Investigation Pricing not found");
            
        }
    }
}

