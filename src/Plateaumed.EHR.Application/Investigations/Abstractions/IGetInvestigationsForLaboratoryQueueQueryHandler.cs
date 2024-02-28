using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Abstractions
{
    public interface IGetInvestigationsForLaboratoryQueueQueryHandler : ITransientDependency
    {
        Task<PagedResultDto<InvestigationsForLaboratoryQueueResponse>> Handle(GetInvestigationsForLaboratoryQueueRequest  request);
    }
}

