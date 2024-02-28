using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.IntakeOutputs.Abstractions;

public interface IDeleteIntakeOutputCommandHandler : ITransientDependency
{
    Task<bool> Handle(long id);
}
