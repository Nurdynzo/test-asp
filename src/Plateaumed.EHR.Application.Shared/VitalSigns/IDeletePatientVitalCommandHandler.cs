using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Session;

namespace Plateaumed.EHR.VitalSigns;

public interface IDeletePatientVitalCommandHandler : ITransientDependency
{
    Task Handle(List<long> patientVitalIds);
}