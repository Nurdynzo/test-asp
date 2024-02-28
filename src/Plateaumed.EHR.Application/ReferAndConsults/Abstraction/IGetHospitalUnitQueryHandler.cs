using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Organizations.Dto;

namespace Plateaumed.EHR.ReferAndConsults.Abstraction;

public interface IGetHospitalUnitQueryHandler : ITransientDependency
{
    Task<List<OrganizationUnitDto>> Handle(long facilityId);
}
