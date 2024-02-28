using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.PriceSettings.Dto;
namespace Plateaumed.EHR.PriceSettings.Abstraction
{
    public interface IGetPriceListQueryHandler: ITransientDependency
    {
        Task<PagedResultDto<GetPriceListQueryResponse>> Handle(GetPriceListQueryRequest request);
    }
}
