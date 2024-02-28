using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Discharges.Abstractions;

public interface ICreateDischargedMedicationsHandler : ITransientDependency
{
    Task<List<PatientMedicationForReturnDto>> Handle(List<CreateDischargeMedicationDto> requestDto, long dischargeId, long patientId);
}