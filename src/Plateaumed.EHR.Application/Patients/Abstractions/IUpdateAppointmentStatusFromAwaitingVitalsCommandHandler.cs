using Abp.Dependency;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Patients.Abstractions
{
    public interface IUpdateAppointmentStatusFromAwaitingVitalsCommandHandler : ITransientDependency
    {
        Task<string> Handle(long encounterId);
    }
}
