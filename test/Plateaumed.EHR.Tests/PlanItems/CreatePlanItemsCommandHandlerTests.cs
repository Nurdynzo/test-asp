using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.PlanItems.Dtos;
using Plateaumed.EHR.PlanItems.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PlanItems;

[Trait("Category", "Unit")]
public class CreatePlanItemsCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();

    [Fact]
    public async Task Handle_GivenEncounterId_ShouldCheckExists()
    {
        // Arrange
        var repository = Substitute.For<IRepository<AllInputs.PlanItems, long>>();

        var encounterManager = Substitute.For<IEncounterManager>();
        var handler = new CreatePlanItemsCommandHandler(repository, _unitOfWork, _objectMapper, encounterManager);
        // Act
        await handler.Handle(new CreatePlanItemsDto { EncounterId = 4 });
        
        // Assert
        await encounterManager.Received(1).CheckEncounterExists(4);
    }
 
    [Fact]
    public async Task Handle_GivenCorrectRequest()
    {
        // Arrange
        var repository = Substitute.For<IRepository<AllInputs.PlanItems, long>>();
        var emptyResponse = new List<AllInputs.PlanItems>().AsQueryable().BuildMock();

        repository.GetAll().Returns(emptyResponse);

        var handler = new CreatePlanItemsCommandHandler(repository, _unitOfWork, _objectMapper, Substitute.For<IEncounterManager>());
        // Act
        await handler.Handle(new CreatePlanItemsDto{EncounterId = 6});
        
        // Assert
        await repository.Received(1).InsertAsync(Arg.Is<AllInputs.PlanItems>(x => x.EncounterId == 6));
    }
    
}