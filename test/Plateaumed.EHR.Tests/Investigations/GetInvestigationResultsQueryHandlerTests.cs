using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class GetInvestigationResultsQueryHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
        
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldReturnInvestigations()
        {
            // Arrange
            var request = new GetInvestigationResultsRequest
            {
                PatientId = 1
            };

            var repository = Substitute.For<IRepository<InvestigationResult, long>>();
            var userRepository = Substitute.For<IRepository<User, long>>();
            repository.GetAll().Returns(GetInvestigations());
            
            var handler = new GetInvestigationResultsQueryHandler(repository, _objectMapper,userRepository);

            // Act
            var results = await handler.Handle(request);
            // Assert
            results.Count.ShouldBe(2);
            results[0].InvestigationComponentResults.Count.ShouldBe(2);
            results[1].InvestigationComponentResults.Count.ShouldBe(1);
        }

        private static IQueryable<InvestigationResult> GetInvestigations()
        {
            return new List<InvestigationResult>
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    InvestigationId = 1,
                    Name = "Investigation 1",
                    InvestigationComponentResults = new List<InvestigationComponentResult>
                    {
                        new()
                        {
                            Category = "Category 1",
                            Name = "Component 1",
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    PatientId = 2,
                    InvestigationId = 5,
                    Name = "Investigation 2",
                    InvestigationComponentResults = new List<InvestigationComponentResult>
                    {
                        new()
                        {
                            Category = "Category 2",
                            Name = "Component 2",
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    PatientId = 1,
                    InvestigationId = 7,
                    Name = "Investigation 3",
                    InvestigationComponentResults = new List<InvestigationComponentResult>
                    {
                        new()
                        {
                            Category = "Category 3",
                            Name = "Component 3",
                        },
                        new()
                        {
                            Category = "Category 4",
                            Name = "Component 4",
                        }
                    }
                }
            }.AsQueryable().BuildMock();
        }
    }
}
