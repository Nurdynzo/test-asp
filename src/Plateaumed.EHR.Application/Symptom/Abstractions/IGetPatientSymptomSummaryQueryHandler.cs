using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Symptom.Dtos;

namespace Plateaumed.EHR.Symptom.Abstractions;

public interface IGetPatientSymptomSummaryQueryHandler : ITransientDependency
{
    Task<List<PatientSymptomSummaryForReturnDto>> Handle(long patientId, int? tenantId, long? encounterId = null);
}
