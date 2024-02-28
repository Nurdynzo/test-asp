using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns;

public interface IGetPatientVitalsQueryHandler : ITransientDependency
{
    Task<List<PatientVitalResponseDto>> Handle(long patientId, long? procedureId = null);
}