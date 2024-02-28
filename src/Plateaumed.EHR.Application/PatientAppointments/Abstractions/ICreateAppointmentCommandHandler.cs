using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientAppointments.Abstractions
{
    /// <summary>
    /// Handler to create an appointment
    /// </summary>
    public interface ICreateAppointmentCommandHandler: ITransientDependency
    {

        /// <summary>
        /// A method to handle appointment creation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<CreateOrEditPatientAppointmentDto> Handle(CreateOrEditPatientAppointmentDto request, int? tenantId);
    }
}
