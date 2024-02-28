using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.BedMaking.Dtos;
using Plateaumed.EHR.BedMaking.Handlers;
using Plateaumed.EHR.Encounters;
using Xunit;

namespace Plateaumed.EHR.Tests.BedMaking;

[Trait("Category", "Unit")]
public class CreateBedMakingCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();

    [Fact]
    public async Task Handle_GivenEncounterId_ShouldCheckEncounterExists()
    {
        // Arrange
        var repository = Substitute.For<IRepository<AllInputs.BedMaking, long>>();

        var encounterManager = Substitute.For<IEncounterManager>();
        var handler = new CreateBedMakingCommandHandler(repository, _unitOfWork, _objectMapper, encounterManager);
        // Act
        await handler.Handle(new CreateBedMakingDto{EncounterId = 7});
        
        // Assert
        await encounterManager.Received().CheckEncounterExists(7);
    }
 
    [Fact]
    public async Task Handle_GivenCorrectRequest_ShouldSave()
    {
        // Arrange
        var repository = Substitute.For<IRepository<AllInputs.BedMaking, long>>();
        var emptyResponse = new List<AllInputs.BedMaking>().AsQueryable().BuildMock();

        repository.GetAll().Returns(emptyResponse);

        var handler = new CreateBedMakingCommandHandler(repository, _unitOfWork, _objectMapper, Substitute.For<IEncounterManager>());
        // Act
        var request = new CreateBedMakingDto { EncounterId = 1 };
        await handler.Handle(request);
        // Assert
        await repository.Received(1).InsertAsync(Arg.Is<AllInputs.BedMaking>(x => x.EncounterId == 1));
    }
}