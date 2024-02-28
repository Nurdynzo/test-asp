using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;

namespace Plateaumed.EHR.Staff.Abstractions;

public interface IGetStaffMemberByFacilityIdQueryHandler : ITransientDependency
{
    Task<List<FacilityStaffDto>> Handle(long facilityId);
}
