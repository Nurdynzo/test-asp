using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using NSubstitute;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.Procedures.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Procedures;

[Trait("Category", "Unit")]
public class CreateAnaesthesiaNoteCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    
    [Fact]
    public async Task Handle_GivenCorrect_Request_TypeNotes()
    {
        // Arrange
        var repository = Substitute.For<IRepository<AnaesthesiaNote, long>>(); 
        
        var testCreateData = CreateTest_RequestData();
        
        var handler = new CreateAnaesthesiaNoteCommandHandler(repository, _unitOfWork, _objectMapper);
        
        // Act
        AnaesthesiaNote anaesthesiaNote = null;
        await repository.InsertAsync(Arg.Do<AnaesthesiaNote>(j => anaesthesiaNote = j));
        
        await handler.Handle(testCreateData);
        
        // Assert 
        anaesthesiaNote.ProcedureId.ShouldBe(testCreateData.ProcedureId);
        anaesthesiaNote.Note.ShouldBe(testCreateData.Note);
    }

    private static CreateAnaesthesiaNoteDto CreateTest_RequestData()
    {
        return new CreateAnaesthesiaNoteDto()
        {
            ProcedureId = 1,
            Note = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
        };
    }
}