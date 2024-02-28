using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Abstraction;

public interface IGetPatientPhysicalExamImagesQueryHandler : ITransientDependency
{
    Task<List<PatientPhysicalExaminationImageFileResponseDto>> Handle(long patientPhysicalExaminationId);
}