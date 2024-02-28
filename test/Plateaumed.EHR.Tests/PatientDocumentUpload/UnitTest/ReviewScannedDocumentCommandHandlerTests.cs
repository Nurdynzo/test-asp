using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.PatientDocument.Command;
using Plateaumed.EHR.Patients;
using Shouldly;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Plateaumed.EHR.PatientDocument.Dto;
using Plateaumed.EHR.Upload.Abstraction;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientDocumentUpload.UnitTest
{
    [Trait("Category", "Unit")]
    public class ReviewScannedDocumentCommandHandlerTests
    {
        private readonly IRepository<PatientScanDocument, long> _patientScanDocumentRepository = Substitute.For<IRepository<PatientScanDocument, long>>();
        private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
        private readonly IUploadService _uploadService = Substitute.For<IUploadService>();


        [Fact]
        public async Task Handle_GivenScannedDocumentDoesNotExist_ShouldThrowException()
        {

            //Arrange
            var request = GetReviewedScannedDocumentRequest(2, MockFile.CreateInstance("AAA-7273782-MM#08092023.PDF")); 
            MockDependencies(scannedDocumentId: 1);
            var handler = GetReviewScannedDocumentCommandHandlerInstance();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));
            message.Message.ShouldBe("Scanned document does not exist");
        }


        [Fact]
        public async Task Handle_GivenScannedDocumentExist_ShouldUpdateScannedDocument()
        {

            //Arrange
            var request = GetReviewedScannedDocumentRequest(formFile:MockFile.CreateInstance("AAA-7273782-MM#08092023.PDF"));
            MockDependencies(request.Id);
            var handler = GetReviewScannedDocumentCommandHandlerInstance();

            // Act
            await handler.Handle( request);

            // Assert
            await _unitOfWork.Current.Received(1).SaveChangesAsync();
            await _uploadService.Received(1).UpdateFile(Arg.Any<Guid>(), Arg.Any<Stream>());
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenFileIsEmpty()
        {
            // Arrange
            var request = GetReviewedScannedDocumentRequest(formFile: null);
           
            MockDependencies(request.Id);
            var handler = GetReviewScannedDocumentCommandHandlerInstance();
            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));
            message.Message.ShouldBe("File is required");
            
        }
        [Fact]
        public async Task Handle_ThrowsException_WhenFile_Extension_Is_Not_Valid()
        {
            // Arrange
            var request = GetReviewedScannedDocumentRequest(formFile: MockFile.CreateInstance("InvalidName.exe"));
            MockDependencies(request.Id);
            var handler = GetReviewScannedDocumentCommandHandlerInstance();
            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));
            message.Message.ShouldBe("Invalid file format. Only PDF files are allowed");
        }

        private static ReviewedScannedDocumentRequest GetReviewedScannedDocumentRequest(long id = 1, IFormFile formFile = null) {
            return new ReviewedScannedDocumentRequest() { 
                Id = id,
                IsApproved = false,
                ReviewNote = "This document belongs to Patient with PatientCode: AAA-90989283-MM",
                FileId = new Guid("{b8172f8e-3412-4626-b013-f5065b50b7f4}"),
                File = formFile
            };
        }
        
        private static PatientScanDocument GetPatientScannedDocument(Guid fileId = default) {
            return new PatientScanDocument { 
                Id = 1,
                TenantId = 1,
                IsApproved = null,
                PatientCode = "AAA-7273782-MM",
                FileId = fileId,
                FileName = "AAA-72737872-MM#08092023",
                ReviewerId = 1,
                AssigneeId = 1,
                ReviewNote = null,
            };
        }

        private void MockDependencies(long scannedDocumentId)
        {
            var scannedDocument = scannedDocumentId > 1 ? null : GetPatientScannedDocument(new Guid("{b8172f8e-3412-4626-b013-f5065b50b7f4}"));
            _patientScanDocumentRepository.FirstOrDefaultAsync(scannedDocumentId).Returns(scannedDocument);
            _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        }


        private ReviewScannedDocumentCommandHandler GetReviewScannedDocumentCommandHandlerInstance()
        {
            var handler = new ReviewScannedDocumentCommandHandler(_patientScanDocumentRepository, _unitOfWork, _uploadService);
            return handler;
        }

    }
}
