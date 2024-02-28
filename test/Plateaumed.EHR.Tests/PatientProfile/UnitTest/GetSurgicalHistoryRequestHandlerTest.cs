using Abp.Domain.Repositories;
using Abp.ObjectMapping;
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
    public class GetSurgicalHistoryRequestHandlerTest
    {
        private readonly IRepository<SurgicalHistory, long> _patientSurgicalHistoryRepository = Substitute.For<IRepository<SurgicalHistory, long>>();
        private readonly IObjectMapper _objectMapper = Substitute.For<IObjectMapper>();
        [Fact]
        public async Task GetPatientSurgicalHitoryRequetHandler_Should_Get_SurgicalHistoryPerPatient()
        {
            //Arrange
            _patientSurgicalHistoryRepository.GetAll().Returns(GetSurgicalHistories().BuildMock());
            //Act
            var handler = new GetPatientSurgicalHistoryQueryHandler(_patientSurgicalHistoryRepository, _objectMapper);
            var result = await handler.Handle(1);
            //Assert
            result.Count.ShouldBe(1);
            result.ShouldBeOfType<List<GetSurgicalHistoryResponseDto>>();
        }


        private static IQueryable<SurgicalHistory> GetSurgicalHistories()
        {
            return new List<SurgicalHistory>
            {
                new()
                {
                    PatientId = 1,
                    Diagnosis = "test diagnosis",
                    DiagnosisSnomedId = 1233,
                    Procedure = "test procedure",
                    ProcedureSnomedId = 3453,
                    Interval = Misc.UnitOfTime.Month,
                    PeriodSinceSurgery = 40,
                    NoComplicationsPresent = true,
                    Note = "test note"
                }
            }.AsQueryable();
        }
    }
}


