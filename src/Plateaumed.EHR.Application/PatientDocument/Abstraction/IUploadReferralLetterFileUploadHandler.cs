using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientDocument.Dto;

namespace Plateaumed.EHR.PatientDocument.Abstraction;

/// <inheritdoc />
public interface IUploadReferralLetterFileUploadHandler: ITransientDependency
{
    /// <summary>
    /// Handles the UploadReferralLetterFile
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ReferralLetterUploadRequest> Handle(ReferralLetterUploadRequest request);
}