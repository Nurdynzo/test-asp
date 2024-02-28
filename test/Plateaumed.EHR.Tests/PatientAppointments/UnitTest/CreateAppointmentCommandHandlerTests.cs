using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.PatientAppointments.Command;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Tests.PatientAppointments.Util;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientAppointments.UnitTest
{
    [Trait("Category", "Unit")]
    public class CreateAppointmentCommandHandlerTests
    {

        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository = Substitute.For<IRepository<PatientAppointment, long>>();
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
        private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();

        [Fact]
        public async Task Handle_GivenAppoitmentStartTimeIsSetToThePast_ShouldThrowException()
        {

            //Arrange
            var request = CommonUtils.GetCreateOrEditAppointmentRequest(DateTime.Now.AddMinutes(-2));
            MockDependencies();
            var handler = GetCreateAppointmentCommandHandlerInstance();

           // Act & Assert
           var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request, tenantId: 1));
           message.Message.ShouldBe("Appointment startime cannot be set to a time in the past.");
        }
        
        [Fact]
        public async Task Handle_GivenAppoitmentAlreadyExistTodayForPatientInSameClinic_ShouldThrowException()
        {

            //Arrange
            var request = CommonUtils.GetCreateOrEditAppointmentRequest(DateTime.Now.AddDays(1).Date.AddHours(1));
            MockDependencies();
            var handler = GetCreateAppointmentCommandHandlerInstance();

           // Act & Assert
           var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request, tenantId: 1));
           message.Message.ShouldBe("Appointment already exist for this patient on this date in the same clinic.");
        }
        
        [Fact]
        public async Task Handle_GivenAppoitmentAlreadyExistTwoHours_ShouldThrowException()
        {

            //Arrange
            var random = new Random();
            var startDay = DateTime.Now.AddDays(1).Date.AddHours(random.Next(1, 17));
            var request = CommonUtils.GetCreateOrEditAppointmentRequest(startDay, AttendingClinicId: 8);
            MockDependencies();
            var handler = GetCreateAppointmentCommandHandlerInstance();

           // Act & Assert
           var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request, tenantId: 1));
           message.Message.ShouldBe("Appointment cannot be created within 2hrs of existing appointment on same day");
        }
        
        [Fact]
        public async Task Handle_GivenAppoitmentMeetsAllRequirements_ShouldCreateAppointment()
        {

            //Arrange
            var request = CommonUtils.GetCreateOrEditAppointmentRequest(DateTime.Now.AddDays(1).Date, AttendingClinicId: 8);
            MockDependencies();
            var handler = GetCreateAppointmentCommandHandlerInstance();

            // Act
            var response = await handler.Handle(request, tenantId: 1);

            // Assert
            response.StartTime.ShouldBe(request.StartTime);
            response.AttendingClinicId.ShouldBe(request.AttendingClinicId);
            response.PatientId.ShouldBe(request.PatientId);
            await _unitOfWork.Current.Received(1).SaveChangesAsync();
        }

        private void MockDependencies() {
            _patientAppointmentRepository.GetAll()
                .Returns(CommonUtils.GetPatientAppointmentsWithinTheNextDayAsQueryable().BuildMock());
            _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        }


        private CreateAppointmentCommandHandler GetCreateAppointmentCommandHandlerInstance()
        {
            var handler = new CreateAppointmentCommandHandler(_patientAppointmentRepository, _objectMapper, _unitOfWork);
            return handler;
        }
    }
}
