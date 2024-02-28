using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Discharges.Abstractions;

public interface IDischargeBaseQuery : ITransientDependency
{
    Task<List<DischargeDto>> GetPatientDischarges(long patientId);
    Task<DischargeDto> GetDischargeInformation(long dischargeId);
    IQueryable<PatientMedicationForReturnDto> GetDischargeMedications(long dischargeId);
    IQueryable<DischargePlanItemDto> GetDischargePlanItem(long dischargeId);
    Task<DischargeDto> GetPatientDischargeWithEncounterId(long patientId, long encounterId);
    NormalDischargeReturnDto MapNormalDischarge(DischargeDto discharge);
    MarkAsDeceaseDischargeReturnDto MapMarkAsDeceaseDischargeReturn(DischargeDto discharge);
}
