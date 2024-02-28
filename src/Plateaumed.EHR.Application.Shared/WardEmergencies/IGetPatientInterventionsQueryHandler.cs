using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.WardEmergencies.Dto;

namespace Plateaumed.EHR.WardEmergencies;

public interface IGetPatientInterventionsQueryHandler : ITransientDependency
{
    Task<List<GetPatientInterventionsResponse>> Handle(GetPatientInterventionsRequest request);
}