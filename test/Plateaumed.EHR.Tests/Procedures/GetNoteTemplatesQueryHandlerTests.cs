using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Procedures.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Procedures;

[Trait("Category", "Unit")]
public class GetNoteTemplatesQueryHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    
    [Fact]
    public async Task Handle_ShouldReturn_ProcedureTemplateNotes()
    {
        // Arrange
        var procedureNoteRepo = Substitute.For<IRepository<NoteTemplate, long>>();
        procedureNoteRepo.GetAll().Returns(CreateTemplateNotesTestData());
        
        var anaesthesiaNoteRepo = Substitute.For<IRepository<NoteTemplate, long>>(); 
            
        var handler = new GetNoteTemplatesQueryHandler(procedureNoteRepo, _objectMapper);

        // Act
        var response = await handler.Handle("ProcedureNote");
        
        // Assert
        response.First().NoteType.ShouldBe(response.First().NoteType);
        response.First().NoteTypeName.ShouldBe(response.First().NoteTypeName);
        response.First().NoteTitle.ShouldBe(response.First().NoteTitle);
        response.First().Note.ShouldBe(response.First().Note);
    }
    
    [Fact]
    public async Task Handle_ShouldReturn_AnaesthesiaNoteTemplateNotes()
    {
        // Arrange
        var procedureNoteRepo = Substitute.For<IRepository<NoteTemplate, long>>();
        procedureNoteRepo.GetAll().Returns(CreateTemplateNotesTestData());
        
        var anaesthesiaNoteRepo = Substitute.For<IRepository<NoteTemplate, long>>(); 
            
        var handler = new GetNoteTemplatesQueryHandler(procedureNoteRepo, _objectMapper);

        // Act
        var response = await handler.Handle("AnaesthesiaNote");
        
        // Assert
        response.First().NoteType.ShouldBe(response.First().NoteType);
        response.First().NoteTypeName.ShouldBe(response.First().NoteTypeName);
        response.First().NoteTitle.ShouldBe(response.First().NoteTitle);
        response.First().Note.ShouldBe(response.First().Note);
    }
    
    [Fact]
    public async Task Handle_GivenWrong_QueryRequest()
    {
        // Arrange
        var procedureNoteRepo = Substitute.For<IRepository<NoteTemplate, long>>(); 
        
        var anaesthesiaNoteRepo = Substitute.For<IRepository<NoteTemplate, long>>(); 
            
        var handler = new GetNoteTemplatesQueryHandler(procedureNoteRepo, _objectMapper);
        
        // Act 
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle("ProcedureNotess"));
        
        // Assert
        exception.Message.ShouldBe("Requested value 'ProcedureNotess' was not found.");
    }
    
    private static IQueryable<NoteTemplate> CreateTemplateNotesTestData()
    {
        return new List<NoteTemplate>
        {
            new() 
            { 
                TenantId = 1, 
                NoteType = NoteType.ProcedureNote,
                NoteTitle = "Template 1",
                Note = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley."
            },
            new() 
            { 
                TenantId = 1, 
                NoteType = NoteType.ProcedureNote,
                NoteTitle = "Template 2",
                Note = "of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing."
            },
            new() 
            { 
                TenantId = 1, 
                NoteType = NoteType.ProcedureNote,
                NoteTitle = "Template 3",
                Note = "publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            },
            new() 
            { 
                TenantId = 1, 
                NoteType = NoteType.AnaesthesiaNote,
                NoteTitle = "Template 4",
                Note = "of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing."
            },
            new() 
            { 
                TenantId = 1, 
                NoteType = NoteType.AnaesthesiaNote,
                NoteTitle = "Template 5",
                Note = "publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            } 
        }.AsQueryable().BuildMock();
    }
}
