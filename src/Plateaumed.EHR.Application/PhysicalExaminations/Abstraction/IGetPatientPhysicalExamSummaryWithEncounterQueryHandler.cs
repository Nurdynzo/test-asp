using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Abstraction;

public interface IGetPatientPhysicalExamSummaryWithEncounterQueryHandler : ITransientDependency
{
    Task<List<PatientPhysicalExaminationResponseDto>> Handle(long patientId, long encounterId, int? tenantId);
}