using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.InputNotes.Dtos;
using Plateaumed.EHR.InputNotes.Handlers;
using Xunit;

namespace Plateaumed.EHR.Tests.InputNotes;

[Trait("Category", "Unit")]
public class CreateInputNotesCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    
    [Fact]
    public async Task Handle_GivenCorrectRequest()
    {
        // Arrange
        var repository = Substitute.For<IRepository<AllInputs.InputNotes, long>>();
        var emptyResponse = new List<AllInputs.InputNotes>().AsQueryable().BuildMock();

        repository.GetAll().Returns(emptyResponse);

        var handler = new CreateInputNotesCommandHandler(repository, _unitOfWork, _objectMapper, Substitute.For<IEncounterManager>());
        // Act
        await handler.Handle(new CreateInputNotesDto{EncounterId = 5});
        
        // Assert
        await repository.Received(1).InsertAsync(Arg.Is<AllInputs.InputNotes>(x => x.EncounterId == 5));
    }

    [Fact]
    public async Task Handle_GivenCorrectRequest_ShouldCheckEncounterExists()
    {
        // Arrange
        var repository = Substitute.For<IRepository<AllInputs.InputNotes, long>>();
        var emptyResponse = new List<AllInputs.InputNotes>().AsQueryable().BuildMock();

        repository.GetAll().Returns(emptyResponse);

        var encounterManager = Substitute.For<IEncounterManager>();
        var handler = new CreateInputNotesCommandHandler(repository, _unitOfWork, _objectMapper, encounterManager);
        // Act
        await handler.Handle(new CreateInputNotesDto { EncounterId = 5 });

        // Assert
        await encounterManager.Received(1).CheckEncounterExists(5);
    }
}