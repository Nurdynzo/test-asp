using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.PatientAppointments.Utils;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientAppointments.Command
{
    /// <inheritdoc />
    public class CreateAppointmentCommandHandler : ICreateAppointmentCommandHandler
    {
        private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWork;

        /// <summary>
        /// Constructor for the <see cref="DeleteAppointmentCommandHandler"/>
        /// </summary>
        /// <param name="patientAppointmentRepository"></param>
        /// <param name="objectMapper"></param>
        /// <param name="unitOfWork"></param>
        public CreateAppointmentCommandHandler(
            IRepository<PatientAppointment, long> patientAppointmentRepository,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWork)
        {
            _objectMapper = objectMapper;
            _unitOfWork = unitOfWork;
            _patientAppointmentRepository = patientAppointmentRepository;
        }

        /// <inheritdoc />
        public async Task<CreateOrEditPatientAppointmentDto> Handle(CreateOrEditPatientAppointmentDto request, int? tenantId)
        {
            var newAppointment = _objectMapper.Map<PatientAppointment>(request);

            await CommonAppointmentValidation.CheckIfAppointmentCreateOrEditRequestIsValid(_patientAppointmentRepository, newAppointment);

            if (tenantId != null)
            {
                newAppointment.TenantId = (int)tenantId;
            }

            await _patientAppointmentRepository.InsertAsync(newAppointment);

            await _unitOfWork.Current.SaveChangesAsync();

            request.Id = newAppointment.Id;

            return request;
        }
    }
}
