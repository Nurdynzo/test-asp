using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Investigations.Abstractions;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Misc.Dtos;

namespace Plateaumed.EHR.Investigations
{
    [AbpAuthorize(AppPermissions.Pages_Investigations)]
    public class InvestigationAppService : EHRAppServiceBase, IInvestigationAppService
    {
        private readonly IGetInvestigationQueryHandler _getInvestigation;
        private readonly IGetAllInvestigationsQueryHandler _getAllInvestigations;
        private readonly IRequestInvestigationCommandHandler _requestInvestigation;
        private readonly IGetInvestigationRequestsQueryHandler _getInvestigationRequests;
        private readonly IRecordInvestigationCommandHandler _recordInvestigation;
        private readonly IGetInvestigationResultsQueryHandler _getInvestigationResults;
        private readonly IDeleteInvestigationRequestCommandHandler _deleteInvestigation;
        private readonly ILinkInvestigationToDiagnosisCommandHandler _linkToDiagnosis;
        private readonly IGetInvestigationsForLaboratoryQueueQueryHandler _laboratoryQueue;
        private readonly IGetInvestigationForPatientCommandHandler _getPatientInvestigation;
        private readonly IGetInvestigationPricesRequestCommandHandler _getInvestigationPrices;
        private readonly ILaboratoryQueueViewTestResultQueryHandler _getLaboratoryQueueTestResult;
        private readonly IGetLaboratoryQueueTestInfoQueryHandler  _laboratoryQueueViewTestInfo;
        private readonly IRecordInvestigationSampleCommandHandler _recordInvestigationSample;
        private readonly ICreateInvestigationResultReviewerHandler _createInvestigationReviewer;
        private readonly IGetInvestigationSpecimensQueryHandler _getInvestigationSpecimens;
        private readonly IRecordInvestigationResultElectroRadPulmCommandHandler _recordInvestigationERadPulm;
        private readonly IGetElectroRadPulmInvestigationResultQueryHandler _getElectroRadPulmInvestigationResult;
        private readonly IGetRadiologyAndPulmonaryInvestigationTypesHandler _getRadiologyPulmonaryInvestigationTypes;
        private readonly IGetElectroRadPulmRecentResultQueryHandler _getRecentResults;

        public InvestigationAppService(IGetInvestigationQueryHandler getInvestigation, 
            IGetAllInvestigationsQueryHandler getAllInvestigations, 
            IRequestInvestigationCommandHandler requestInvestigation,
            IGetInvestigationRequestsQueryHandler getInvestigationRequests,
            IRecordInvestigationCommandHandler recordInvestigation,
            IGetInvestigationResultsQueryHandler getInvestigationResults,
            IDeleteInvestigationRequestCommandHandler deleteInvestigation,
            ILinkInvestigationToDiagnosisCommandHandler linkToDiagnosis,
            IGetInvestigationsForLaboratoryQueueQueryHandler laboratoryQueue,
            IGetInvestigationForPatientCommandHandler getPatientInvestigation,
            IGetInvestigationPricesRequestCommandHandler getInvestigationPrices,
            ILaboratoryQueueViewTestResultQueryHandler getLaboratoryQueueTestResult,
            IGetLaboratoryQueueTestInfoQueryHandler  laboratoryQueueViewTestInfo,
            IRecordInvestigationSampleCommandHandler recordInvestigationSample,
            ICreateInvestigationResultReviewerHandler createInvestigationReviewer,
            IGetInvestigationSpecimensQueryHandler getInvestigationSpecimens,
            IRecordInvestigationResultElectroRadPulmCommandHandler recordInvestigationERadPulm,
            IGetElectroRadPulmInvestigationResultQueryHandler getElectroRadPulmInvestigationResult,
            IGetRadiologyAndPulmonaryInvestigationTypesHandler getRadiologyPulmonaryInvestigationTypes,
            IGetElectroRadPulmRecentResultQueryHandler getRecentResults)
        {
            _getInvestigation = getInvestigation;
            _getAllInvestigations = getAllInvestigations;
            _requestInvestigation = requestInvestigation;
            _getInvestigationRequests = getInvestigationRequests;
            _recordInvestigation = recordInvestigation;
            _getInvestigationResults = getInvestigationResults;
            _deleteInvestigation = deleteInvestigation;
            _linkToDiagnosis = linkToDiagnosis;
            _laboratoryQueue = laboratoryQueue;
            _getPatientInvestigation = getPatientInvestigation;
            _getInvestigationPrices = getInvestigationPrices;
            _laboratoryQueueViewTestInfo = laboratoryQueueViewTestInfo;
            _getLaboratoryQueueTestResult = getLaboratoryQueueTestResult;
            _recordInvestigationSample = recordInvestigationSample;
            _createInvestigationReviewer = createInvestigationReviewer;
            _getInvestigationSpecimens = getInvestigationSpecimens;
            _recordInvestigationERadPulm = recordInvestigationERadPulm;
            _getElectroRadPulmInvestigationResult = getElectroRadPulmInvestigationResult;
            _getRadiologyPulmonaryInvestigationTypes = getRadiologyPulmonaryInvestigationTypes;
            _getRecentResults = getRecentResults;
        }

        public async Task<List<GetAllInvestigationsResponse>> GetAll(GetAllInvestigationsRequest request)
        {
            return await _getAllInvestigations.Handle(request);
        }

        public async Task<GetInvestigationResponse> GetInvestigation(GetInvestigationRequest request)
        {
            return await _getInvestigation.Handle(request);
        }

        public async Task RequestInvestigation(List<RequestInvestigationRequest> request)
        {
            await _requestInvestigation.Handle(request);
        }

        public async Task<List<GetInvestigationRequestsResponse>> GetInvestigationsRequests(GetInvestigationRequestsRequest request)
        {
            return await _getInvestigationRequests.Handle(request);
        }

        public async Task RecordInvestigation(RecordInvestigationRequest request)
        {
            var facilityId = GetCurrentUserFacilityId();
            await _recordInvestigation.Handle(request, facilityId);
        }

        public async Task<List<GetInvestigationResultsResponse>> GetInvestigationResults(GetInvestigationResultsRequest request)
        {
            return await _getInvestigationResults.Handle(request);
        }

        public async Task DeleteInvestigationRequest(long requestId)
        {
            await _deleteInvestigation.Handle(requestId);
        }

        public async Task LinkToDiagnosis(LinkInvestigationToDiagnosisRequest request)
        {
            await _linkToDiagnosis.Handle(request);
        }

        public List<string> GetInvestigationTypes()
        {
            return new List<string>()
            {
                InvestigationTypes.Chemistry,
                InvestigationTypes.Electrophysiology,
                InvestigationTypes.Haematology,
                InvestigationTypes.Microbiology,
                InvestigationTypes.RadiologyAndPulm,
                InvestigationTypes.Serology,
                InvestigationTypes.Others
            };
        }

        public List<IdentificationTypeDto> GetILaboratoryQueueInvestigationStatusList() =>
                   Enum.GetValues<InvestigationStatus>()
                   .AsEnumerable()
                    .Select(x => new IdentificationTypeDto()
                    {
                        Value = x.ToString(),
                        Label = Regex.Replace(x.ToString(), "([a-z])([A-Z])", "$1 $2")
                    }).ToList();

        public async Task<PagedResultDto<InvestigationsForLaboratoryQueueResponse>> GetLaboratoryQueueInvestigationResults(GetInvestigationsForLaboratoryQueueRequest request)
            => await _laboratoryQueue.Handle(request);

        public async Task<Dictionary<long, GetInvestigationForPatientResponse>> InvestigationRequestForPatient(GetPatientInvestigationRequest request)
            => await _getPatientInvestigation.GetInvestigationForPatient(request);

        public async Task<GetInvestigationPricessResponse> LabQueueSummaryInvestigationPrices(GetInvestigationPricesRequest command)
            => await _getInvestigationPrices.GetInvestigationPrice(command);

        public async Task<ViewTestInfoResponse> GetLabResultTestInfo(ViewTestInfoRequestCommand request) =>
            await _laboratoryQueueViewTestInfo.Handle(request);

        public async Task<ViewTestResultResponse> GetLabTestResult(ViewTestInfoRequestCommand request) =>
            await _getLaboratoryQueueTestResult.Handle(request);

        public async Task RecordInvestigationSample(RecordInvestigationSampleRequest request) =>
            await _recordInvestigationSample.Handle(request);

        public async Task CreateOrUpdateInvestigationResultReviewer(InvestigationResultReviewerRequestDto request)
        {
            var facilityId = GetCurrentUserFacilityId();
            await _createInvestigationReviewer.Handle(request, facilityId);
        }

        public async Task<GetInvestigationSpecimensResponse> GetInvestigationSpecimens(GetInvestigationSpecimensRequest request) =>
            await _getInvestigationSpecimens.Handle(request);

        public async Task RecordInvestigationResultForElectroRadAndPulm([FromForm] ElectroRadPulmInvestigationResultRequestDto request) =>
            await _recordInvestigationERadPulm.Handle(request, GetCurrentUserFacilityId());

        public async Task<ElectroRadPulmInvestigationResultResponseDto> GetInvestigationResultForElectroRadAndPulm(long patientId, long investigationRequestId)
            => await _getElectroRadPulmInvestigationResult.Handle(patientId, investigationRequestId);

        public async Task<List<RadiologyAndPulmonaryInvestigationDto>> GetRadiologyAndPulmonaryInvestigations(GetElectroRadPulmInvestigationResultDto request)
            => await _getRadiologyPulmonaryInvestigationTypes.Handle(request);

        public async Task<List<ElectroRadPulmInvestigationResultResponseDto>> GetRadAndPulmRecentInvestigationResults(GetInvestigationResultWithNameTypeFilterDto request) =>
            await _getRecentResults.Handle(request);        
    }
}
