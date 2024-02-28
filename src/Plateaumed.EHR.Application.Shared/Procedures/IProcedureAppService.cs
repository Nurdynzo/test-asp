using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.Snowstorm.Dtos;

namespace Plateaumed.EHR.Procedures;

public interface IProcedureAppService : IApplicationService
{
    Task<List<SnowstormSimpleResponseDto>> GetProcedureSuggestions();
    Task<GetPatientInterventionsResponseDto> GetPatientInterventions(long patientId);
    Task CreateSpecializedProcedureNurseDetail(CreateSpecializedProcedureNurseDetailCommand request);
    Task<SpecializedProcedureSafetyCheckListDto> GetCheckedSafetyList(long patientId, long procedureId);
    Task<SpecializedProcedureSafetyCheckListDto> GetSpecializedProcedureSafetyCheckList(long patientId, long procedureId);
    Task UpdateSpecializedProcedureCheckList(SpecializedProcedureSafetyCheckListDto request);
    Task DeleteSpecializedProcedureNurseDetail(long id);
    Task<GetSpecializedProcedureNurseDetailResponse> GetSpecializedProcedureNurseDetail(long procedureId);
    Task DeleteProcedure(long input);
}
