using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.PatientDocument.Abstraction;

/// <summary>
/// Get list of scanned documents for review
/// </summary>
public interface IGetScannedDocumentsForReviewQueryHandler: ITransientDependency
{
    /// <summary>
    /// Handler for GetScannedDocumentsForReviewQuery   
    /// </summary>
    /// <returns></returns>
    Task<List<GetScannedDocumentsForReviewResponse>> Handle(bool showOnlyRejectedDocuments = false);
}