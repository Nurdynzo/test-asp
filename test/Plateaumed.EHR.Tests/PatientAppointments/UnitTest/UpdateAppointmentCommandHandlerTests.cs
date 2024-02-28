using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.PatientAppointments.Command;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Tests.PatientAppointments.Util;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientAppointments.UnitTest
{
    [Trait("Category", "Unit")]
    public class UpdateAppointmentCommandHandlerTests
    {

        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository = Substitute.For<IRepository<PatientAppointment, long>>();
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
        private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
        private readonly IRepository<PatientReferralDocument, long> _referralDocumentRepository =
            Substitute.For<IRepository<PatientReferralDocument, long>>();

        [Fact]
        public async Task Handle_GivenAppoitmentStartTimeIsSetToThePast_ShouldThrowException()
        {

            //Arrange
            var request = CommonUtils.GetCreateOrEditAppointmentRequest(DateTime.Now.AddMinutes(-2), AppointmentId: 3);
            MockDependencies(request);
            var handler = GetUpdateAppointmentCommandHandlerInstance();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(handler.Handle(request));
            message.Message.ShouldBe("Appointment startime cannot be set to a time in the past.");
        }

        [Fact]
        public async Task Handle_GivenAppoitmentAlreadyExistTodayForPatientInSameClinic_ShouldThrowException()
        {

            //Arrange
            var startDay = DateTime.Now.AddDays(1).Date.AddHours(1);
            var request = CommonUtils.GetCreateOrEditAppointmentRequest(startDay, AppointmentId: 20);
            MockDependencies(request);
            var handler = GetUpdateAppointmentCommandHandlerInstance();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(handler.Handle(request));
            message.Message.ShouldBe("Appointment already exist for this patient on this date in the same clinic.");
        }

        [Fact]
        public async Task Handle_GivenAppoitmentAlreadyExistTwoHours_ShouldThrowException()
        {

            //Arrange
            var random = new Random();
            var startDay = DateTime.Now.AddDays(1).Date.AddHours(random.Next(1, 17));
            var request = CommonUtils.GetCreateOrEditAppointmentRequest(startDay, AppointmentId: 3);
            MockDependencies(request);
            var handler = GetUpdateAppointmentCommandHandlerInstance();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(handler.Handle(request));
            message.Message.ShouldBe("Appointment cannot be updated within 2hrs of existing appointment on same day");
        }

        [Fact]
        public async Task Handle_GivenAppoitmentIsNotModified_ShouldThrowException() 
        {

            //Arrange
            var startDay = DateTime.Now.AddDays(1).Date.AddHours(3 * 2.5);
            var request = CommonUtils.GetCreateOrEditAppointmentRequest(startDay, AppointmentId: 3);
            MockDependencies(request);
            var handler = GetUpdateAppointmentCommandHandlerInstance();

            // Act
            var message = await Should.ThrowAsync<UserFriendlyException>(handler.Handle(request));
            message.Message.ShouldBe("No change detected");
        }
        
        [Fact]
        public async Task Handle_GivenAppoitmentMeetsAllRequirements_ShouldUpdateAppointment()
        {

            //Arrange
            var request = CommonUtils.GetCreateOrEditAppointmentRequest(DateTime.Now.AddDays(1).Date, AppointmentId: 3);
            MockDependencies(request);
            var handler = GetUpdateAppointmentCommandHandlerInstance();

            // Act
            var response = await handler.Handle(request);

            // Assert
            response.StartTime.ShouldBe(request.StartTime);
            response.AttendingClinicId.ShouldBe(request.AttendingClinicId);
            response.PatientId.ShouldBe(request.PatientId);
            await _unitOfWork.Current.Received(1).SaveChangesAsync();
        }

        private void MockDependencies(CreateOrEditPatientAppointmentDto request)
        {
            _patientAppointmentRepository.GetAll()
                .Returns(CommonUtils.GetPatientAppointmentsWithinTheNextDayAsQueryable().BuildMock());

            var patientAppointmentMock = CommonUtils.GetPatientAppointmentsWithinTheNextDayAsQueryable()
                .FirstOrDefault(x => x.Id == request.Id) 
                ?? CommonUtils.ConvertAppointmentRequestToPatientAppointment(request, applyStartTime: false);

            _patientAppointmentRepository.FirstOrDefaultAsync((long)request.Id)
                .Returns(patientAppointmentMock);
            _referralDocumentRepository.GetAll()
                .Returns(CommonUtils.GetReferralDocuments().BuildMock());

            _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        }


        private UpdateAppointmentCommandHandler GetUpdateAppointmentCommandHandlerInstance()
        {
            var handler = new UpdateAppointmentCommandHandler(_patientAppointmentRepository, _objectMapper, _unitOfWork, _referralDocumentRepository);
            return handler;
        }
    }
}
