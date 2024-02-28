using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Misc.Dtos;

namespace Plateaumed.EHR.Investigations;

public interface IInvestigationAppService
{
    Task<GetInvestigationResponse> GetInvestigation(GetInvestigationRequest request);
    Task<List<GetAllInvestigationsResponse>> GetAll(GetAllInvestigationsRequest request);
    Task RequestInvestigation(List<RequestInvestigationRequest> request);
    Task<List<GetInvestigationRequestsResponse>> GetInvestigationsRequests(GetInvestigationRequestsRequest request);
    Task RecordInvestigation(RecordInvestigationRequest request);
    Task<List<GetInvestigationResultsResponse>> GetInvestigationResults(GetInvestigationResultsRequest request);
    Task LinkToDiagnosis(LinkInvestigationToDiagnosisRequest request);
    List<string> GetInvestigationTypes();
    List<IdentificationTypeDto> GetILaboratoryQueueInvestigationStatusList();
    Task<PagedResultDto<InvestigationsForLaboratoryQueueResponse>> GetLaboratoryQueueInvestigationResults(GetInvestigationsForLaboratoryQueueRequest request);
    Task<ViewTestInfoResponse> GetLabResultTestInfo(ViewTestInfoRequestCommand request);
    Task<ViewTestResultResponse> GetLabTestResult(ViewTestInfoRequestCommand request);
    Task RecordInvestigationSample(RecordInvestigationSampleRequest request);
    Task CreateOrUpdateInvestigationResultReviewer(InvestigationResultReviewerRequestDto request);
    Task<GetInvestigationSpecimensResponse> GetInvestigationSpecimens(GetInvestigationSpecimensRequest request);
    Task<List<RadiologyAndPulmonaryInvestigationDto>> GetRadiologyAndPulmonaryInvestigations(GetElectroRadPulmInvestigationResultDto request);
}