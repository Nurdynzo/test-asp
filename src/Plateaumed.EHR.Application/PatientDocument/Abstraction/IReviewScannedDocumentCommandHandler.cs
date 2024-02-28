
using Abp.Dependency;
using System.Threading.Tasks;
using Plateaumed.EHR.PatientDocument.Dto;

namespace Plateaumed.EHR.PatientDocument.Abstraction
{
    /// <summary>
    /// Handler for a reviewer to approve or reject assigned scanned documents
    /// </summary>
    public interface IReviewScannedDocumentCommandHandler : ITransientDependency
    {
        /// <summary>
        /// Handles the Assigning Uploaded Files to Reviewer
        /// </summary>
        /// <param name="request"/>
        Task Handle(ReviewedScannedDocumentRequest request);
    }
}
