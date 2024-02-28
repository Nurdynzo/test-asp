using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientProfile.Command;
using Plateaumed.EHR.PatientProfile.Dto;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class UpdateRecreationalDrugHistoryTest
    {
        private readonly IRepository<RecreationalDrugHistory, long> _recreationalDrugHistoryRepository
            = Substitute.For<IRepository<RecreationalDrugHistory, long>>();
        private readonly IObjectMapper _mapper = Substitute.For<IObjectMapper>();

        [Fact]
        public async Task UpdateRecreationalDrugCommandHandlerShould_UpdateSuccessfully()
        {
            //Arrange
            _recreationalDrugHistoryRepository.GetAll().Returns(GetRecreationalDrugHistories().BuildMock());
            var edit = new CreateRecreationalDrugsHistoryRequestDto
            {
                PatientId = 1,
                PatientDoesNotTakeRecreationalDrugs = false,
                DrugUsed = "Test Edit drug",
                Route = "mouth",
                StillUsingDrugs = false,
                From = DateTime.Now.AddMonths(3),
                To = DateTime.Now,
                Note = "just an edit note",
                Id = 1
            };
            //Act
            var handler = new UpdateRecreationalDrugHistoryCommandHandler(_recreationalDrugHistoryRepository);
            await handler.Handle(edit);
            var result = _recreationalDrugHistoryRepository.GetAll().SingleOrDefault(x => x.Id == 1);
            //Assert
            result.DrugUsed.ShouldBe("Test Edit drug");
            result.Note.ShouldBe("just an edit note");
        }

        private static IQueryable<RecreationalDrugHistory> GetRecreationalDrugHistories()
        {
            return new List<RecreationalDrugHistory>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    PatientDoesNotTakeRecreationalDrugs = false,
                    DrugUsed = "Test drug",
                    Route = "mouth",
                    StillUsingDrugs = false,
                    From = DateTime.Now.AddMonths(3),
                    To = DateTime.Now,
                    Note = "just a note"
                }
            }.AsQueryable();
        }
    }
}
