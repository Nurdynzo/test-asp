using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using NSubstitute;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Staff.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Staff
{
    [Trait("Category", "Unit")]
    public class CreateJobTitleCommandHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenValidJobTitle_ShouldSave()
        {
            //Arrange
            const int tenantId = 3;
            var repository = Substitute.For<IRepository<JobTitle, long>>();
            var abpSession = Substitute.For<IAbpSession>();
            abpSession.TenantId.Returns(tenantId);

            var handler = new CreateJobTitleCommandHandler(repository, abpSession, _objectMapper);
            //Act
            JobTitle jobTitle = null;
            await repository.InsertAsync(Arg.Do<JobTitle>(j => jobTitle = j));

            await handler.Handle(new CreateOrEditJobTitleDto { Name = "Title1", ShortName = "T1" });
            //Assert
            jobTitle.Name.ShouldBe("Title1");
            jobTitle.ShortName.ShouldBe("T1");
            jobTitle.TenantId.ShouldBe(tenantId);
        }
    }
}
