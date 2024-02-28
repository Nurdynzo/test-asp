using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using NSubstitute;
using Plateaumed.EHR.Patients;
using System;
using System.Threading.Tasks;
using Xunit;
using Plateaumed.EHR.PatientAppointments.Command;
using Abp.Application.Services.Dto;
using Abp.UI;
using Shouldly;
using Plateaumed.EHR.Tests.PatientAppointments.Util;

namespace Plateaumed.EHR.Tests.PatientAppointments.UnitTest
{

    [Trait("Category", "Unit")]
    public class DeleteAppointmentCommandHandlerTest
    {
        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository = Substitute.For<IRepository<PatientAppointment, long>>();
        private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
        
        [Fact]
        public async Task Handle_GivenAppointmentIdDoesNotExist_ShouldThrowException() {

            //Arrange
            var request = new EntityDto<long>() { Id = 2 };
            MockDependencies();
            var handler = GetDeleteAppointmentCommandHandlerInstance();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));
            message.Message.ShouldBe($"Appointment with Id {request.Id} does not exist");
        }
        
        [Fact]
        public async Task Handle_GivenAppointmentStatusIsNotProcessingOrNotArrived_ShouldThrowException() {  

            //Arrange
            var request = new EntityDto<long>() { Id = 1 };
            MockDependencies();
            var handler = GetDeleteAppointmentCommandHandlerInstance();

            // Act & Assert
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));
            message.Message.ShouldBe("Appointment cannot be deleted");
        }

        [Fact]
        public async Task Handle_GivenAppointmentExistAndStatusIsNotArrived_ShouldDelete()
        {

            //Arrange
            var request = new EntityDto<long>() { Id = 1 };
            MockDependencies(appointmentStatus: AppointmentStatusType.Not_Arrived);
            var handler = GetDeleteAppointmentCommandHandlerInstance();

            // Act
            await handler.Handle(request);

            // Assert
            _unitOfWork.Current.Received(1).SaveChanges();
        }
        
        [Fact]
        public async Task Handle_GivenAppointmentExistAndStatusIsProcessing_ShouldDelete()
        {

            //Arrange
            var request = new EntityDto<long>() { Id = 1 };
            MockDependencies(appointmentStatus: AppointmentStatusType.Processing);
            var handler = GetDeleteAppointmentCommandHandlerInstance();

            // Act
            await handler.Handle(request);

            // Assert
            _unitOfWork.Current.Received(1).SaveChanges();
            
        }

        private void MockDependencies(AppointmentStatusType appointmentStatus = AppointmentStatusType.Awaiting_Vitals)
        {
            var patientAppointmentMock = CommonUtils.GetPatientAppointment(appointmentStatus);
            _patientAppointmentRepository.FirstOrDefaultAsync(patientAppointmentMock.Id)
                .Returns(patientAppointmentMock);

            _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        }

        private DeleteAppointmentCommandHandler GetDeleteAppointmentCommandHandlerInstance()
        {
            var handler = new DeleteAppointmentCommandHandler(_patientAppointmentRepository, _unitOfWork);
            return handler;
        }

    }
}
