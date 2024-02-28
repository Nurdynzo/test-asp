using Abp.Application.Services;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Medication.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Discharges
{
    public interface IDischargeAppService : IApplicationService
    {
        Task<NormalDischargeReturnDto> CreateOrEditNormalDischarge(CreateNormalDischargeDto input);
        Task<MarkAsDeceaseDischargeReturnDto> CreateOrEditMarkAsDeceaseDischarge(CreateMarkAsDeceasedDischargeDto input);
        Task<List<PatientMedicationForReturnDto>> GetDischargeMedications(int dischargeId);
        Task<List<DischargePlanItemDto>> GetDischargePlanItems(int dischargeId);
        Task<DischargeDto> GetDischargeById(int dischargeId);
        Task<List<DischargeDto>> GetPatientDischarge(int patientId);
    }
}