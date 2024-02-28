using Abp.Dependency;
using Plateaumed.EHR.Diagnoses.Dto;

namespace Plateaumed.EHR.Diagnoses.Abstraction
{
    public interface IFormatDiagnosisHandler : ITransientDependency
    {
        string FormatDiagnoses(CreateDiagnosisDto createDiagnosis);
    }
}
