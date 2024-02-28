using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientAppointments.Abstractions
{
    /// <summary>
    /// Handler to update an appointment
    /// </summary>
    public interface IUpdateAppointmentCommandHandler : ITransientDependency
    {

        /// <summary>
        /// A method to handle update to an appointment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CreateOrEditPatientAppointmentDto> Handle(CreateOrEditPatientAppointmentDto request);
    }
}
