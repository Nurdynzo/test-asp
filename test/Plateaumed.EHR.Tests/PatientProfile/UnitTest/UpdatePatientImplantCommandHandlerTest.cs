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
    public class UpdatePatientImplantCommandHandlerTest
    {
        private readonly IRepository<PatientImplant, long> _patientImplantRepository
            = Substitute.For<IRepository<PatientImplant, long>>();
        private readonly IObjectMapper _mapper = Substitute.For<IObjectMapper>();

        [Fact]
        public async Task UpdatePatientImplantShould_UpdateSuccessfully()
        {
            //Arrange
            _patientImplantRepository.GetAll().Returns(GetRecreationalDrugHistories().BuildMock());
            var edit = new CreatePatientImplantCommandRequestDto
            {
                PatientId = 1,
                Name = "Tongue",
                SnomedId = 490584,
                IsIntact = true,
                HasComplications = false,
                DateInserted = DateTime.Now.AddMonths(3),
                Note = "just an edit note",
                Id = 1
            };
            //Act
            var handler = new UpdatePatientImplantCommandHandler(_patientImplantRepository);
            await handler.Handle(edit);
            var result = _patientImplantRepository.GetAll().SingleOrDefault(x => x.Id == 1);
            //Assert
            result.HasComplications.ShouldBe(false);
            result.Note.ShouldBe("just an edit note");
        }

        private static IQueryable<PatientImplant> GetRecreationalDrugHistories()
        {
            return new List<PatientImplant>()
            {
                new()
                {
                    PatientId = 1,
                    Name = "Tongue",
                    SnomedId = 490584,
                    IsIntact = true,
                    HasComplications = true,
                    DateInserted = DateTime.Now.AddMonths(3),
                    Note = "just a note",
                    Id = 1
                }
            }.AsQueryable();
        }
    }
}
