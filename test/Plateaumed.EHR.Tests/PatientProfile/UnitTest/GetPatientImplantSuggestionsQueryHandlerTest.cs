using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.PatientProfile.Query;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetPatientImplantSuggestionsQueryHandlerTest
    {
        private readonly IRepository<PatientImplantSuggestion, long> _patientImplantSuggestion
        = Substitute.For<IRepository<PatientImplantSuggestion, long>>();


        [Fact]
        public async Task GetImplantSuggestionQueryHandler_Should_Return_Implant_Suggestions()
        {
            //Arrange
            _patientImplantSuggestion.GetAll().Returns(GetPatientImplantSuggestions().BuildMock());
            //Act
            var handler = new GetImplantSuggestionQueryHandler(_patientImplantSuggestion);
            var result = await handler.Handle();
            //Assert
            result.Count.ShouldBeGreaterThan(0);
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<GetImplantSuggestionResponse>>();
        }

        private static IQueryable<PatientImplantSuggestion> GetPatientImplantSuggestions()
        {
            return new List<PatientImplantSuggestion>()
        {
            new()
            {
                Name = "First test implant",
                SnomedId = 2394893,
                Id = 1
            },
            new()
            {
                Name = "Second test implant",
                SnomedId = 2391113,
                Id = 2
            },
            new()
            {
                Name = "Third test implant",
                SnomedId = 1112393,
                Id = 3
            }
        }.AsQueryable();
        }
    }
}
