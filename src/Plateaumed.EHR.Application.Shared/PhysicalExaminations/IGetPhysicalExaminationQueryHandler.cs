using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations;

public interface IGetPhysicalExaminationQueryHandler : ITransientDependency
{
    Task<GetPhysicalExaminationResponse> Handle(long id);
}