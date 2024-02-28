using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Bogus;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.PhysicalExaminations;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.PhysicalExaminations.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PhysicalExaminations
{
    public class GetPhysicalExaminationHeadersQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenType_ShouldFilterDistinctHeaders()
        {
            // Arrange
            var request = new GetPhysicalExaminationHeadersRequest { PhysicalExaminationTypeId = 1};

            var repository = Substitute.For<IRepository<PhysicalExamination, long>>();
            repository.GetAll().Returns(GetData());

            var handler = new GetPhysicalExaminationHeadersQueryHandler(repository);
            
            // Act
            var response = await handler.Handle(request);
            
            // Assert
            response.Headers.Count.ShouldBe(2);
            response.Headers[0].ShouldBe("Head");
            response.Headers[1].ShouldBe("Ears");
        }
        
        private IQueryable<PhysicalExamination> GetData()
        {
            return new List<PhysicalExamination>()
            {
                CreatePhysicalExamination(1, "General", "Head"),
                CreatePhysicalExamination(1, "General", "Head"),
                CreatePhysicalExamination(1, "General", "Head"),
                CreatePhysicalExamination(1, "General", "Ears"),
                CreatePhysicalExamination(1, "General", "Ears"),
                CreatePhysicalExamination(2, "Cardiology", "Pulse"),
                CreatePhysicalExamination(2, "Cardiology", "Pulse")
            }.AsQueryable().BuildMock();
        }

        private PhysicalExamination CreatePhysicalExamination(long id, string type, string headers)
        {
            return new PhysicalExamination
            {
                PhysicalExaminationTypeId = id,
                Type = type,
                Header = headers
            };
        }
    }
}
