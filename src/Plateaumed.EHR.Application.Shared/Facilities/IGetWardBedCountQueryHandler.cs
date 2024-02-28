using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;

namespace Plateaumed.EHR.Facilities;

public interface IGetWardBedCountQueryHandler : ITransientDependency
{
    Task<List<GetWardBedCountResponse>> Handle(GetWardBedCountRequest request);
}