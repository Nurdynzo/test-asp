using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.Admissions;

public interface ICompleteTransferPatientCommandHandler : ITransientDependency
{
    Task Handle(long encounterId);
}