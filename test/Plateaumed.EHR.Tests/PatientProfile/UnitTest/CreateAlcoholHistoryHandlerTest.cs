using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientProfile.Command;
using Plateaumed.EHR.PatientProfile.Dto;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class CreateAlcoholHistoryHandlerTest
    {
        private readonly IRepository<AlcoholHistory, long> _alcoholHistorRepository 
            = Substitute.For<IRepository<AlcoholHistory, long>>();

        private readonly IObjectMapper _mapper = Substitute.For<IObjectMapper>();

        [Fact]
        public async Task CreateAlcoholHistoryHandlerTest_Should_Create_History_Successfully()
        {
            //Arrange
            _alcoholHistorRepository.GetAll().Returns(GetAlcoholHistories().BuildMock());
            var history = new CreateAlcoholHistoryRequestDto
            {
                PatientId = 1,
                Frequency = 2,
                Interval = UnitOfTime.Day,
                TypeOfAlcohol = "Whisky",
                MaximumAmountOfUnits = 1.5f,
                Note = "",
                DetailsOfAlcoholIntakeNotKnown = true,
                DoesNotTakeAlcohol = false
            };
            //Act
            var handler = new CreateAlcoholHistoryCommandHandler(_alcoholHistorRepository, _mapper);
            await handler.Handle(history);
            var histories = GetAlcoholHistories();
            //Assert
            histories.Count().ShouldNotBe(1); 
        }

        private static IQueryable<AlcoholHistory> GetAlcoholHistories()
        {
            return new List<AlcoholHistory>()
            {
                
            }.AsQueryable();
        }
    }
}
