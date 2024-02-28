using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.PatientDocument.Abstraction;

/// <summary>
/// Interface ISendUploadedFilesToReviewerCommandHandler
/// </summary>
public interface ISendUploadedFilesToReviewerCommandHandler: ITransientDependency
{
    /// <summary>
    /// Handles the Assignment of Uploaded Files to Reviewer
    /// </summary>
    /// <param name="scanDocumentIds"></param>
    /// <param name="reviewerId"></param>
    Task Handle(List<long> scanDocumentIds, long reviewerId);
}