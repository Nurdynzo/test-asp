using Abp.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.IntakeOutputs.Dtos;

namespace Plateaumed.EHR.IntakeOutputs
{
    public interface IIntakeOutputAppService : IApplicationService
    {
        Task<IntakeOutputReturnDto> CreateOrEditIntake(CreateIntakeOutputDto input);
        Task<IntakeOutputReturnDto> CreateOrEditOutput(CreateIntakeOutputDto input);
        Task<bool> DeleteIntakeOrOutput(long intakeOrOutputId);
        Task<PatientIntakeOutputDto> GetIntakeSuggestions(int patientId);
        Task<PatientIntakeOutputDto> GetOutputSuggestions(int patientId);
        Task<List<PatientIntakeOutputDto>> GetIntakeOutputSavedHistory(int patientId, long? procedureId = null);
    }
}
