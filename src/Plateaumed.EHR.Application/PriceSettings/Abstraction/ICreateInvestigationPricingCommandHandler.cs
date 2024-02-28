using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PriceSettings.Dto;

namespace Plateaumed.EHR.PriceSettings.Abstraction
{
    public interface ICreateInvestigationPricingCommandHandler : ITransientDependency
    {
        Task Handle(List<CreateInvestigationPricingDto> request);
    }
}

