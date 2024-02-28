using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Staff.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Staff.Abstractions
{
    public interface IActivateOrDeactivateStaffMemberHandler: ITransientDependency
    {
        Task Handle(ActivateOrDeactivateStaffMemberRequest request);
    }
}
