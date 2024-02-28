using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Discharges.Dtos;

namespace Plateaumed.EHR.Discharges.Abstractions;

public interface IGetPatientDischargeQueryHandler : ITransientDependency
{
    Task<List<DischargeDto>> Handle(long patientId);
}
