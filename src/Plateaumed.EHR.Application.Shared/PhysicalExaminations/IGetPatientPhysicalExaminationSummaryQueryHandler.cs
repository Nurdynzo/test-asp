using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations;

public interface IGetPatientPhysicalExaminationSummaryQueryHandler : ITransientDependency
{
    Task<List<PatientPhysicalExaminationResponseDto>> Handle(long patientId, long? procedureId = null);
}