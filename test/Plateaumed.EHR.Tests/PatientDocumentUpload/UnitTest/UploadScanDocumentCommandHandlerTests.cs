using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.PatientDocument.Command;
using Plateaumed.EHR.PatientDocument.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Upload.Abstraction;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientDocumentUpload.UnitTest;

[Trait("Category", "Unit")]
public class UploadScanDocumentCommandHandlerTests
{
    private readonly IUploadService _uploadService = Substitute.For<IUploadService>();
    private readonly IRepository<PatientScanDocument, long> _repository = Substitute.For<IRepository<PatientScanDocument, long>>();
    private readonly IAbpSession _abpSession = Substitute.For<IAbpSession>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();

    [Fact]
    public async Task Handle_ShouldUploadAndSaveDocument()
    {
        // Arrange
        var handler = CreateUploadScanDocumentCommandHandlerInstance();
        var request = CreatePatientScanDocumentUploadRequestInstance();

        // Act
        var result = await handler.Handle(request);

        // Assert
        await _uploadService.Received(1).UploadFile(Arg.Any<Guid>(), Arg.Any<Stream>(), Arg.Any<IDictionary<string, string>>());
        await _repository.Received(1).InsertAsync(Arg.Any<PatientScanDocument>());
        await _unitOfWork.Received(1).Current.SaveChangesAsync();
        result.ShouldNotBeNull();
    }

   


    [Fact]
    public async Task Handle_ThrowsException_WhenFileNameInvalid()
    {
        // Arrange
        var handler = CreateUploadScanDocumentCommandHandlerInstance();
        var request = CreatePatientScanDocumentUploadRequestInstance("InvalidName.PDF");

        // Act & Assert
        var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));
        message.Message.ShouldBe("Invalid file name format, please use the format: PatientCode#FirstName#LastName");
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenFileIsEmpty()
    {
        // Arrange
        var handler = CreateUploadScanDocumentCommandHandlerInstance();
        var request = CreatePatientScanDocumentUploadRequestInstance("");
        // Act & Assert
        var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));
        message.Message.ShouldBe("File name cannot be empty");
        
    }
    [Fact]
    public async Task Handle_ThrowsException_WhenFile_Extension_Is_Not_Valid()
    {
        // Arrange
        var handler = CreateUploadScanDocumentCommandHandlerInstance();
        var request = CreatePatientScanDocumentUploadRequestInstance("Patient123#John#Doe.Docx");
        // Act & Assert
        var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));
        message.Message.ShouldBe("Invalid file format. Only PDF files are allowed");
    }
    
    private static PatientScanDocumentUploadRequest CreatePatientScanDocumentUploadRequestInstance(string fileName = "Patient123#John#Doe.PDF")
    {
        var request = new PatientScanDocumentUploadRequest
        {
            File = MockFile.CreateInstance(fileName)
        };
        return request;
    }
    
    private UploadScanDocumentCommandHandler CreateUploadScanDocumentCommandHandlerInstance()
    {
        var handler = new UploadScanDocumentCommandHandler(_uploadService, _repository, _abpSession, _unitOfWork);
        return handler;
    }
}