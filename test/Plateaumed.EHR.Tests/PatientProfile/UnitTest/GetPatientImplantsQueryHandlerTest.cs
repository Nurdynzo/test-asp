using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.PatientProfile.Query;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetPatientImplantsQueryHandlerTest
    {
        private readonly IRepository<PatientImplant, long> _patientImplant = Substitute.For<IRepository<PatientImplant, long>>();

        [Fact]
        public async Task GetPatientImplantsQueryHandler_Should_Return_Implants_Per_Patient()
        {
            //Arrange
            _patientImplant.GetAll().Returns(GetPatientImplants().BuildMock());
            //Act
            var handler = new GetPatientImplantQueryHandler(_patientImplant);
            var result = await handler.Handle(1);
            //Assert
            result.Count.ShouldBeGreaterThan(0);
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<GetPatientImplantResponseDto>>();
        }

        private static IQueryable<PatientImplant> GetPatientImplants()
        {
            return new List<PatientImplant>()
        {
            new()
            {
                    PatientId = 1,
                    Name = "Breast",
                    SnomedId = 2345765,
                    IsIntact = false,
                    HasComplications = false,
                    Note = "No test note",
                    DateInserted = DateTime.Now.AddMonths(-1),
                    DateRemoved = DateTime.Now,
                    CreatorUserId = 1
            },
        }.AsQueryable();
        }
    }
}
