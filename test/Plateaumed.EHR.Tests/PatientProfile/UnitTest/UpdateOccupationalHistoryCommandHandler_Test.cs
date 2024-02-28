using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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
    public class UpdateOccupationalHistoryCommandHandler_Test
    {
        private readonly IRepository<OccupationalHistory, long> _occupationalHistorRepository
            = Substitute.For<IRepository<OccupationalHistory, long>>();

        [Fact]
        public async Task CreateOccupationalHistoryCommandHandler_Should_Create_Correctly()
        {
            //Arrange
            _occupationalHistorRepository.GetAll().Returns(GetOccupationalHistories().BuildMock());
            var history = new CreateOccupationalHistoryDto
            {
                PatientId = 1,
                WorkLocation = "Test New location",
                From = DateTime.Now.AddYears(-2),
                Occupation = "Test Occupation",
                Note = "Test note for test occupation",
                IsCurrent = true,
                Id = 1
            };
            //Act
            var handler = new UpdateOccupationalHistoryCommandHandler(_occupationalHistorRepository);
            await handler.Handle(history);
            var result =  await _occupationalHistorRepository.GetAll().SingleOrDefaultAsync(x => x.Id == history.Id);
            //Assert
            result.WorkLocation.ShouldBe("Test New location");
        }

        private static IQueryable<OccupationalHistory> GetOccupationalHistories()
        {
            return new List<OccupationalHistory>()
            {
                new()
                {   
                    PatientId = 1,
                    WorkLocation = "Test location",
                    From = DateTime.Now.AddYears(-2),
                    Occupation = "Test Occupation",
                    Note = "Test note for test occupation",
                    IsCurrent = true,
                    Id = 1,
                }
            }.AsQueryable();
        }
    }
}
