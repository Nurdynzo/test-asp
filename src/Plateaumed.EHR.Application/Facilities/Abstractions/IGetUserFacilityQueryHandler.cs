using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface IGetUserFacilityQueryHandler : ITransientDependency
    {
        Task<List<GetUserFacilityViewDto>> Handle(string emailAddress);
    }
}
