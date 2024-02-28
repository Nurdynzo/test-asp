using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Symptom.Dtos;

namespace Plateaumed.EHR.Symptom.Abstractions;

public interface ICreateSymptomCommandHandler : ITransientDependency
{
    Task<AllInputs.Symptom> Handle(CreateSymptomDto symptom);
}