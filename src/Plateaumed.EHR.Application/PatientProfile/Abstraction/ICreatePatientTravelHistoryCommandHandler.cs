using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;

namespace Plateaumed.EHR.PatientProfile.Abstraction;

public interface ICreatePatientTravelHistoryCommandHandler: ITransientDependency
{
    Task Handle(List<CreatePatientTravelHistoryCommand> request);
}