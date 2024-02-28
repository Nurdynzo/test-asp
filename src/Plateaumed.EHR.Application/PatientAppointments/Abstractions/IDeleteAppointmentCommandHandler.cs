using Abp.Application.Services.Dto;
using Abp.Dependency;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientAppointments.Abstractions
{
    /// <summary>
    /// Handler to delete appointment
    /// </summary>
    public interface IDeleteAppointmentCommandHandler : ITransientDependency
    {
        /// <summary>
        /// A method to handle appointment deletion.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Handle(EntityDto<long> request);
    }
}
