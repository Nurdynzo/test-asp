using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Diagnoses.Dto;

namespace Plateaumed.EHR.Diagnoses.Abstraction
{
    public interface IGetPatientDiagnosisWithEncounterQueryHandler : ITransientDependency
    {
        Task<List<DiagnosisDto>> Handle(long PatientId, long encounterId);

    }
}