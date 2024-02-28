using System;
using System.Threading.Tasks;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.PatientAppointments.Query;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Tests.PatientAppointments.Util;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientAppointments.UnitTest;

public class GetAppointmentsByPatientIdTest
{
    private readonly IBaseQuery _baseQueryMock = CommonQuery.GetAppointmentBaseQueryInstance();
    
    [Fact]
    public async Task GetAppointmentsByPatientId_Filter_By_PatientId_Should_ReturnsPatientAppointmentsFor_The_Filter()
    {
        //arrange
        var handler = GetAppointmentByPatientIdQueryHandlerInstance();
        var request = new GetAppointmentsByPatientIdRequest { PatientId = 1 };
        //act
        var result = await handler.Handle(request);
        //assert
        result.Items.Count.ShouldBe(4);
        result.Items[0].AttendingPhysician.FullName.ShouldBe("Dr TestName1 T. TestSurname1");
        result.Items[0].AttendingClinic.DisplayName.ShouldBe("Clinic1");
        result.Items[0].Title.ShouldBe("Test Title");
        result.Items[0].Notes.ShouldBe("Test Notes");
        result.Items[0].StartTime.ShouldBeInRange(DateTime.Now.Date, DateTime.Now.Date.AddMinutes(20)); // assert order by start time
        result.Items[1].StartTime.ShouldBe(DateTime.Today.AddDays(3));
        result.Items[1].AttendingPhysician.FullName.ShouldBe("Dr TestName2 T. TestSurname2");
    }
    
    
    [Fact]
    public async Task GetAppointmentsByPatientId_Filter_By_PatientId_Should_ReturnPatientAppointments_In_Order() 
    {
        //arrange
        var handler = GetAppointmentByPatientIdQueryHandlerInstance();
        var request = new GetAppointmentsByPatientIdRequest { PatientId = 1 };
        //act
        var result = await handler.Handle(request);

        // assert
        result.Items.Count.ShouldBe(4);

        // assert items are ordered by startTime in the following order today's appointments, upcoming appointments, past appointments
        result.Items[0].StartTime.ShouldBeInRange(DateTime.Now.Date, DateTime.Now.Date.AddMinutes(20));
        result.Items[1].StartTime.ShouldBe(DateTime.Today.Date.AddDays(3));
        result.Items[2].StartTime.ShouldBe(DateTime.Today.Date.AddDays(10));
        result.Items[3].StartTime.ShouldBe(DateTime.Today.Date.AddDays(-2));
    }
    
    
    [Fact]
    public async Task GetAppointmentsByPatientId_Filter_By_PatientId_StartDate_And_EndDate_Should_ReturnPatientAppointments_For_Filter()  
    {
        //arrange
        var handler = GetAppointmentByPatientIdQueryHandlerInstance();
        var request = new GetAppointmentsByPatientIdRequest { PatientId = 1, StartDate= DateTime.Now, EndDate= DateTime.Now.AddDays(3) };
        //act
        var result = await handler.Handle(request);

        // assert
        result.Items.Count.ShouldBe(2);
        result.Items[0].StartTime.ShouldBeInRange(DateTime.Now.Date, DateTime.Now.Date.AddMinutes(20));
        result.Items[0].AttendingClinic.DisplayName.ShouldBe("Clinic1");
        result.Items[0].AttendingPhysician.FullName.ShouldBe("Dr TestName1 T. TestSurname1");
        result.Items[1].StartTime.ShouldBe(DateTime.Today.AddDays(3));
        result.Items[0].AttendingClinic.DisplayName.ShouldBe("Clinic1");
        result.Items[1].AttendingPhysician.FullName.ShouldBe("Dr TestName2 T. TestSurname2");

        // assert items are ordered by startTime
        result.Items[0].StartTime.ShouldBeInRange(DateTime.Now.Date, DateTime.Now.Date.AddMinutes(20));
        result.Items[1].StartTime.ShouldBe(DateTime.Today.Date.AddDays(3));
    }

    private GetAppointmentByPatientIdQueryHandler GetAppointmentByPatientIdQueryHandlerInstance()
    {
        return new GetAppointmentByPatientIdQueryHandler(_baseQueryMock);
    }
}