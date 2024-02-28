using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations
{
    public interface IGetInvestigationPricesRequestCommandHandler: ITransientDependency
    {
        Task<GetInvestigationPricessResponse> GetInvestigationPrice(GetInvestigationPricesRequest command);
    }
}

