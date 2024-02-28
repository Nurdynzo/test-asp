using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientProfile.Command;
using Plateaumed.EHR.PatientProfile.Dto;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class UpdateReviewOfSystemsCommandHandlerTest
    {
        private readonly IRepository<ReviewOfSystem, long> _reviewOfSystemsRepository
            = Substitute.For<IRepository<ReviewOfSystem, long>>();



        [Fact]
        public async Task UpdateReviewOfSystemsCommandHandler_Should_Update_Correctly()
        {
            //Arrange
            _reviewOfSystemsRepository.GetAll().Returns(GetReviewOfSystemData().BuildMock());
            var data = new CreateReviewOfSystemsRequestDto()
            {
                Name = "Cold",
                SnomedId = 3904873,
                Category = SymptomsCategory.Respiratory,
                Id = 1
            };
            
            //Act
            var handler = new UpdateReviewOfSystemsDataCommandHandler(_reviewOfSystemsRepository);
            await handler.Handle(data);
            var result = await _reviewOfSystemsRepository.GetAll().SingleOrDefaultAsync(x => x.Id == data.Id);
            //Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe("Cold");
        }

        private static IQueryable<ReviewOfSystem> GetReviewOfSystemData()
        {
            return new List<ReviewOfSystem>()
            {

                new()
                {
                    Id = 1,
                    Name = "Cough",
                    SnomedId = 3904873,
                    Category = SymptomsCategory.Respiratory,
                    PatientId = 1
                },
                new()
                {
                    Id = 2,
                    Name = "Fever",
                    SnomedId = 113475,
                    Category = SymptomsCategory.Systemic,
                    PatientId = 1
                }
                
            }.AsQueryable();
        }
    }
}
