using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Meals.Dtos;
using Plateaumed.EHR.Meals.Handlers;
using Xunit;

namespace Plateaumed.EHR.Tests.Meals;

[Trait("Category", "Unit")]
public class CreateMealsCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    
    [Fact]
    public async Task Handle_GivenCorrectRequest()
    {
        // Arrange
        var repository = Substitute.For<IRepository<AllInputs.Meals, long>>();
        var emptyResponse = new List<AllInputs.Meals>().AsQueryable().BuildMock();

        repository.GetAll().Returns(emptyResponse);

        var handler = new CreateMealsCommandHandler(repository, _unitOfWork, _objectMapper, Substitute.For<IEncounterManager>());
        // Act
        await handler.Handle(new CreateMealsDto{EncounterId = 5});
        
        // Assert
        await repository.Received(1).InsertAsync(Arg.Is<AllInputs.Meals>(x => x.EncounterId == 5));
    }

    [Fact]
    public async Task Handle_ShouldCheckEncounterExists()
    {
        // Arrange
        var repository = Substitute.For<IRepository<AllInputs.Meals, long>>();

        var encounterManager = Substitute.For<IEncounterManager>();
        var handler = new CreateMealsCommandHandler(repository, _unitOfWork, _objectMapper, encounterManager);
        // Act
        await handler.Handle(new CreateMealsDto { EncounterId = 5 });

        // Assert
        await encounterManager.Received(1).CheckEncounterExists(5);
    }

}