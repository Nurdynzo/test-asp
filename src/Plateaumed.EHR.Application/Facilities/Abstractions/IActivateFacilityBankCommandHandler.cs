using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface IActivateFacilityBankCommandHandler : ITransientDependency
    {
        Task<FacilityBank> Handle(ActivateBankRequest request);
    }
}
