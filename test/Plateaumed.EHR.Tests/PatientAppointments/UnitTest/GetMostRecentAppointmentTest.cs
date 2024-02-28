using System;
using System.Threading.Tasks;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.PatientAppointments.Dtos;
using Plateaumed.EHR.PatientAppointments.Query;
using Plateaumed.EHR.Tests.PatientAppointments.Util;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientAppointments.UnitTest;

public class GetMostRecentAppointmentTest
{
    private readonly IBaseQuery _baseQueryMock = CommonQuery.GetAppointmentBaseQueryInstance();

    [Fact]
    public async Task GetMostRecentAppointmentForPatient_Should_Return_Most_Recent_Appointment()
    {
        //arrange
        var handler = new GetMostRecentAppointmentQueryHandler(_baseQueryMock);
        var request = new GetMostRecentAppointmentForPatientRequest { PatientId = 1 };
        
        //act
        var result = await handler.Handle(request);

        result.ShouldNotBeNull();
        result.Title.ShouldContain("Test Title 4");
        result.StartTime.ShouldBe(DateTime.Today.Date.AddDays(10));
    }
}