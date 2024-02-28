using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.Patients;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientAppointments.Command
{
    /// <inheritdoc />
    public class DeleteAppointmentCommandHandler : IDeleteAppointmentCommandHandler
    {
        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
        private readonly IUnitOfWorkManager _unitOfWork;

        /// <summary>
        /// Constructor for the <see cref="DeleteAppointmentCommandHandler"/>
        /// </summary>
        /// <param name="patientAppointmentRepository"></param>
        /// <param name="unitOfWork"></param>
        public DeleteAppointmentCommandHandler(IRepository<PatientAppointment, long> patientAppointmentRepository, IUnitOfWorkManager unitOfWork)
        {
            _patientAppointmentRepository = patientAppointmentRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task Handle(EntityDto<long> request)
        {
            var appointment = await _patientAppointmentRepository.FirstOrDefaultAsync(request.Id);

            ValidateDeleteRequest(appointment, request);

            await _patientAppointmentRepository.DeleteAsync(request.Id);

            _unitOfWork.Current.SaveChanges();
        }

        private void ValidateDeleteRequest(PatientAppointment appointment, EntityDto<long> request)
        {
            if (appointment == null)
            {
                throw new UserFriendlyException($"Appointment with Id {request.Id} does not exist");
            }

            if (!(appointment.Status == AppointmentStatusType.Not_Arrived || appointment.Status == AppointmentStatusType.Processing))
            {

                throw new UserFriendlyException("Appointment cannot be deleted");
            }
        }
    }
}
