using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Newtonsoft.Json;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.Procedures.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Procedures;

[Trait("Category", "Unit")]
public class CreateProcedureCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();

    [Fact]
    public async Task Handle_GivenCorrectRequestFor_RequestProcedure()
    {
        // Arrange
        var repository = Substitute.For<IRepository<Procedure, long>>();
        var testCreateData = CreateTestData();
        testCreateData.ProcedureType = "RequestProcedure";
        
        var handler = CreateHandler(repository);
        
        // Act
        Procedure procedure = null;
        await repository.InsertAsync(Arg.Do<Procedure>(j => procedure = j));
        
        await handler.Handle(testCreateData);
        
        // Assert
        var selectedProcedure = JsonConvert.DeserializeObject<List<SelectedProcedureDto>>(procedure.SelectedProcedures);
        
        procedure.Note.ShouldBe(testCreateData.Note);
        selectedProcedure.First().SnowmedId.ShouldBe(testCreateData.SelectedProcedures.First().SnowmedId);
        selectedProcedure.First().ProcedureName.ShouldBe(testCreateData.SelectedProcedures.First().ProcedureName);
    }

    [Fact]
    public async Task Handle_GivenCorrectRequestFor_RecordProcedure()
    {
        // Arrange
        var repository = Substitute.For<IRepository<Procedure, long>>();
        var testCreateData = CreateTestData();
        testCreateData.ProcedureType = "RecordProcedure";
        
        var handler = CreateHandler(repository);
        
        // Act
        Procedure procedure = null;
        await repository.InsertAsync(Arg.Do<Procedure>(j => procedure = j));
        
        await handler.Handle(testCreateData);
        
        // Assert
        var selectedProcedure = JsonConvert.DeserializeObject<List<SelectedProcedureDto>>(procedure.SelectedProcedures);
        
        procedure.Note.ShouldBe(testCreateData.Note);
        procedure.EncounterId.ShouldBe(testCreateData.EncounterId);
        selectedProcedure.First().SnowmedId.ShouldBe(testCreateData.SelectedProcedures.First().SnowmedId);
        selectedProcedure.First().ProcedureName.ShouldBe(testCreateData.SelectedProcedures.First().ProcedureName);
    }

    [Fact]
    public async Task Handle_ShouldCheckEncounterExists()
    {
        // Arrange
        var repository = Substitute.For<IRepository<Procedure, long>>();
        var testCreateData = CreateTestData();
        testCreateData.ProcedureType = "RecordProcedure";

        var encounterManager = Substitute.For<IEncounterManager>();

        var handler = CreateHandler(repository, encounterManager);

        // Act

        await handler.Handle(testCreateData);

        // Assert

        await encounterManager.Received(1).CheckEncounterExists(testCreateData.EncounterId);
    }

    private CreateProcedureCommandHandler CreateHandler(IRepository<Procedure, long> repository, IEncounterManager encounterManager = null)
    {
        return new CreateProcedureCommandHandler(repository, _unitOfWork, _objectMapper,
            encounterManager ?? Substitute.For<IEncounterManager>());
    }

    private static CreateProcedureDto CreateTestData()
    {
        return new CreateProcedureDto
        {
            PatientId = 1, 
            Note = "Testing from what has been added to the Unit Test code base.",
            SelectedProcedures = new List<SelectedProcedureDto>()
            {
                new()
                {
                    SnowmedId = 182531007,
                    ProcedureName = "Dressing of wound"
                }   
            },
            EncounterId = 5
        };
    }
}