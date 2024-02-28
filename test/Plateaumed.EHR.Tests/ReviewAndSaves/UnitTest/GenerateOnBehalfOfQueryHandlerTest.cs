using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using Abp.UI;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.Staff.Abstractions;
using Abp.ObjectMapping;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.ReviewAndSaves.Query;

namespace Plateaumed.EHR.Tests.ReviewAndSaves.UnitTest;

[Trait("Category", "Unit")]
public class GenerateOnBehalfOfQueryHandlerTest
{
    private readonly IDoctorReviewAndSaveBaseQuery _basedQueryMock = Substitute.For<IDoctorReviewAndSaveBaseQuery>();
    private readonly IGetJobTitlesQueryHandler _jobTitleQueryHandlerMock = Substitute.For<IGetJobTitlesQueryHandler>();

    [Fact]
    public async Task GenerateOnBehalfOfQueryHandler_Handle_With_No_UserId_Result()
    {
        //arrange 
        long unitId = 1;
        long staffUserId = 1;
        long facilityId = 1;
        _basedQueryMock.GetStaffByUserId(1).ReturnsForAnyArgs(Util.Common.GetStaffByUserId(staffUserId));

        _jobTitleQueryHandlerMock.Handle(new GetAllJobTitlesInput(){FacilityId = facilityId }).ReturnsForAnyArgs(Util.Common.GetStaffJobs(new GetAllJobTitlesInput() { FacilityId = facilityId }));
        _basedQueryMock.GetDoctorsByUnit(unitId, 1).ReturnsForAnyArgs(Util.Common.GetDoctorsByUnit(unitId, facilityId, 1));

        var handler = new GenerateOnBehalfOfQueryHandler(_basedQueryMock, _jobTitleQueryHandlerMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(0, facilityId));
        //Assert
        result.Message.ShouldBe("User Id is required.");

    }


    [Fact]
    public async Task GenerateOnBehalfOfQueryHandler_Handle_With_No_FacilityId_Result()
    {
        //arrange 
        long unitId = 1;
        long staffUserId = 1;
        long facilityId = 1;
        _basedQueryMock.GetStaffByUserId(1).ReturnsForAnyArgs(Util.Common.GetStaffByUserId(staffUserId));

        _jobTitleQueryHandlerMock.Handle(new GetAllJobTitlesInput() { FacilityId = facilityId }).ReturnsForAnyArgs(Util.Common.GetStaffJobs(new GetAllJobTitlesInput() { FacilityId = facilityId }));
        _basedQueryMock.GetDoctorsByUnit(unitId, 1).ReturnsForAnyArgs(Util.Common.GetDoctorsByUnit(unitId, facilityId, 1));

        var handler = new GenerateOnBehalfOfQueryHandler(_basedQueryMock, _jobTitleQueryHandlerMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(staffUserId, 0));
        //Assert
        result.Message.ShouldBe("Facility Id is required.");
    }


    [Fact]
    public async Task GenerateOnBehalfOfQueryHandler_Handle_staff_With_No_Jobt_Result()
    {
        //arrange 
        long unitId = 1;
        long staffUserId = 1;
        long facilityId = 1;
        _basedQueryMock.GetStaffByUserId(1).ReturnsForAnyArgs(Util.Common.GetDoctorByUserId_No_Job(staffUserId));

        _jobTitleQueryHandlerMock.Handle(new GetAllJobTitlesInput() { FacilityId = facilityId }).ReturnsForAnyArgs(Util.Common.GetStaffJobs(new GetAllJobTitlesInput() { FacilityId = facilityId }));
        _basedQueryMock.GetDoctorsByUnit(unitId, 1).ReturnsForAnyArgs(Util.Common.GetDoctorsByUnit(unitId, facilityId, 1));

        var handler = new GenerateOnBehalfOfQueryHandler(_basedQueryMock, _jobTitleQueryHandlerMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(staffUserId, facilityId));
        //Assert
        result.Message.ShouldBe("No job profile found.");
    }


    [Fact]
    public async Task GenerateOnBehalfOfQueryHandler_Handle_staff_With_No_Unit_Result()
    {
        //arrange 
        long unitId = 1;
        long staffUserId = 1;
        long facilityId = 1;
        _basedQueryMock.GetStaffByUserId(1).ReturnsForAnyArgs(Util.Common.GetDoctorByUserId_No_JobUnit_Id(staffUserId));

        _jobTitleQueryHandlerMock.Handle(new GetAllJobTitlesInput() { FacilityId = facilityId }).ReturnsForAnyArgs(Util.Common.GetStaffJobs(new GetAllJobTitlesInput() { FacilityId = facilityId }));
        _basedQueryMock.GetDoctorsByUnit(unitId, 1).ReturnsForAnyArgs(Util.Common.GetDoctorsByUnit(unitId, facilityId, 1));

        var handler = new GenerateOnBehalfOfQueryHandler(_basedQueryMock, _jobTitleQueryHandlerMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(staffUserId, facilityId));
        //Assert
        result.Message.ShouldBe("No unit found for this staff.");
    }

    [Fact]
    public async Task GenerateOnBehalfOfQueryHandler_Handle_successful_Result()
    {
        //arrange 
        long unitId = 1;
        long staffUserId = 1;
        long facilityId = 1;
        _basedQueryMock.GetStaffByUserId(1).ReturnsForAnyArgs(Util.Common.GetStaffByUserId(staffUserId));

        _jobTitleQueryHandlerMock.Handle(new GetAllJobTitlesInput() { FacilityId = facilityId }).ReturnsForAnyArgs(Util.Common.GetStaffJobs(new GetAllJobTitlesInput() { FacilityId = facilityId }));
        _basedQueryMock.GetDoctorsByUnit(unitId, 1).ReturnsForAnyArgs(Util.Common.GetDoctorsByUnit(unitId, facilityId, 1));

        var handler = new GenerateOnBehalfOfQueryHandler(_basedQueryMock, _jobTitleQueryHandlerMock);

        //Act
        var result = await handler.Handle(staffUserId, facilityId);
        var resultId = result == null ? 0 : result.Count;
        //Assert
        resultId.ShouldBeGreaterThan(0);
    }
}