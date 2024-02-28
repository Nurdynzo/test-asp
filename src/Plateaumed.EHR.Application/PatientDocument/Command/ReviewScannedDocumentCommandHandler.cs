using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.PatientDocument.Abstraction;
using Plateaumed.EHR.Patients;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Plateaumed.EHR.PatientDocument.Dto;
using Plateaumed.EHR.Upload.Abstraction;

namespace Plateaumed.EHR.PatientDocument.Command
{
 
    
    public class ReviewScannedDocumentCommandHandler : IReviewScannedDocumentCommandHandler
    {
        private readonly IRepository<PatientScanDocument, long> _patientScanDocumentRepository;
        private readonly IUnitOfWorkManager _unitOfWork;
        private readonly IUploadService _uploadService;
        private const string AcceptedFileExtensions = ".pdf";

        /// <param name="patientScanDocumentRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="uploadService"></param>
        public ReviewScannedDocumentCommandHandler(IRepository<PatientScanDocument, long> patientScanDocumentRepository,
        IUnitOfWorkManager unitOfWork,
        IUploadService uploadService)
        {
            _patientScanDocumentRepository = patientScanDocumentRepository;
            _unitOfWork = unitOfWork;
            _uploadService = uploadService;
        }

        /// <inheritdoc/>
        public async Task Handle(ReviewedScannedDocumentRequest request)
        {
            ValidateFile(request.File);
            var scannedDocument = await GetExistingScannedDocumentForUpdate(request);
                      
            scannedDocument.IsApproved = request.IsApproved;

            scannedDocument.ReviewNote = request.ReviewNote;

            await _patientScanDocumentRepository.UpdateAsync(scannedDocument);

            await _unitOfWork.Current.SaveChangesAsync();
            
            await _uploadService.UpdateFile(request.FileId, request.File.OpenReadStream());
        }

        private void ValidateFile(IFormFile file)
        {
            if (file == null)
            {
                throw new UserFriendlyException("File is required");
            }
            if (!file.FileName.ToLower().EndsWith(AcceptedFileExtensions))
            {
                throw new UserFriendlyException("Invalid file format. Only PDF files are allowed");
            }
        }

        private async Task<PatientScanDocument> GetExistingScannedDocumentForUpdate(ReviewedScannedDocumentRequest request)
        {
            var scannedDocument = await _patientScanDocumentRepository.FirstOrDefaultAsync(request.Id)
                                  ?? throw new UserFriendlyException("Scanned document does not exist");
            if (scannedDocument.FileId != request.FileId)
            {
                throw new UserFriendlyException("The file Id in the request does not match the file Id in the scanned document");
            }
            return scannedDocument;
        }
    }
}
