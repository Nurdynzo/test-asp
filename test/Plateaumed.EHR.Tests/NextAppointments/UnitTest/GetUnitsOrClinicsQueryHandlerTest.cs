using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using Abp.UI;
using Abp.Runtime.Session;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.Tests.NextAppointments.Util;
using Plateaumed.EHR.NextAppointments.Query;
using Plateaumed.EHR.NextAppointments.Dtos;
using System.Collections.Generic;
using Plateaumed.EHR.Encounters.Abstractions;
using MockQueryable.NSubstitute;

namespace Plateaumed.EHR.Tests.NextAppointments.UnitTest;

[Trait("Category", "Unit")]
public class GetUnitsOrClinicsQueryHandlerTest
{
    INextAppointmentBaseQuery _baseQueryMock = Substitute.For<INextAppointmentBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly IGetPatientEncounterQueryHandler _patientEncounterQueryhandlerMock = Substitute.For<IGetPatientEncounterQueryHandler>();


    [Fact]
    public async Task GetUnitsOrClinicsQueryHandler_Handle_With_Invalid_PatientId_Result()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        var request = common.GetUnitOrClinicRequest(1);
        _baseQueryMock.GetAllPossibleUnitsAndClinics(request.UserId, 0, request.TenantId, request.FacilityId, request.EncounterId)
            .ReturnsForAnyArgs(common.GetAllPosibleUnitsAndClinics());
        _patientEncounterQueryhandlerMock.Handle(request.EncounterId).ReturnsForAnyArgs(common.GetPatientEncounter(request.EncounterId, false));
        var handler = new GetUnitsOrClinicsQueryHandler(_baseQueryMock, _abpSessionMock, _patientEncounterQueryhandlerMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request.UserId, request.FacilityId, request.EncounterId));
        //Assert
        result.Message.ShouldBe("Patient Id is required.");

    }


    [Fact]
    public async Task GetUnitsOrClinicsQueryHandler_Handle_With_Invalid_UserId_Result()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        var request = common.GetUnitOrClinicRequest(1);
        _baseQueryMock.GetAllPossibleUnitsAndClinics(0, request.PatientId, request.TenantId, request.FacilityId, request.EncounterId)
            .ReturnsForAnyArgs(common.GetAllPosibleUnitsAndClinics());
        _patientEncounterQueryhandlerMock.Handle(request.EncounterId).ReturnsForAnyArgs(common.GetPatientEncounter(request.EncounterId));

        var handler = new GetUnitsOrClinicsQueryHandler(_baseQueryMock, _abpSessionMock, _patientEncounterQueryhandlerMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(0, request.FacilityId, request.EncounterId));
        //Assert
        result.Message.ShouldBe("Doctor User Id is required.");
    }

    [Fact]
    public async Task GetUnitsOrClinicsQueryHandler_Handle_With_Empty_Response_Result()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        var request = common.GetUnitOrClinicRequest(1);
        _baseQueryMock.GetAllPossibleUnitsAndClinics(request.UserId, request.PatientId, request.TenantId, request.FacilityId, request.EncounterId)
            .ReturnsForAnyArgs(new List<NextAppointmentUnitReturnDto>());
        _patientEncounterQueryhandlerMock.Handle(request.EncounterId).ReturnsForAnyArgs(common.GetPatientEncounter(request.EncounterId));

        var handler = new GetUnitsOrClinicsQueryHandler(_baseQueryMock, _abpSessionMock, _patientEncounterQueryhandlerMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request.UserId, request.FacilityId, request.EncounterId));
        //Assert
        result.Message.ShouldBe("No unit found for this doctor.");
    }

    [Fact]
    public async Task GetUnitsOrClinicsQueryHandler_Handle_With_Success_Result()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        var request = common.GetUnitOrClinicRequest(1);
        _baseQueryMock.GetAllPossibleUnitsAndClinics(request.UserId, request.PatientId, request.TenantId, request.FacilityId, request.EncounterId)
            .ReturnsForAnyArgs(common.GetAllPosibleUnitsAndClinics());
        _patientEncounterQueryhandlerMock.Handle(request.EncounterId).ReturnsForAnyArgs(common.GetPatientEncounter(request.EncounterId));

        var handler = new GetUnitsOrClinicsQueryHandler(_baseQueryMock, _abpSessionMock, _patientEncounterQueryhandlerMock);
        //Act
        var result = await handler.Handle(request.UserId, request.FacilityId, request.EncounterId);
        var resultId = result == null ? 0 : result.Count;
        //Assert
        resultId.ShouldBeGreaterThan(0);
    }
}