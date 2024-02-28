using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.PatientDocument.Abstraction;

public interface IGetListOfReviewerForScannedDocumentQueryHandler: ITransientDependency
{
    Task<List<ScannedDocumentReviewerQueryResponse>> Handle();
}