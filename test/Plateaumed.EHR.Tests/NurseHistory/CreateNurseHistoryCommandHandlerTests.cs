using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using NSubstitute;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.NurseHistory.Dtos;
using Plateaumed.EHR.NurseHistory.Handlers;
using Plateaumed.EHR.Procedures;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.NurseHistory;

[Trait("Category", "Unit")]
public class CreateNurseHistoryCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    
    [Fact]
    public async Task Handle_GivenCorrect_Request()
    {
        // Arrange
        var repository = Substitute.For<IRepository<NursingHistory, long>>(); 
        
        var testCreateData = Create_RequestData();
        
        var handler = new CreateNurseHistoryCommandHandler(repository, _unitOfWork, _objectMapper);
        
        // Act
        NursingHistory nursingHistory = null;
        await repository.InsertAsync(Arg.Do<NursingHistory>(j => nursingHistory = j));
        
        await handler.Handle(testCreateData);
        
        // Assert  
        nursingHistory.PatientId.ShouldBe(testCreateData.PatientId);
        nursingHistory.Note.ShouldBe(testCreateData.Note);
    }
    
    private static CreateNurseHistoryDto Create_RequestData()
    {
        return new CreateNurseHistoryDto()
        {
            PatientId = 1,
            Note = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
        };
    }
}