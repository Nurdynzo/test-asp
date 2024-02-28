using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Abstraction;

public interface IUploadPatientPhysicalExamImagesCommandHandler : ITransientDependency
{
    Task Handle(UploadPatientPhysicalExamImageDto requestDto, bool skipUploadService = false);
}