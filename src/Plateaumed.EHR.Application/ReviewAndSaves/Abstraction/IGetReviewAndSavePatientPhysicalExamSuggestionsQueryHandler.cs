using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction;

public interface IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler : ITransientDependency
{
    Task<List<PatientPhysicalExaminationDto>> Handle(List<PatientPhysicalExaminationResponseDto> physicalExamination);
}
