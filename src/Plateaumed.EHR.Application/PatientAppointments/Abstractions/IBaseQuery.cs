using Abp.Dependency;
using Plateaumed.EHR.PatientAppointments.Query.BaseQueryHelper;
using System.Linq;

namespace Plateaumed.EHR.PatientAppointments.Abstractions
{
    public interface IBaseQuery: ITransientDependency
    {

        public IQueryable<AppointmentBaseQuery> GetAppointmentsBaseQuery();
    }
}
