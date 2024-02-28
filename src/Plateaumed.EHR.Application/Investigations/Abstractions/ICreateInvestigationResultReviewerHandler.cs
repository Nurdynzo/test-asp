using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Abstractions
{
    public interface ICreateInvestigationResultReviewerHandler: ITransientDependency
    {
        Task Handle(InvestigationResultReviewerRequestDto request, long facilityId);
    }
}

