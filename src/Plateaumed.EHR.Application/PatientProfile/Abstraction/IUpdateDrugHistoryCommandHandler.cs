using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IUpdateDrugHistoryCommandHandler : ITransientDependency
    {
        Task Handle(CreateDrugHistoryRequestDto request);
    }
}
