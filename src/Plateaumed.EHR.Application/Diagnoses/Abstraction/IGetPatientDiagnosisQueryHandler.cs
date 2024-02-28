using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Diagnoses.Dto;

namespace Plateaumed.EHR.Diagnoses.Abstraction
{
    public interface IGetPatientDiagnosisQueryHandler : ITransientDependency
    {
        Task<List<PatientDiagnosisReturnDto>> Handle(int patientId);

    }
}
