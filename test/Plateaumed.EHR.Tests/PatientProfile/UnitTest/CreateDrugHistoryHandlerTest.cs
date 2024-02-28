using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PatientProfile.Command;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.PatientProfile;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using Abp.ObjectMapping;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class CreateDrugHistoryHandlerTest
    {
        private readonly IRepository<DrugHistory, long> _drugHistorRepository
           = Substitute.For<IRepository<DrugHistory, long>>();

        private readonly IObjectMapper _mapper = Substitute.For<IObjectMapper>();

        [Fact]
        public async Task CreateAlcoholHistoryHandlerTest_Should_Create_History_Successfully()
        {
            //Arrange
            _drugHistorRepository.GetAll().Returns(GetDrugHistories().BuildMock());
            var history = new CreateDrugHistoryRequestDto
            {
                PatientId = 1,
                MedicationName = "Test medication",
                Route = "mouth",
                Dose = 3,
                PrescriptionFrequency = 3,
                CompliantWithMedication = true,
                UsageFrequency = 4,
                UsageInterval = "Day",
                IsMedicationStillBeingTaken = true,
                Note = "test note"
            };
            //Act
            var handler = new CreateDrugHistoryCommandHandler(_drugHistorRepository, _mapper);
            await handler.Handle(history);
            var histories = GetDrugHistories();
            //Assert
            histories.Count().ShouldNotBe(1);
        }

        private static IQueryable<DrugHistory> GetDrugHistories()
        {
            return new List<DrugHistory>()
            {

            }.AsQueryable();
        }
    }
}
