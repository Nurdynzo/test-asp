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
using Plateaumed.EHR.Symptom;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Procedures;

[Trait("Category", "Unit")]
public class GetNoteListQueryHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    
    [Fact]
    public async Task Handle_ShouldReturn_ProcedureNotes()
    {
        // Arrange
        var procedureNoteRepo = Substitute.For<IRepository<ProcedureNote, long>>();
        procedureNoteRepo.GetAll().Returns(CreateProcedureNotesTestData());
        
        var anaesthesiaNoteRepo = Substitute.For<IRepository<AnaesthesiaNote, long>>(); 
            
        var handler = new GetNoteListQueryHandler(procedureNoteRepo, anaesthesiaNoteRepo, _objectMapper);

        // Act
        var response = await handler.Handle(5, "ProcedureNote");
        
        // Assert
        response.First().ProcedureId.ShouldBe(response.First().ProcedureId);
        response.First().Note.ShouldBe(response.First().Note);
    }
    
    [Fact]
    public async Task Handle_ShouldReturn_AnaesthesiaNotes()
    {
        // Arrange
        var procedureNoteRepo = Substitute.For<IRepository<ProcedureNote, long>>();
        var anaesthesiaNoteRepo = Substitute.For<IRepository<AnaesthesiaNote, long>>();
        anaesthesiaNoteRepo.GetAll().Returns(CreateAnaesthesiaNotesTestData());
            
        var handler = new GetNoteListQueryHandler(procedureNoteRepo, anaesthesiaNoteRepo, _objectMapper);

        // Act
        var response = await handler.Handle(2, "AnaesthesiaNote");
        
        // Assert
        response.First().ProcedureId.ShouldBe(response.First().ProcedureId);
        response.First().Note.ShouldBe(response.First().Note);
    }
    
    [Fact]
    public async Task Handle_GivenWrong_QueryRequest()
    {
        // Arrange
        var procedureNoteRepo = Substitute.For<IRepository<ProcedureNote, long>>();
        var anaesthesiaNoteRepo = Substitute.For<IRepository<AnaesthesiaNote, long>>(); 
            
        var handler = new GetNoteListQueryHandler(procedureNoteRepo, anaesthesiaNoteRepo, _objectMapper);

        // Act 
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(2, "AnaesthessiaNotes"));
        
        // Assert
        exception.Message.ShouldBe("Requested value 'AnaesthessiaNotes' was not found."); 
    }
    
    private static IQueryable<ProcedureNote> CreateProcedureNotesTestData()
    {
        return new List<ProcedureNote>
        {
            new() 
            { 
                TenantId = 1, 
                ProcedureId = 5,
                Note = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley."
            },
            new() 
            { 
                TenantId = 1, 
                ProcedureId = 5,
                Note = "of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing."
            },
            new() 
            { 
                TenantId = 1, 
                ProcedureId = 4,
                Note = "publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            } 
        }.AsQueryable().BuildMock();
    }
    
    private static IQueryable<AnaesthesiaNote> CreateAnaesthesiaNotesTestData()
    {
        return new List<AnaesthesiaNote>
        {
            new() 
            { 
                TenantId = 1, 
                ProcedureId = 2,
                Note = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley."
            },
            new() 
            { 
                TenantId = 1, 
                ProcedureId = 2,
                Note = "of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing."
            },
            new() 
            { 
                TenantId = 1, 
                ProcedureId = 2,
                Note = "publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            } 
        }.AsQueryable().BuildMock();
    }
}
