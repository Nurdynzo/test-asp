using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.IntakeOutputs.Dtos;

namespace Plateaumed.EHR.IntakeOutputs.Abstractions;

public interface IBaseQuery : ITransientDependency
{
    Task<PatientIntakeOutputDto> GetIntakesSuggestions(long patientId);
    Task<List<PatientIntakeOutputDto>> GetPatientIntakeOutputHistory(long patientId, long? procedureId = null);
    Task<PatientIntakeOutputDto> GetOutputSuggestions(long patientId);
    Task<IntakeOutputReturnDto> GetIntakeOutputById(long id);
    Task<List<IntakeOutputReturnDto>> GetIntakeOutputByText(long patientId, string text);
}
