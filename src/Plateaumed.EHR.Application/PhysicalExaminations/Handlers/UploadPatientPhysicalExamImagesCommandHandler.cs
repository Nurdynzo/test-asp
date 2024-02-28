using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.Upload.Abstraction;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class UploadPatientPhysicalExamImagesCommandHandler : IUploadPatientPhysicalExamImagesCommandHandler
{
    private readonly IRepository<PatientPhysicalExaminationImageFile, long> _patientPhysicalExamImageFileRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IUploadService _uploadService;
    
    public UploadPatientPhysicalExamImagesCommandHandler(IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper, IUploadService uploadService, IRepository<PatientPhysicalExaminationImageFile, long> patientPhysicalExamImageFileRepository)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _uploadService = uploadService;
        _patientPhysicalExamImageFileRepository = patientPhysicalExamImageFileRepository;
    }

    public async Task Handle(UploadPatientPhysicalExamImageDto requestDto, bool skipUploadService = false)
    {
        // check and validate any uploaded images
        CheckAndValidatePatientPhysicalExamImageFileFormat(requestDto);
        
        // check if image files were added
        if (requestDto.ImageFiles?.Count > 0)
        {
            foreach (var imageFileRequest in requestDto.ImageFiles)
            {
                // initiate
                var imageFile = new PatientPhysicalExaminationImageFile();
                imageFile.PatientPhysicalExaminationId = requestDto.PatientPhysicalExaminationId;
                imageFile.FileId = Guid.NewGuid().ToString();
                imageFile.FileName = imageFileRequest.FileName;
                
                // upload file to server
                if (!skipUploadService)
                    imageFile.FileUrl =
                        await _uploadService.UploadPublicAccessFileAsync(imageFile.FileId,
                            imageFileRequest.OpenReadStream());
                
                // add each item
                await _patientPhysicalExamImageFileRepository.InsertAsync(imageFile);
            }
            
            // save changes
            await _unitOfWorkManager.Current.SaveChangesAsync(); 
        }
    }
    
    private void CheckAndValidatePatientPhysicalExamImageFileFormat(UploadPatientPhysicalExamImageDto requestDto)
    {
        // check if image files were added
        if (requestDto.ImageFiles?.Count > 0)
            foreach (var imageFileRequest in requestDto.ImageFiles)
                if (!imageFileRequest.ContentType.Contains("image"))
                    throw new UserFriendlyException("Only image files are allowed");
    } 
}