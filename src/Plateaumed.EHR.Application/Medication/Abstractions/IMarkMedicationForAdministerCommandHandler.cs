using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
namespace Plateaumed.EHR.Medication.Abstractions
{
    public interface IMarkMedicationForAdministerCommandHandler: ITransientDependency
    {
        Task Handle(List<long> medicationId);
    }
}
