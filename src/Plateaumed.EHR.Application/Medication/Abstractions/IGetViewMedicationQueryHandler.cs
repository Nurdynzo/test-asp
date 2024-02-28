using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Medication.Dtos;
namespace Plateaumed.EHR.Medication.Abstractions
{
    public interface IGetViewMedicationQueryHandler: ITransientDependency
    {
        Task<List<MedicalViewResponse>> Handle(long patientId);
    }
}
