using Abp.Application.Services;
using Plateaumed.EHR.NextAppointments.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.NextAppointments
{
    public interface INextAppointmentAppService : IApplicationService
    {
        Task<CreateNextAppointmentDto> CreateOrEdit(CreateNextAppointmentDto input);
        Task<List<NextAppointmentReturnDto>> GetPatientNextAppointments(int patientId);
        Task<List<NextAppointmentUnitReturnDto>> GetAvailableUnitAndClinics(long encounterId);
        Task<List<NextAppointmentReturnDto>> GetDoctorNextAppointments();
        Task<List<NextAppointmentReturnDto>> GetDoctorPatientNextAppointments(long patientId);

    }
}