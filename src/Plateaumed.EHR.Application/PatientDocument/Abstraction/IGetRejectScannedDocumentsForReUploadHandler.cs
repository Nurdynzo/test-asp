using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.PatientDocument.Abstraction;

public interface IGetRejectScannedDocumentsForReUploadHandler: ITransientDependency
{
    Task<List<GetScannedDocumentsForReviewResponse>> Handle();
}