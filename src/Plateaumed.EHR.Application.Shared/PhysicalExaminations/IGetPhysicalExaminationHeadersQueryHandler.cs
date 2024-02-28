using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations;

public interface IGetPhysicalExaminationHeadersQueryHandler : ITransientDependency
{
    Task<GetPhysicalExaminationHeadersResponse> Handle(GetPhysicalExaminationHeadersRequest request);
}