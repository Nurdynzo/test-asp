using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations;

public interface IGetPhysicalExaminationTypesQueryHandler : ITransientDependency
{
    Task<List<GetPhysicalExaminationTypeResponseDto>> Handle();
}