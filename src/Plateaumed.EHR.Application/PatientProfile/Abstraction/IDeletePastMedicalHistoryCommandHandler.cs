using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IDeletePastMedicalHistoryCommandHandler : ITransientDependency
    {
        Task Handle(long id);
    }
}
