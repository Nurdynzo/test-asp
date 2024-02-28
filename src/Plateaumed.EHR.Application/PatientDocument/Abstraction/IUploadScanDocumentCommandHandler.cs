using System.Threading.Tasks;
using Abp.Dependency;
using Abp.UI;
using Plateaumed.EHR.PatientDocument.Dto;

namespace Plateaumed.EHR.PatientDocument.Abstraction;

/// <summary>
/// Interface for the UploadScanDocumentCommandHandler
/// </summary>
public interface IUploadScanDocumentCommandHandler : ITransientDependency
{
    /// <summary>
    /// Handles the UploadScanDocument
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    Task<PatientScanDocumentUploadRequest> Handle(PatientScanDocumentUploadRequest request);
}