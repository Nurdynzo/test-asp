using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations
{
    public class PhysicalExaminationsAppService : EHRAppServiceBase, IPhysicalExaminationsAppService
    {
        private readonly IGetPhysicalExaminationHeadersQueryHandler _getPhysicalExaminationHeaders;
        private readonly IGetPhysicalExaminationQueryHandler _getPhysicalExamination;
        private readonly IGetPhysicalExaminationListQueryHandler _getPhysicalExaminationList;
        private readonly IGetPhysicalExaminationTypesQueryHandler _getPhysicalExaminationTypes;
        private readonly ICreatePatientPhysicalExaminationCommandHandler _createPatientPhysicalExamination;
        private readonly IDeletePatientPhysicalExaminationCommandHandler _deletePatientPhysicalExamination;
        private readonly IDeletePatientPhysicalExamImageQueryHandler _deletePatientPhysicalExamImage;
        private readonly IGetPatientPhysicalExaminationSummaryQueryHandler _getPatientPhysicalExaminationSummary;
        private readonly IGetPatientPhysicalExamImagesQueryHandler _getPatientPhysicalExamImagesQueryHandler;
        private readonly IUploadPatientPhysicalExamImagesCommandHandler _uploadPatientPhysicalExamImagesCommandHandler;

        public PhysicalExaminationsAppService(IGetPhysicalExaminationHeadersQueryHandler getPhysicalExaminationHeaders,
            IGetPhysicalExaminationQueryHandler getPhysicalExamination,
            IGetPhysicalExaminationListQueryHandler getPhysicalExaminationList, IGetPhysicalExaminationTypesQueryHandler getPhysicalExaminationTypes, ICreatePatientPhysicalExaminationCommandHandler createPatientPhysicalExamination, IDeletePatientPhysicalExaminationCommandHandler deletePatientPhysicalExamination, IDeletePatientPhysicalExamImageQueryHandler deletePatientPhysicalExamImage, IGetPatientPhysicalExaminationSummaryQueryHandler getPatientPhysicalExaminationSummary, IGetPatientPhysicalExamImagesQueryHandler getPatientPhysicalExamImagesQueryHandler, IUploadPatientPhysicalExamImagesCommandHandler uploadPatientPhysicalExamImagesCommandHandler)
        {
            _getPhysicalExaminationHeaders = getPhysicalExaminationHeaders;
            _getPhysicalExamination = getPhysicalExamination;
            _getPhysicalExaminationList = getPhysicalExaminationList;
            _getPhysicalExaminationTypes = getPhysicalExaminationTypes;
            _createPatientPhysicalExamination = createPatientPhysicalExamination;
            _deletePatientPhysicalExamination = deletePatientPhysicalExamination;
            _deletePatientPhysicalExamImage = deletePatientPhysicalExamImage;
            _getPatientPhysicalExaminationSummary = getPatientPhysicalExaminationSummary;
            _getPatientPhysicalExamImagesQueryHandler = getPatientPhysicalExamImagesQueryHandler;
            _uploadPatientPhysicalExamImagesCommandHandler = uploadPatientPhysicalExamImagesCommandHandler;
        }

        public async Task<long> CreatePatientPhysicalExamination(CreatePatientPhysicalExaminationDto input) =>
            await _createPatientPhysicalExamination.Handle(input);

        public async Task UploadPatientPhysicalExamImages([FromForm] UploadPatientPhysicalExamImageDto input) =>
            await _uploadPatientPhysicalExamImagesCommandHandler.Handle(input);

        public async Task<List<PatientPhysicalExaminationResponseDto>> GetPatientPhysicalExaminationSummary([Required] long patientId, long? procedureId = null) 
            => await _getPatientPhysicalExaminationSummary.Handle(patientId, procedureId);
        
        public async Task DeletePatientPhysicalExamination([Required] long patientPhysicalExaminationId) =>
            await _deletePatientPhysicalExamination.Handle(patientPhysicalExaminationId);

        public async Task<List<PatientPhysicalExaminationImageFileResponseDto>>
            GetPatientPhysicalExaminationUploadedImages([Required] long patientPhysicalExaminationId)
        {
            return await _getPatientPhysicalExamImagesQueryHandler.Handle(patientPhysicalExaminationId);
        }
        
        public async Task DeletePatientPhysicalExaminationImage([Required] long patientPhysicalExaminationImageFileId) =>
            await _deletePatientPhysicalExamImage.Handle(patientPhysicalExaminationImageFileId);
        
        public async Task<List<GetPhysicalExaminationTypeResponseDto>> GetPhysicalExaminationTypes() =>
            await _getPhysicalExaminationTypes.Handle();

        public async Task<GetPhysicalExaminationHeadersResponse> GetHeaders(GetPhysicalExaminationHeadersRequest request) =>
            await _getPhysicalExaminationHeaders.Handle(request);

        public async Task<List<GetPhysicalExaminationListResponse>> GetList(GetPhysicalExaminationListRequest request)
        {
            return await _getPhysicalExaminationList.Handle(request);
        }

        public async Task<GetPhysicalExaminationResponse> Get([Required] long id)
        {
            return await _getPhysicalExamination.Handle(id);
        }
    }
}