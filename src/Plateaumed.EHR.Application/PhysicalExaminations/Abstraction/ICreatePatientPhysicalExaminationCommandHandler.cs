using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Abstraction;

public interface ICreatePatientPhysicalExaminationCommandHandler : ITransientDependency
{
    Task<long> Handle(CreatePatientPhysicalExaminationDto requestDto);
}