using System;
using System.Threading.Tasks;
using NSubstitute;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.PatientAppointments.Dtos;
using Plateaumed.EHR.PatientAppointments.Query;
using Plateaumed.EHR.Tests.PatientAppointments.Util;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using IBaseQuery = Plateaumed.EHR.PatientAppointments.Abstractions.IBaseQuery;

namespace Plateaumed.EHR.Tests.PatientAppointments.UnitTest;

public class GetPatientAppointmentsForTheDayTest
{

    private readonly IBaseQuery _baseQueryMock = CommonQuery.GetAppointmentBaseQueryInstance();


    [Fact]
    public async Task GetPatientAppointmentsForTheDay_By_StartTime_Filter_Should_ReturnsPatientAppointmentsFor_The_Filter()
    {
        //arrange
      
        var handler = GetAppointmentForTodayQueryHandlerInstance();
        var request = new GetAppointmentForTodayQueryRequest { StartTime = DateTime.Now.AddDays(3) };
        //act
      
        var result = await handler.Handle(request);
        
        //assert
        result.Items.Count.ShouldBe(2);
        result.Items[0].Id.ShouldBe(3);
        result.Items[1].Id.ShouldBe(5);
    }

   

    [Fact]
    public async Task GetPatientAppointmentsForTheDay_By_AttendingClinic_Filter_Should_ReturnsPatientAppointmentsFor_The_Filter()
    {
        //arrange
      
        var handler = GetAppointmentForTodayQueryHandlerInstance();
        var request = new GetAppointmentForTodayQueryRequest { AttendingClinic = "Clinic1" };
        //act
      
        var result = await handler.Handle(request);
        
        //assert
        result.Items.Count.ShouldBe(2);
        result.Items.ShouldNotContain(x=>x.Id == 3); // filter by current date only

    }
    [Fact]
    public async Task GetPatientAppointmentsForTheDay_By_PatientCode_Filter_Should_ReturnsPatientAppointmentsFor_The_Filter()
    {
        //arrange
      
        var handler = GetAppointmentForTodayQueryHandlerInstance();
        var request = new GetAppointmentForTodayQueryRequest { PatientCode = "TestCode1" };
        //act
      
        var result = await handler.Handle(request);
        
        //assert
        result.Items.Count.ShouldBe(1);
        result.Items[0].Id.ShouldBe(1);

    }
    [Fact]
    public async Task GetPatientAppointmentsForTheDayTest_WhenCalled_ReturnsPatientAppointmentsFor_The_Day()
    {
        //arrange
      
        var handler = GetAppointmentForTodayQueryHandlerInstance();
        //act
      
        var result = await handler.Handle(new GetAppointmentForTodayQueryRequest());
        
        //assert
        result.Items.Count.ShouldBe(2);
        result.Items.ShouldNotContain(x => x.Id == 3);

    }
    [Fact]
    public async Task GetPatientAppointmentsForTheDay_By_EndTime_Filter_Should_ReturnsPatientAppointmentsFor_The_Filter()
    {
        //arrange
      
        var handler = GetAppointmentForTodayQueryHandlerInstance();
        var request = new GetAppointmentForTodayQueryRequest { EndTime = DateTime.Now.AddDays(4) };
        //act
      
        var result = await handler.Handle(request);
        
        //assert
        result.Items.Count.ShouldBe(3);

    }
    [Fact]
    public async Task GetPatientAppointmentsForTheDay_With_More_One_Patient_Appointment_Should_Have_AppointmentCountSet()
    {
        //arrange
      
        var handler = GetAppointmentForTodayQueryHandlerInstance();
        var request = new GetAppointmentForTodayQueryRequest
        {
            EndTime = DateTime.Now.AddDays(10),
            StartTime = DateTime.Now.AddDays(-2)
        };
        //act
      
        var result = await handler.Handle(request);
        
        //assert
       result.Items.ShouldContain(x=>x.AppointmentCount == 4);

    }
    
    
  
    private GetAppointmentForTodayQueryHandler GetAppointmentForTodayQueryHandlerInstance()
    {
        var handler = new GetAppointmentForTodayQueryHandler(_baseQueryMock, Substitute.For<IGetCurrentUserFacilityIdQueryHandler>());
        return handler;
    }
}