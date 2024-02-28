using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Dtos;

namespace Plateaumed.EHR.IntakeOutputs
{
    [AbpAuthorize(AppPermissions.Pages_IntakeOutput)]
    public class IntakeOutputAppService : EHRAppServiceBase, IIntakeOutputAppService
    {
        private readonly ICreateIntakeOutputCommandHandler _createIntakeOutputCommandHandler;
        private readonly IEditIntakeOutputCommandHandler _editIntakeOutputCommandHandler;
        private readonly IDeleteIntakeOutputCommandHandler _deleteIntakeOutputCommandHandler;
        private readonly IGetIntakeQueryHandler _getIntakeQueryHandler;
        private readonly IGetOutputQueryHandler _getOutputQueryHandler;
        private readonly IGetIntakeOutputSavedHistoryQueryHandler _getHistoryQueryHandler;
        
        public IntakeOutputAppService(ICreateIntakeOutputCommandHandler createIntakeOutputCommandHandler,
            IEditIntakeOutputCommandHandler editIntakeOutputCommandHandler,
            IDeleteIntakeOutputCommandHandler deleteIntakeOutputCommandHandler,
            IGetIntakeQueryHandler getIntakeQueryHandler,
            IGetOutputQueryHandler getOutputQueryHandler,
            IGetIntakeOutputSavedHistoryQueryHandler getHistoryQueryHandler)
        {
            _createIntakeOutputCommandHandler = createIntakeOutputCommandHandler;
            _editIntakeOutputCommandHandler = editIntakeOutputCommandHandler;
            _deleteIntakeOutputCommandHandler = deleteIntakeOutputCommandHandler;
            _getIntakeQueryHandler = getIntakeQueryHandler;
            _getOutputQueryHandler = getOutputQueryHandler;
            _getHistoryQueryHandler = getHistoryQueryHandler;
        }

        [AbpAuthorize(AppPermissions.Pages_IntakeOutput_Create)]
        public async Task<IntakeOutputReturnDto> CreateOrEditIntake(CreateIntakeOutputDto input)
        {
            input.Type = ChartingType.INTAKE;
            return input.Id > 0
                ? await _editIntakeOutputCommandHandler.Handle(input)
                : await _createIntakeOutputCommandHandler.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_IntakeOutput_Create)]
        public async Task<IntakeOutputReturnDto> CreateOrEditOutput(CreateIntakeOutputDto input)
        {
            input.Type = ChartingType.OUTPUT;
            return input.Id > 0
                ? await _editIntakeOutputCommandHandler.Handle(input)
                : await _createIntakeOutputCommandHandler.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_IntakeOutput_Delete)]
        public async Task<bool> DeleteIntakeOrOutput(long intakeOrOutputId)
        {
            return await _deleteIntakeOutputCommandHandler.Handle(intakeOrOutputId);
        }

        public async Task<PatientIntakeOutputDto> GetIntakeSuggestions(int patientId)
        {
            return await _getIntakeQueryHandler.Handle(patientId);
        }

        public async Task<PatientIntakeOutputDto> GetOutputSuggestions(int patientId)
        {
            return await _getOutputQueryHandler.Handle(patientId);
        }

        public async Task<List<PatientIntakeOutputDto>> GetIntakeOutputSavedHistory(int patientId, long? procedureId = null)
        {
            return await _getHistoryQueryHandler.Handle(patientId, procedureId);
        }
    }
}