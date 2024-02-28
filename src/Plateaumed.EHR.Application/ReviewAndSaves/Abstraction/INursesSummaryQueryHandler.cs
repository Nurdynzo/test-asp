using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction;

public interface INursesSummaryQueryHandler : ITransientDependency
{
    Task<NurseSummaryDto> Handle(long encounterId);
}
