using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.NextAppointments.Dtos;

namespace Plateaumed.EHR.NextAppointments.Abstractions;

public interface IGetUnitsOrClinicsQueryHandler : ITransientDependency
{
    Task<List<NextAppointmentUnitReturnDto>> Handle(long userId, long facilityId, long encounterId);
}