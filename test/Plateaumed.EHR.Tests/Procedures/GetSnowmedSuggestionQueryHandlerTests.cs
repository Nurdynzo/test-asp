using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Snowstorm.Abstractions;
using Plateaumed.EHR.Snowstorm.Dtos;
using Plateaumed.EHR.Snowstorm.Query;
using Plateaumed.EHR.Symptom;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Procedures;

[Trait("Category", "Unit")]
public class GetSnowmedSuggestionQueryHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    
    [Fact]
    public async Task Handle_ShouldReturnProcedureSuggestions()
    {
        // Arrange
        var repository = Substitute.For<ISuggestionBaseQuery>();
        var testData = CreateTestData();
        repository.GetSuggestionsBaseQuery(null, AllInputType.Procedure).Returns(testData);
            
        var handler = new GetSnowmedSuggestionQueryHandler(repository, _objectMapper);

        // Act
        var response = await handler.Handle(null, AllInputType.Procedure.ToString(), null);
        
        // Assert
        response.First().Id.ShouldBe(testData.First().SourceSnowmedId);
        response.First().Name.ShouldBe(testData.First().SourceName); 
    }
    
    private static IQueryable<SnowmedSuggestion> CreateTestData()
    {
        return new List<SnowmedSuggestion>
            {
                new() 
                { 
                    SourceSnowmedId = "182531007", 
                    SourceName = "Dressing of wound", 
                    Name  = "Old dressing dry and intact", 
                    SnowmedId = null, 
                    Type = AllInputType.Procedure 
                } 
            }.AsQueryable().BuildMock();
    }
}