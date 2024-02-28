using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.IntakeOutputs.Dtos;

namespace Plateaumed.EHR.IntakeOutputs.Abstractions;

public interface IGetIntakeOutputSavedHistoryQueryHandler : ITransientDependency
{
    Task<List<PatientIntakeOutputDto>> Handle(long patientId, long? procedureId = null);
}
