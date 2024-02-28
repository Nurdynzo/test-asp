using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Investigations.Abstractions;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Upload.Abstraction;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class RecordInvestigationResultElectroRadPulmCommandHandler : IRecordInvestigationResultElectroRadPulmCommandHandler
    {
        private readonly IRepository<ElectroRadPulmInvestigationResult, long> _repository;
        private readonly IRepository<InvestigationRequest, long> _investigationRequestRepository;
        private readonly IRepository<Investigation, long> _investigationRepository;
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IEncounterManager _encounterManager;
        private readonly IAbpSession _abpSession;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<StaffMember, long> _staffMember;
        private readonly IRepository<InvestigationResultReviewer, long> _reviewer;
        private readonly IRepository<ElectroRadPulmInvestigationResultImages, long> _resultImages;
        private readonly IUploadService _uploadService;

        public RecordInvestigationResultElectroRadPulmCommandHandler(IRepository<ElectroRadPulmInvestigationResult, long> repository,
            IRepository<InvestigationRequest, long> investigationRequestRepository,
            IRepository<Patient, long> patientRepository, IRepository<Investigation, long> investigationRepository,
            IEncounterManager encounterManager,       
            IAbpSession abpSession,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<StaffMember, long> staffMember,
            IRepository<InvestigationResultReviewer, long> reviewer,
            IRepository<ElectroRadPulmInvestigationResultImages, long> resultImages,
            IUploadService uploadService)
        {
            _repository = repository;    
            _investigationRequestRepository = investigationRequestRepository;
            _patientRepository = patientRepository;
            _encounterManager = encounterManager;
            _investigationRepository = investigationRepository;           
            _abpSession = abpSession;
            _unitOfWorkManager = unitOfWorkManager;
            _staffMember = staffMember;
            _reviewer = reviewer;
            _resultImages = resultImages;
            _uploadService = uploadService;
        }

        public async Task Handle(ElectroRadPulmInvestigationResultRequestDto request, long facilityId, bool skipUploadService = false)
        {
            var investigationRequest = await ValidateInputsAndReturnInvestigationRequest(request);

            var tenantId = _abpSession.TenantId ?? throw new UserFriendlyException("Tenant not found");           

            var savedResult = await _repository.InsertAsync(new ElectroRadPulmInvestigationResult
            {
                EncounterId = request.EncounterId,
                TenantId = tenantId,
                Conclusion = request.Conclusion,
                InvestigationId = request.InvestigationId,
                InvestigationRequestId = request.InvestigationRequestId,
                PatientId = request.PatientId,
                ResultDate = request.ResultDate,
                ResultTime = request.ResultTime,
                ProcedureId = request.ProcedureId
            });

            await _unitOfWorkManager.Current.SaveChangesAsync();

            await UploadAndSaveimages(request, savedResult, skipUploadService);

            if (request.ReviewerId.HasValue)
            {
                await InsertInvestigationReviewer(new InvestigationResultReviewer
                {
                    TenantId = tenantId,
                    ReviewerId = request.ReviewerId,
                    CreatorUserId = _abpSession.UserId,
                    FacilityId = facilityId,
                    ElectroRadPulmInvestigationResultId = savedResult.Id
                });
            }           

            await UpdateInvestigationRequest(investigationRequest);
        }

        private async Task UploadAndSaveimages(ElectroRadPulmInvestigationResultRequestDto request, ElectroRadPulmInvestigationResult savedResult, bool skipUploadService)
        {
            if (request.ImageFiles?.Count > 0)
            {
                foreach (var imageFileRequest in request.ImageFiles)
                {                  
                    var imageFile = new ElectroRadPulmInvestigationResultImages
                    {
                        ElectroRadPulmInvestigationResultId = savedResult.Id,
                        TenantId = savedResult.TenantId,
                        FileId = Guid.NewGuid().ToString(),
                        FileName = imageFileRequest.FileName
                    };

                    if (!skipUploadService)
                        imageFile.ImageUrl = await _uploadService.UploadPublicAccessFileAsync(imageFile.FileId, imageFileRequest.OpenReadStream());

                    await SaveResultImages(imageFile);                   
                }
            }
        }
        private async Task SaveResultImages(ElectroRadPulmInvestigationResultImages model) => await _resultImages.InsertAsync(model);

        private async Task InsertInvestigationReviewer(InvestigationResultReviewer request) => await _reviewer.InsertAsync(request);

        private async Task<InvestigationRequest> ValidateInputsAndReturnInvestigationRequest(ElectroRadPulmInvestigationResultRequestDto request)
        {
            var investigationRequest = await _investigationRequestRepository.GetAsync(request.InvestigationRequestId) ??
                throw new UserFriendlyException("Investigation request not found");

            _ = await _patientRepository.GetAsync(request.PatientId) ??
                throw new UserFriendlyException("Patient not found");

            _ = await _investigationRepository.GetAsync(request.InvestigationId) ??
                throw new UserFriendlyException("Investigation not found");

            if (request.ReviewerId.HasValue)
                _ = await _staffMember.GetAsync(request.ReviewerId.Value) ?? throw new UserFriendlyException("Staff Member not found");

            if (request.EncounterId.HasValue)
                await _encounterManager.CheckEncounterExists(request.EncounterId.Value);

            return investigationRequest;
        }       

        private async Task UpdateInvestigationRequest(InvestigationRequest investigationRequest)
        {
            investigationRequest.InvestigationStatus = Misc.InvestigationStatus.ImageReady;
            await _investigationRequestRepository.UpdateAsync(investigationRequest);
        }
    }
}

