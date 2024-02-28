using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.NextAppointments.Dtos;

namespace Plateaumed.EHR.NextAppointments.Abstractions;

public interface IGetPatientAllNextAppointmentQueryHandler : ITransientDependency
{
    Task<List<NextAppointmentReturnDto>> Handle(long patientId);
}
