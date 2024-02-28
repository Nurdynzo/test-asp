using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.ReferAndConsults.Dtos;

namespace Plateaumed.EHR.ReferAndConsults.Abstraction;

public interface IGetHospitalListQueryHandler : ITransientDependency
{
    Task<List<HospitalDto>> Handle();
}
