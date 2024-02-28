using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PriceSettings.Dto;

namespace Plateaumed.EHR.PriceSettings.Abstraction
{
    public interface IUpdateInvestigationPricingCommandHandler : ITransientDependency
    {   
        Task Handle(UpdateInvestigationPricingRequestCommand request);
    }
}

