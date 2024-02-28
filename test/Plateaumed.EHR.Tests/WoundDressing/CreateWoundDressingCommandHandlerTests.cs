using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.WoundDressing.Dtos;
using Plateaumed.EHR.WoundDressing.Handlers;
using Xunit;

namespace Plateaumed.EHR.Tests.WoundDressing;

[Trait("Category", "Unit")]
public class CreateWoundDressingCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    
    [Fact]
    public async Task Handle_GivenCorrectRequest_ShouldSaveAndCheckEncounter()
    {
        // Arrange
        var repository = Substitute.For<IRepository<AllInputs.WoundDressing, long>>();
        var emptyResponse = new List<AllInputs.WoundDressing>().AsQueryable().BuildMock();

        repository.GetAll().Returns(emptyResponse);

        var encounterManager  = Substitute.For<IEncounterManager>();
        var handler = new CreateWoundDressingCommandHandler(repository, _unitOfWork, _objectMapper, encounterManager);
        // Act
        await handler.Handle(new CreateWoundDressingDto { EncounterId = 4 });
        
        // Assert
        await repository.Received(1).InsertAsync(Arg.Is<AllInputs.WoundDressing>(x => x.EncounterId == 4));
        await encounterManager.Received().CheckEncounterExists(4);
    }
}