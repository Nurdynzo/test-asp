using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations;

public interface IGetPhysicalExaminationListQueryHandler : ITransientDependency
{
    Task<List<GetPhysicalExaminationListResponse>> Handle(GetPhysicalExaminationListRequest request);
}