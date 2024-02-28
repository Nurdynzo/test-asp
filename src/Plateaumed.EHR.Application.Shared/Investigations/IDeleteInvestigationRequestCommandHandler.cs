using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.Investigations;

public interface IDeleteInvestigationRequestCommandHandler : ITransientDependency
{
    Task Handle(long requestId);
}