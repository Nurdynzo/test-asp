using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Abstractions;

public interface IUpdateJobCommandHandler : ITransientDependency
{
    Task Handle(CreateOrEditJobRequest request);
}