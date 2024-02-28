using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using Plateaumed.EHR.Symptom.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction;

public interface IGetReviewAndSavePatientTypeNotesQueryHandler : ITransientDependency
{
    Task<List<PatientTypeNoteDto>> Handle(long doctorUserId, List<PatientSymptomSummaryForReturnDto> symptoms,
                                                            List<PatientPhysicalExaminationResponseDto> physicalExamination);
}