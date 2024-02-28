using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Abstraction;

public interface IGetPhysicalExaminationUploadedImagesQueryHandler : ITransientDependency
{
    Task<List<UploadedImageDto>> Handle(long patientPhysicalExaminationId);
}