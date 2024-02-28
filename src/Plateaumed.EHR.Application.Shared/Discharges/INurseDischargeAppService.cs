using Abp.Application.Services;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Medication.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Discharges
{
    public interface INurseDischargeAppService : IApplicationService
    {
        Task<NormalDischargeReturnDto> FinalizeNormalDischarge(FinalizeNormalDischargeDto input);
        Task<MarkAsDeceaseDischargeReturnDto> FinalizeMarkAsDeceased(FinalizeMarkAsDeceaseDischargeDto input);
        Task<DischargeDto> GetPatientDischargeWithEncounterId(long encounterId);
        Task<DischargeDto> GetPatientDischarge(long dischargeId);
    }
}
