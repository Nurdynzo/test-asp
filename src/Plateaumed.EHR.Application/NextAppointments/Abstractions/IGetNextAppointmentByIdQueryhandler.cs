using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.NextAppointments.Dtos;

namespace Plateaumed.EHR.NextAppointments.Abstractions;

public interface IGetNextAppointmentByIdQueryhandler : ITransientDependency
{
    Task<NextAppointmentReturnDto> Handle(long nextAppointmentId); 
}
