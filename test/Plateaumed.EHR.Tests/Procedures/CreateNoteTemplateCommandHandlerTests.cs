using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.Procedures.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Procedures;

[Trait("Category", "Unit")]
public class CreateNoteTemplateCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    
    [Fact]
    public async Task Handle_GivenCorrect_Request_ProcedureNoteTest()
    {
        // Arrange
        var repository = Substitute.For<IRepository<NoteTemplate, long>>(); 
        
        var testCreateData = Create_ProcedureNoteTest_RequestData();
        
        var handler = new CreateNoteTemplateCommandHandler(repository, _unitOfWork, _objectMapper);
        
        // Act
        NoteTemplate noteTemplate = null;
        await repository.InsertAsync(Arg.Do<NoteTemplate>(j => noteTemplate = j));
        
        await handler.Handle(testCreateData);
        
        // Assert 
        noteTemplate.NoteType.ToString().ShouldBe(testCreateData.NoteType);
        noteTemplate.NoteTitle.ShouldBe(testCreateData.NoteTitle);
        noteTemplate.Note.ShouldBe(testCreateData.Note);
    }
    
    [Fact]
    public async Task Handle_GivenCorrect_Request_AnaesthesiaNote()
    {
        // Arrange
        var repository = Substitute.For<IRepository<NoteTemplate, long>>(); 
        
        var testCreateData = Create_ProcedureNoteTest_AnaesthesiaNote();
        
        var handler = new CreateNoteTemplateCommandHandler(repository, _unitOfWork, _objectMapper);
        
        // Act
        NoteTemplate noteTemplate = null;
        await repository.InsertAsync(Arg.Do<NoteTemplate>(j => noteTemplate = j));
        
        await handler.Handle(testCreateData);
        
        // Assert 
        noteTemplate.NoteType.ToString().ShouldBe(testCreateData.NoteType);
        noteTemplate.NoteTitle.ShouldBe(testCreateData.NoteTitle);
        noteTemplate.Note.ShouldBe(testCreateData.Note);
    }
    
    [Fact]
    public async Task Handle_GivenWrong_NoteType()
    {
        // Arrange
        var repository = Substitute.For<IRepository<NoteTemplate, long>>(); 
        
        var testCreateData = Create_ProcedureNoteTest_AnaesthesiaNote();
            
        var handler = new CreateNoteTemplateCommandHandler(repository, _unitOfWork, _objectMapper);
        testCreateData.NoteType = "ProcedureNotesss";

        // Act 
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(testCreateData));
        
        // Assert
        exception.Message.ShouldBe("Requested value 'ProcedureNotesss' was not found."); 
    }
    
    private static CreateNoteTemplateDto Create_ProcedureNoteTest_RequestData()
    {
        return new CreateNoteTemplateDto()
        {
            NoteType = "ProcedureNote",
            NoteTitle = "Template 1",
            Note = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
        };
    }
    
    private static CreateNoteTemplateDto Create_ProcedureNoteTest_AnaesthesiaNote()
    {
        return new CreateNoteTemplateDto()
        {
            NoteType = "AnaesthesiaNote",
            NoteTitle = "Template 2",
            Note = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
        };
    }
}
