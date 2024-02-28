using System.Collections.Generic;
using Abp.Dependency;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Symptom.Dtos;

namespace Plateaumed.EHR.ReferAndConsults.Abstraction;

public interface IReferAndConsultBasedQuery : ITransientDependency
{
    List<string> GenerateSummary(Patient patient, List<PatientSymptomSummaryForReturnDto> symptoms, long userId);
}
