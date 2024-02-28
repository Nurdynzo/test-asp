using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Staff.Handlers;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Staff;

public class UpdateJobTitleCommandHandlerTests
{
    [Fact]
    public async Task Handle_GiveNoMatchingId_ShouldThrow()
    {
        //Arrange
        const long jobTitleId = 12;
        var repository = Substitute.For<IRepository<JobTitle, long>>();
        repository.FirstOrDefaultAsync(jobTitleId).Returns(Task.FromResult((JobTitle)null));
        var handler = new UpdateJobTitleCommandHandler(repository);

        var input = new CreateOrEditJobTitleDto { Id = jobTitleId, Name = "Title1", ShortName = "T1" };
        //Act
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(async () => { await handler.Handle(input); });
        //Assert
        exception.Message.ShouldBe("Job title not found");
    }

    [Fact]
    public async Task Handle_GivenValidJobTitle_ShouldSave()
    {
        //Arrange
        const int jobTitleId = 16;
        var repository = Substitute.For<IRepository<JobTitle, long>>();
        var savedTitle = TestJobTitleBuilder.Create(1).WithNames("OldT", "OT").Build();

        repository.FirstOrDefaultAsync(jobTitleId).Returns(Task.FromResult(savedTitle));
        var handler = new UpdateJobTitleCommandHandler(repository);

        var input = new CreateOrEditJobTitleDto { Id = jobTitleId, Name = "Title1", ShortName = "T1", IsActive = false};

        //Act
        JobTitle jobTitle = null;
        await repository.UpdateAsync(Arg.Do<JobTitle>(j => jobTitle = j));

        await handler.Handle(input);
        //Assert
        jobTitle.Name.ShouldBe(input.Name);
        jobTitle.ShortName.ShouldBe(input.ShortName);
        jobTitle.IsActive.ShouldBe(input.IsActive);
    }
}