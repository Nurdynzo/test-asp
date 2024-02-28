using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PriceSettings.Dto;
namespace Plateaumed.EHR.PriceSettings.Abstraction
{
    public interface IGetUnifyPriceItemSearchQueryHandler : ITransientDependency
    {
        Task<List<PriceItemsSearchResponse>> Handle(PriceItemsSearchRequest request);
    }
}
